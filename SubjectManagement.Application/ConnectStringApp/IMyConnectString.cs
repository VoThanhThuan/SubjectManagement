using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.InfoDatabase;
using SubjectManagement.Common.Result;

namespace SubjectManagement.Application.ConnectStringApp
{
    public interface IMyConnectString
    {
        public Result<string> CreateConnectString(InfoDb infoDb);
        public Result<InfoDb> ReadConnectString();

        public bool TestConnectString();
    }
}
