using System;

namespace Moosend.Api.Common.Responses
{
    public class ApiResponse
    {
        public ApiResponse()
        { }

        public ApiResponse(int code)
        {
            Code = code;
        }

        public int Code { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public ApiResponse()
        { }

        public ApiResponse(int code, string error, T context)
            : base(code)
        {
            if (error == null) throw new ArgumentNullException("error");

            Error = error;
            Context = context;
        }

        public T Context { get; set; }
        public string Error { get; set; }
    }
}
