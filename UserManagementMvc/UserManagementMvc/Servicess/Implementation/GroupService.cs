using AutoMapper;
using System.Net;
using UserManagementMvc.Entities;
using UserManagementMvc.Models;
using UserManagementMvc.Models.CustomResponse;
using UserManagementMvc.Models.Group;
using UserManagementMvc.Models.Pagination;
using UserManagementMvc.Repos.Abstraction;
using UserManagementMvc.Repos.UnitOfWork.Abstraction;
using UserManagementMvc.Servicess.Abstraction;

namespace UserManagementMvc.Servicess.Implementation
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = _unitOfWork.GetRepository<IGroupRepository>();
            _mapper = mapper;
        }
        public async Task<ApiResponse<bool>> CreateAsync(CreateGroupDto groupDto)
        {
            var group = _mapper.Map<Group>(groupDto);

            await _groupRepository.AddAsync(group);

            await _unitOfWork.CommitAsync();

            return ApiResponse<bool>.Success(true, HttpStatusCode.Created, Statics.StaticMessages.CreatedGroup);
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var group = await _groupRepository.GetByIdAsync(id);

            if (group == null)
            {
                return ApiResponse<bool>.Failure(Statics.StaticMessages.GroupNotFound, HttpStatusCode.NotFound);
            }

            await _groupRepository.DeleteAsync(group);

            await _unitOfWork.CommitAsync();

            return ApiResponse<bool>.Success(true, HttpStatusCode.NoContent, Statics.StaticMessages.DeletedGroup);
        }

        public async Task<ApiResponse<PaginationResponseDto<GroupDto>>> GetAllAsync(PaginationDto paginationDto)
        {
            var groups = await _groupRepository.GetAllAsync(paginationDto.PageNumber, paginationDto.PageSize);

            if (groups == null || groups.TotalCount == 0)
            {
                return ApiResponse<PaginationResponseDto<GroupDto>>.Failure(Statics.StaticMessages.GroupNotFound, HttpStatusCode.NotFound);
            }

            PaginationResponseDto<GroupDto> paginationResult = new PaginationResponseDto<GroupDto>
            {
                PageNumber = paginationDto.PageNumber,
                PageSize = paginationDto.PageSize,
                TotalCount = groups.TotalCount,
                Items = _mapper.Map<IEnumerable<GroupDto>>(groups.Data)
            };

            return ApiResponse<PaginationResponseDto<GroupDto>>.Success(paginationResult, HttpStatusCode.OK);
        }

        public async Task<ApiResponse<GroupDto>> GetByIdAsync(int id)
        {
            var group = await _groupRepository.GetByIdAsync(id);

            if (group == null)
            {
                return ApiResponse<GroupDto>.Failure(Statics.StaticMessages.GroupNotFound, HttpStatusCode.NotFound);
            }

            return ApiResponse<GroupDto>.Success(_mapper.Map<GroupDto>(group), HttpStatusCode.OK);
        }

        public async Task<ApiResponse<bool>> UpdateAsync(int id, UpdateGroupDto groupDto)
        {
            var group = await _groupRepository.GetByIdAsync(id);

            if (group == null)
            {
                ApiResponse<bool>.Failure(Statics.StaticMessages.GroupNotFound, HttpStatusCode.NotFound);
            }

            group.Name = groupDto.Name;

            await _groupRepository.UpdateAsync(group);

            await _unitOfWork.CommitAsync();

            return ApiResponse<bool>.Success(true, HttpStatusCode.OK, Statics.StaticMessages.UpdatedGroup);
        }

        public async Task<ApiResponse<PaginationResponseDto<GroupDto>>> GetGroupsWithTeacherId(int id, PaginationDto paginationDto)
        {
            var groups = await _groupRepository.GetGroupsWithTeacherId(id, paginationDto.PageNumber, paginationDto.PageSize);

            if (groups == null || groups.TotalCount == 0)
            {
                return ApiResponse<PaginationResponseDto<GroupDto>>.Failure(Statics.StaticMessages.GroupNotFound, HttpStatusCode.NotFound);
            }

            PaginationResponseDto<GroupDto> response = new PaginationResponseDto<GroupDto>
            {
                TotalCount = groups.TotalCount,
                PageNumber = paginationDto.PageNumber,
                PageSize = paginationDto.PageSize,
                Items = _mapper.Map<IEnumerable<GroupDto>>(groups.Data)
            };

            return ApiResponse<PaginationResponseDto<GroupDto>>.Success(response, HttpStatusCode.OK);
        }
        public async Task<ApiResponse<PaginationResponseDto<GroupsWithSubIdDto>>> GetAllGroupsWithSubjId(int id, PaginationDto paginationDto)
        {
            var groups = await _groupRepository.GetAllGroupsWithSubjId(id, paginationDto.PageNumber, paginationDto.PageSize);

            if (groups == null || groups.TotalCount == 0)
            {
                return ApiResponse<PaginationResponseDto<GroupsWithSubIdDto>>.Failure(Statics.StaticMessages.GroupNotFound, HttpStatusCode.NotFound);
            }

            PaginationResponseDto<GroupsWithSubIdDto> response = new PaginationResponseDto<GroupsWithSubIdDto>
            {
                TotalCount = groups.TotalCount,
                PageNumber = paginationDto.PageNumber,
                PageSize = paginationDto.PageSize,
                Items = _mapper.Map<IEnumerable<GroupsWithSubIdDto>>(groups.Data)
            };

            return ApiResponse<PaginationResponseDto<GroupsWithSubIdDto>>.Success(response, HttpStatusCode.OK);

        }
        public async Task<ApiResponse<PaginationResponseDto<GroupDto>>> GetAllGroupsWithCourseId(int id, PaginationDto paginationDto)
        {
            var groups = await _groupRepository.GetAllGroupsWithCourseId(id, paginationDto.PageNumber, paginationDto.PageSize);

            if (groups == null || groups.TotalCount == 0)
            {
                return ApiResponse<PaginationResponseDto<GroupDto>>.Failure(Statics.StaticMessages.GroupNotFound, HttpStatusCode.NotFound);
            }

            PaginationResponseDto<GroupDto> response = new PaginationResponseDto<GroupDto>
            {
                TotalCount = groups.TotalCount,
                PageNumber = paginationDto.PageNumber,
                PageSize = paginationDto.PageSize,
                Items = _mapper.Map<IEnumerable<GroupDto>>(groups.Data)
            };

            return ApiResponse<PaginationResponseDto<GroupDto>>.Success(response, HttpStatusCode.OK);
        }


    }
}
