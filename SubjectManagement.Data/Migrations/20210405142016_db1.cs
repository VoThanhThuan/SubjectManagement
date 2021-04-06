using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SubjectManagement.Data.Migrations
{
    public partial class db1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRole",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRole", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ElectiveGroup",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectiveGroup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculty", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeGroup",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeGroup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDClass = table.Column<int>(type: "int", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credit = table.Column<int>(type: "int", nullable: false),
                    TypeCourse = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfTheory = table.Column<int>(type: "int", nullable: false),
                    NumberOfPractice = table.Column<int>(type: "int", nullable: false),
                    Prerequisite = table.Column<int>(type: "int", nullable: true),
                    LearnFirst = table.Column<int>(type: "int", nullable: true),
                    Parallel = table.Column<int>(type: "int", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Semester = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => new { x.ID, x.IDClass });
                    table.ForeignKey(
                        name: "FK_Subject_Class_IDClass",
                        column: x => x.IDClass,
                        principalTable: "Class",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassInFaculty",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDClass = table.Column<int>(type: "int", nullable: false),
                    IDFaculty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassInFaculty", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClassInFaculty_Class_IDClass",
                        column: x => x.IDClass,
                        principalTable: "Class",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassInFaculty_Faculty_IDFaculty",
                        column: x => x.IDFaculty,
                        principalTable: "Faculty",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlternativeSubject",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDOld = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDNew = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDClass = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubjectIDClass = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternativeSubject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AlternativeSubject_Class_IDClass",
                        column: x => x.IDClass,
                        principalTable: "Class",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlternativeSubject_Subject_SubjectID_SubjectIDClass",
                        columns: x => new { x.SubjectID, x.SubjectIDClass },
                        principalTable: "Subject",
                        principalColumns: new[] { "ID", "IDClass" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectInElectiveGroup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDSubject = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDCLass = table.Column<int>(type: "int", nullable: false),
                    IDElectiveGroup = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectInElectiveGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubjectInElectiveGroup_ElectiveGroup_IDElectiveGroup",
                        column: x => x.IDElectiveGroup,
                        principalTable: "ElectiveGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectInElectiveGroup_Subject_IDSubject_IDCLass",
                        columns: x => new { x.IDSubject, x.IDCLass },
                        principalTable: "Subject",
                        principalColumns: new[] { "ID", "IDClass" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectInKnowledgeGroup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDSubject = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDClass = table.Column<int>(type: "int", nullable: false),
                    IDKnowledgeGroup = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectInKnowledgeGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubjectInKnowledgeGroup_KnowledgeGroup_IDKnowledgeGroup",
                        column: x => x.IDKnowledgeGroup,
                        principalTable: "KnowledgeGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectInKnowledgeGroup_Subject_IDSubject_IDClass",
                        columns: x => new { x.IDSubject, x.IDClass },
                        principalTable: "Subject",
                        principalColumns: new[] { "ID", "IDClass" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { "admin", "Quyền Cao Cấp", "admin" },
                    { "guest", "Quyền Người Xem", "guest" },
                    { "dev", "Quyền Của Thằng Lập Trình", "dev" }
                });

            migrationBuilder.InsertData(
                table: "AppUserRole",
                columns: new[] { "ID", "RoleID", "UserID" },
                values: new object[,]
                {
                    { 1, "admin", "TK01" },
                    { 2, "guest", "TK02" },
                    { 3, "guest", "TK03" },
                    { 4, "admin", "TK04" }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "ID", "Avatar", "FirstName", "LastName", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { "TK04", "", "Truyền", "Nguyễn Thị Mỹ", "ipy+CjQc6p4LS8IWvcIq3Q==", "truyen" },
                    { "TK03", "", "Sơn", "Nguyễn Ngọc", "SY08a/oDP23Bvk/MPDcKpw==", "son" },
                    { "TK02", "", "Anh", "Lê Thị Ngọc", "X+vaPQ75BzemDeL9fG13KA==", "anh" },
                    { "TK01", "", "Thuận", "Võ Thành", "wZIa07fMB/OKgTNIFKmWVw==", "thuan" }
                });

            migrationBuilder.InsertData(
                table: "Class",
                columns: new[] { "ID", "CodeClass", "Name", "Year" },
                values: new object[,]
                {
                    { 1, "DH19PM", "Kỹ thuật phầm mềm", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "DH20PM", "Kỹ thuật phầm mềm", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Faculty",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Công nghệ thông tin" });

            migrationBuilder.InsertData(
                table: "KnowledgeGroup",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { new Guid("57955971-4be8-40fd-b149-eee225daea4c"), "Khối kiến thức đại cương" },
                    { new Guid("d881a11f-bc9e-4f07-828f-9467c3045838"), "Khối kiến thức cơ sở ngành" },
                    { new Guid("e3f2dfdf-85e9-40d1-adc1-95926f68011d"), "Khối kiến thức chuyên ngành" }
                });

            migrationBuilder.InsertData(
                table: "ClassInFaculty",
                columns: new[] { "ID", "IDClass", "IDFaculty" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Subject",
                columns: new[] { "ID", "IDClass", "CourseCode", "Credit", "Details", "LearnFirst", "Name", "NumberOfPractice", "NumberOfTheory", "Parallel", "Prerequisite", "Semester", "TypeCourse" },
                values: new object[,]
                {
                    { new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0010"), 1, "SEE101", 1, "", null, "Giới thiệu ngành – ĐH KTPM", 0, 15, null, null, null, false },
                    { new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0011"), 1, "COS106", 4, "", null, "Lập trình căn bản", 50, 35, null, null, null, false },
                    { new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0012"), 1, "TIE501", 4, "", 20, "Lập trình .Net", 60, 30, null, null, null, false },
                    { new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0013"), 1, "SEE301", 2, "", null, "Nhập môn công nghệ phần mềm", 20, 20, null, null, null, true },
                    { new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0014"), 1, "SEE508", 2, "", 38, "Quản lý dự án phần mềm", 20, 20, null, null, null, true },
                    { new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0015"), 1, "SEE505", 3, "", 38, "Phân tích và thiết kế phần mềm hướng đối tượng", 30, 30, null, null, null, true }
                });

            migrationBuilder.InsertData(
                table: "SubjectInKnowledgeGroup",
                columns: new[] { "ID", "IDClass", "IDKnowledgeGroup", "IDSubject" },
                values: new object[,]
                {
                    { 1, 1, new Guid("57955971-4be8-40fd-b149-eee225daea4c"), new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0010") },
                    { 2, 1, new Guid("d881a11f-bc9e-4f07-828f-9467c3045838"), new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0011") },
                    { 3, 1, new Guid("d881a11f-bc9e-4f07-828f-9467c3045838"), new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0012") },
                    { 4, 1, new Guid("e3f2dfdf-85e9-40d1-adc1-95926f68011d"), new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0013") },
                    { 5, 1, new Guid("e3f2dfdf-85e9-40d1-adc1-95926f68011d"), new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0014") },
                    { 6, 1, new Guid("e3f2dfdf-85e9-40d1-adc1-95926f68011d"), new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0015") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlternativeSubject_IDClass",
                table: "AlternativeSubject",
                column: "IDClass");

            migrationBuilder.CreateIndex(
                name: "IX_AlternativeSubject_SubjectID_SubjectIDClass",
                table: "AlternativeSubject",
                columns: new[] { "SubjectID", "SubjectIDClass" });

            migrationBuilder.CreateIndex(
                name: "IX_ClassInFaculty_IDClass",
                table: "ClassInFaculty",
                column: "IDClass",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassInFaculty_IDFaculty",
                table: "ClassInFaculty",
                column: "IDFaculty");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_IDClass",
                table: "Subject",
                column: "IDClass");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInElectiveGroup_IDElectiveGroup",
                table: "SubjectInElectiveGroup",
                column: "IDElectiveGroup");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInElectiveGroup_IDSubject_IDCLass",
                table: "SubjectInElectiveGroup",
                columns: new[] { "IDSubject", "IDCLass" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInKnowledgeGroup_IDKnowledgeGroup",
                table: "SubjectInKnowledgeGroup",
                column: "IDKnowledgeGroup");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInKnowledgeGroup_IDSubject_IDClass",
                table: "SubjectInKnowledgeGroup",
                columns: new[] { "IDSubject", "IDClass" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlternativeSubject");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUserRole");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "ClassInFaculty");

            migrationBuilder.DropTable(
                name: "SubjectInElectiveGroup");

            migrationBuilder.DropTable(
                name: "SubjectInKnowledgeGroup");

            migrationBuilder.DropTable(
                name: "Faculty");

            migrationBuilder.DropTable(
                name: "ElectiveGroup");

            migrationBuilder.DropTable(
                name: "KnowledgeGroup");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Class");
        }
    }
}
