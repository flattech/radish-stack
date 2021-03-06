USE [master]
GO
/****** Object:  Database [RadishStackDB]    Script Date: 2017-07-31 11:33:38 AM ******/
CREATE DATABASE [RadishStackDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RadishStackDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\RadishStackDB.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RadishStackDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\RadishStackDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [RadishStackDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RadishStackDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RadishStackDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RadishStackDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RadishStackDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RadishStackDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RadishStackDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [RadishStackDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RadishStackDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RadishStackDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RadishStackDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RadishStackDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RadishStackDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RadishStackDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RadishStackDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RadishStackDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RadishStackDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RadishStackDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RadishStackDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RadishStackDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RadishStackDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RadishStackDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RadishStackDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RadishStackDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RadishStackDB] SET RECOVERY FULL 
GO
ALTER DATABASE [RadishStackDB] SET  MULTI_USER 
GO
ALTER DATABASE [RadishStackDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RadishStackDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RadishStackDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RadishStackDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [RadishStackDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'RadishStackDB', N'ON'
GO
USE [RadishStackDB]
GO
/****** Object:  Table [dbo].[Media]    Script Date: 2017-07-31 11:33:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Media](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NULL,
	[RelativePath] [nvarchar](255) NOT NULL,
	[Status] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_Media_CreationDate]  DEFAULT (getutcdate()),
	[LastModified] [datetime] NOT NULL CONSTRAINT [DF_Media_LastModified]  DEFAULT (getutcdate()),
	[Type] [int] NOT NULL CONSTRAINT [DF__Media__Type__25869641]  DEFAULT ((0)),
	[PublicId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuItem]    Script Date: 2017-07-31 11:33:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItem](
	[Id] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[EntityName] [nvarchar](255) NULL,
	[Url] [nvarchar](500) NULL,
	[DisplayOrder] [int] NOT NULL CONSTRAINT [DF_MenuItem_DisplayOrder]  DEFAULT ((0)),
	[IsMega] [bit] NOT NULL CONSTRAINT [DF_MenuItem_IsMega]  DEFAULT ((0)),
	[IncludeInHeader] [bit] NOT NULL CONSTRAINT [DF_MenuItem_IncludeInHeader]  DEFAULT ((1)),
	[IncludeInFooter] [bit] NOT NULL CONSTRAINT [DF_MenuItem_IncludeInFooter]  DEFAULT ((0)),
	[PublicId] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[EntityId] [uniqueidentifier] NULL,
	[WidgetId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_MenuItem_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Post]    Script Date: 2017-07-31 11:33:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[Id] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Status] [int] NOT NULL,
	[Detail] [nvarchar](max) NULL,
	[IsInitial] [bit] NOT NULL,
	[ViewPath] [nvarchar](255) NULL,
	[Attachments] [nvarchar](max) NULL,
	[PostMetaValues] [nvarchar](255) NULL,
	[PublicId] [int] IDENTITY(1,1) NOT NULL,
	[PostTypeId] [uniqueidentifier] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[Photo] [nvarchar](255) NULL,
	[Author] [nvarchar](255) NULL,
	[Gallery] [nvarchar](max) NULL,
	[Widgets] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[PublishedDate] [datetime] NULL,
	[UrlKey] [nvarchar](255) NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PostMeta]    Script Date: 2017-07-31 11:33:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostMeta](
	[Id] [uniqueidentifier] NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
	[MetaKey] [nvarchar](255) NOT NULL,
	[MetaValue] [nvarchar](500) NOT NULL,
	[LastModified] [datetime] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PostMeta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PostTerm]    Script Date: 2017-07-31 11:33:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostTerm](
	[Id] [uniqueidentifier] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
	[TermId] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_PostTerm_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PostType]    Script Date: 2017-07-31 11:33:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostType](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](400) NOT NULL,
	[UrlKey] [nvarchar](255) NULL,
	[ViewPath] [nvarchar](400) NULL,
	[DisplayOrder] [int] NOT NULL,
	[EnableFeatureImage] [bit] NOT NULL,
	[EnableTags] [bit] NOT NULL,
	[EnableDescription] [bit] NOT NULL,
	[EnableSummary] [bit] NOT NULL,
	[EnableDateChoose] [bit] NULL,
	[EnableGallery] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[EnableCategories] [bit] NOT NULL,
	[EnableWidgets] [bit] NOT NULL,
	[EnableViewPath] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Icon] [nvarchar](255) NULL,
	[IsSystem] [bit] NOT NULL,
	[Status] [int] NOT NULL,
	[PublicId] [int] IDENTITY(1,1) NOT NULL,
	[TermViewPath] [nvarchar](255) NULL,
	[PostMetaFields] [nvarchar](max) NULL,
	[PostMediaList] [nvarchar](max) NULL,
	[TermTaxonomyList] [nvarchar](max) NULL,
 CONSTRAINT [PK_PostType_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Setting]    Script Date: 2017-07-31 11:33:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setting](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Value] [nvarchar](2000) NOT NULL,
	[Status] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_Config_CreationDate]  DEFAULT (getutcdate()),
	[PublicId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_Config] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Term]    Script Date: 2017-07-31 11:33:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Term](
	[Id] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[TaxonomyId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[IncludeInTopMenu] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsPublic] [bit] NOT NULL,
	[PublicId] [int] IDENTITY(1,1) NOT NULL,
	[PostTypeId] [uniqueidentifier] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[IsActive] [bit] NULL,
	[TaxonomyType] [nvarchar](255) NULL,
	[UrlKey] [nvarchar](255) NULL,
 CONSTRAINT [PK_Term] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 2017-07-31 11:33:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[PublicId] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Username] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Role] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[PasswordHash] [varchar](256) NOT NULL,
	[PasswordSalt] [varchar](256) NOT NULL,
	[Photo] [varchar](256) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Widget]    Script Date: 2017-07-31 11:33:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Widget](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[SourceCategories] [nvarchar](255) NULL,
	[SourceTags] [nvarchar](255) NULL,
	[SourcePosts] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[ViewPath] [nvarchar](255) NULL,
	[PostCount] [int] NOT NULL,
	[PublicId] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NOT NULL,
	[ReturnTags] [bit] NOT NULL,
	[ReturnCategories] [bit] NOT NULL,
	[ReturnPosts] [bit] NOT NULL,
	[PostType] [nvarchar](255) NOT NULL,
	[Tags] [nvarchar](max) NOT NULL,
	[Categories] [nvarchar](max) NOT NULL,
	[Posts] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Widget] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Media] ON 

INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'12216a4e-a6ee-4231-89d0-094e3a57adcc', NULL, N'/content/uploaded/05710623-777e-4251-a3f1-0e54550a966e.jpg', 0, CAST(N'2017-07-29 02:31:08.967' AS DateTime), CAST(N'2017-07-28 23:31:08.993' AS DateTime), 0, 1005)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'827c584b-1956-4cd9-b39a-1187fb6c286b', NULL, N'/content/uploaded/a7d15be4-4c1d-4ffe-8233-56c201a77f19.png', 0, CAST(N'2017-07-03 20:31:12.837' AS DateTime), CAST(N'2017-07-03 17:31:12.843' AS DateTime), 0, 5)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'6a917615-7b51-46ea-8f55-1891f7df26d4', NULL, N'/content/uploaded/0ad043d5-aa26-49b8-85b3-1eb4f53e26fc.png', 0, CAST(N'2017-07-03 20:29:58.473' AS DateTime), CAST(N'2017-07-03 17:29:58.480' AS DateTime), 0, 1)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'67ec67f1-4603-4eac-b942-2508ad38704d', NULL, N'/content/uploaded/cf1e1edb-89d8-4041-9078-42b5ab361d07.png', 0, CAST(N'2017-07-04 00:17:09.857' AS DateTime), CAST(N'2017-07-03 21:17:09.870' AS DateTime), 0, 6)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'070948e2-4f62-40d9-a395-346c11df74d9', NULL, N'/content/uploaded/190cb20d-2630-4e8b-a0f2-3baafdde09be.jpg', 0, CAST(N'2017-07-29 02:41:00.530' AS DateTime), CAST(N'2017-07-28 23:41:00.547' AS DateTime), 0, 1009)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'9697d155-8b20-4322-b9a5-35e9ec203f52', NULL, N'/content/uploaded/c4cc6199-2651-4f55-a475-93ca3922a9ef.png', 0, CAST(N'2017-07-03 20:30:03.887' AS DateTime), CAST(N'2017-07-03 17:30:03.890' AS DateTime), 0, 2)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'd9e3f498-2c82-4321-a247-387e875d90eb', NULL, N'/content/uploaded/4dffce16-c0ff-4fa6-9bca-eae64ce33395.jpg', 0, CAST(N'2017-07-29 02:42:42.750' AS DateTime), CAST(N'2017-07-28 23:42:42.760' AS DateTime), 0, 1010)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'4d0f77c2-5b9f-49f3-966e-40721f47c6d9', NULL, N'/content/uploaded/7d7739f6-5995-4070-8293-d05d19d94b08.png', 0, CAST(N'2017-07-22 15:09:11.120' AS DateTime), CAST(N'2017-07-22 12:09:11.137' AS DateTime), 0, 9)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'ccdc0b5c-c78d-4024-9396-4b169a76aadf', NULL, N'/content/uploaded/0fec837e-bc66-4264-8806-28e9636ed660.jpg', 0, CAST(N'2017-07-29 02:44:17.477' AS DateTime), CAST(N'2017-07-28 23:44:17.493' AS DateTime), 0, 1011)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'ec7b3ef6-f1b2-40a0-91db-8f81399b61b9', NULL, N'/content/uploaded/2dc6acb8-7b62-4de6-b16a-ff2ffdaf0414.png', 0, CAST(N'2017-07-22 15:09:08.100' AS DateTime), CAST(N'2017-07-22 12:09:08.110' AS DateTime), 0, 8)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'1f34421d-f2c8-487b-8c0c-aa7e49542f9d', NULL, N'/content/uploaded/8e54e0c2-1111-4373-8bde-4f808a046114.jpg', 0, CAST(N'2017-07-29 02:35:05.180' AS DateTime), CAST(N'2017-07-28 23:35:05.193' AS DateTime), 0, 1006)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'5ba990b9-20d2-4102-94bf-c58e50d34b7b', NULL, N'/content/uploaded/1652b416-615c-4413-b064-d74b72e94f2c.png', 0, CAST(N'2017-07-03 20:31:10.897' AS DateTime), CAST(N'2017-07-03 17:31:10.903' AS DateTime), 0, 4)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'97eee9fb-d884-41cd-aa40-d50cc43c88c1', NULL, N'/content/uploaded/c988e995-a374-4d5d-8895-6b22a7636895.jpg', 0, CAST(N'2017-07-29 02:39:03.790' AS DateTime), CAST(N'2017-07-28 23:39:03.797' AS DateTime), 0, 1008)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'8b59fbff-7329-42ff-a8a5-e6386ea037cc', NULL, N'/content/uploaded/b4f3d95f-1ffc-4a8c-9a52-3ebe9fbbca72.png', 0, CAST(N'2017-07-03 20:30:08.677' AS DateTime), CAST(N'2017-07-03 17:30:08.683' AS DateTime), 0, 3)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'ad12b86c-0ecc-41ef-8dfb-e91dcb29bca3', NULL, N'/content/uploaded/fa99c2e8-185b-479b-81a0-5b2ee1ed8837.jpg', 0, CAST(N'2017-07-29 02:38:31.783' AS DateTime), CAST(N'2017-07-28 23:38:31.810' AS DateTime), 0, 1007)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'578c1a22-47eb-4305-a71e-f2b4c244c1ef', NULL, N'/content/uploaded/e0a5ba11-2a30-46d9-98df-336391b958a5.jpg', 0, CAST(N'2017-07-22 15:07:20.230' AS DateTime), CAST(N'2017-07-22 12:07:20.250' AS DateTime), 0, 7)
SET IDENTITY_INSERT [dbo].[Media] OFF
SET IDENTITY_INSERT [dbo].[MenuItem] ON 

INSERT [dbo].[MenuItem] ([Id], [CreationDate], [IsActive], [Title], [EntityName], [Url], [DisplayOrder], [IsMega], [IncludeInHeader], [IncludeInFooter], [PublicId], [Status], [ParentId], [EntityId], [WidgetId]) VALUES (N'884412d1-f73d-4086-a913-22286c9bb0ad', CAST(N'2017-07-27 18:42:45.113' AS DateTime), 1, N'News', N'None', NULL, 2, 0, 1, 1, 3, 0, NULL, NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [CreationDate], [IsActive], [Title], [EntityName], [Url], [DisplayOrder], [IsMega], [IncludeInHeader], [IncludeInFooter], [PublicId], [Status], [ParentId], [EntityId], [WidgetId]) VALUES (N'16979339-db6c-4495-88d7-614559d2d0d1', CAST(N'2017-07-27 18:43:29.267' AS DateTime), 1, N'Politics', N'Term', NULL, 3, 0, 1, 1, 4, 0, N'884412d1-f73d-4086-a913-22286c9bb0ad', NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [CreationDate], [IsActive], [Title], [EntityName], [Url], [DisplayOrder], [IsMega], [IncludeInHeader], [IncludeInFooter], [PublicId], [Status], [ParentId], [EntityId], [WidgetId]) VALUES (N'4de0921f-3420-404c-94db-9a0a566d0a47', CAST(N'2017-07-29 03:09:42.827' AS DateTime), 1, N'Sport', N'Term', NULL, 4, 0, 1, 1, 5, 0, NULL, NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [CreationDate], [IsActive], [Title], [EntityName], [Url], [DisplayOrder], [IsMega], [IncludeInHeader], [IncludeInFooter], [PublicId], [Status], [ParentId], [EntityId], [WidgetId]) VALUES (N'b5320b76-03d8-4209-b46d-f0847c850e12', CAST(N'2017-07-27 18:30:51.767' AS DateTime), 1, N'Home', N'Post', NULL, 0, 0, 1, 1, 2, 0, NULL, NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [CreationDate], [IsActive], [Title], [EntityName], [Url], [DisplayOrder], [IsMega], [IncludeInHeader], [IncludeInFooter], [PublicId], [Status], [ParentId], [EntityId], [WidgetId]) VALUES (N'75138a02-dfab-4bde-b12c-fe8ad12d34ed', CAST(N'2017-07-29 03:14:29.303' AS DateTime), 1, N'Latest', N'None', N'#', 3, 1, 1, 0, 6, 0, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[MenuItem] OFF
SET IDENTITY_INSERT [dbo].[Post] ON 

INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'81a7af2a-76d2-41ec-a612-02887b82cee0', CAST(N'2017-07-03 20:20:40.753' AS DateTime), N'David''s character argued that women and children shouldn''t be let', 20, N'<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.</p>
<p>Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
<p>Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est.</p>
<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
<p></p>
<p><strong>"Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo."</strong></p>
<p></p>
<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>', 0, NULL, NULL, N'[{"Key":"facebook","Value":""},{"Key":"twitter","Value":""}]', 11, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, N'/content/uploaded/c988e995-a374-4d5d-8895-6b22a7636895.jpg', NULL, N'[
  {
    "Path": "/content/uploaded/a7d15be4-4c1d-4ffe-8233-56c201a77f19.png?width=300",
    "Title": null
  },
  {
    "Path": "/content/uploaded/1652b416-615c-4413-b064-d74b72e94f2c.png?width=300",
    "Title": null
  }
]', NULL, NULL, CAST(N'2017-07-28 23:40:04.770' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'b7e67ea4-4aea-4a59-8e93-0be7befd55e8', CAST(N'2017-07-29 02:44:34.833' AS DateTime), N'Taking a line right out of David''s book, Sanders responded', 20, N'<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.</p>
<p>Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
<p>Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est.</p>
<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
<p></p>
<p><strong>"Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo."</strong></p>
<p></p>
<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>', 0, NULL, NULL, N'[{"Key":"facebook","Value":""},{"Key":"twitter","Value":""}]', 1013, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, N'/content/uploaded/0fec837e-bc66-4264-8806-28e9636ed660.jpg', NULL, N'[]', NULL, NULL, CAST(N'2017-07-28 23:44:34.823' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'017b8c1c-6ac3-419a-b88e-105a5c92a97c', CAST(N'2017-07-03 20:20:08.020' AS DateTime), N'sdfh', -100, N'<p><strong>fdshfdhg</strong></p>', 0, NULL, NULL, NULL, 9, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, NULL, NULL, N'[]', NULL, NULL, NULL, NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'86bca7c3-7f4a-4dcf-a39d-2d6b7629c934', CAST(N'2017-07-03 20:13:39.407' AS DateTime), N'Home', 20, NULL, 0, NULL, NULL, NULL, 3, N'0004aef4-1380-40e1-bdbf-5c6dd6e7bddf', NULL, N'/content/uploaded/7d7739f6-5995-4070-8293-d05d19d94b08.png', NULL, N'[]', N'{"rows":[{"classname":"","cols":[{"classname":"","lg":"12","text":"c2","rows":[],"widgets":[{"title":"slider","widgetid":"9761548f-92af-4377-bedc-147d9a7aa2f4"}]}]}]}', NULL, CAST(N'2017-07-28 23:47:54.760' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'268db371-209f-4fdc-a613-518a676fae3d', CAST(N'2017-07-22 15:07:53.330' AS DateTime), N'David''s character argued that women and children shouldn''t be let', 20, N'<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.</p>
<p>Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
<p>Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est.</p>
<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
<p class="speakable-p-1 p-text"></p>
<p><strong>"Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo."</strong></p>
<p class="speakable-p-1 p-text"></p>
<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>', 0, NULL, NULL, N'[{"Key":"facebook","Value":null},{"Key":"twitter","Value":null}]', 12, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, N'/content/uploaded/8e54e0c2-1111-4373-8bde-4f808a046114.jpg', NULL, N'[]', NULL, NULL, CAST(N'2017-07-28 23:37:45.440' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'99078224-5880-4306-906f-98272610ea5c', CAST(N'2017-07-29 02:42:49.843' AS DateTime), N'Those people later turn out to be key to his slim loss to Hillary ', 20, N'<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.</p>
<p>Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
<p>Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est.</p>
<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
<p></p>
<p><strong>"Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo."</strong></p>
<p></p>
<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>', 0, NULL, NULL, N'[{"Key":"facebook","Value":""},{"Key":"twitter","Value":""}]', 1012, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, N'/content/uploaded/4dffce16-c0ff-4fa6-9bca-eae64ce33395.jpg', NULL, N'[]', NULL, NULL, CAST(N'2017-07-28 23:42:49.833' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'dcf3a3ba-bc71-4393-bd11-a367dd1ce278', CAST(N'2017-07-03 20:19:46.623' AS DateTime), N'dfgdfg', -100, NULL, 0, NULL, NULL, NULL, 7, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, NULL, NULL, N'[]', NULL, NULL, NULL, NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'699b4958-75ba-4c18-81a0-a7370d02c4f6', CAST(N'2017-07-03 20:19:02.143' AS DateTime), N'he comes to us', -100, N'<p><strong>asdasd</strong></p>', 0, NULL, NULL, NULL, 5, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, NULL, NULL, N'[]', NULL, NULL, NULL, NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'8aabee41-9798-4e91-a7d0-bb2cdae90b82', CAST(N'2017-07-03 20:20:13.550' AS DateTime), N'sdfh', -100, N'<p><strong>fdshfdhg</strong></p>', 0, NULL, NULL, NULL, 10, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, NULL, NULL, N'[]', NULL, NULL, NULL, NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'168171ba-207a-480f-a766-e7c3dd2f7af8', CAST(N'2017-07-03 20:19:13.597' AS DateTime), N'he comes to us', -100, N'<p><strong>asdasd</strong></p>', 0, NULL, NULL, NULL, 6, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, NULL, NULL, N'[]', NULL, NULL, NULL, NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'4a5e6914-498b-4a6c-886e-f832c63b6e71', CAST(N'2017-07-29 02:41:08.580' AS DateTime), N'The sketch had David once again impersonating Sanders as voters cussed', 20, N'<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.</p>
<p>Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
<p>Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est.</p>
<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
<p></p>
<p><strong>"Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo."</strong></p>
<p></p>
<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>', 0, NULL, NULL, N'[{"Key":"facebook","Value":""},{"Key":"twitter","Value":""}]', 1011, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, N'/content/uploaded/190cb20d-2630-4e8b-a0f2-3baafdde09be.jpg', NULL, N'[]', NULL, NULL, CAST(N'2017-07-28 23:42:10.620' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'42057c65-52c3-4339-8bf6-ff8a66c91de1', CAST(N'2017-07-03 20:19:54.860' AS DateTime), N'sdfh', -100, N'<p><strong>fdshfdhg</strong></p>', 0, NULL, NULL, NULL, 8, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, NULL, NULL, N'[]', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Post] OFF
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'677e0567-813d-4cc4-9472-006eb7a82471', 3, CAST(N'2017-07-29 02:40:04.833' AS DateTime), N'81a7af2a-76d2-41ec-a612-02887b82cee0', N'cd92ad30-8edf-413f-b119-1bedbe76ea5d', 0)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'fcb31508-3572-433f-b6bc-0a6872b1cdac', 1, CAST(N'2017-07-29 02:41:59.973' AS DateTime), N'4a5e6914-498b-4a6c-886e-f832c63b6e71', N'2821a0bc-1c04-46d4-a22c-722e924a2652', 0)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'b3861cc3-79d6-4ecc-911d-334c5cf2587a', 1, CAST(N'2017-07-29 02:39:54.100' AS DateTime), N'81a7af2a-76d2-41ec-a612-02887b82cee0', N'3338f38e-ebe1-4367-bea2-9c5fed06380c', 0)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'dd638910-60ef-4e61-884e-4dbe78c6b2e0', 2, CAST(N'2017-07-22 15:07:53.377' AS DateTime), N'268db371-209f-4fdc-a613-518a676fae3d', N'ae20e7b6-0c29-407e-b29a-2bc88c2efd28', 0)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'198e3e77-3f04-4b2b-9547-5f1fe8dc2926', 2, CAST(N'2017-07-03 20:23:50.070' AS DateTime), N'81a7af2a-76d2-41ec-a612-02887b82cee0', N'25e90a30-0f4b-403c-9b5a-4aa5c2699e36', -100)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'7f6ce100-381f-4326-832e-61a4592c9640', 3, CAST(N'2017-07-29 02:37:45.530' AS DateTime), N'268db371-209f-4fdc-a613-518a676fae3d', N'1d9b7506-a35a-4a16-a505-89d00b1d2e6f', 0)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'374627da-7e00-41ce-b70a-7a5896328908', 1, CAST(N'2017-07-29 02:44:34.843' AS DateTime), N'b7e67ea4-4aea-4a59-8e93-0be7befd55e8', N'7a3d1c87-0e90-4ae5-971a-9c5bba4ea81e', 0)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'6cdc9ee2-6963-49cc-91e2-803e3e049eb4', 3, CAST(N'2017-07-03 20:24:22.390' AS DateTime), N'81a7af2a-76d2-41ec-a612-02887b82cee0', N'a818b888-6fe9-4881-bd52-699cc51b8f15', -100)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'd0877c30-29ba-4d69-97a9-806544a4ccdc', 1, CAST(N'2017-07-03 20:23:39.537' AS DateTime), N'81a7af2a-76d2-41ec-a612-02887b82cee0', N'25e90a30-0f4b-403c-9b5a-4aa5c2699e36', -100)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'22bcfcca-4b78-44b6-b1bf-b33e2c6fd347', 4, CAST(N'2017-07-03 20:23:47.843' AS DateTime), N'81a7af2a-76d2-41ec-a612-02887b82cee0', N'1d9b7506-a35a-4a16-a505-89d00b1d2e6f', -100)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'3f3f5992-7986-4731-beea-ba0a92daeb04', 2, CAST(N'2017-07-29 02:42:10.657' AS DateTime), N'4a5e6914-498b-4a6c-886e-f832c63b6e71', N'cd92ad30-8edf-413f-b119-1bedbe76ea5d', 0)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'5c2bd9fa-fac2-486f-80a9-c38ce79cf0ff', 2, CAST(N'2017-07-29 02:39:29.310' AS DateTime), N'81a7af2a-76d2-41ec-a612-02887b82cee0', N'50b6a7d1-a902-4060-8006-721211ca2be9', 0)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'2f06cfc9-5f8b-4b61-b6bf-cbbb111419f8', 1, CAST(N'2017-07-29 02:37:45.513' AS DateTime), N'268db371-209f-4fdc-a613-518a676fae3d', N'55f755ae-e5b7-49a9-a1c4-8c5299c95884', 0)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'fe7281e8-d49a-4cfe-b77e-d8a6b5d478da', 1, CAST(N'2017-07-29 02:42:49.857' AS DateTime), N'99078224-5880-4306-906f-98272610ea5c', N'e8821ed0-8002-4252-aba3-722c3b7ef4fd', 0)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'c19c3fec-f6bb-4ea8-a86c-e56362f6afd0', 1, CAST(N'2017-07-03 20:23:44.673' AS DateTime), N'81a7af2a-76d2-41ec-a612-02887b82cee0', N'e8821ed0-8002-4252-aba3-722c3b7ef4fd', -100)
INSERT [dbo].[PostTerm] ([Id], [DisplayOrder], [CreationDate], [PostId], [TermId], [Status]) VALUES (N'727c94be-cd41-4297-b63f-ef3140bb001e', 1, CAST(N'2017-07-22 15:07:53.367' AS DateTime), N'268db371-209f-4fdc-a613-518a676fae3d', N'25e90a30-0f4b-403c-9b5a-4aa5c2699e36', -100)
SET IDENTITY_INSERT [dbo].[PostType] ON 

INSERT [dbo].[PostType] ([Id], [Title], [UrlKey], [ViewPath], [DisplayOrder], [EnableFeatureImage], [EnableTags], [EnableDescription], [EnableSummary], [EnableDateChoose], [EnableGallery], [CreationDate], [EnableCategories], [EnableWidgets], [EnableViewPath], [IsActive], [Icon], [IsSystem], [Status], [PublicId], [TermViewPath], [PostMetaFields], [PostMediaList], [TermTaxonomyList]) VALUES (N'37ce72cf-6376-47d1-81c7-0322c95587f7', N'News', N'news', N'', 1, 1, 1, 1, 1, NULL, 1, CAST(N'2017-07-03 19:52:09.470' AS DateTime), 1, 0, 0, 1, N'pencil', 0, 0, 3, N'news', N'[{"key":"facebook","type":"text"},{"key":"twitter","type":"text"}]', N'[{"key":"Slider","type":"image"},{"key":"PDF","type":"file"}]', N'[{"key":"Category","taxonomy":"tree"},{"key":"Tags","taxonomy":"tag"},{"key":"Location","taxonomy":"dropdown"}]')
INSERT [dbo].[PostType] ([Id], [Title], [UrlKey], [ViewPath], [DisplayOrder], [EnableFeatureImage], [EnableTags], [EnableDescription], [EnableSummary], [EnableDateChoose], [EnableGallery], [CreationDate], [EnableCategories], [EnableWidgets], [EnableViewPath], [IsActive], [Icon], [IsSystem], [Status], [PublicId], [TermViewPath], [PostMetaFields], [PostMediaList], [TermTaxonomyList]) VALUES (N'0004aef4-1380-40e1-bdbf-5c6dd6e7bddf', N'Pages', N'page', N'', 1, 1, 0, 1, 1, 1, 1, CAST(N'2017-10-10 00:00:00.000' AS DateTime), 0, 1, 1, 1, N'tag', 0, 0, 1, N'page', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PostType] OFF
SET IDENTITY_INSERT [dbo].[Setting] ON 

INSERT [dbo].[Setting] ([Id], [Name], [Value], [Status], [CreationDate], [PublicId], [Type]) VALUES (N'827c584b-1956-4cd9-b39a-1187fb6c286b', N'ThemeConfig', N'{"ThemeName":"DailyNews"}', 10, CAST(N'2017-10-10 00:00:00.000' AS DateTime), 1, 10)
SET IDENTITY_INSERT [dbo].[Setting] OFF
SET IDENTITY_INSERT [dbo].[Term] ON 

INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'cd92ad30-8edf-413f-b119-1bedbe76ea5d', CAST(N'2017-07-29 02:40:04.810' AS DateTime), N'David', 20, 0, 0, 0, 1, 1009, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, NULL)
INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'ae20e7b6-0c29-407e-b29a-2bc88c2efd28', CAST(N'2017-07-22 15:07:53.357' AS DateTime), N'USA', 20, 0, 0, 0, 1, 8, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, NULL)
INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'25e90a30-0f4b-403c-9b5a-4aa5c2699e36', CAST(N'2017-07-03 20:08:24.610' AS DateTime), N'Politics', 10, 0, 0, 4, 1, 3, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, NULL)
INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'7eec77d0-4211-4738-baed-5238e5a90eeb', CAST(N'2017-07-29 02:32:50.070' AS DateTime), N'Business', 10, 0, 0, 5, 1, 1004, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, NULL)
INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'a818b888-6fe9-4881-bd52-699cc51b8f15', CAST(N'2017-07-03 20:24:22.350' AS DateTime), N'Money', 20, 0, 0, 0, 1, 6, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, NULL)
INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'2ca9d1cd-8dc9-4e21-8a85-716897feee27', CAST(N'2017-07-29 02:33:13.770' AS DateTime), N'Technology', 10, 0, 0, 8, 1, 1006, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, NULL)
INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'50b6a7d1-a902-4060-8006-721211ca2be9', CAST(N'2017-07-29 02:39:29.287' AS DateTime), N'Music', 20, 0, 0, 0, 1, 1007, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, NULL)
INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'e8821ed0-8002-4252-aba3-722c3b7ef4fd', CAST(N'2017-07-03 20:08:35.820' AS DateTime), N'Sport', 10, 0, 0, 3, 1, 4, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, NULL)
INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'2821a0bc-1c04-46d4-a22c-722e924a2652', CAST(N'2017-07-29 02:41:50.570' AS DateTime), N'Lifestyle', 10, 0, 0, 10, 1, 1010, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, N'lifestyle')
INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'1d9b7506-a35a-4a16-a505-89d00b1d2e6f', CAST(N'2017-07-03 20:10:28.847' AS DateTime), N'Ebook', 20, 0, 0, 0, 1, 5, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, NULL)
INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'55f755ae-e5b7-49a9-a1c4-8c5299c95884', CAST(N'2017-07-29 02:31:48.387' AS DateTime), N'World', 10, 0, 0, 1, 1, 1003, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, NULL)
INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'7a3d1c87-0e90-4ae5-971a-9c5bba4ea81e', CAST(N'2017-07-29 02:33:01.320' AS DateTime), N'Entertainment', 10, 0, 0, 6, 1, 1005, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, NULL)
INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'3338f38e-ebe1-4367-bea2-9c5fed06380c', CAST(N'2017-07-29 02:39:39.130' AS DateTime), N'Music', 10, 0, 0, 0, 1, 1008, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, NULL)
INSERT [dbo].[Term] ([Id], [CreationDate], [Title], [TaxonomyId], [Status], [IncludeInTopMenu], [DisplayOrder], [IsPublic], [PublicId], [PostTypeId], [ParentId], [IsActive], [TaxonomyType], [UrlKey]) VALUES (N'cafb98ba-2903-433a-9f1c-ef2b64235b1b', CAST(N'2017-07-03 20:27:30.563' AS DateTime), N'Money', 10, 0, 0, 2, 1, 7, N'37ce72cf-6376-47d1-81c7-0322c95587f7', NULL, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Term] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [PublicId], [CreationDate], [Name], [Username], [Email], [Role], [Status], [PasswordHash], [PasswordSalt], [Photo]) VALUES (N'0004aef4-1380-40e1-bdbf-5c6dd6e7bddf', 1, CAST(N'2017-10-10 00:00:00.000' AS DateTime), N'admin', N'admin', N'admin', 10, 10, N'btWDPPNShuv4Zit7WUnw10K77D8=', N'QRhr7ksom8wrAkQOcycax6PyiI576UkZGUKjOEk+Ewk=', N'')
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[Widget] ON 

INSERT [dbo].[Widget] ([Id], [Title], [CreationDate], [SourceCategories], [SourceTags], [SourcePosts], [IsActive], [ViewPath], [PostCount], [PublicId], [Status], [ReturnTags], [ReturnCategories], [ReturnPosts], [PostType], [Tags], [Categories], [Posts]) VALUES (N'9761548f-92af-4377-bedc-147d9a7aa2f4', N'slider', CAST(N'2017-07-03 23:06:43.907' AS DateTime), NULL, NULL, NULL, 1, N'slider', 10, 4, 0, 0, 0, 1, N'', N'', N'', N'b7e67ea4-4aea-4a59-8e93-0be7befd55e8,99078224-5880-4306-906f-98272610ea5c,4a5e6914-498b-4a6c-886e-f832c63b6e71,268db371-209f-4fdc-a613-518a676fae3d,81a7af2a-76d2-41ec-a612-02887b82cee0')
INSERT [dbo].[Widget] ([Id], [Title], [CreationDate], [SourceCategories], [SourceTags], [SourcePosts], [IsActive], [ViewPath], [PostCount], [PublicId], [Status], [ReturnTags], [ReturnCategories], [ReturnPosts], [PostType], [Tags], [Categories], [Posts]) VALUES (N'ca8b74a6-7602-4da4-9c4e-7ca3612ce768', N'news', CAST(N'2017-07-09 00:14:12.120' AS DateTime), NULL, NULL, NULL, 1, N'slider', 10, 5, 0, 1, 1, 1, N'37ce72cf-6376-47d1-81c7-0322c95587f7', N'1d9b7506-a35a-4a16-a505-89d00b1d2e6f', N'cafb98ba-2903-433a-9f1c-ef2b64235b1b', N'268db371-209f-4fdc-a613-518a676fae3d')
SET IDENTITY_INSERT [dbo].[Widget] OFF
USE [master]
GO
ALTER DATABASE [RadishStackDB] SET  READ_WRITE 
GO
