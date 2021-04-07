﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SubjectManagement.Data.EF;

namespace SubjectManagement.Data.Migrations
{
    [DbContext(typeof(SubjectDbContext))]
    partial class SubjectDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SubjectManagement.Data.Entities.AlternativeSubject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IDClass")
                        .HasColumnType("int");

                    b.Property<Guid>("IDNew")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDOld")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("SubjectIDClass")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IDClass");

                    b.HasIndex("SubjectID", "SubjectIDClass");

                    b.ToTable("AlternativeSubject");
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.AppUser", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            ID = "TK01",
                            Avatar = "",
                            FirstName = "Thuận",
                            LastName = "Võ Thành",
                            PasswordHash = "wZIa07fMB/OKgTNIFKmWVw==",
                            Role = "admin",
                            Username = "thuan"
                        },
                        new
                        {
                            ID = "TK02",
                            Avatar = "",
                            FirstName = "Sơn",
                            LastName = "Nguyễn Ngọc",
                            PasswordHash = "SY08a/oDP23Bvk/MPDcKpw==",
                            Role = "guest",
                            Username = "son"
                        },
                        new
                        {
                            ID = "TK03",
                            Avatar = "",
                            FirstName = "Truyền",
                            LastName = "Nguyễn Thị Mỹ",
                            PasswordHash = "ipy+CjQc6p4LS8IWvcIq3Q==",
                            Role = "admin",
                            Username = "truyen"
                        },
                        new
                        {
                            ID = "TK04",
                            Avatar = "",
                            FirstName = "Toàn",
                            LastName = "Nguyễn Thanh",
                            PasswordHash = "cwHuoXLomiI3mEZ9SpHnqQ==",
                            Role = "guest",
                            Username = "toan"
                        });
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.Class", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodeClass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Year")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Class");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CodeClass = "DH19PM",
                            Name = "Kỹ thuật phầm mềm",
                            Year = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ID = 2,
                            CodeClass = "DH20PM",
                            Name = "Kỹ thuật phầm mềm",
                            Year = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.ClassInFaculty", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IDClass")
                        .HasColumnType("int");

                    b.Property<int>("IDFaculty")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IDClass")
                        .IsUnique();

                    b.HasIndex("IDFaculty");

                    b.ToTable("ClassInFaculty");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            IDClass = 1,
                            IDFaculty = 1
                        },
                        new
                        {
                            ID = 2,
                            IDClass = 2,
                            IDFaculty = 1
                        });
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.ElectiveGroup", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("ElectiveGroup");
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.Faculty", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Faculty");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Công nghệ thông tin"
                        });
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.KnowledgeGroup", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("KnowledgeGroup");

                    b.HasData(
                        new
                        {
                            ID = new Guid("57955971-4be8-40fd-b149-eee225daea4c"),
                            Name = "Khối kiến thức đại cương"
                        },
                        new
                        {
                            ID = new Guid("d881a11f-bc9e-4f07-828f-9467c3045838"),
                            Name = "Khối kiến thức cơ sở ngành"
                        },
                        new
                        {
                            ID = new Guid("e3f2dfdf-85e9-40d1-adc1-95926f68011d"),
                            Name = "Khối kiến thức chuyên ngành"
                        });
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.Subject", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IDClass")
                        .HasColumnType("int");

                    b.Property<string>("CourseCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Credit")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LearnFirst")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPractice")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfTheory")
                        .HasColumnType("int");

                    b.Property<int?>("Parallel")
                        .HasColumnType("int");

                    b.Property<int?>("Prerequisite")
                        .HasColumnType("int");

                    b.Property<int?>("Semester")
                        .HasColumnType("int");

                    b.Property<bool>("TypeCourse")
                        .HasColumnType("bit");

                    b.HasKey("ID", "IDClass");

                    b.HasIndex("IDClass");

                    b.ToTable("Subject");

                    b.HasData(
                        new
                        {
                            ID = new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0010"),
                            IDClass = 1,
                            CourseCode = "SEE101",
                            Credit = 1,
                            Details = "",
                            Name = "Giới thiệu ngành – ĐH KTPM",
                            NumberOfPractice = 0,
                            NumberOfTheory = 15,
                            TypeCourse = false
                        },
                        new
                        {
                            ID = new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0011"),
                            IDClass = 1,
                            CourseCode = "COS106",
                            Credit = 4,
                            Details = "",
                            Name = "Lập trình căn bản",
                            NumberOfPractice = 50,
                            NumberOfTheory = 35,
                            TypeCourse = false
                        },
                        new
                        {
                            ID = new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0012"),
                            IDClass = 1,
                            CourseCode = "TIE501",
                            Credit = 4,
                            Details = "",
                            LearnFirst = 20,
                            Name = "Lập trình .Net",
                            NumberOfPractice = 60,
                            NumberOfTheory = 30,
                            TypeCourse = false
                        },
                        new
                        {
                            ID = new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0013"),
                            IDClass = 1,
                            CourseCode = "SEE301",
                            Credit = 2,
                            Details = "",
                            Name = "Nhập môn công nghệ phần mềm",
                            NumberOfPractice = 20,
                            NumberOfTheory = 20,
                            TypeCourse = true
                        },
                        new
                        {
                            ID = new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0014"),
                            IDClass = 1,
                            CourseCode = "SEE508",
                            Credit = 2,
                            Details = "",
                            LearnFirst = 38,
                            Name = "Quản lý dự án phần mềm",
                            NumberOfPractice = 20,
                            NumberOfTheory = 20,
                            TypeCourse = true
                        },
                        new
                        {
                            ID = new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0015"),
                            IDClass = 1,
                            CourseCode = "SEE505",
                            Credit = 3,
                            Details = "",
                            LearnFirst = 38,
                            Name = "Phân tích và thiết kế phần mềm hướng đối tượng",
                            NumberOfPractice = 30,
                            NumberOfTheory = 30,
                            TypeCourse = true
                        });
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.SubjectInElectiveGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IDCLass")
                        .HasColumnType("int");

                    b.Property<Guid>("IDElectiveGroup")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDSubject")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("IDElectiveGroup");

                    b.HasIndex("IDSubject", "IDCLass")
                        .IsUnique();

                    b.ToTable("SubjectInElectiveGroup");
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.SubjectInKnowledgeGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IDClass")
                        .HasColumnType("int");

                    b.Property<Guid>("IDKnowledgeGroup")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDSubject")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("IDKnowledgeGroup");

                    b.HasIndex("IDSubject", "IDClass")
                        .IsUnique();

                    b.ToTable("SubjectInKnowledgeGroup");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            IDClass = 1,
                            IDKnowledgeGroup = new Guid("57955971-4be8-40fd-b149-eee225daea4c"),
                            IDSubject = new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0010")
                        },
                        new
                        {
                            ID = 2,
                            IDClass = 1,
                            IDKnowledgeGroup = new Guid("d881a11f-bc9e-4f07-828f-9467c3045838"),
                            IDSubject = new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0011")
                        },
                        new
                        {
                            ID = 3,
                            IDClass = 1,
                            IDKnowledgeGroup = new Guid("d881a11f-bc9e-4f07-828f-9467c3045838"),
                            IDSubject = new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0012")
                        },
                        new
                        {
                            ID = 4,
                            IDClass = 1,
                            IDKnowledgeGroup = new Guid("e3f2dfdf-85e9-40d1-adc1-95926f68011d"),
                            IDSubject = new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0013")
                        },
                        new
                        {
                            ID = 5,
                            IDClass = 1,
                            IDKnowledgeGroup = new Guid("e3f2dfdf-85e9-40d1-adc1-95926f68011d"),
                            IDSubject = new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0014")
                        },
                        new
                        {
                            ID = 6,
                            IDClass = 1,
                            IDKnowledgeGroup = new Guid("e3f2dfdf-85e9-40d1-adc1-95926f68011d"),
                            IDSubject = new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0015")
                        });
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.AlternativeSubject", b =>
                {
                    b.HasOne("SubjectManagement.Data.Entities.Class", "Class")
                        .WithMany("AlternativeSubjects")
                        .HasForeignKey("IDClass")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SubjectManagement.Data.Entities.Subject", null)
                        .WithMany("AlternativeSubjects")
                        .HasForeignKey("SubjectID", "SubjectIDClass");

                    b.Navigation("Class");
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.ClassInFaculty", b =>
                {
                    b.HasOne("SubjectManagement.Data.Entities.Class", "Class")
                        .WithOne("ClassInFaculty")
                        .HasForeignKey("SubjectManagement.Data.Entities.ClassInFaculty", "IDClass")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SubjectManagement.Data.Entities.Faculty", "Faculty")
                        .WithMany("ClassInFaculties")
                        .HasForeignKey("IDFaculty")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.Subject", b =>
                {
                    b.HasOne("SubjectManagement.Data.Entities.Class", "Class")
                        .WithMany("Subject")
                        .HasForeignKey("IDClass")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.SubjectInElectiveGroup", b =>
                {
                    b.HasOne("SubjectManagement.Data.Entities.ElectiveGroup", "ElectiveGroup")
                        .WithMany("SubjectInElectiveGroups")
                        .HasForeignKey("IDElectiveGroup")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SubjectManagement.Data.Entities.Subject", "Subject")
                        .WithOne("SubjectInElectiveGroup")
                        .HasForeignKey("SubjectManagement.Data.Entities.SubjectInElectiveGroup", "IDSubject", "IDCLass")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ElectiveGroup");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.SubjectInKnowledgeGroup", b =>
                {
                    b.HasOne("SubjectManagement.Data.Entities.KnowledgeGroup", "KnowledgeGroup")
                        .WithMany("SubjectInKnowledgeGroups")
                        .HasForeignKey("IDKnowledgeGroup")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SubjectManagement.Data.Entities.Subject", "Subject")
                        .WithOne("SubjectInKnowledgeGroup")
                        .HasForeignKey("SubjectManagement.Data.Entities.SubjectInKnowledgeGroup", "IDSubject", "IDClass")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KnowledgeGroup");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.Class", b =>
                {
                    b.Navigation("AlternativeSubjects");

                    b.Navigation("ClassInFaculty");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.ElectiveGroup", b =>
                {
                    b.Navigation("SubjectInElectiveGroups");
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.Faculty", b =>
                {
                    b.Navigation("ClassInFaculties");
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.KnowledgeGroup", b =>
                {
                    b.Navigation("SubjectInKnowledgeGroups");
                });

            modelBuilder.Entity("SubjectManagement.Data.Entities.Subject", b =>
                {
                    b.Navigation("AlternativeSubjects");

                    b.Navigation("SubjectInElectiveGroup");

                    b.Navigation("SubjectInKnowledgeGroup");
                });
#pragma warning restore 612, 618
        }
    }
}
