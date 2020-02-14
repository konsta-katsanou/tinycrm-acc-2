using System;
using System.Collections.Generic;
using TinyCrm.Core.Model;

namespace TinyCrmConsole.Model
{
    public class ApiResult<T>
    {
        public StatusCode Error { get; set; }

        public string ErrorText { get; set; }

        public T Data {get; set;}
    }
}
