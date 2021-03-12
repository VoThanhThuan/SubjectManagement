﻿using System;
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
                    Credit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeCourse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfTheory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfPractice = table.Column<string>(type: "nvarchar(max)", nullable: true),
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