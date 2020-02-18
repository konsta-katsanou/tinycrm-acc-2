using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TinyCrmApi.Model
{
    public enum StatusCode
    {
        Success = 200,
        NotFound = 404,
        BadRequest = 403,
        InternalServerError = 500
    }
}