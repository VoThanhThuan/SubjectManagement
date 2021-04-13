using ClosedXML.Excel;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Common.Dialog;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Media;

namespace SubjectManagement.GUI.Controller
{
    public class ExportController
    {
        public ExportController(Class _class)
        {
            _subjectService = new SubjectService();
            _Class = _class;
        }

        private readonly ISubjectService _subjectService;
        private Class _Class { get; init; }

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            var properties =
                TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable("DUMANO");
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, typeof(string));
            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public async void ExportForExcel(string filename)
        {
            try
            {
                using var workbook = new XLWorkbook();
                var group = await _subjectService.LoadKnowledgeGroup();
                var worksheet = workbook.Worksheets.Add("Sample Sheet");

                var cell = 1;
                foreach (var item in group)
                {
                    var subjectInGroup = _subjectService.LoadSubjectWithGroup(item.ID, _Class.ID);
                    var title = $"{item.Name}" +
                                $" - {subjectInGroup.Sum(x => x.Credit)} TC " +
                                $" - {subjectInGroup.Count(x => x.TypeCourse == true)} Bắt buộc";

                    worksheet.Cell($"A{cell}").Value = title;
                    cell++;
                    worksheet.Cell($"A{cell}").Value = "Mã môn";
                    worksheet.Cell($"B{cell}").Value = "Tên môn";
                    worksheet.Cell($"C{cell}").Value = "Tín chỉ";
                    worksheet.Cell($"D{cell}").Value = "Loại học phần";
                    worksheet.Cell($"E{cell}").Value = "Tiết lý thuyết";
                    worksheet.Cell($"F{cell}").Value = "Tiết thực hành";
                    worksheet.Cell($"G{cell}").Value = "Mô tả";

                    worksheet.Cell($"A{cell}").Style.Font.Bold = true;
                    worksheet.Cell($"B{cell}").Style.Font.Bold = true;
                    worksheet.Cell($"C{cell}").Style.Font.Bold = true;
                    worksheet.Cell($"D{cell}").Style.Font.Bold = true;
                    worksheet.Cell($"E{cell}").Style.Font.Bold = true;
                    worksheet.Cell($"F{cell}").Style.Font.Bold = true;
                    worksheet.Cell($"G{cell}").Style.Font.Bold = true;

                    cell++;
                    foreach (var subject in subjectInGroup)
                    {
                        worksheet.Cell($"A{cell}").Value = subject.CourseCode;
                        worksheet.Cell($"B{cell}").Value = subject.Name;
                        worksheet.Cell($"C{cell}").Value = subject.Credit;
                        worksheet.Cell($"D{cell}").Value = subject.TypeCourse;
                        worksheet.Cell($"E{cell}").Value = subject.NumberOfTheory;
                        worksheet.Cell($"F{cell}").Value = subject.NumberOfPractice;
                        worksheet.Cell($"G{cell}").Value = subject.Details;
                        cell++;
                    }

                }
                workbook.SaveAs(filename);
            }
            catch (Exception e)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Lỗi lưu file" },
                    tbl_Message = { Text = $"{e.Message}" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
            }
            var mess1 = new MessageDialog()
            {
                tbl_Title = { Text = $"Lưu Thành Công" },
                tbl_Message = { Text = $"Đã lưu file vào {filename}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(68, 140, 203)) },
                Topmost = true
            };
            mess1.ShowDialog();
        }

        public async void ExportForJSON(string filename)
        {
            try
            {
                var groups = await _subjectService.LoadKnowledgeGroup();

                var _data = new List<PrintJSON>();

                foreach (var item in groups)
                {
                    var subjectInGroup = _subjectService.LoadSubjectWithGroup(item.ID, _Class.ID);

                    var title = $"{item.Name}" +
                                $" - {subjectInGroup.Sum(x => x.Credit)} TC " +
                                $" - {subjectInGroup.Count(x => x.TypeCourse == true)} Bắt buộc";

                    var contentJson = new PrintJSON();//Chứa nội dung file json

                    contentJson.KnowledgeGroup = title;//dang sách các nhóm học phần

                    var listSubject = subjectInGroup.Select(subject => new Subject()
                        {
                            ID = subject.ID,
                            CourseCode = subject.CourseCode,
                            Name = subject.Name,
                            Credit = subject.Credit,
                            TypeCourse = subject.TypeCourse,
                            NumberOfTheory = subject.NumberOfTheory,
                            NumberOfPractice = subject.NumberOfPractice,
                            Prerequisite = subject.Prerequisite,
                            LearnFirst = subject.LearnFirst,
                            Parallel = subject.Parallel,
                            Details = subject.Details,
                            IDClass = subject.IDClass
                        })
                        .ToList(); // các học phần trong nhóm

                    contentJson.Subjects = listSubject; //add nội dung cho nhóm môn học

                    _data.Add(contentJson);

                }

                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };

                await using var createStream = File.Create(filename);
                await JsonSerializer.SerializeAsync(createStream, _data, options);
            }
            catch (Exception e)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Lỗi lưu file" },
                    tbl_Message = { Text = $"{e.Message}" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
            }
            var mess1 = new MessageDialog()
            {
                tbl_Title = { Text = $"Lưu Thành Công" },
                tbl_Message = { Text = $"Đã lưu file vào {filename}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(68, 140, 203)) },
                Topmost = true
            };
            mess1.ShowDialog();
        }

    }

    class PrintJSON
    {
        public string KnowledgeGroup { get; set; }
        public List<Subject> Subjects { get; set; }

    }

}
