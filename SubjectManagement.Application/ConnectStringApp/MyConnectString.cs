using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SubjectManagement.Common.InfoDatabase;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.EF;

namespace SubjectManagement.Application.ConnectStringApp
{
    public class MyConnectString : IMyConnectString
    {

        private string Encode(string str)
        {
            var strEncode = "";
            for (var i = 0; i < str.Length; i++)
            {
                if (i != str.Length - 1)
                {
                    strEncode += $"{Convert.ToChar(Convert.ToInt32(str[i]) + 1)}-";
                    continue;
                }
                strEncode += $"{Convert.ToChar(Convert.ToInt32(str[i]) + 1)}";
            }

            return strEncode;
        }

        private string Uncode(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            var listChar = str.Split('-');

            return listChar.Aggregate("", (current, t) => current + $"{Convert.ToChar(Convert.ToInt32(Convert.ToChar(t)) - 1)}");
        }

        public Result<string> CreateConnectString(InfoDb infoDb)
        {
            try
            {
                infoDb.Uid = Encode( infoDb.Uid);

                infoDb.Password = Encode(infoDb.Password);


                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };
                using var createStream = File.Create("ConnectString.json");
                JsonSerializer.SerializeAsync(createStream, infoDb, options);
            }
            catch (Exception e)
            {
                return new ResultError<string>($"Lỗi tạo file {e}");
            }

            return new ResultSuccess<string>("Đã tạo chuỗi kết nối");
        }

        public Result<InfoDb> ReadConnectString()
        {
            if (File.Exists("ConnectString.json") != true) return new ResultError<InfoDb>("Không tìm thấy chuỗi kết nối");
            var items = new InfoDb();
            try
            {
                using var r = new StreamReader("ConnectString.json");
                var json = r.ReadToEnd();
                items = JsonSerializer.Deserialize<InfoDb>(json);

                items.Uid = Uncode(items.Uid);
                items.Password = Uncode(items.Password);

                ////Đọc file
                //using var binReader = new BinaryReader(new FileStream("ConnectString.json", FileMode.Open, FileAccess.Read));

                ////Giải mã
                //var connectString = "";
                //while (binReader.BaseStream.Position != binReader.BaseStream.Length)
                //{
                //    var text = char.ConvertFromUtf32(binReader.ReadInt32() - 1);
                //    connectString += text;
                //}
                if (items.AccessMode == "authentication")
                    MyConnect.ConnectString = $@"Server ={items.ServerName}; Database={items.DatabaseName}; Trusted_Connection=True;";
                else
                    MyConnect.ConnectString = $@"Server ={items.ServerName}; Database={items.DatabaseName}; User Id={items.Uid}; Password={items.Password}; MultipleActiveResultSets=true;";
                
            }
            catch (Exception e)
            {
                return new ResultError<InfoDb>($"Lỗi đọc chuỗi kết nối {e}");
            }

            var check = TestConnectString();
            if (!check) return new ResultError<InfoDb>("không kết nối được CSDL");
            return new ResultSuccess<InfoDb>(items, "Đã lấy được chuỗi kế nối");

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
