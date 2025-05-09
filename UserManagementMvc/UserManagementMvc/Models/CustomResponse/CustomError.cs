using System.Net;


namespace UserManagementMvc.Models.CustomResponse
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse()
        {
        }

        private ApiResponse(bool isSuccess, string message, HttpStatusCode statusCode, T data = default, List<string>? errors = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
            Data = data;
            Errors = errors;
        }

        public static ApiResponse<T> Success(T data, HttpStatusCode statusCode, string message = "Operation successful")
            => new(true, message, statusCode, data);

        public static ApiResponse<T> Success(HttpStatusCode statusCode, string message = "Operation successful")
           => new(true, message, statusCode);

        public static ApiResponse<T> Failure(string message, HttpStatusCode statusCode, List<string>? errors = null)
            => new(false, message, statusCode, default, errors);

    }
}
