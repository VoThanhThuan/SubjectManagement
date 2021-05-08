﻿using System;
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
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Lỗi tạo kết nối" },
                    tbl_Message = { Text = $"{conn.Message}" },
                    title_color = { Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(68, 140, 203)) },
                    Topmost = true
                };
                mess.ShowDialog();
            }
            else
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Lỗi tạo kết nối" },
                    tbl_Message = { Text = $"{conn.Message}" },
                    title_color = { Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
            }

        }

        public Result<InfoDb> ReadConnectString()
        {
            var conn = _connect.ReadConnectString();
            if (conn.IsSuccessed) return conn;
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"{conn.Message}" },
                tbl_Message = { Text = $"Vào cài đặt để thiết lập chuỗi kết nối" },
                title_color = { Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
            return conn;
        }

        public void TestConnect()
        {
            var result = _connect.TestConnectString();
            if (result)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Thành công" },
                    tbl_Message = { Text = $"Đã kết nối được cơ sở dữ liệu" },
                    title_color = { Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(68, 140, 203)) },
                    Topmost = true
                };
                mess.ShowDialog();
            }
            else
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Lỗi kết nối" },
                    tbl_Message = { Text = $"Không thể kết nối đến cơ sở dữ liệu" },
                    title_color = { Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
            }

        }

    }
}
