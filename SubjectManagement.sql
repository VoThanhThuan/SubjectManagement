USE [SubjectDatabase]
GO
/****** Object:  Table [dbo].[AlternativeSubject]    Script Date: 4/28/2021 1:13:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlternativeSubject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDOld] [uniqueidentifier] NOT NULL,
	[IDNew] [uniqueidentifier] NOT NULL,
	[IDClass] [int] NOT NULL,
	[SubjectID] [uniqueidentifier] NULL,
	[SubjectIDClass] [int] NULL,
 CONSTRAINT [PK_AlternativeSubject] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUsers]    Script Date: 4/28/2021 1:13:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUsers](
	[ID] [nvarchar](450) NOT NULL,
	[Username] [nvarchar](max) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[FirstName] [nvarchar](200) NOT NULL,
	[LastName] [nvarchar](200) NOT NULL,
	[Avatar] [nvarchar](max) NULL,
	[Role] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 4/28/2021 1:13:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CodeClass] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Year] [datetime2](7) NOT NULL,
	[CanEdit] [bit] NOT NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassInFaculty]    Script Date: 4/28/2021 1:13:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassInFaculty](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDClass] [int] NOT NULL,
	[IDFaculty] [int] NOT NULL,
 CONSTRAINT [PK_ClassInFaculty] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ElectiveGroup]    Script Date: 4/28/2021 1:13:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ElectiveGroup](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Details] [nvarchar](max) NULL,
 CONSTRAINT [PK_ElectiveGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faculty]    Script Date: 4/28/2021 1:13:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculty](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Faculty] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KnowledgeGroup]    Script Date: 4/28/2021 1:13:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KnowledgeGroup](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_KnowledgeGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 4/28/2021 1:13:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[ID] [uniqueidentifier] NOT NULL,
	[IDClass] [int] NOT NULL,
	[CourseCode] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Credit] [int] NOT NULL,
	[TypeCourse] [bit] NOT NULL,
	[NumberOfTheory] [int] NOT NULL,
	[NumberOfPractice] [int] NOT NULL,
	[Prerequisite] [int] NULL,
	[LearnFirst] [int] NULL,
	[Parallel] [int] NULL,
	[Details] [nvarchar](max) NULL,
	[Semester] [nvarchar](max) NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[IDClass] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubjectInElectiveGroup]    Script Date: 4/28/2021 1:13:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubjectInElectiveGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDSubject] [uniqueidentifier] NOT NULL,
	[IDCLass] [int] NOT NULL,
	[IDElectiveGroup] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SubjectInElectiveGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubjectInKnowledgeGroup]    Script Date: 4/28/2021 1:13:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubjectInKnowledgeGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDSubject] [uniqueidentifier] NOT NULL,
	[IDClass] [int] NOT NULL,
	[IDKnowledgeGroup] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SubjectInKnowledgeGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[AppUsers] ([ID], [Username], [PasswordHash], [FirstName], [LastName], [Avatar], [Role]) VALUES (N'TK01', N'thuan', N'wZIa07fMB/OKgTNIFKmWVw==', N'Thuận', N'Võ Thành', N'', N'admin')
INSERT [dbo].[AppUsers] ([ID], [Username], [PasswordHash], [FirstName], [LastName], [Avatar], [Role]) VALUES (N'TK02', N'son', N'SY08a/oDP23Bvk/MPDcKpw==', N'Sơn', N'Nguyễn Ngọc', N'', N'guest')
INSERT [dbo].[AppUsers] ([ID], [Username], [PasswordHash], [FirstName], [LastName], [Avatar], [Role]) VALUES (N'TK03', N'truyen', N'ipy+CjQc6p4LS8IWvcIq3Q==', N'Truyền', N'Nguyễn Thị Mỹ', N'', N'admin')
INSERT [dbo].[AppUsers] ([ID], [Username], [PasswordHash], [FirstName], [LastName], [Avatar], [Role]) VALUES (N'TK04', N'toan', N'cwHuoXLomiI3mEZ9SpHnqQ==', N'Toàn', N'Nguyễn Thanh', N'', N'guest')
SET IDENTITY_INSERT [dbo].[Class] ON 

INSERT [dbo].[Class] ([ID], [CodeClass], [Name], [Year], [CanEdit]) VALUES (1, N'DH19PM', N'Kỹ thuật phầm mềm', CAST(N'2019-01-01T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Class] ([ID], [CodeClass], [Name], [Year], [CanEdit]) VALUES (2, N'DH20PM', N'Kỹ thuật phầm mềm', CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Class] OFF
SET IDENTITY_INSERT [dbo].[ClassInFaculty] ON 

INSERT [dbo].[ClassInFaculty] ([ID], [IDClass], [IDFaculty]) VALUES (1, 1, 1)
INSERT [dbo].[ClassInFaculty] ([ID], [IDClass], [IDFaculty]) VALUES (2, 2, 1)
SET IDENTITY_INSERT [dbo].[ClassInFaculty] OFF
SET IDENTITY_INSERT [dbo].[Faculty] ON 

INSERT [dbo].[Faculty] ([ID], [Name]) VALUES (1, N'Công nghệ thông tin')
SET IDENTITY_INSERT [dbo].[Faculty] OFF
INSERT [dbo].[KnowledgeGroup] ([ID], [Name]) VALUES (N'd881a11f-bc9e-4f07-828f-9467c3045838', N'Khối kiến thức cơ sở ngành')
INSERT [dbo].[KnowledgeGroup] ([ID], [Name]) VALUES (N'e3f2dfdf-85e9-40d1-adc1-95926f68011d', N'Khối kiến thức chuyên ngành')
INSERT [dbo].[KnowledgeGroup] ([ID], [Name]) VALUES (N'57955971-4be8-40fd-b149-eee225daea4c', N'Khối kiến thức đại cương')
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0405b87a-a614-4dce-87a1-07efbf5e2176', 1, N'VRP101', N'Đường lối cách mạng của Đảng Cộng sản Việt Nam', 3, 1, 32, 26, 0, 4, 0, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'11b3dd11-00a1-4a05-b6c0-34a9cf9593e8', 1, N'MAX102', N'Nhữnng nguyên lý cơ bản của chủ nghĩa Mác-Lênin 2', 3, 1, 32, 26, 0, 2, 0, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'81a85a7c-0fc8-4de5-a5d0-3725f4ce7b14', 1, N'MAX101', N'Nhữnng nguyên lý cơ bản của chủ nghĩa Mác-Lênin1', 2, 1, 15, 16, 0, 0, 0, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'29d39bc7-9ac9-4966-b89e-3c5b8ecd5807', 1, N'CHI102', N'Tiếng Trung 2', 4, 1, 60, 0, 0, 0, 0, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0f7b55fc-4968-49d8-b9bd-402301fa0010', 1, N'SEE101', N'Giới thiệu ngành – ĐH KTPM', 1, 0, 15, 0, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0f7b55fc-4968-49d8-b9bd-402301fa0010', 2, N'SEE101', N'Giới thiệu ngành – ĐH KTPM', 1, 0, 15, 0, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0f7b55fc-4968-49d8-b9bd-402301fa0011', 1, N'COS106', N'Lập trình căn bản', 4, 0, 35, 50, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0f7b55fc-4968-49d8-b9bd-402301fa0011', 2, N'COS106', N'Lập trình căn bản', 4, 0, 35, 50, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0f7b55fc-4968-49d8-b9bd-402301fa0012', 1, N'TIE501', N'Lập trình .Net', 4, 0, 30, 60, NULL, 20, NULL, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0f7b55fc-4968-49d8-b9bd-402301fa0012', 2, N'TIE501', N'Lập trình .Net', 4, 0, 30, 60, NULL, 20, NULL, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0f7b55fc-4968-49d8-b9bd-402301fa0013', 1, N'SEE301', N'Nhập môn công nghệ phần mềm', 2, 1, 20, 20, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0f7b55fc-4968-49d8-b9bd-402301fa0013', 2, N'SEE301', N'Nhập môn công nghệ phần mềm', 2, 1, 20, 20, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0f7b55fc-4968-49d8-b9bd-402301fa0014', 1, N'SEE508', N'Quản lý dự án phần mềm', 2, 1, 20, 20, NULL, 38, NULL, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0f7b55fc-4968-49d8-b9bd-402301fa0014', 2, N'SEE508', N'Quản lý dự án phần mềm', 2, 1, 20, 20, NULL, 38, NULL, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0f7b55fc-4968-49d8-b9bd-402301fa0015', 1, N'SEE505', N'Phân tích và thiết kế phần mềm hướng đối tượng', 3, 1, 30, 30, NULL, 38, NULL, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0f7b55fc-4968-49d8-b9bd-402301fa0015', 2, N'SEE505', N'Phân tích và thiết kế phần mềm hướng đối tượng', 3, 1, 30, 30, NULL, 38, NULL, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'4da75b64-fa91-44b1-8ee1-41d3ba339e86', 1, N'FSL101', N'Tiếng Pháp 1', 3, 1, 45, 0, 0, 0, 0, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'81de5489-9862-4f06-abae-5116531741eb', 1, N'FSL102', N'Tiếng Pháp 2', 4, 1, 60, 0, 0, 0, 0, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'fda53729-b05d-48a3-8862-56326ced489d', 1, N'ENG102', N'Tiếng Anh 2', 4, 1, 60, 0, 0, 0, 0, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'0930a02c-418b-42be-9141-8b21a142836c', 1, N'PHT101', N'Giáo dục thể chất(*1)', 3, 1, 0, 90, 0, 0, 0, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'7f9a032a-9949-4e93-9dd8-db245f3a7e3d', 1, N'ENG101', N'Tiếng Anh 1', 3, 1, 45, 0, 0, 0, 0, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'80d5d8d4-8350-4397-a009-dcee4f3e3518', 1, N'HCM101', N'Tư tưởng Hồ Chí Minh', 2, 1, 21, 18, 0, 3, 0, N'', NULL)
INSERT [dbo].[Subject] ([ID], [IDClass], [CourseCode], [Name], [Credit], [TypeCourse], [NumberOfTheory], [NumberOfPractice], [Prerequisite], [LearnFirst], [Parallel], [Details], [Semester]) VALUES (N'a57ca5cf-8ae5-467e-a3fa-fb2037f29e73', 1, N'CHI101', N'Tiếng Trung 1', 3, 1, 45, 0, 0, 0, 0, N'', NULL)
SET IDENTITY_INSERT [dbo].[SubjectInKnowledgeGroup] ON 

INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (1, N'0f7b55fc-4968-49d8-b9bd-402301fa0010', 1, N'57955971-4be8-40fd-b149-eee225daea4c')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (2, N'0f7b55fc-4968-49d8-b9bd-402301fa0011', 1, N'd881a11f-bc9e-4f07-828f-9467c3045838')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (3, N'0f7b55fc-4968-49d8-b9bd-402301fa0012', 1, N'd881a11f-bc9e-4f07-828f-9467c3045838')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (4, N'0f7b55fc-4968-49d8-b9bd-402301fa0013', 1, N'e3f2dfdf-85e9-40d1-adc1-95926f68011d')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (5, N'0f7b55fc-4968-49d8-b9bd-402301fa0014', 1, N'e3f2dfdf-85e9-40d1-adc1-95926f68011d')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (6, N'0f7b55fc-4968-49d8-b9bd-402301fa0015', 1, N'e3f2dfdf-85e9-40d1-adc1-95926f68011d')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (7, N'0f7b55fc-4968-49d8-b9bd-402301fa0010', 2, N'57955971-4be8-40fd-b149-eee225daea4c')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (8, N'0f7b55fc-4968-49d8-b9bd-402301fa0011', 2, N'd881a11f-bc9e-4f07-828f-9467c3045838')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (9, N'0f7b55fc-4968-49d8-b9bd-402301fa0012', 2, N'd881a11f-bc9e-4f07-828f-9467c3045838')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (10, N'0f7b55fc-4968-49d8-b9bd-402301fa0013', 2, N'e3f2dfdf-85e9-40d1-adc1-95926f68011d')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (11, N'0f7b55fc-4968-49d8-b9bd-402301fa0014', 2, N'e3f2dfdf-85e9-40d1-adc1-95926f68011d')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (12, N'0f7b55fc-4968-49d8-b9bd-402301fa0015', 2, N'e3f2dfdf-85e9-40d1-adc1-95926f68011d')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (13, N'81a85a7c-0fc8-4de5-a5d0-3725f4ce7b14', 1, N'57955971-4be8-40fd-b149-eee225daea4c')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (14, N'11b3dd11-00a1-4a05-b6c0-34a9cf9593e8', 1, N'57955971-4be8-40fd-b149-eee225daea4c')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (15, N'80d5d8d4-8350-4397-a009-dcee4f3e3518', 1, N'57955971-4be8-40fd-b149-eee225daea4c')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (16, N'0405b87a-a614-4dce-87a1-07efbf5e2176', 1, N'57955971-4be8-40fd-b149-eee225daea4c')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (17, N'a57ca5cf-8ae5-467e-a3fa-fb2037f29e73', 1, N'57955971-4be8-40fd-b149-eee225daea4c')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (18, N'7f9a032a-9949-4e93-9dd8-db245f3a7e3d', 1, N'57955971-4be8-40fd-b149-eee225daea4c')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (19, N'4da75b64-fa91-44b1-8ee1-41d3ba339e86', 1, N'57955971-4be8-40fd-b149-eee225daea4c')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (20, N'81de5489-9862-4f06-abae-5116531741eb', 1, N'57955971-4be8-40fd-b149-eee225daea4c')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (21, N'fda53729-b05d-48a3-8862-56326ced489d', 1, N'57955971-4be8-40fd-b149-eee225daea4c')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (22, N'29d39bc7-9ac9-4966-b89e-3c5b8ecd5807', 1, N'57955971-4be8-40fd-b149-eee225daea4c')
INSERT [dbo].[SubjectInKnowledgeGroup] ([ID], [IDSubject], [IDClass], [IDKnowledgeGroup]) VALUES (23, N'0930a02c-418b-42be-9141-8b21a142836c', 1, N'57955971-4be8-40fd-b149-eee225daea4c')
SET IDENTITY_INSERT [dbo].[SubjectInKnowledgeGroup] OFF
ALTER TABLE [dbo].[AlternativeSubject]  WITH CHECK ADD  CONSTRAINT [FK_AlternativeSubject_Class_IDClass] FOREIGN KEY([IDClass])
REFERENCES [dbo].[Class] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AlternativeSubject] CHECK CONSTRAINT [FK_AlternativeSubject_Class_IDClass]
GO
ALTER TABLE [dbo].[AlternativeSubject]  WITH CHECK ADD  CONSTRAINT [FK_AlternativeSubject_Subject_SubjectID_SubjectIDClass] FOREIGN KEY([SubjectID], [SubjectIDClass])
REFERENCES [dbo].[Subject] ([ID], [IDClass])
GO
ALTER TABLE [dbo].[AlternativeSubject] CHECK CONSTRAINT [FK_AlternativeSubject_Subject_SubjectID_SubjectIDClass]
GO
ALTER TABLE [dbo].[ClassInFaculty]  WITH CHECK ADD  CONSTRAINT [FK_ClassInFaculty_Class_IDClass] FOREIGN KEY([IDClass])
REFERENCES [dbo].[Class] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClassInFaculty] CHECK CONSTRAINT [FK_ClassInFaculty_Class_IDClass]
GO
ALTER TABLE [dbo].[ClassInFaculty]  WITH CHECK ADD  CONSTRAINT [FK_ClassInFaculty_Faculty_IDFaculty] FOREIGN KEY([IDFaculty])
REFERENCES [dbo].[Faculty] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClassInFaculty] CHECK CONSTRAINT [FK_ClassInFaculty_Faculty_IDFaculty]
GO
ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Class_IDClass] FOREIGN KEY([IDClass])
REFERENCES [dbo].[Class] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_Class_IDClass]
GO
ALTER TABLE [dbo].[SubjectInElectiveGroup]  WITH CHECK ADD  CONSTRAINT [FK_SubjectInElectiveGroup_ElectiveGroup_IDElectiveGroup] FOREIGN KEY([IDElectiveGroup])
REFERENCES [dbo].[ElectiveGroup] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SubjectInElectiveGroup] CHECK CONSTRAINT [FK_SubjectInElectiveGroup_ElectiveGroup_IDElectiveGroup]
GO
ALTER TABLE [dbo].[SubjectInElectiveGroup]  WITH CHECK ADD  CONSTRAINT [FK_SubjectInElectiveGroup_Subject_IDSubject_IDCLass] FOREIGN KEY([IDSubject], [IDCLass])
REFERENCES [dbo].[Subject] ([ID], [IDClass])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SubjectInElectiveGroup] CHECK CONSTRAINT [FK_SubjectInElectiveGroup_Subject_IDSubject_IDCLass]
GO
ALTER TABLE [dbo].[SubjectInKnowledgeGroup]  WITH CHECK ADD  CONSTRAINT [FK_SubjectInKnowledgeGroup_KnowledgeGroup_IDKnowledgeGroup] FOREIGN KEY([IDKnowledgeGroup])
REFERENCES [dbo].[KnowledgeGroup] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SubjectInKnowledgeGroup] CHECK CONSTRAINT [FK_SubjectInKnowledgeGroup_KnowledgeGroup_IDKnowledgeGroup]
GO
ALTER TABLE [dbo].[SubjectInKnowledgeGroup]  WITH CHECK ADD  CONSTRAINT [FK_SubjectInKnowledgeGroup_Subject_IDSubject_IDClass] FOREIGN KEY([IDSubject], [IDClass])
REFERENCES [dbo].[Subject] ([ID], [IDClass])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SubjectInKnowledgeGroup] CHECK CONSTRAINT [FK_SubjectInKnowledgeGroup_Subject_IDSubject_IDClass]
GO
