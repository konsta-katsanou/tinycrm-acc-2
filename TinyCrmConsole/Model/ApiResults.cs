using System;
using System.Collections.Generic;
using TinyCrm.Core.Model;
using TinyCrmApi.Model;
using TinyCrmConsole.Model;

namespace TinyCrm.Core
{
    public class ApiResult<T>
    {
        public T Data { get; set; }

        public string ErrorText { get; set; }

        public StatusCode ErrorCode { get; set; }

        public bool Success => ErrorCode == StatusCode.Success;


        public ApiResult()
        { }


        public ApiResult(StatusCode code,
            string errortext)
        {
            ErrorCode = code;
            ErrorText = errortext;
        }

        //auta einai factoring den xreiazetai na kanoume new me ton constructor
        public static ApiResult<T> Create<Y>(
            ApiResult<Y> result)
        {
            return new ApiResult<T>()
            {
                ErrorCode = result.ErrorCode,
                ErrorText = result.ErrorText
            };
        }

        public static ApiResult<T> CreateSuccessful(
            T data)
        {
            return new ApiResult<T>()
            {
                ErrorCode = StatusCode.Success,
                Data = data
            };
        }


        public static ApiResult<T> CreateUnSuccessful( 
            StatusCode code,
            string errortext)
        {
            return new ApiResult<T>()
            {
                ErrorCode = code ,
                ErrorText = errortext
                
            };
        }
    }
}
