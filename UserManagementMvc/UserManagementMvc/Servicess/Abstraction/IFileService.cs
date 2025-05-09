using UserManagementMvc.Entities;
using UserManagementMvc.Models.CustomResponse;

namespace UserManagementMvc.Servicess.Abstraction
{
    public interface IFileService
    {
        Task<FileResponseDto<FileDetails>> UploadFile(IFormFile file);
        Task<FileResponseDto<(byte[] fileBytes, string contentType, string fileName)>> Download(int id);
    }

}