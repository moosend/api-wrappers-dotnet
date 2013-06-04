using System;
using System.Collections.Generic;
using System.Text;

namespace Moosend.API.Client
{
    public class ApiResult<T>
    {        
        public int Code { get; set; }
        
        public T Context { get; set; }
        
        public string Error { get; set; }
    }
}
