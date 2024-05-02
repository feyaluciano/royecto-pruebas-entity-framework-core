using System.Net;

namespace ProyectoPruebasEntityFrameworkCore.Utils
{
    public class BaseResponse<T>
    {
        public bool IsSuccess { get; set; }
        
        public System.Net.HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public T Result { get; set; }
        public List<string> ErrorMessages { get; set; }

        public BaseResponse(T result)
        {
            Result = result;
        }

        public BaseResponse()
        {
            ErrorMessages = new List<string>();
        }

        public BaseResponse<T> WithResultOk(T result)
        {
            this.StatusCode = System.Net.HttpStatusCode.OK;
            this.Result = result;
            this.IsSuccess = true;
            return this;
        }

        public BaseResponse<T> WithResultNoContent()
        {
            this.StatusCode = HttpStatusCode.NoContent;
            return this;
        }

        public BaseResponse<T> WithNotFound(string message)
        {
            this.StatusCode = HttpStatusCode.NotFound;
            this.Message = message;
            this.IsSuccess = false;
            return this;
        }

        public BaseResponse<T> WithBadRequest(string message)
        {
            this.StatusCode = HttpStatusCode.BadRequest;
            this.Message = message;
            return this;
        }

    }
}
