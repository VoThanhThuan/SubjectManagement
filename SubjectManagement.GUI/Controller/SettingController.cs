using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;
using SubjectManagement.Application;
using SubjectManagement.Application.ConnectStringApp;
using SubjectManagement.Common.InfoDatabase;
using SubjectManagement.Common.Result;
using SubjectManagement.GUI.Constant;
using SubjectManagement.GUI.Dialog;
using Color = DocumentFormat.OpenXml.Spreadsheet.Color;
using ConnectString = DocumentFormat.OpenXml.Wordprocessing.ConnectString;

namespace SubjectManagement.GUI.Controller
{
    public class SettingController
    {
        public SettingController()
        {
            _connect = new MyConnectString();
        }

        private IMyConnectString _connect;
        public void WriteSetting(SettingApp setting)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            using var createStream = File.Create("setting.json");
            JsonSerializer.SerializeAsync(createStream, setting, options);
        }

        public SettingApp ReadSetting()
        {
            if (File.Exists("setting.json") != true) return null;
            using var r = new StreamReader("setting.json");
            var json = r.ReadToEnd();
            var items = JsonSerializer.Deserialize<SettingApp>(json);
            return items;
        }

        public InfoDb CreateConnectString(bool isWindowsAuthentication, string serverName, string databaseName,string username = "", string password = "")
        {
            //var connectString = "";
            var infoDb = new InfoDb();
            if (isWindowsAuthentication)
            {
                infoDb.AccessMode = "authentication";
                infoDb.ServerName = serverName;
                infoDb.DatabaseName = databaseName;
                //connectString = $@"Server ={serverName}; Database={databaseName}; Trusted_Connection=True;";
            }
            else
            {
                infoDb.AccessMode = "uid";
                infoDb.ServerName = serverName;
                infoDb.DatabaseName = databaseName;
                infoDb.Uid = username;
                infoDb.Password = password;
                // = $@"Server ={serverName}; Database={databaseName}; User Id={username}; Password={password}; Trusted_Connection=True; MultipleActiveResultSets=true;";
            }

            return infoDb;
        }


        public void WriteConnectString(InfoDb infoDb)
        {
            var conn = _connect.CreateConnectString(infoDb);
            if (conn.IsSuccessed)
            {
                MyCommonDialog.MessageDialog("Lỗi tạo kết nối", $"{conn.Message}");
            }
            else
            {
                MyCommonDialog.MessageDialog("Lỗi tạo kết nối", $"{conn.Message}");
            }

        }

        public Result<InfoDb> ReadConnectString()
        {
            var conn = _connect.ReadConnectString();
            if (conn.IsSuccessed) return conn;

            MyCommonDialog.MessageDialog($"{conn.Message}", "Vào cài đặt để thiết lập chuỗi kết nối");

            return conn;
        }

        public void TestConnect()
        {
            var result = _connect.TestConnectString();
            if (result)
            {
                MyCommonDialog.MessageDialog("Thành công", "Đã kết nối được cơ sở dữ liệu", Colors.DeepSkyBlue);
            }
            else
            {
                MyCommonDialog.MessageDialog("Lỗi kết nối", "Không thể kết nối đến cơ sở dữ liệu");
            }

        }

    }
}
