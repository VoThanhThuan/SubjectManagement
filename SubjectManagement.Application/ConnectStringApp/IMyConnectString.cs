using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;

namespace SubjectManagement.Application.ConnectStringApp
{
    public interface IMyConnectString
    {
        public Result<string> CreateConnectString(string connectString);
        public Result<string> ReadConnectString();

        public bool TestConnectString();
    }
}
