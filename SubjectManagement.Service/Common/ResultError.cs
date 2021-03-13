using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Service.Common
{
    public class ResultError<T> : Result<T>
    {
        public ResultError(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public ResultError()
        {
            IsSuccessed = false;
        }
    }
}
