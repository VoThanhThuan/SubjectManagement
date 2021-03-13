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
                    { "TK01", "", "Thuận", "Võ Thành", "wZIa07fMB/OKgTNIFKmWVw==", "thuan" },
                    { "TK02", "", "Anh", "Lê Thị Ngọc", "X+vaPQ75BzemDeL9fG13KA==", "anh" },
                    { "TK03", "", "Sơn", "Nguyễn Ngọc", "SY08a/oDP23Bvk/MPDcKpw==", "son" },
                    { "TK04", "", "Truyền", "Nguyễn Thị Mỹ", "ipy+CjQc6p4LS8IWvcIq3Q==", "truyen" }
                });

            migrationBuilder.InsertData(
                table: "Subject",
                columns: new[] { "ID", "CourseCode", "Credit", "Details", "IsOffical", "Name", "NumberOfPractice", "NumberOfTheory", "TypeCourse" },
                values: new object[,]
                {
                    { new Guid("dfa4dc5c-3963-4515-88e3-2236b95ca73c"), "SEE101", 1, "", true, "Giới thiệu ngành – ĐH KTPM", 0, 15, true },
                    { new Guid("80df5307-e86e-429e-89f2-c686675137c3"), "COS106", 4, "", true, "Lập trình căn bản", 50, 35, true },
                    { new Guid("1188f271-325f-40f9-804e-1ae2a9661084"), "TIE501", 4, "", true, "Lập trình .Net", 60, 30, true }
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
