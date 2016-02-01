using System;

namespace Moosend.Api.Common
{
    public class ApiClientException : Exception
    {
        public ApiClientException()
        {
        }

        public ApiClientException(string message) : base(message)
        {
        }

        public ApiClientException(string message, int code)
        : base(message)
        {
            Code = code;
        }

        public ApiClientException(string message, int code, Exception inner)
            : base(message, inner)
        {
            Code = code;
        }

        public int Code { get; set; }
    }
}
