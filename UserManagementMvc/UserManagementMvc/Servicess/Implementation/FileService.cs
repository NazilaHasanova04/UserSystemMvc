using Microsoft.AspNetCore.StaticFiles;
using UserManagementMvc.Data;
using UserManagementMvc.Entities;
using UserManagementMvc.Models.CustomResponse;
using UserManagementMvc.Repos.Abstraction;
using UserManagementMvc.Servicess.Abstraction;

namespace UserManagementMvc.Servicess.Implementation
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IConfiguration _configuration;


        public FileService(UserDbContext studentDbContext, IFileRepository fileRepository,
               IConfiguration configuration)
        {
            _fileRepository = fileRepository;
            _configuration = configuration;
        }


        public async Task<FileResponseDto<FileDetails>> UploadFile(IFormFile file)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            string fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{timestamp}{Path.GetExtension(file.FileName)}";

            string fullPath = Path.Combine(_configuration["FilePath"], fileName);

            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(fullPath, FileMode.CreateNew))
                {
                    await file.CopyToAsync(fileStream);
                }

                var fileDetails = new FileDetails()
                {
                    Name = fileName,
                    FileType = file.ContentType,
                    Path = fullPath,
                    StudentFiles = new List<StudentFile>()
                };

                await _fileRepository.AddAsync(fileDetails);

                return FileResponseDto<FileDetails>.Success(fileDetails);
            }
            return FileResponseDto<FileDetails>.Failure(Statics.StaticMessages.FileNotExist);
        }

        public async Task<FileResponseDto<(byte[] fileBytes, string contentType, string fileName)>> Download(int id)
        {
            var fileDetail = await _fileRepository.GetByIdAsync(id);

            if (fileDetail == null || !File.Exists(fileDetail.Path))
            {
                return FileResponseDto<(byte[] fileBytes, string contentType, string fileName)>
                    .Failure(Statics.StaticMessages.FileNotFound);
            }

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileDetail.Path, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var fileBytes = await File.ReadAllBytesAsync(fileDetail.Path);

            return FileResponseDto<(byte[] fileBytes, string contentType, string fileName)>
                .Success((fileBytes, contentType, Path.GetFileName(fileDetail.Path)));
        }
    }
}
