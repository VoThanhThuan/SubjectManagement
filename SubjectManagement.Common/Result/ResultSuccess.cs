using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Common.Result
{
    public class ResultSuccess<T> : Result<T>
    { 
        public ResultSuccess(T resultObj, string message = "")
        {
            IsSuccessed = true;
            ResultObj = resultObj;
            Message = message;
        }

        public ResultSuccess(string message = "")
        {
            IsSuccessed = true;
            Message = message;
        }

        public ResultSuccess()
        {
            IsSuccessed = true;
        }
    }
}
