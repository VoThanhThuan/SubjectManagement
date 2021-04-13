using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.EF;

namespace SubjectManagement.Application.ConnectStringApp
{
    public class MyConnectString : IMyConnectString
    {

        public Result<string> CreateConnectString(string connectString)
        {
            var encode = Uri.EscapeUriString(connectString);
            try
            {
                using var binWriter = new BinaryWriter(new FileStream("ConnectString.json", FileMode.Create, FileAccess.Write));
                foreach (var num in encode.Select(c => Convert.ToInt32(c) + 1))
                {
                    binWriter.Write(num);
                }
            }
            catch (Exception e)
            {
                return new ResultError<string>("Lỗi tạo file");
                throw;
            }

            return new ResultSuccess<string>("Đã tạo chuỗi kết nối");
        }

        public Result<string> ReadConnectString()
        {
            if (File.Exists("ConnectString.json") != true) return new ResultError<string>("Không tìm thấy chuỗi kết nối");
            try
            {
                //Đọc file
                using var binReader = new BinaryReader(new FileStream("ConnectString.json", FileMode.Open, FileAccess.Read));

                //Giải mã
                var connectString = "";
                while (binReader.BaseStream.Position != binReader.BaseStream.Length)
                {
                    var text = char.ConvertFromUtf32(binReader.ReadInt32() - 1);
                    connectString += text;
                }
                MyConnect.ConnectString = Uri.UnescapeDataString(connectString);
            }
            catch (Exception e)
            {
                return new ResultError<string>("Lỗi đọc chuỗi kết nối");
                throw;
            }

            var check = TestConnectString();
            if (!check) return new ResultError<string>("không kết nối được CSDL");
            return new ResultSuccess<string>("Đã lấy được chuỗi kế nối");
        }

        public bool TestConnectString()
        {
            try
            {
                using var connection = new SqlConnection(MyConnect.ConnectString);
                connection.Open();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
