using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.EF;
using SubjectManagement.Data.Entities;
using SubjectManagement.ViewModels.Subject;

namespace SubjectManagement.Application.CompareApp
{
    public class CompareService : ICompareService
    {
        public CompareService()
        {
            var connect = new Db();
            _db = connect.Context;
        }

        private readonly SubjectDbContext _db;

        public Result<List<SubjectCompareVM>> CompareSubject(int idClassCurrent, int idClassCompare)
        {
            var listSubject = new List<SubjectCompareVM>(); //Thằng này để lưu bảng dữ liệu hiển thị lên giao diện sau khi so sánh

            var class_1 = _db.Subjects.Where(x => x.IDClass == idClassCurrent).Select(x => x).ToList();
            var class_2 = _db.Subjects.Where(x => x.IDClass == idClassCompare).Select(x => x).ToList();
            //sẽ có trường hợp 1 mảng lớn hơn mảng còn lại, nên mình lấy số lượng của 2 mảng để xử lý.
            var indexLarge = class_1.Count > class_2.Count ? class_1.Count : class_2.Count;
            var indexSmall = class_1.Count > class_2.Count ? class_2.Count : class_1.Count;

            var subjectLarge = new List<Subject>();
            var subjectSmall = new List<Subject>();

            var year1 = _db.Classes.Find(idClassCurrent);
            var year2 = _db.Classes.Find(idClassCompare);

            var newYear = (year1.Year < year2.Year);

            if (class_1.Count > class_2.Count)
            {
                subjectLarge = class_1;
                subjectSmall = class_2;
            }
            else
            {
                subjectLarge = class_2;
                subjectSmall = class_1;
            }


            //Lấy mảng nhỏ hởn so sánh mảng lớn hơn
            for (var i = 0; i < indexSmall; indexSmall--)
            {
                var subjectCurrent = new SubjectCompareVM
                {
                    ID = subjectSmall[i].ID,
                    IdClass = subjectSmall[i].IDClass,
                    CourseCode = subjectSmall[i].CourseCode,
                    Name = subjectSmall[i].Name,
                    Credit = $"{subjectSmall[i].Credit}",
                    TypeCourse = subjectSmall[i].TypeCourse ? "Bắt buộc" : "Tự chọn",
                    NumberOfTheory = $"{subjectSmall[i].NumberOfTheory}",
                    NumberOfPractice = $"{subjectSmall[i].NumberOfPractice}",
                    Prerequisite = $"{subjectSmall[i].Prerequisite}",
                    LearnFirst = $"{subjectSmall[i].LearnFirst}",
                    Parallel = $"{subjectSmall[i].Parallel}",
                    CodeClass = $"{subjectSmall[i].Class.CodeClass}"
                };

                var subjectCompare = new SubjectCompareVM();
                var iOfLagre = -1;

                //tìm kiếm dữ liệu ở bảng cần so sánh
                for (var j = 0; j < subjectLarge.Count; j++)
                {
                    if (subjectSmall[i].ID != subjectLarge[j].ID) continue;

                    subjectCompare.ID = subjectLarge[j].ID;
                    subjectCompare.IdClass = subjectLarge[j].IDClass;
                    subjectCompare.CourseCode = subjectLarge[j].CourseCode;
                    subjectCompare.Name = subjectLarge[j].Name;
                    subjectCompare.Credit = $"{subjectLarge[j].Credit}";
                    subjectCompare.TypeCourse = subjectLarge[j].TypeCourse ? "Bắt buộc" : "Tự chọn";
                    subjectCompare.NumberOfTheory = $"{subjectLarge[j].NumberOfTheory}";
                    subjectCompare.NumberOfPractice = $"{subjectLarge[j].NumberOfPractice}";
                    subjectCompare.Prerequisite = $"{subjectLarge[j].Prerequisite}";
                    subjectCompare.LearnFirst = $"{subjectLarge[j].LearnFirst}";
                    subjectCompare.Parallel = $"{subjectLarge[j].Parallel}";
                    subjectCompare.CodeClass = $"{subjectLarge[j].Class.CodeClass}";

                    iOfLagre = j;
                }

                if (subjectCurrent.Equals(subjectCompare))
                {
                    listSubject.Add(subjectCurrent);
                }
                else
                {
                    subjectCompare.Different = SubjectDifferent.Different.SubjectChange;
                    subjectCurrent.Different = SubjectDifferent.Different.SubjectOriginal;
                    if (subjectCompare.ID != Guid.Empty)
                        listSubject.Add(subjectCompare);
                    listSubject.Add(subjectCurrent);
                }

                switch (iOfLagre)
                {
                    //Remove
                    case > -1:
                        subjectLarge.RemoveAt(iOfLagre);
                        break;
                    case < 0 when year1.Year > year2.Year:
                        subjectCurrent.Different = SubjectDifferent.Different.SubjectRemove;
                        break;
                }

                subjectSmall.RemoveAt(i);

            }

            if (subjectLarge.Count <= 0) return new ResultSuccess<List<SubjectCompareVM>>(listSubject, "ok");
            {
                listSubject.AddRange(subjectLarge.Select(t => new SubjectCompareVM()
                {
                    ID = t.ID,
                    IdClass = t.IDClass,
                    CourseCode = t.CourseCode,
                    Name = t.Name,
                    Credit = $"{t.Credit}",
                    TypeCourse = t.TypeCourse ? "Bắt buộc" : "Tự chọn",
                    NumberOfTheory = $"{t.NumberOfTheory}",
                    NumberOfPractice = $"{t.NumberOfPractice}",
                    Prerequisite = $"{t.Prerequisite}",
                    LearnFirst = $"{t.LearnFirst}",
                    Parallel = $"{t.Parallel}",

                    Different = SubjectDifferent.Different.SubjectNew
                }));
            }

            return new ResultSuccess<List<SubjectCompareVM>>(listSubject, "ok");

        }
    }
}
