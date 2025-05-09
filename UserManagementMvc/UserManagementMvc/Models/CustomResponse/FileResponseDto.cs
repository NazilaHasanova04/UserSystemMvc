namespace UserManagementMvc.Models.CustomResponse
{
    public class FileResponseDto<T>
    {
        public T Data { get; }
        public string Message { get; }
        public bool IsSuccess { get; }

        private FileResponseDto(T data, string message, bool isSuccess)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public static FileResponseDto<T> Success(T data, string message = "Operation successful")
            => new(data, message, true);

        public static FileResponseDto<T> Failure(string message, T data = default)
            => new(data, message, false);
    }
}
