using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using SubjectManagement.Common.Subject;
using SubjectManagement.Common.User;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole()
                {
                    ID = "admin",
                    Name = "admin",
                    Description = "Quyền Cao Cấp"
                },
                new AppRole()
                {
                    ID = "guest",
                    Name = "guest",
                    Description = "Quyền Người Xem"
                },
                new AppRole()
                {
                    ID = "dev",
                    Name = "dev",
                    Description = "Quyền Của Thằng Lập Trình"
                }
                );

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser()
                {
                    ID = "TK01",
                    Username = "thuan",
                    PasswordHash = ServiceForUser.PasswordHash("thuan"),
                    FirstName = "Thuận",
                    LastName = "Võ Thành",
                    Avatar = ""
                },
                new AppUser()
                {
                    ID = "TK02",
                    Username = "anh",
                    PasswordHash = ServiceForUser.PasswordHash("anh"),
                    FirstName = "Anh",
                    LastName = "Lê Thị Ngọc",
                    Avatar = ""
                },
                new AppUser()
                {
                    ID = "TK03",
                    Username = "son",
                    PasswordHash = ServiceForUser.PasswordHash("son"),
                    FirstName = "Sơn",
                    LastName = "Nguyễn Ngọc",
                    Avatar = ""
                },
                new AppUser()
                {
                    ID = "TK04",
                    Username = "truyen",
                    PasswordHash = ServiceForUser.PasswordHash("truyen"),
                    FirstName = "Truyền",
                    LastName = "Nguyễn Thị Mỹ",
                    Avatar = ""
                }
            );

            modelBuilder.Entity<AppUserRole>().HasData(
                new AppUserRole()
                {
                    ID = 1,
                    UserID = "TK01",
                    RoleID = "admin"
                },
                new AppUserRole()
                {
                    ID = 2,
                    UserID = "TK02",
                    RoleID = "guest"
                },
                new AppUserRole()
                {
                    ID = 3,
                    UserID = "TK03",
                    RoleID = "guest"
                },
                new AppUserRole()
                {
                    ID = 4,
                    UserID = "TK04",
                    RoleID = "admin"
                }
            );

            modelBuilder.Entity<Subject>().HasData(
                //kiến thức đại cương
                new Subject()
                {
                    ID = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0010"),
                    CourseCode = "SEE101",
                    Name = "Giới thiệu ngành – ĐH KTPM",
                    Credit = 1,
                    TypeCourse = Course.Elective,
                    NumberOfTheory = 15,
                    NumberOfPractice = 0,
                    IsOffical = true,
                    Details = ""
                },
                //Kiến thức cơ sở ngành
                new Subject()
                {
                    ID = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0011"),
                    CourseCode = "COS106",
                    Name = "Lập trình căn bản",
                    Credit = 4,
                    TypeCourse = Course.Elective,
                    NumberOfTheory = 35,
                    NumberOfPractice = 50,
                    IsOffical = true,
                    Details = ""
                },
                new Subject()
                {
                    ID = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0012"),
                    CourseCode = "TIE501",
                    Name = "Lập trình .Net",
                    Credit = 4,
                    TypeCourse = Course.Elective,
                    NumberOfTheory = 30,
                    NumberOfPractice = 60,
                    LearnFirst = 20,
                    IsOffical = true,
                    Details = ""
                },
                //Kiến thức Chuyên ngành
                new Subject()
                {
                    ID = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0013"),
                    CourseCode = "SEE301",
                    Name = "Nhập môn công nghệ phần mềm",
                    Credit = 2,
                    TypeCourse = Course.Obligatory,
                    NumberOfTheory = 20,
                    NumberOfPractice = 20,
                    IsOffical = true,
                    Details = ""
                },
                new Subject()
                {
                    ID = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0014"),
                    CourseCode = "SEE508",
                    Name = "Quản lý dự án phần mềm",
                    Credit = 2,
                    TypeCourse = Course.Obligatory,
                    NumberOfTheory = 20,
                    NumberOfPractice = 20,
                    LearnFirst = 38,
                    IsOffical = true,
                    Details = ""
                },
                new Subject()
                {
                    ID = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0015"),
                    CourseCode = "SEE505",
                    Name = "Phân tích và thiết kế phần mềm hướng đối tượng",
                    Credit = 3,
                    TypeCourse = Course.Obligatory,
                    NumberOfTheory = 30,
                    NumberOfPractice = 30,
                    LearnFirst = 38,
                    IsOffical = true,
                    Details = ""
                });

            modelBuilder.Entity<KnowledgeGroup>().HasData(
                new KnowledgeGroup()
                {
                    ID = Guid.Parse("57955971-4BE8-40FD-B149-EEE225DAEA4C"),
                    Name = "Khối kiến thức đại cương"
                },
                new KnowledgeGroup()
                {
                    ID = Guid.Parse("D881A11F-BC9E-4F07-828F-9467C3045838"),
                    Name = "Khối kiến thức cơ sở ngành"
                },
                new KnowledgeGroup()
                {
                    ID = Guid.Parse("E3F2DFDF-85E9-40D1-ADC1-95926F68011D"),
                    Name = "Khối kiến thức chuyên ngành"
                });

            modelBuilder.Entity<SubjectInKnowledgeGroup>().HasData(
                new SubjectInKnowledgeGroup()
                {
                    ID = 1,
                    IDSubject = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0010"),//Giới thiệu ngành
                    IDKnowledgeGroup = Guid.Parse("57955971-4BE8-40FD-B149-EEE225DAEA4C")//Đại cương
                },

                new SubjectInKnowledgeGroup()
                {
                    ID = 2,
                    IDSubject = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0011"),//
                    IDKnowledgeGroup = Guid.Parse("D881A11F-BC9E-4F07-828F-9467C3045838")//Khối kiến thức cơ sở ngàn
                },
                new SubjectInKnowledgeGroup()
                {
                    ID = 3,
                    IDSubject = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0012"),//
                    IDKnowledgeGroup = Guid.Parse("D881A11F-BC9E-4F07-828F-9467C3045838")//Khối kiến thức cơ sở ngàn
                },

                new SubjectInKnowledgeGroup()
                {
                    ID = 4,
                    IDSubject = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0013"),//
                    IDKnowledgeGroup = Guid.Parse("E3F2DFDF-85E9-40D1-ADC1-95926F68011D")//Khối kiến thức chuyên ngành
                },
                new SubjectInKnowledgeGroup()
                {
                    ID = 5,
                    IDSubject = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0014"),//
                    IDKnowledgeGroup = Guid.Parse("E3F2DFDF-85E9-40D1-ADC1-95926F68011D")//Khối kiến thức chuyên ngành
                },
                new SubjectInKnowledgeGroup()
                {
                    ID = 6,
                    IDSubject = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0015"),//
                    IDKnowledgeGroup = Guid.Parse("E3F2DFDF-85E9-40D1-ADC1-95926F68011D")//Khối kiến thức chuyên ngành
                });

            modelBuilder.Entity<Class>().HasData(
                new Class()
                {
                    ID = 1,
                    CodeClass = "DH19PM",
                    Name = "Kỹ thuật phầm mềm",
                    Year = DateTime.Parse("2019-01-01")
                });
            modelBuilder.Entity<Faculty>().HasData(
                new Faculty()
                {
                    ID = 1,
                    Name = "Công nghệ thông tin"
                });

            modelBuilder.Entity<ClassInFaculty>().HasData(
                new ClassInFaculty()
                {
                    ID = 1,
                    IDClass = 1,
                    IDFaculty = 1
                });

            modelBuilder.Entity<Semester>().HasData(
                new Semester()
                {
                    ID = 1,
                    Term = 1
                },
                new Semester()
                {
                    ID = 2,
                    Term = 2
                },
                new Semester()
                {
                    ID = 3,
                    Term = 3
                },
                new Semester()
                {
                    ID = 4,
                    Term = 4
                },
                new Semester()
                {
                    ID = 5,
                    Term = 5
                },
                new Semester()
                {
                    ID = 6,
                    Term = 6
                },
                new Semester()
                {
                    ID = 7,
                    Term = 7
                },
                new Semester()
                {
                    ID = 8,
                    Term = 8
                });

            modelBuilder.Entity<SemesterOfClass>().HasData(
                new SemesterOfClass()
                {
                    ID = 1,
                    IDClass = 1,
                    IDSemester = 1
                });


            modelBuilder.Entity<SubjectOfClass>().HasData(
                new SubjectOfClass()
                {
                    IDClass = 1,
                    IDSubject = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0010")//Giới thiệu ngành
                },
                new SubjectOfClass()
                {
                    IDClass = 1,
                    IDSubject = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0011")
                },
                new SubjectOfClass()
                {
                    IDClass = 1,
                    IDSubject = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0012")
                },
                new SubjectOfClass()
                {
                    IDClass = 1,
                    IDSubject = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0013")
                },
                new SubjectOfClass()
                {
                    IDClass = 1,
                    IDSubject = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0014")
                },
                new SubjectOfClass()
                {
                    IDClass = 1,
                    IDSubject = Guid.Parse("0F7B55FC-4968-49D8-B9BD-402301FA0015")
                });
        }
    }
}
