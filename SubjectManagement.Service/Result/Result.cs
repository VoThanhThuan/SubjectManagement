using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Common.Result
{
    public class Result<T>
    {
        public bool IsSuccessed { get; set; }
        public string Message { get; set; }
        public T ResultObj { get; set; }
    }
}
