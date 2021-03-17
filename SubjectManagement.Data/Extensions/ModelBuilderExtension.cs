using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using SubjectManagement.Common.Subject;
using SubjectManagement.Data.Entities;
using SubjectManagement.Service.User;

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
                new Subject()
                {
                    ID = Guid.NewGuid(),
                    CourseCode = "SEE101",
                    Name = "Giới thiệu ngành – ĐH KTPM",
                    Credit = 1,
                    TypeCourse = Course.Elective,
                    NumberOfTheory = 15,
                    NumberOfPractice = 0,
                    IsOffical = true,
                    Details = ""
                },
                new Subject()
                {
                    ID = Guid.NewGuid(),
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
                    ID = Guid.NewGuid(),
                    CourseCode = "TIE501",
                    Name = "Lập trình .Net",
                    Credit = 4,
                    TypeCourse = Course.Elective,
                    NumberOfTheory = 30,
                    NumberOfPractice = 60,
                    IsOffical = true,
                    Details = ""
                }
                );

        }
    }
}
