using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using System.Text.Json;
using UserManagementMvc.Entities;
using UserManagementMvc.Models;
using UserManagementMvc.Models.CustomResponse;
using UserManagementMvc.Models.File;
using UserManagementMvc.Models.Pagination;
using UserManagementMvc.Models.Student;
using UserManagementMvc.Repos.Abstraction;
using UserManagementMvc.Repos.UnitOfWork.Abstraction;
using UserManagementMvc.Servicess.Abstraction;

namespace UserManagementMvc.Servicess.Implementation;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly HttpClient httpClient;
    private readonly IMapper _mapper;
    private readonly string _baseUrl;

    public StudentService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService,
        IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _studentRepository = _unitOfWork.GetRepository<IStudentRepository>();
        _mapper = mapper;
        _groupRepository = _unitOfWork.GetRepository<IGroupRepository>();
        _fileService = fileService;
        httpClient = httpClientFactory.CreateClient("MyClient");

        _baseUrl = config.GetSection("Url")["StudentBaseUrl"];

    }

    public async Task<Models.CustomResponse.ApiResponse<bool>> CreateAsync([FromForm] CreateStudentDto studentDto)
    {
        var student = _mapper.Map<Student>(studentDto);

        var std = await _studentRepository.GetQuery(x => x.Email == student.Email).FirstOrDefaultAsync();

        if (std != null)
        {
            return ApiResponse<bool>.Failure("Email exists in DB", HttpStatusCode.BadRequest);
        }

        List<Group> groups = await _groupRepository.GetQuery(g => studentDto.GroupIds.Contains(g.Id)).ToListAsync();

        foreach (var group in groups)
        {
            student.StudentGroups.Add(new StudentGroup()
            {
                StudentId = student.Id,
                Group = group
            });
        }

        var fileDetail = await _fileService.UploadFile(studentDto.Image);

        if (fileDetail == null)
        {
            return ApiResponse<bool>.Failure(Statics.StaticMessages.NullFile, HttpStatusCode.NotFound);
        }

        student.StudentFiles.Add(new StudentFile
        {
            FileId = fileDetail.Data.Id,
            StudentId = student.Id
        });

        await _fileService.Download(fileDetail.Data.Id);

        await _studentRepository.AddAsync(student);

        await _unitOfWork.CommitAsync();

        return ApiResponse<bool>.Success(true, HttpStatusCode.Created, Statics.StaticMessages.CreatedStudent);
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);

        if (student == null)
        {
            return ApiResponse<bool>.Failure(Statics.StaticMessages.StudentNotFound, HttpStatusCode.NotFound);
        }

        await _studentRepository.DeleteAsync(student);

        await _unitOfWork.CommitAsync();

        return ApiResponse<bool>.Success(true, HttpStatusCode.NoContent, Statics.StaticMessages.DeletedStudent);
    }

    public async Task<ApiResponse<PaginationResponseDto<StudentDto>>> GetAllAsync(PaginationDto paginationDto)
    {
        var students = await _studentRepository.GetAllAsync(paginationDto.PageNumber, paginationDto.PageSize);

        if (students.TotalCount == 0 || students == null)
        {
            return ApiResponse<PaginationResponseDto<StudentDto>>.Failure(Statics.StaticMessages.StudentNotFound, HttpStatusCode.NotFound);
        }

        PaginationResponseDto<StudentDto> paginationResult = new PaginationResponseDto<StudentDto>
        {
            TotalCount = students.TotalCount,
            PageNumber = paginationDto.PageNumber,
            PageSize = paginationDto.PageSize,
            Items = _mapper.Map<IEnumerable<StudentDto>>(students.Data)
        };

        return ApiResponse<PaginationResponseDto<StudentDto>>.Success(paginationResult, HttpStatusCode.OK);
    }

    public async Task<ApiResponse<StudentDto>> GetByIdAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);

        if (student == null)
        {
            return ApiResponse<StudentDto>.Failure(Statics.StaticMessages.StudentNotFound, HttpStatusCode.NotFound);
        }

        return ApiResponse<StudentDto>.Success(_mapper.Map<StudentDto>(student), HttpStatusCode.OK);
    }

    public async Task<ApiResponse<bool>> UpdateAsync(int id, UpdateStudentDto studentDto)
    {
        var student = await _studentRepository.GetByIdAsync(id);

        if (student == null)
        {
            return ApiResponse<bool>.Failure(Statics.StaticMessages.StudentNotFound, HttpStatusCode.NotFound);
        }

        student.Name = studentDto.Name;
        student.Email = studentDto.Email;
        student.Phone = studentDto.Phone;
        student.Age = studentDto.Age;

        await _studentRepository.UpdateAsync(student);

        await _unitOfWork.CommitAsync();

        return ApiResponse<bool>.Success(true, HttpStatusCode.OK, Statics.StaticMessages.UpdatedStudent);
    }

    public async Task<ApiResponse<bool>> UpdateGroupsAsync(int id, UpdateStudentGroupDto stdGroup)
    {
        var student = await _studentRepository.GetByIdAsync(id);

        if (student == null)
        {
            return ApiResponse<bool>.Failure(Statics.StaticMessages.StudentNotFound, HttpStatusCode.NotFound);
        }

        var lastGroups = student.StudentGroups.Select(x => x.GroupId).ToList();
        var newGroups = stdGroup.GroupIds;

        var toInsert = newGroups.Except(lastGroups).ToList();
        var toDelete = lastGroups.Except(newGroups).ToList();

        var studentGroupsToRemove = student.StudentGroups
           .Where(sg => toDelete.Contains(sg.GroupId)).ToList();

        foreach (var group in studentGroupsToRemove)
        {
            student.StudentGroups.Remove(group);
        }

        var groups = await _groupRepository.GetQuery(g => stdGroup.GroupIds.Contains(g.Id)).ToListAsync();

        foreach (var group in groups)
        {
            student.StudentGroups.Add(new StudentGroup
            {
                StudentId = student.Id,
                Group = group
            });
        }

        await _studentRepository.UpdateAsync(student);

        await _unitOfWork.CommitAsync();

        return ApiResponse<bool>.Success(true, HttpStatusCode.OK, Statics.StaticMessages.UpdatedStudentGroups);
    }



    public async Task<ApiResponse<PaginationResponseDto<StudentDto>>> GetStudentsByGroupId(int id, PaginationDto paginationDto)
    {
        var students = await _studentRepository.GetStudentsByGroupId(id, paginationDto.PageNumber, paginationDto.PageSize);

        if (students == null || students.TotalCount == 0)
        {
            return ApiResponse<PaginationResponseDto<StudentDto>>
                .Failure(Statics.StaticMessages.StudentNotFound, HttpStatusCode.NotFound);
        }

        PaginationResponseDto<StudentDto> paginationResponse = new
           PaginationResponseDto<StudentDto>
        {
            TotalCount = students.TotalCount,
            PageNumber = paginationDto.PageNumber,
            PageSize = paginationDto.PageSize,
            Items = _mapper.Map<List<StudentDto>>(students.Data)
        };


        return ApiResponse<PaginationResponseDto<StudentDto>>
            .Success(paginationResponse, HttpStatusCode.OK);
    }


    public async Task<ApiResponse<StudentWithFileDto>> GetStudentFileDetails(int id)
    {
        Student student = await _studentRepository.StudentWithFile(id);

        if (student == null)
        {
            return ApiResponse<StudentWithFileDto>
               .Failure(Statics.StaticMessages.StudentNotFound, HttpStatusCode.NotFound);

        }

        var studentInfo = new StudentWithFileDto
        {
            Id = student.Id,
            FileInfo = student.StudentFiles.Select(sf => sf.FileDetails)
            .Select(fd => new FileDetailsDto
            {
                Id = fd.Id,
                Path = fd.Path
            })
            .ToList()
        };

        return ApiResponse<StudentWithFileDto>.Success(studentInfo, HttpStatusCode.OK);

    }

    public async Task<ApiResponse<bool>> CreateMvcAsync([FromForm] CreateStudentDto studentDto)
    {
        httpClient.BaseAddress = new Uri(_baseUrl);

        var form = new MultipartFormDataContent();

        form.Add(new StringContent(studentDto.Name), "Name");
        form.Add(new StringContent(studentDto.Email), "Email");
        form.Add(new StringContent(studentDto.Phone), "Phone");
        form.Add(new StringContent(studentDto.Age.ToString()), "Age");

        foreach (var groupId in studentDto.GroupIds)
        {
            form.Add(new StringContent(groupId.ToString()), "GroupIds");
        }

        var imageContent = new StreamContent(studentDto.Image.OpenReadStream());

        imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(studentDto.Image.ContentType);

        form.Add(imageContent, "Image", studentDto.Image.FileName);

        var response = await httpClient.PostAsync($"/api/Student/Create", form);

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ApiResponse<bool>>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result!;
    }

    public async Task<ApiResponse<PaginationResponseDto<StudentDto>>> GetAllMvcAsync(PaginationDto paginationDto)
    {
        httpClient.BaseAddress = new Uri(_baseUrl);

        var jsonResponse = await httpClient.GetAsync($"/api/Student/GetAll?PageNumber={paginationDto.PageNumber}&PageSize={paginationDto.PageSize}");

        var response = await jsonResponse.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ApiResponse<PaginationResponseDto<StudentDto>>>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<ApiResponse<bool>> UpdateMvcAsync(int id, UpdateStudentDto studentDto)
    {
        httpClient.BaseAddress = new Uri(_baseUrl);

        var jsonData = JsonSerializer.Serialize(studentDto);

        var data = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var jsonResponse = await httpClient.PutAsync($"/api/Student/Update?id={id}", data);

        var response = await jsonResponse.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ApiResponse<bool>>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<ApiResponse<bool>> DeleteMvcAsync(int id)
    {
        httpClient.BaseAddress = new Uri(_baseUrl);

        var jsonResponse = await httpClient.DeleteAsync($"/api/Student/Delete?id={id}");

        var response = await jsonResponse.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ApiResponse<bool>>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}
