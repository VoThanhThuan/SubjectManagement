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
                name: "Semeter",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Term = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semeter", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credit = table.Column<int>(type: "int", nullable: false),
                    TypeCourse = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfTheory = table.Column<int>(type: "int", nullable: false),
                    NumberOfPractice = table.Column<int>(type: "int", nullable: false),
                    Prerequisite = table.Column<int>(type: "int", nullable: false),
                    LearnFirst = table.Column<int>(type: "int", nullable: false),
                    Parallel = table.Column<int>(type: "int", nullable: false),
                    IsOffical = table.Column<bool>(type: "bit", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AlternativeSubject",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDOld = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDNew = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternativeSubject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AlternativeSubject_Subject_IDNew",
                        column: x => x.IDNew,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectInElectiveGroup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDSubject = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_SubjectInElectiveGroup_Subject_IDSubject",
                        column: x => x.IDSubject,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectInKnowledgeGroup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDSubject = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_SubjectInKnowledgeGroup_Subject_IDSubject",
                        column: x => x.IDSubject,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectInSemeter",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDSubject = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDSemeter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectInSemeter", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubjectInSemeter_Semeter_IDSemeter",
                        column: x => x.IDSemeter,
                        principalTable: "Semeter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectInSemeter_Subject_IDSubject",
                        column: x => x.IDSubject,
                        principalTable: "Subject",
                        principalColumn: "ID",
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
                    { "TK01", "", "Thuận", "Võ Thành", "wZIa07fMB/OKgTNIFKmWVw==", "thuan" },
                    { "TK02", "", "Anh", "Lê Thị Ngọc", "X+vaPQ75BzemDeL9fG13KA==", "anh" }
                });

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
                table: "Subject",
                columns: new[] { "ID", "CourseCode", "Credit", "Details", "IsOffical", "LearnFirst", "Name", "NumberOfPractice", "NumberOfTheory", "Parallel", "Prerequisite", "TypeCourse" },
                values: new object[,]
                {
                    { new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0014"), "SEE508", 2, "", true, 38, "Quản lý dự án phần mềm", 20, 20, 0, 0, true },
                    { new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0010"), "SEE101", 1, "", true, 0, "Giới thiệu ngành – ĐH KTPM", 0, 15, 0, 0, false },
                    { new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0011"), "COS106", 4, "", true, 0, "Lập trình căn bản", 50, 35, 0, 0, false },
                    { new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0012"), "TIE501", 4, "", true, 20, "Lập trình .Net", 60, 30, 0, 0, false },
                    { new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0013"), "SEE301", 2, "", true, 0, "Nhập môn công nghệ phần mềm", 20, 20, 0, 0, true },
                    { new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0015"), "SEE505", 3, "", true, 38, "Phân tích và thiết kế phần mềm hướng đối tượng", 30, 30, 0, 0, true }
                });

            migrationBuilder.InsertData(
                table: "SubjectInKnowledgeGroup",
                columns: new[] { "ID", "IDKnowledgeGroup", "IDSubject" },
                values: new object[,]
                {
                    { 1, new Guid("57955971-4be8-40fd-b149-eee225daea4c"), new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0010") },
                    { 2, new Guid("d881a11f-bc9e-4f07-828f-9467c3045838"), new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0011") },
                    { 4, new Guid("d881a11f-bc9e-4f07-828f-9467c3045838"), new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0012") },
                    { 5, new Guid("e3f2dfdf-85e9-40d1-adc1-95926f68011d"), new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0013") },
                    { 6, new Guid("e3f2dfdf-85e9-40d1-adc1-95926f68011d"), new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0014") },
                    { 7, new Guid("e3f2dfdf-85e9-40d1-adc1-95926f68011d"), new Guid("0f7b55fc-4968-49d8-b9bd-402301fa0015") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlternativeSubject_IDNew",
                table: "AlternativeSubject",
                column: "IDNew",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInElectiveGroup_IDElectiveGroup",
                table: "SubjectInElectiveGroup",
                column: "IDElectiveGroup");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInElectiveGroup_IDSubject",
                table: "SubjectInElectiveGroup",
                column: "IDSubject",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInKnowledgeGroup_IDKnowledgeGroup",
                table: "SubjectInKnowledgeGroup",
                column: "IDKnowledgeGroup");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInKnowledgeGroup_IDSubject",
                table: "SubjectInKnowledgeGroup",
                column: "IDSubject",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInSemeter_IDSemeter",
                table: "SubjectInSemeter",
                column: "IDSemeter");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInSemeter_IDSubject",
                table: "SubjectInSemeter",
                column: "IDSubject",
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
                name: "SubjectInElectiveGroup");

            migrationBuilder.DropTable(
                name: "SubjectInKnowledgeGroup");

            migrationBuilder.DropTable(
                name: "SubjectInSemeter");

            migrationBuilder.DropTable(
                name: "ElectiveGroup");

            migrationBuilder.DropTable(
                name: "KnowledgeGroup");

            migrationBuilder.DropTable(
                name: "Semeter");

            migrationBuilder.DropTable(
                name: "Subject");
        }
    }
}
