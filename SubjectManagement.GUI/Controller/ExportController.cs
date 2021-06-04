using ClosedXML.Excel;
using SubjectManagement.Application.Alternative;
using SubjectManagement.Application.CompareApp;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Dialog;
using SubjectManagement.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows.Media;

namespace SubjectManagement.GUI.Controller
{
    public class ExportController
    {
        public ExportController(Class _class)
        {
            _subjectService = new SubjectService();
            _alternativeService = new AlternativeService();
            _compareService = new CompareService();

            _Class = _class;
        }
        public ExportController(Class _class, Class _classCompare)
        {
            _compareService = new CompareService();
            _ClassCompare = _classCompare;
            _Class = _class;
        }
        private readonly ISubjectService _subjectService;
        private readonly IAlternativeService _alternativeService;
        private readonly ICompareService _compareService; 

        public Class _ClassOld { get; set; }
        private Class _Class { get; init; }
        private Class _ClassCompare { get; init; }

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

        public void ExportSubjectForExcel(string filename)
        {
            try
            {
                using var workbook = new XLWorkbook();
                var group = _subjectService.GetKnowledgeGroup();
                var worksheet = workbook.Worksheets.Add("Sample Sheet");

                var cell = 1;
                foreach (var item in group)
                {
                    var subjectInGroup = _subjectService.GetSubjectWithGroup(item.ID, _Class.ID);
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
                    worksheet.Cell($"G{cell}").Value = "Học kỳ";
                    worksheet.Cell($"H{cell}").Value = "Mô tả";

                    worksheet.Cell($"A{cell}").Style.Font.Bold = true;
                    worksheet.Cell($"B{cell}").Style.Font.Bold = true;
                    worksheet.Cell($"C{cell}").Style.Font.Bold = true;
                    worksheet.Cell($"D{cell}").Style.Font.Bold = true;
                    worksheet.Cell($"E{cell}").Style.Font.Bold = true;
                    worksheet.Cell($"F{cell}").Style.Font.Bold = true;
                    worksheet.Cell($"G{cell}").Style.Font.Bold = true;
                    worksheet.Cell($"H{cell}").Style.Font.Bold = true;

                    cell++;
                    foreach (var subject in subjectInGroup)
                    {
                        worksheet.Cell($"A{cell}").Value = subject.CourseCode;
                        worksheet.Cell($"B{cell}").Value = subject.Name;
                        worksheet.Cell($"C{cell}").Value = subject.Credit;
                        worksheet.Cell($"D{cell}").Value = subject.TypeCourse ? "Bắt buộc" : "Tự chọn";
                        worksheet.Cell($"E{cell}").Value = subject.NumberOfTheory;
                        worksheet.Cell($"F{cell}").Value = subject.NumberOfPractice;
                        worksheet.Cell($"G{cell}").Value = subject.Semester;
                        worksheet.Cell($"H{cell}").Value = subject.Details;
                        cell++;
                    }

                }
                worksheet.Columns().AdjustToContents();  // Adjust column width
                worksheet.Rows().AdjustToContents();     // Adjust row heights
                workbook.SaveAs(filename);
            }
            catch (Exception e)
            {
                MyCommonDialog.MessageDialog("Lỗi lưu file", $"{e.Message}");

            }
        }

        public async void ExportSubjectForJSON(string filename)
        {
            try
            {
                var groups = _subjectService.GetKnowledgeGroup();

                var _data = new List<PrintSubjectJSON>();

                foreach (var item in groups)
                {
                    var subjectInGroup = _subjectService.GetSubjectWithGroup(item.ID, _Class.ID);

                    var title = $"{item.Name}" +
                                $" - {subjectInGroup.Sum(x => x.Credit)} TC " +
                                $" - {subjectInGroup.Count(x => x.TypeCourse == true)} Bắt buộc";

                    var contentJson = new PrintSubjectJSON();//Chứa nội dung file json

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
                MyCommonDialog.MessageDialog("Lỗi lưu file", $"{e.Message}");
            }
        }

        public void ExportAlternativeForExcel(string filename)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("AlternativeSubject");
            var subjects = _subjectService.GetSubject(_Class.ID);
            var cell = 1;
            worksheet.Cell($"A{cell}").Value = "Nhhóm học phần cũ";
            worksheet.Range("A1:D1").Merge();
            worksheet.Cell($"E{cell}").Value = "Nhhóm học phần mới";
            worksheet.Range("E1:H1").Merge();
            foreach (var subject in subjects)
            {
                cell++;
                worksheet.Cell($"A{cell}").Value = "Mã môn";
                worksheet.Cell($"B{cell}").Value = "Tên môn";
                worksheet.Cell($"C{cell}").Value = "Tín chỉ";
                worksheet.Cell($"D{cell}").Value = "Học kỳ";
                worksheet.Cell($"E{cell}").Value = "Mã môn";
                worksheet.Cell($"F{cell}").Value = "Tên môn";
                worksheet.Cell($"G{cell}").Value = "Tín chỉ";
                worksheet.Cell($"H{cell}").Value = "Học kỳ";
                var alterSubjects = _alternativeService.GetAlternative(_Class.ID, subject.ID, _ClassOld.ID);
                if (alterSubjects == null) continue;
                cell++;

                worksheet.Cell($"A{cell}").Value = subject.CourseCode;
                worksheet.Cell($"B{cell}").Value = subject.Name;
                worksheet.Cell($"C{cell}").Value = subject.Credit;
                worksheet.Cell($"D{cell}").Value = subject.Semester;

                var startColor = cell;
                foreach (var alter in alterSubjects)
                {
                    worksheet.Cell($"E{cell}").Value = alter.CourseCode;
                    worksheet.Cell($"F{cell}").Value = alter.Name;
                    worksheet.Cell($"G{cell}").Value = alter.Credit;
                    worksheet.Cell($"H{cell}").Value = alter.Semester;
                    cell++;
                }
                worksheet.Range($"A{startColor}:H{cell}").Style.Fill.BackgroundColor = XLColor.PowderBlue;
                cell++;
            }

            worksheet.Columns().AdjustToContents();  // Adjust column width
            worksheet.Rows().AdjustToContents();     // Adjust row heights

            workbook.SaveAs(filename);
        }

        public async void ExportAlternativeForJson(string filename)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("AlternativeSubject");
            var subjects = _subjectService.GetSubject(_Class.ID);
            var _data = new List<PrintAlternativeJSON>();
            foreach (var subject in subjects)
            {
                var content = new PrintAlternativeJSON();
                content.Subject = subject;

                var alterSubjects = _alternativeService.GetAlternative(_Class.ID, subject.ID, _ClassOld.ID);
                if (alterSubjects == null) continue;
                foreach (var alter in alterSubjects)
                {
                    content.SubjectsAlternative.Add(alter);
                }
                _data.Add(content);
            }

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            await using var createStream = File.Create(filename);
            await JsonSerializer.SerializeAsync(createStream, _data, options);

        }


        List<SubjectCompareVM> _listDefault = new();
        List<SubjectCompareVM> _listOriginal = new();
        List<SubjectCompareVM> _listChange = new();
        List<SubjectCompareVM> _listPlus = new();
        List<SubjectCompareVM> _listRemove = new();
        private List<List<SubjectCompareVM>> _listSubjects = new();

        private List<List<SubjectCompareVM>> LoadValueCompare()
        {

            var list = _compareService.CompareSubject(_Class.ID, _ClassCompare.ID).ResultObj;

            foreach (var item in list)
            {
                switch (item.Different)
                {
                    case SubjectDifferent.Different.SubjectDefault:
                        _listDefault.Add(item);
                        break;
                    case SubjectDifferent.Different.SubjectChange:
                        _listChange.Add(item);
                        break;
                    case SubjectDifferent.Different.SubjectNew:
                        _listPlus.Add(item);
                        break;
                    case SubjectDifferent.Different.SubjectRemove:
                        _listRemove.Add(item);
                        break;
                    case SubjectDifferent.Different.SubjectOriginal:
                        _listOriginal.Add(item);
                        break;
                    default:
                        MyCommonDialog.MessageDialog("Lỗi ngoài ý muốn khi so sánh");
                        break;
                }
            }

            _listSubjects.Add(_listDefault);
            _listSubjects.Add(_listOriginal);
            _listSubjects.Add(_listChange);
            _listSubjects.Add(_listPlus);
            _listSubjects.Add(_listRemove);

            return _listSubjects;
        }

        public void ExportCompareForExcel(string filename)
        {
            try
            {
                var listSubjects = LoadValueCompare();

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("CompareSubject");
                var cell = 0;
                string[] listName = { "Các môn không có thay đổi", "Các môn học có thay đổi", "Môn học gốc", "Các môn thêm mới", "Các môn xóa bỏ" };

                //cell++;
                //worksheet.Cell($"B{cell}").Value = $"{_listOriginal[0].CodeClass} sẽ không có màu";
                //cell++;
                //worksheet.Cell($"B{cell}").Value = $"{_listChange[0].CodeClass} sẽ có màu AntiqueBrass như thế này";
                //worksheet.Range($"B{cell}").Style.Fill.BackgroundColor = XLColor.AntiqueBrass;

                for (var i = 0; i < 5; i++)
                {
                    cell++;
                    worksheet.Cell($"A{cell}").Value = listName[i];
                    worksheet.Range($"A{cell}:H{cell}").Style.Fill.BackgroundColor = XLColor.Almond;
                    cell++;
                    worksheet.Cell($"A{cell}").Value = "Mã môn";
                    worksheet.Cell($"B{cell}").Value = "Tên môn";
                    worksheet.Cell($"C{cell}").Value = "Tín chỉ";
                    worksheet.Cell($"D{cell}").Value = "Loại học phần";
                    worksheet.Cell($"E{cell}").Value = "Tiết lý thuyết";
                    worksheet.Cell($"F{cell}").Value = "Tiết thực hành";
                    worksheet.Cell($"G{cell}").Value = "Học kỳ";
                    worksheet.Cell($"H{cell}").Value = "Mô tả";
                    worksheet.Cell($"I{cell}").Value = "Lớp";

                    if (i == 1)
                    {
                        
                        for (var j = 0; j < _listOriginal.Count; j++)
                        {
                            cell++;
                            worksheet.Cell($"A{cell}").Value = _listOriginal[j].CourseCode;
                            worksheet.Cell($"B{cell}").Value = _listOriginal[j].Name;
                            worksheet.Cell($"C{cell}").Value = _listOriginal[j].Credit;
                            worksheet.Cell($"D{cell}").Value = _listOriginal[j].TypeCourse;
                            worksheet.Cell($"E{cell}").Value = _listOriginal[j].NumberOfTheory;
                            worksheet.Cell($"F{cell}").Value = _listOriginal[j].NumberOfPractice;
                            worksheet.Cell($"G{cell}").Value = _listOriginal[j].Semester;
                            worksheet.Cell($"H{cell}").Value = _listOriginal[j].Details;
                            worksheet.Cell($"I{cell}").Value = _listOriginal[j].CodeClass;

                            cell++;
                            worksheet.Cell($"A{cell}").Value = _listChange[j].CourseCode;
                            worksheet.Cell($"B{cell}").Value = _listChange[j].Name;
                            worksheet.Cell($"C{cell}").Value = _listChange[j].Credit;
                            worksheet.Cell($"D{cell}").Value = _listChange[j].TypeCourse;
                            worksheet.Cell($"E{cell}").Value = _listChange[j].NumberOfTheory;
                            worksheet.Cell($"F{cell}").Value = _listChange[j].NumberOfPractice;
                            worksheet.Cell($"G{cell}").Value = _listChange[j].Semester;
                            worksheet.Cell($"H{cell}").Value = _listChange[j].Details;
                            worksheet.Cell($"I{cell}").Value = _listChange[j].CodeClass;
                            worksheet.Range($"A{cell}:I{cell}").Style.Fill.BackgroundColor = XLColor.MediumAquamarine;
                            cell++;
                        }

                        i++;
                        continue;
                    }

                    foreach (var subject in listSubjects[i])
                    {
                        cell++;
                        worksheet.Cell($"A{cell}").Value = subject.CourseCode;
                        worksheet.Cell($"B{cell}").Value = subject.Name;
                        worksheet.Cell($"C{cell}").Value = subject.Credit;
                        worksheet.Cell($"D{cell}").Value = subject.TypeCourse;
                        worksheet.Cell($"E{cell}").Value = subject.NumberOfTheory;
                        worksheet.Cell($"F{cell}").Value = subject.NumberOfPractice;
                        worksheet.Cell($"G{cell}").Value = subject.Semester;
                        worksheet.Cell($"H{cell}").Value = subject.Details;
                    }

                    cell++;
                }

                worksheet.Columns().AdjustToContents();  // Adjust column width
                worksheet.Rows().AdjustToContents();     // Adjust row heights

                workbook.SaveAs(filename);
            }
            catch (Exception e)
            {
                MyCommonDialog.MessageDialog("Lỗi lưu file", $"{e.Message}");
            }
        }

        public async void ExportCompareForJson(string filename)
        {
            try
            {

                LoadValueCompare();

                var _data = new List<PrintCompareJSON>();
                var content = new PrintCompareJSON();

                content.Name = "Môn không thay đổi";
                content.Subjects = _listDefault;
                _data.Add(content);

                content.Name = "Môn thay đổi";
                content.Subjects = _listChange;
                _data.Add(content);

                content.Name = "Môn thêm mới";
                content.Subjects = _listPlus;
                _data.Add(content);

                content.Name = "Môn xóa bỏ";
                content.Subjects = _listRemove;
                _data.Add(content);

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
                MyCommonDialog.MessageDialog("Lỗi lưu file", $"{e.Message}");
            }

        }


    }

    class PrintSubjectJSON
    {
        public string KnowledgeGroup { get; set; }
        public List<Subject> Subjects { get; set; }

    }
    class PrintAlternativeJSON
    {
        public Subject Subject { get; set; }
        public List<Subject> SubjectsAlternative { get; set; }

    }
    class PrintCompareJSON
    {
        public string Name { get; set; }
        public List<SubjectCompareVM> Subjects { get; set; }

    }
}
