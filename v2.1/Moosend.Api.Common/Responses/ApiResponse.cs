namespace Moosend.Api.Common.Responses
{
    public class ApiResponse
    {
        public ApiResponse()
        {
        }

        public ApiResponse(int code, string error)
        {
            Code = code;
            Error = error;
        }

        public int Code { get; set; }
        public string Error { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public ApiResponse()
        {
        }

        public ApiResponse(int code, string error, T context)
            : base(code, error)
        {
            Context = context;
        }

        public T Context { get; set; }
    }
}