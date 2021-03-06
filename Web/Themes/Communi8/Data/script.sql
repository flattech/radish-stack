USE [Communi8DB]
GO
/****** Object:  Table [dbo].[Media]    Script Date: 8/22/2017 3:34:16 AM ******/
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
/****** Object:  Table [dbo].[MenuItem]    Script Date: 8/22/2017 3:34:16 AM ******/
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
/****** Object:  Table [dbo].[Post]    Script Date: 8/22/2017 3:34:16 AM ******/
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
/****** Object:  Table [dbo].[PostMeta]    Script Date: 8/22/2017 3:34:16 AM ******/
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
/****** Object:  Table [dbo].[PostTerm]    Script Date: 8/22/2017 3:34:16 AM ******/
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
/****** Object:  Table [dbo].[PostType]    Script Date: 8/22/2017 3:34:16 AM ******/
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
/****** Object:  Table [dbo].[Setting]    Script Date: 8/22/2017 3:34:16 AM ******/
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
/****** Object:  Table [dbo].[Term]    Script Date: 8/22/2017 3:34:16 AM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 8/22/2017 3:34:16 AM ******/
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
/****** Object:  Table [dbo].[Widget]    Script Date: 8/22/2017 3:34:16 AM ******/
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
	[PostType] [nvarchar](255) NULL,
	[Tags] [nvarchar](max) NULL,
	[Categories] [nvarchar](max) NULL,
	[Posts] [nvarchar](max) NULL,
 CONSTRAINT [PK_Widget] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Media] ON 

INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'732ffd09-8020-43b0-a646-07ee06e5cc7c', NULL, N'/content/uploaded/b2fdee30-2363-45ce-90e2-dae8f2bf9601.jpg', 0, CAST(N'2017-08-22 00:16:13.217' AS DateTime), CAST(N'2017-08-21 21:16:13.227' AS DateTime), 0, 1031)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'12216a4e-a6ee-4231-89d0-094e3a57adcc', NULL, N'/content/uploaded/05710623-777e-4251-a3f1-0e54550a966e.jpg', 0, CAST(N'2017-07-29 02:31:08.967' AS DateTime), CAST(N'2017-07-28 23:31:08.993' AS DateTime), 0, 1005)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'827c584b-1956-4cd9-b39a-1187fb6c286b', NULL, N'/content/uploaded/a7d15be4-4c1d-4ffe-8233-56c201a77f19.png', 0, CAST(N'2017-07-03 20:31:12.837' AS DateTime), CAST(N'2017-07-03 17:31:12.843' AS DateTime), 0, 5)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'80037657-1499-495b-917a-14d1eca7d0a9', NULL, N'/content/uploaded/f89704e1-2ed6-4eb0-a900-daef165b7158.jpg', 0, CAST(N'2017-08-22 01:17:30.323' AS DateTime), CAST(N'2017-08-21 22:17:30.343' AS DateTime), 0, 1037)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'6a917615-7b51-46ea-8f55-1891f7df26d4', NULL, N'/content/uploaded/0ad043d5-aa26-49b8-85b3-1eb4f53e26fc.png', 0, CAST(N'2017-07-03 20:29:58.473' AS DateTime), CAST(N'2017-07-03 17:29:58.480' AS DateTime), 0, 1)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'75ea4e84-ae20-46ec-979d-247fa50a73be', NULL, N'/content/uploaded/9dec9190-8151-4374-b720-1cf12e699b12.jpg', 0, CAST(N'2017-08-22 00:05:42.567' AS DateTime), CAST(N'2017-08-21 21:05:42.583' AS DateTime), 0, 1015)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'93027f47-21db-4573-8156-24bf0b96596a', NULL, N'/content/uploaded/84986ef0-7113-4b80-8b89-54e9e5a0696d.jpg', 0, CAST(N'2017-08-22 02:10:49.463' AS DateTime), CAST(N'2017-08-21 23:10:49.473' AS DateTime), 0, 1042)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'67ec67f1-4603-4eac-b942-2508ad38704d', NULL, N'/content/uploaded/cf1e1edb-89d8-4041-9078-42b5ab361d07.png', 0, CAST(N'2017-07-04 00:17:09.857' AS DateTime), CAST(N'2017-07-03 21:17:09.870' AS DateTime), 0, 6)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'3a8afdaa-0c98-4223-b2b5-2b859537dc7f', NULL, N'/content/uploaded/6f806524-a291-4e63-9cd6-a3c2cc7911df.jpg', 0, CAST(N'2017-08-22 03:12:34.953' AS DateTime), CAST(N'2017-08-22 00:12:34.960' AS DateTime), 0, 1051)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'680abfb1-7f5d-4a39-96ee-2c729c65dfd7', NULL, N'/content/uploaded/952498eb-0e63-4068-9828-500e806e2ffc.jpg', 0, CAST(N'2017-08-22 00:15:43.090' AS DateTime), CAST(N'2017-08-21 21:15:43.097' AS DateTime), 0, 1030)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'deb4e584-4578-48dc-9f41-2deb3586e4a2', NULL, N'/content/uploaded/741b0142-47d7-4efd-89c7-dc62a737bcad.png', 0, CAST(N'2017-08-21 23:28:42.173' AS DateTime), CAST(N'2017-08-21 20:28:42.197' AS DateTime), 0, 1014)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'b9c58048-bd41-4769-80ff-31706b273a0d', NULL, N'/content/uploaded/40d8fb35-a127-466e-932b-859d0596b5ad.jpg', 0, CAST(N'2017-08-22 03:12:07.797' AS DateTime), CAST(N'2017-08-22 00:12:07.800' AS DateTime), 0, 1046)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'070948e2-4f62-40d9-a395-346c11df74d9', NULL, N'/content/uploaded/190cb20d-2630-4e8b-a0f2-3baafdde09be.jpg', 0, CAST(N'2017-07-29 02:41:00.530' AS DateTime), CAST(N'2017-07-28 23:41:00.547' AS DateTime), 0, 1009)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'9697d155-8b20-4322-b9a5-35e9ec203f52', NULL, N'/content/uploaded/c4cc6199-2651-4f55-a475-93ca3922a9ef.png', 0, CAST(N'2017-07-03 20:30:03.887' AS DateTime), CAST(N'2017-07-03 17:30:03.890' AS DateTime), 0, 2)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'a4cefa38-e0a9-4d39-a380-36955db2a024', NULL, N'/content/uploaded/015539af-1b28-4307-bfc3-a95be4e5a2bc.png', 0, CAST(N'2017-08-07 23:55:20.090' AS DateTime), CAST(N'2017-08-07 20:55:20.103' AS DateTime), 0, 1013)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'd9e3f498-2c82-4321-a247-387e875d90eb', NULL, N'/content/uploaded/4dffce16-c0ff-4fa6-9bca-eae64ce33395.jpg', 0, CAST(N'2017-07-29 02:42:42.750' AS DateTime), CAST(N'2017-07-28 23:42:42.760' AS DateTime), 0, 1010)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'5459e3f4-e8d6-43bc-bd87-3883a166eff1', NULL, N'/content/uploaded/4dd074f6-676f-4226-8084-6175a03e3aaf.jpg', 0, CAST(N'2017-08-22 03:12:24.560' AS DateTime), CAST(N'2017-08-22 00:12:24.567' AS DateTime), 0, 1049)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'4d0f77c2-5b9f-49f3-966e-40721f47c6d9', NULL, N'/content/uploaded/7d7739f6-5995-4070-8293-d05d19d94b08.png', 0, CAST(N'2017-07-22 15:09:11.120' AS DateTime), CAST(N'2017-07-22 12:09:11.137' AS DateTime), 0, 9)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'e1af26af-92b9-43e5-8174-41dfe5e98b55', NULL, N'/content/uploaded/f81120aa-2570-4e97-b40b-0af90fbc52e5.jpg', 0, CAST(N'2017-08-22 03:12:15.907' AS DateTime), CAST(N'2017-08-22 00:12:15.913' AS DateTime), 0, 1047)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'ccdc0b5c-c78d-4024-9396-4b169a76aadf', NULL, N'/content/uploaded/0fec837e-bc66-4264-8806-28e9636ed660.jpg', 0, CAST(N'2017-07-29 02:44:17.477' AS DateTime), CAST(N'2017-07-28 23:44:17.493' AS DateTime), 0, 1011)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'37a00ae2-ea7f-4a7d-a1b8-509ddf497c33', NULL, N'/content/uploaded/43212393-080a-478e-aa0d-1684eeda0756.jpg', 0, CAST(N'2017-08-22 01:18:48.017' AS DateTime), CAST(N'2017-08-21 22:18:48.030' AS DateTime), 0, 1039)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'92d0a980-ea87-4a21-b824-51d9ce6e2f0d', NULL, N'/content/uploaded/f7b851d1-05d0-4378-b46f-194be5ece874.jpg', 0, CAST(N'2017-08-07 23:28:44.493' AS DateTime), CAST(N'2017-08-07 20:28:44.507' AS DateTime), 0, 1012)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'38472c8a-079a-4b18-b970-596cb7807483', NULL, N'/content/uploaded/8db702e2-abe9-4312-92ce-9ed9b6f62771.jpg', 0, CAST(N'2017-08-22 00:17:43.707' AS DateTime), CAST(N'2017-08-21 21:17:43.717' AS DateTime), 0, 1034)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'772d22b3-7ac6-4b48-9a33-59ff6aebfc1a', NULL, N'/content/uploaded/f376b89c-ec8f-4fec-9633-22502fc2c6b2.jpg', 0, CAST(N'2017-08-22 00:09:43.900' AS DateTime), CAST(N'2017-08-21 21:09:43.907' AS DateTime), 0, 1021)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'18aaa600-6396-4899-8e94-5d6a18999622', NULL, N'/content/uploaded/d6d3e90f-3ab9-483f-b814-c36124ee1955.jpg', 0, CAST(N'2017-08-22 03:13:36.927' AS DateTime), CAST(N'2017-08-22 00:13:36.933' AS DateTime), 0, 1054)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'19cbe603-8e1c-4416-97e3-663b46bf4a9a', NULL, N'/content/uploaded/21949e77-ef63-4070-8016-ec1a5791e96d.jpg', 0, CAST(N'2017-08-22 00:08:09.537' AS DateTime), CAST(N'2017-08-21 21:08:09.543' AS DateTime), 0, 1018)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'45b7db32-37d5-40fa-8314-6eb1a060461f', NULL, N'/content/uploaded/cbc36128-7a67-483d-864f-850515ba7491.jpg', 0, CAST(N'2017-08-22 00:17:22.423' AS DateTime), CAST(N'2017-08-21 21:17:22.430' AS DateTime), 0, 1033)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'be9b7330-1eea-4413-8f4b-706bf5dbaeb0', NULL, N'/content/uploaded/7480cc84-a7a3-4dd3-86df-aa6001c7ec6c.jpg', 0, CAST(N'2017-08-22 03:12:30.650' AS DateTime), CAST(N'2017-08-22 00:12:30.657' AS DateTime), 0, 1050)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'f56a24f4-edc0-4081-aca8-78c295bf7ee0', NULL, N'/content/uploaded/9966ec0c-4c7a-4d85-a098-1e669aaf0f0b.jpg', 0, CAST(N'2017-08-22 03:11:49.723' AS DateTime), CAST(N'2017-08-22 00:11:49.730' AS DateTime), 0, 1044)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'4f999659-2b5f-435b-9203-7cf2f36b88e3', NULL, N'/content/uploaded/14e98067-1b8b-4f0a-9a3c-c92fbaa65834.jpg', 0, CAST(N'2017-08-22 03:12:46.240' AS DateTime), CAST(N'2017-08-22 00:12:46.247' AS DateTime), 0, 1052)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'555d3b80-f722-4a21-a55c-80ab26f22e0a', NULL, N'/content/uploaded/fa4b8961-3c9a-44ce-81ff-d6a372141ad7.jpg', 0, CAST(N'2017-08-22 01:17:32.687' AS DateTime), CAST(N'2017-08-21 22:17:32.690' AS DateTime), 0, 1038)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'4546239b-c2ba-4dc1-8ce7-825b1ef120f5', NULL, N'/content/uploaded/eb73b029-df7b-48b2-bdcb-6eba0404777f.jpg', 0, CAST(N'2017-08-22 00:12:02.677' AS DateTime), CAST(N'2017-08-21 21:12:02.690' AS DateTime), 0, 1025)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'cacc1d5a-1237-49d8-80bf-855bac514e09', NULL, N'/content/uploaded/1ef22207-1de6-4922-8686-5f696e4cbc2d.jpg', 0, CAST(N'2017-08-22 00:13:05.627' AS DateTime), CAST(N'2017-08-21 21:13:05.633' AS DateTime), 0, 1027)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'9fff30a7-21a6-4a2a-881e-8b747313e626', NULL, N'/content/uploaded/a4617d20-fe67-4ce1-81f9-dbd3a4f61214.jpg', 0, CAST(N'2017-08-22 03:12:20.507' AS DateTime), CAST(N'2017-08-22 00:12:20.513' AS DateTime), 0, 1048)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'ef374ccd-4386-453c-8eab-8efaa0ea2efc', NULL, N'/content/uploaded/cf01e6d1-4b8a-45c7-9158-1809a67c32b6.jpg', 0, CAST(N'2017-08-22 00:06:14.423' AS DateTime), CAST(N'2017-08-21 21:06:14.430' AS DateTime), 0, 1016)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'ec7b3ef6-f1b2-40a0-91db-8f81399b61b9', NULL, N'/content/uploaded/2dc6acb8-7b62-4de6-b16a-ff2ffdaf0414.png', 0, CAST(N'2017-07-22 15:09:08.100' AS DateTime), CAST(N'2017-07-22 12:09:08.110' AS DateTime), 0, 8)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'f04d1528-d346-47d5-b880-97f910451bde', NULL, N'/content/uploaded/8e54a947-8727-4180-be88-34adb61a6714.jpg', 0, CAST(N'2017-08-22 00:12:31.573' AS DateTime), CAST(N'2017-08-21 21:12:31.580' AS DateTime), 0, 1026)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'6080b6e5-c222-463f-93bb-98a5c60866fc', NULL, N'/content/uploaded/97bb33f8-bf3c-48f0-87fd-1ac59e30039e.jpg', 0, CAST(N'2017-08-22 01:19:07.690' AS DateTime), CAST(N'2017-08-21 22:19:07.697' AS DateTime), 0, 1041)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'3c4a0769-8f56-41c1-a37e-a12656920a8c', NULL, N'/content/uploaded/0cf584ad-7a7a-428e-8170-665f559f2c6b.jpg', 0, CAST(N'2017-08-22 00:10:33.360' AS DateTime), CAST(N'2017-08-21 21:10:33.367' AS DateTime), 0, 1022)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'540e2bd1-d8b1-47e0-a756-a571c62e5859', NULL, N'/content/uploaded/7ad75799-1282-44d3-b67b-6b339bc5030f.jpg', 0, CAST(N'2017-08-22 00:16:43.423' AS DateTime), CAST(N'2017-08-21 21:16:43.430' AS DateTime), 0, 1032)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'd18e2bca-e56d-44e2-ad97-a70df3c5ea28', NULL, N'/content/uploaded/8c1e23e3-af50-4d2b-901e-9eb7f036f2a9.jpg', 0, CAST(N'2017-08-22 00:09:23.013' AS DateTime), CAST(N'2017-08-21 21:09:23.020' AS DateTime), 0, 1020)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'1f34421d-f2c8-487b-8c0c-aa7e49542f9d', NULL, N'/content/uploaded/8e54e0c2-1111-4373-8bde-4f808a046114.jpg', 0, CAST(N'2017-07-29 02:35:05.180' AS DateTime), CAST(N'2017-07-28 23:35:05.193' AS DateTime), 0, 1006)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'd315eefb-7c5e-4509-a9ee-b12b74b89908', NULL, N'/content/uploaded/0d6531c9-5aa6-4320-bbbf-66ef4f730d8e.jpg', 0, CAST(N'2017-08-22 01:14:40.490' AS DateTime), CAST(N'2017-08-21 22:14:40.497' AS DateTime), 0, 1036)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'868f035b-a749-4b5d-85e1-b775313a16d5', NULL, N'/content/uploaded/b3d3a216-0d5c-4ec7-9e4d-84ce8a404767.jpg', 0, CAST(N'2017-08-22 00:08:36.537' AS DateTime), CAST(N'2017-08-21 21:08:36.543' AS DateTime), 0, 1019)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'4db63be2-32db-46eb-b513-bc15a59c9811', NULL, N'/content/uploaded/dd25a05e-2244-43e3-8434-a73944fbee2e.jpg', 0, CAST(N'2017-08-22 00:10:54.290' AS DateTime), CAST(N'2017-08-21 21:10:54.293' AS DateTime), 0, 1023)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'5ba990b9-20d2-4102-94bf-c58e50d34b7b', NULL, N'/content/uploaded/1652b416-615c-4413-b064-d74b72e94f2c.png', 0, CAST(N'2017-07-03 20:31:10.897' AS DateTime), CAST(N'2017-07-03 17:31:10.903' AS DateTime), 0, 4)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'606ea5f1-7aa2-4e01-bac1-c899da5f54d4', NULL, N'/content/uploaded/474f002c-7f43-483f-b16e-7fd13db7fe9a.jpg', 0, CAST(N'2017-08-22 00:06:19.357' AS DateTime), CAST(N'2017-08-21 21:06:19.363' AS DateTime), 0, 1017)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'1966a809-39c8-41f6-b0d1-d4957a7695cc', NULL, N'/content/uploaded/726de414-128b-493b-adbb-be095c6f1a17.jpg', 0, CAST(N'2017-08-22 03:11:46.013' AS DateTime), CAST(N'2017-08-22 00:11:46.023' AS DateTime), 0, 1043)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'97eee9fb-d884-41cd-aa40-d50cc43c88c1', NULL, N'/content/uploaded/c988e995-a374-4d5d-8895-6b22a7636895.jpg', 0, CAST(N'2017-07-29 02:39:03.790' AS DateTime), CAST(N'2017-07-28 23:39:03.797' AS DateTime), 0, 1008)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'34cc342c-f11e-48b2-bf5d-d51c25998820', NULL, N'/content/uploaded/c721d505-6cd2-4cd6-82f6-b6888d705ad7.jpg', 0, CAST(N'2017-08-22 01:19:01.713' AS DateTime), CAST(N'2017-08-21 22:19:01.720' AS DateTime), 0, 1040)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'0d60a0c7-73d0-4332-b65a-dc3033afb3a6', NULL, N'/content/uploaded/fd9f2563-75bf-4ccf-b4f9-6994ff78ecaa.jpg', 0, CAST(N'2017-08-22 00:11:01.680' AS DateTime), CAST(N'2017-08-21 21:11:01.690' AS DateTime), 0, 1024)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'ce545146-c9da-4518-9b3d-ddeb95bd1e5e', NULL, N'/content/uploaded/d6e78eb4-f89d-4f27-b775-c7a03b2c1b15.jpg', 0, CAST(N'2017-08-22 03:12:01.463' AS DateTime), CAST(N'2017-08-22 00:12:01.477' AS DateTime), 0, 1045)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'f3477047-6d9c-4598-afb7-e16abfb74c96', NULL, N'/content/uploaded/33beec8e-6098-4a91-a8fd-82b8f1206fb5.jpg', 0, CAST(N'2017-08-22 00:13:39.757' AS DateTime), CAST(N'2017-08-21 21:13:39.763' AS DateTime), 0, 1028)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'20d762a9-d3eb-4d6c-8d8d-e5f44143eb1c', NULL, N'/content/uploaded/63b4a309-42d4-493b-a88a-bd34390c9d8b.jpg', 0, CAST(N'2017-08-22 00:14:45.697' AS DateTime), CAST(N'2017-08-21 21:14:45.703' AS DateTime), 0, 1029)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'8b59fbff-7329-42ff-a8a5-e6386ea037cc', NULL, N'/content/uploaded/b4f3d95f-1ffc-4a8c-9a52-3ebe9fbbca72.png', 0, CAST(N'2017-07-03 20:30:08.677' AS DateTime), CAST(N'2017-07-03 17:30:08.683' AS DateTime), 0, 3)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'ad12b86c-0ecc-41ef-8dfb-e91dcb29bca3', NULL, N'/content/uploaded/fa99c2e8-185b-479b-81a0-5b2ee1ed8837.jpg', 0, CAST(N'2017-07-29 02:38:31.783' AS DateTime), CAST(N'2017-07-28 23:38:31.810' AS DateTime), 0, 1007)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'b6943f61-7412-4f59-a7bf-ee0acc848b20', NULL, N'/content/uploaded/0c82cb71-f3c4-443c-8a6a-f45f63d65ab0.jpg', 0, CAST(N'2017-08-22 03:12:52.653' AS DateTime), CAST(N'2017-08-22 00:12:52.660' AS DateTime), 0, 1053)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'578c1a22-47eb-4305-a71e-f2b4c244c1ef', NULL, N'/content/uploaded/e0a5ba11-2a30-46d9-98df-336391b958a5.jpg', 0, CAST(N'2017-07-22 15:07:20.230' AS DateTime), CAST(N'2017-07-22 12:07:20.250' AS DateTime), 0, 7)
INSERT [dbo].[Media] ([Id], [Title], [RelativePath], [Status], [CreationDate], [LastModified], [Type], [PublicId]) VALUES (N'3cb284cf-7fda-481b-91cd-f40c5a5041df', NULL, N'/content/uploaded/08f5f7dc-2665-498b-bac3-c58606393f62.jpg', 0, CAST(N'2017-08-22 01:14:34.610' AS DateTime), CAST(N'2017-08-21 22:14:34.627' AS DateTime), 0, 1035)
SET IDENTITY_INSERT [dbo].[Media] OFF
SET IDENTITY_INSERT [dbo].[MenuItem] ON 

INSERT [dbo].[MenuItem] ([Id], [CreationDate], [IsActive], [Title], [EntityName], [Url], [DisplayOrder], [IsMega], [IncludeInHeader], [IncludeInFooter], [PublicId], [Status], [ParentId], [EntityId], [WidgetId]) VALUES (N'd112dc69-ba3e-4d95-a7e2-0005997e778a', CAST(N'2017-08-22 03:26:16.707' AS DateTime), 1, N'Work', N'None', N'#ourwork', 4, 0, 0, 0, 9, 0, NULL, NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [CreationDate], [IsActive], [Title], [EntityName], [Url], [DisplayOrder], [IsMega], [IncludeInHeader], [IncludeInFooter], [PublicId], [Status], [ParentId], [EntityId], [WidgetId]) VALUES (N'd1835c65-2726-4b63-9d62-1650e5ab4865', CAST(N'2017-08-22 03:23:44.680' AS DateTime), 1, N'Services', N'None', N'#services', 3, 0, 0, 0, 8, 0, NULL, NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [CreationDate], [IsActive], [Title], [EntityName], [Url], [DisplayOrder], [IsMega], [IncludeInHeader], [IncludeInFooter], [PublicId], [Status], [ParentId], [EntityId], [WidgetId]) VALUES (N'a28860fc-d3e6-4077-9a56-3ce82bc7303a', CAST(N'2017-08-22 03:26:40.173' AS DateTime), 1, N'Contact', N'None', N'#getintouch', 5, 0, 0, 0, 10, 0, NULL, NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [CreationDate], [IsActive], [Title], [EntityName], [Url], [DisplayOrder], [IsMega], [IncludeInHeader], [IncludeInFooter], [PublicId], [Status], [ParentId], [EntityId], [WidgetId]) VALUES (N'd4f05b63-f07b-40d9-9c82-3e01dfec0227', CAST(N'2017-08-07 23:01:12.850' AS DateTime), 1, N'About us', N'CustomLink', N'#aboutus', 2, 0, 1, 1, 7, 0, NULL, NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [CreationDate], [IsActive], [Title], [EntityName], [Url], [DisplayOrder], [IsMega], [IncludeInHeader], [IncludeInFooter], [PublicId], [Status], [ParentId], [EntityId], [WidgetId]) VALUES (N'b5320b76-03d8-4209-b46d-f0847c850e12', CAST(N'2017-07-27 18:30:51.767' AS DateTime), 1, N'Home', N'Post', N'#top', 0, 0, 1, 1, 2, 0, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[MenuItem] OFF
SET IDENTITY_INSERT [dbo].[Post] ON 

INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'9196bbbd-b2ec-44c3-a89c-0bd4e26c0df7', CAST(N'2017-08-08 00:54:25.657' AS DateTime), N'IDENTITY MANAGEMENT', 20, NULL, 0, NULL, NULL, N'[]', 1020, N'6142d69b-ed57-4ebd-9e66-66010c6ee0e8', NULL, NULL, NULL, N'[]', NULL, NULL, CAST(N'2017-08-07 21:54:25.643' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'fe038161-7b13-4516-b740-1bedb2fd67b1', CAST(N'2017-08-22 00:12:35.843' AS DateTime), N'Corys', 20, N'<p><span>Unlike others, we only select the most appropriate channels for our client&rsquo;s campaigns and we ensure all campaigns can be integrated into a cohesive and engaging approach.</span><br /><br /><span>A key benefit of digital campaigns is being able to track and report, and tweak as necessary to optimize results. With the assistance of our Google Analytics <g class="gr_ gr_18 gr-alert gr_gramm gr_inline_cards gr_run_anim Punctuation only-ins replaceWithoutSep" id="18" data-gr-id="18">experts</g> we were able to cater <g class="gr_ gr_17 gr-alert gr_gramm gr_inline_cards gr_run_anim Grammar multiReplace" id="17" data-gr-id="17">Corys</g> dynamics need, monitor the campaign&rsquo;s ROI and get the best behind each campaign.</span><br /><br /><span>Our responsibilities included:</span><br /><span>o Creation of digital campaign</span><br /><span>o Tracking and report campaign results</span><br /><span>o Monitor ROIs</span></p>', 0, NULL, NULL, N'[{"Key":"SubTitle","Value":"CORYS DIGITAL ACTIVITY"}]', 1026, N'6a4c0552-f8a2-4c9d-b61b-b5a9d07cf76f', NULL, N'/content/uploaded/eb73b029-df7b-48b2-bdcb-6eba0404777f.jpg', NULL, N'[
  {
    "Id": "f04d1528-d346-47d5-b880-97f910451bde",
    "Path": "/content/uploaded/8e54a947-8727-4180-be88-34adb61a6714.jpg"
  }
]', NULL, NULL, CAST(N'2017-08-21 21:12:35.827' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'ab10f5c2-6365-48c6-9b8d-2096cac3a8d8', CAST(N'2017-08-22 00:17:48.780' AS DateTime), N'HMG', 20, N'<p>Whether it is an established company or a start-up, when creating a brand, we seek a profound understanding of what makes your company identity tick. We look at who you are, your goals, what you believe in and what message you want to communicate.</p>
<p>With these facts, we created HMG the brand that is right for their businesses.</p>
<p>Our responsibilities included:<br />o Stamp<br />o Logo<br />o Light box<br />o Signage - billboards<br />o Digital screen<br />o Trophy design</p>', 0, NULL, N'[{"Key":"Image1","Id":"1966a809-39c8-41f6-b0d1-d4957a7695cc","Value":"/content/uploaded/726de414-128b-493b-adbb-be095c6f1a17.jpg","Path":null,"PostAttachments":null},{"Key":"Image2","Id":"f56a24f4-edc0-4081-aca8-78c295bf7ee0","Value":"/content/uploaded/9966ec0c-4c7a-4d85-a098-1e669aaf0f0b.jpg","Path":null,"PostAttachments":null},{"Key":"Image3","Id":"ce545146-c9da-4518-9b3d-ddeb95bd1e5e","Value":"/content/uploaded/d6e78eb4-f89d-4f27-b775-c7a03b2c1b15.jpg","Path":null,"PostAttachments":null},{"Key":"Image4","Id":"b9c58048-bd41-4769-80ff-31706b273a0d","Value":"/content/uploaded/40d8fb35-a127-466e-932b-859d0596b5ad.jpg","Path":null,"PostAttachments":null},{"Key":"Image5","Id":"e1af26af-92b9-43e5-8174-41dfe5e98b55","Value":"/content/uploaded/f81120aa-2570-4e97-b40b-0af90fbc52e5.jpg","Path":null,"PostAttachments":null},{"Key":"Image6","Id":"9fff30a7-21a6-4a2a-881e-8b747313e626","Value":"/content/uploaded/a4617d20-fe67-4ce1-81f9-dbd3a4f61214.jpg","Path":null,"PostAttachments":null},{"Key":"Image7","Id":"5459e3f4-e8d6-43bc-bd87-3883a166eff1","Value":"/content/uploaded/4dd074f6-676f-4226-8084-6175a03e3aaf.jpg","Path":null,"PostAttachments":null},{"Key":"Image8","Id":"be9b7330-1eea-4413-8f4b-706bf5dbaeb0","Value":"/content/uploaded/7480cc84-a7a3-4dd3-86df-aa6001c7ec6c.jpg","Path":null,"PostAttachments":null},{"Key":"Image9","Id":"3a8afdaa-0c98-4223-b2b5-2b859537dc7f","Value":"/content/uploaded/6f806524-a291-4e63-9cd6-a3c2cc7911df.jpg","Path":null,"PostAttachments":null},{"Key":"Image10","Id":"4f999659-2b5f-435b-9203-7cf2f36b88e3","Value":"/content/uploaded/14e98067-1b8b-4f0a-9a3c-c92fbaa65834.jpg","Path":null,"PostAttachments":null},{"Key":"Image11","Id":"b6943f61-7412-4f59-a7bf-ee0acc848b20","Value":"/content/uploaded/0c82cb71-f3c4-443c-8a6a-f45f63d65ab0.jpg","Path":null,"PostAttachments":null},{"Key":"Image12","Id":"18aaa600-6396-4899-8e94-5d6a18999622","Value":"/content/uploaded/d6d3e90f-3ab9-483f-b814-c36124ee1955.jpg","Path":null,"PostAttachments":null}]', N'[{"Key":"SubTitle","Value":"BRANDING CREATION"}]', 1030, N'6a4c0552-f8a2-4c9d-b61b-b5a9d07cf76f', NULL, N'/content/uploaded/cbc36128-7a67-483d-864f-850515ba7491.jpg', NULL, N'[
  {
    "Path": "/content/uploaded/8db702e2-abe9-4312-92ce-9ed9b6f62771.jpg",
    "Title": null
  }
]', NULL, NULL, CAST(N'2017-08-22 00:13:39.583' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'ad72ec4b-6242-48cc-a2b8-2c9f0171f413', CAST(N'2017-08-22 00:15:47.390' AS DateTime), N'Mecidiye', 20, N'<p><span>Our work involves creating new brands for new businesses or product categories and also developing existing brands and evolving them over time. A strong brand identity takes critical thinking and creative work.</span><br /><br /><span>We built Mecidiye brands with integrity and substance, resulting in deeper and richer brand.</span><br /><span>The food and drink brands are always interesting, due to the very competitive nature of the business, <g class="gr_ gr_19 gr-alert gr_gramm gr_inline_cards gr_run_anim Punctuation only-ins replaceWithoutSep" id="19" data-gr-id="19">nevertheless</g> we managed to create Mecidiye brand guidelines that helped them define their communication position to the market from logo creation to labels, brochures, <g class="gr_ gr_20 gr-alert gr_gramm gr_inline_cards gr_run_anim Punctuation only-ins replaceWithoutSep" id="20" data-gr-id="20">flyers</g> and websites.</span><br /><br /><span>Our responsibilities included:</span><br /><span>o Logo creation</span><br /><span>o Label design</span><br /><span>o Brochures &amp; Flyers</span><br /><span>o Website design</span></p>', 0, NULL, NULL, N'[{"Key":"SubTitle","Value":"BRAND CREATION"}]', 1028, N'6a4c0552-f8a2-4c9d-b61b-b5a9d07cf76f', NULL, N'/content/uploaded/63b4a309-42d4-493b-a88a-bd34390c9d8b.jpg', NULL, N'[
  {
    "Id": "680abfb1-7f5d-4a39-96ee-2c729c65dfd7",
    "Path": "/content/uploaded/952498eb-0e63-4068-9828-500e806e2ffc.jpg"
  }
]', NULL, NULL, CAST(N'2017-08-21 21:15:47.380' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'3f4b1dac-3730-4a3b-bfec-2ca91c7a1c8f', CAST(N'2017-08-22 00:16:46.950' AS DateTime), N'Nesa', 20, N'<p><span>We pride ourselves on always producing <g class="gr_ gr_12 gr-alert gr_spell gr_inline_cards gr_run_anim ContextualSpelling ins-del multiReplace" id="12" data-gr-id="12">high quality</g>, creative work. We set ourselves high standards and ensure all output is on-brief and on-brand.</span><br /><br /><span>We created for DAMAC a range of brochures and flyers, to promote this company that has for many years strived to provide dream homes and unique living concepts to customers from all over the world.</span><br /><br /><span>Our responsibilities included:</span><br /><span>o Brochures</span><br /><span>o Flyers</span></p>', 0, NULL, NULL, N'[{"Key":"SubTitle","Value":"CREATIVE SERVICES"}]', 1029, N'6a4c0552-f8a2-4c9d-b61b-b5a9d07cf76f', NULL, N'/content/uploaded/b2fdee30-2363-45ce-90e2-dae8f2bf9601.jpg', NULL, N'[
  {
    "Id": "540e2bd1-d8b1-47e0-a756-a571c62e5859",
    "Path": "/content/uploaded/7ad75799-1282-44d3-b67b-6b339bc5030f.jpg"
  }
]', NULL, NULL, CAST(N'2017-08-21 21:16:46.940' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'86bca7c3-7f4a-4dcf-a39d-2d6b7629c934', CAST(N'2017-07-03 20:13:39.407' AS DateTime), N'Home', 20, NULL, 0, NULL, NULL, NULL, 3, N'0004aef4-1380-40e1-bdbf-5c6dd6e7bddf', NULL, N'0', NULL, N'[]', N'{"rows":[{"classname":"","cols":[{"classname":"","lg":"12","text":"c2","rows":[],"widgets":[{"title":"main banner","widgetid":"497d5c86-ba95-426f-bfe8-3982fc0de330"},{"title":"aboutus","widgetid":"2724569b-8b40-4e77-b179-4bba75978d6b"},{"title":"Our Services","widgetid":"1c8c8ebd-8cf2-46bc-867d-a64a2ed9f23a"},{"title":"ourwork","widgetid":"41f9251a-72bd-438b-8931-0ca54aa23cc9"},{"title":"ourclients","widgetid":"d1e5d4b0-7bd3-4b97-af9e-53b755c5c39a"},{"title":"funfacts","widgetid":"64cd46e1-df05-4c6d-8db0-e5b67b6d5726"},{"widgetid":"1a821477-9fbe-4d3e-9519-667aab8439d4","title":"getintouch"}]}]}]}', NULL, CAST(N'2017-08-22 00:00:49.130' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'abd4490f-8874-4878-8ac1-2f7177c43234', CAST(N'2017-08-22 00:07:07.583' AS DateTime), N'QB', 20, N'<p><span>Creating a brand is not a project to be undertaken lightly. Whilst it can be challenging work, the benefits can be substantial, as a brand can encourage customer loyalty and reinforce differentiation from competitors.&nbsp;</span><br /><span>We have been involved in the brand creation and development for many businesses and products. For QB, our role was to learn and communicate the values of the construction business to their customers.<span>&nbsp;</span></span><br /><br /><span>Our responsibilities included, (or we delivered):</span><br /><span>o Concept design</span><br /><span>o Logo creating</span><br /><span>o Branding &amp; guidelines creation</span><br /><span>o Website</span><br /><span>o Corporate Profile</span></p>', 0, NULL, NULL, N'[]', 1022, N'6a4c0552-f8a2-4c9d-b61b-b5a9d07cf76f', NULL, N'/content/uploaded/9dec9190-8151-4374-b720-1cf12e699b12.jpg', NULL, N'[
  {
    "Id": "606ea5f1-7aa2-4e01-bac1-c899da5f54d4",
    "Path": "/content/uploaded/474f002c-7f43-483f-b16e-7fd13db7fe9a.jpg"
  }
]', NULL, NULL, CAST(N'2017-08-21 21:07:07.570' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'a404315c-464c-4544-9451-470e21bd1b7a', CAST(N'2017-08-08 00:53:54.847' AS DateTime), N'TRADING IDEAS', 20, NULL, 0, NULL, NULL, N'[]', 1018, N'6142d69b-ed57-4ebd-9e66-66010c6ee0e8', NULL, NULL, NULL, N'[]', NULL, NULL, CAST(N'2017-08-07 21:53:54.837' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'6419f2ff-12d6-48d1-87b3-486539e52db2', CAST(N'2017-08-22 00:09:54.653' AS DateTime), N'Exceed', 20, N'<p><span>Product innovation is not just about being new or being different. It&rsquo;s about customizing new products that customers will love, creating a unique path consumers will want to follow.</span><br /><span>That&rsquo;s where our expertise in product innovation comes into its own. Providing our clients an edge.</span><br /><br /><span>With our global view, we succeeded in expanding Exceed&rsquo;s creative horizons giving them new perspective by putting innovation in context.</span><br /><br /><span>Our responsibilities included (or we delivered)</span><br /><span>o Interactive digital boxes</span><br /><span>o Exceptional trophies</span><br /><span>o Creative backdrops</span><br /><span>o <g class="gr_ gr_21 gr-alert gr_spell gr_inline_cards gr_run_anim ContextualSpelling ins-del multiReplace" id="21" data-gr-id="21">High end</g> company brochures design &amp; printing</span><br /><span>o VIP customized gift-items</span></p>', 0, NULL, NULL, N'[]', 1024, N'6a4c0552-f8a2-4c9d-b61b-b5a9d07cf76f', NULL, N'/content/uploaded/8c1e23e3-af50-4d2b-901e-9eb7f036f2a9.jpg', NULL, N'[
  {
    "Path": "/content/uploaded/f376b89c-ec8f-4fec-9633-22502fc2c6b2.jpg",
    "Title": null
  }
]', NULL, NULL, CAST(N'2017-08-21 21:09:57.683' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'bae208b0-a72d-4d88-8946-49a1f73be4cd', CAST(N'2017-08-07 23:09:23.273' AS DateTime), N'Main banner', 20, N'<div class="et_pb_text et_pb_module et_pb_bg_layout_light et_pb_text_align_left  et_pb_text_0" style="position: absolute; width: 100%;">
<h1 style="font-size: 8em;" class="">HOT&amp;</h1>
<h1 style="font-size: 8em; padding-right: 40px;">CRISPY</h1>
<h1 style="font-size: 8em;">IDEAS</h1>
<h2 style="font-size: 3em; padding-right: 40px;">SERVED</h2>
</div>', 0, NULL, NULL, N'[]', 1014, N'caa69a2e-dc5b-4906-a0ed-06f8eafaf2ab', NULL, N'/content/uploaded/741b0142-47d7-4efd-89c7-dc62a737bcad.png', NULL, NULL, NULL, NULL, CAST(N'2017-08-21 20:28:44.910' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'201daf17-b2fb-4d31-a1b6-7ca1e0f0e136', CAST(N'2017-08-07 23:28:40.050' AS DateTime), N's1', -100, NULL, 0, NULL, NULL, N'[]', 1015, N'6142d69b-ed57-4ebd-9e66-66010c6ee0e8', NULL, N'/content/uploaded/f7b851d1-05d0-4378-b46f-194be5ece874.jpg', NULL, N'[]', NULL, NULL, CAST(N'2017-08-07 20:28:46.033' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'29c10219-6f2e-4af5-a336-7cf9c94fe2a8', CAST(N'2017-08-22 02:34:16.587' AS DateTime), N'SLEEPLESS NIGHTS', 20, NULL, 0, NULL, N'[]', N'[{"Key":"Count","Value":"506"}]', 1034, N'c7f37179-0dc1-4d8b-93e9-898e02985c23', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-08-21 23:34:16.577' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'574cab82-edb9-4d0f-8c13-842204a636fe', CAST(N'2017-08-08 00:53:30.077' AS DateTime), N'ADVERTISING ESSENTIALS', 20, NULL, 0, NULL, NULL, N'[]', 1016, N'6142d69b-ed57-4ebd-9e66-66010c6ee0e8', NULL, NULL, NULL, N'[]', NULL, NULL, CAST(N'2017-08-07 21:53:30.067' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'30bb570b-348a-45ef-a9da-8847ed011d1b', CAST(N'2017-08-08 00:54:44.310' AS DateTime), N' INTERNAL COMMUNICATION', 20, NULL, 0, NULL, NULL, N'[]', 1021, N'6142d69b-ed57-4ebd-9e66-66010c6ee0e8', NULL, NULL, NULL, N'[]', NULL, NULL, CAST(N'2017-08-07 21:54:44.300' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'022c61f0-2d7f-4158-b6ec-88c7dc3458f9', CAST(N'2017-08-22 02:11:48.387' AS DateTime), N'client1', 20, NULL, 0, NULL, N'[]', N'[]', 1031, N'da793770-7a47-4d06-80b6-4df908cd23d7', NULL, N'/content/uploaded/84986ef0-7113-4b80-8b89-54e9e5a0696d.jpg', NULL, NULL, NULL, NULL, CAST(N'2017-08-21 23:11:48.363' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'ef59006c-8080-4a76-b0d6-89a44385c5f2', CAST(N'2017-08-08 00:54:10.057' AS DateTime), N'DESIGN MATERIALS', 20, NULL, 0, NULL, NULL, N'[]', 1019, N'6142d69b-ed57-4ebd-9e66-66010c6ee0e8', NULL, NULL, NULL, N'[]', NULL, NULL, CAST(N'2017-08-07 21:54:10.043' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'b89ef757-e996-41db-ad43-95031f45c76c', CAST(N'2017-08-22 00:08:50.967' AS DateTime), N'MRS', 20, N'<p><span>We deliver creative event and brand experiences while unlocking a little magic along the way; a fully integrated event management.</span><br /><span>With our 360-degree event <g class="gr_ gr_13 gr-alert gr_gramm gr_inline_cards gr_run_anim Punctuation only-ins replaceWithoutSep" id="13" data-gr-id="13">management</g> we delivered MRS an insightful and creative event resulting in experiential solutions.</span><br /><br /><span>Our responsibilities included (or we delivered):</span><br /><span>&middot; pre-event &amp; on-site event flow management</span><br /><span>&middot; Branding</span><br /><span>&middot; Invitation Design</span><br /><span>&middot; Event photography and videography</span><br /><span>&middot; PR &amp; Social media services</span><br /><span>&middot; Hostesses booking and registration management</span></p>', 0, NULL, NULL, N'[]', 1023, N'6a4c0552-f8a2-4c9d-b61b-b5a9d07cf76f', NULL, N'/content/uploaded/21949e77-ef63-4070-8016-ec1a5791e96d.jpg', NULL, N'[
  {
    "Id": "868f035b-a749-4b5d-85e1-b775313a16d5",
    "Path": "/content/uploaded/b3d3a216-0d5c-4ec7-9e4d-84ce8a404767.jpg"
  }
]', NULL, NULL, CAST(N'2017-08-21 21:08:50.957' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'6934ea2b-a625-4096-a72c-95783f61798e', CAST(N'2017-08-08 00:53:44.543' AS DateTime), N'PRINTING REQUISITES', 20, NULL, 0, NULL, NULL, N'[]', 1017, N'6142d69b-ed57-4ebd-9e66-66010c6ee0e8', NULL, NULL, NULL, N'[]', NULL, NULL, CAST(N'2017-08-07 21:53:44.533' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'0a811992-5d8e-41ca-9a03-a4a5e6eedb95', CAST(N'2017-08-22 00:13:43.917' AS DateTime), N'Kitty Paw', 20, N'<p><span>Product innovation is not just about being new or being different. It&rsquo;s about customizing new products that customers will love, creating a unique path consumers will want to follow.</span><br /><span>That&rsquo;s where our expertise in product innovation comes into its own. Providing our clients an edge that stands out from their competitors.</span><br /><br /><span>It''s our job to create or evolve a brand that represents the uniqueness of our clients'' products or services and that was accomplished by creating the Kitty Paw product, reinforcing the ''feeling'' they wish their customer to have as a result of buying from them.</span><br /><br /><span>Our responsibilities included:</span><br /><span>o Creating a customized solution</span><br /><span>o Creating an innovative product &ndash; <g class="gr_ gr_19 gr-alert gr_spell gr_inline_cards gr_run_anim ContextualSpelling ins-del" id="19" data-gr-id="19">kitty</g> Paw</span><br /><span>o Product patent process &amp; approvals</span><br /><span>o Designing and manufacturing the product</span></p>', 0, NULL, NULL, N'[{"Key":"SubTitle","Value":"KITTY PAW INNOVATIVE PRODUCTS"}]', 1027, N'6a4c0552-f8a2-4c9d-b61b-b5a9d07cf76f', NULL, N'/content/uploaded/1ef22207-1de6-4922-8686-5f696e4cbc2d.jpg', NULL, N'[
  {
    "Id": "f3477047-6d9c-4598-afb7-e16abfb74c96",
    "Path": "/content/uploaded/33beec8e-6098-4a91-a8fd-82b8f1206fb5.jpg"
  }
]', NULL, NULL, CAST(N'2017-08-21 21:13:43.910' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'ca947ff7-4e46-4671-ac1d-aae6da8bc46d', CAST(N'2017-08-22 02:34:37.873' AS DateTime), N'CUPS OF COFFEE', 20, NULL, 0, NULL, N'[]', N'[{"Key":"Count","Value":"1603"}]', 1035, N'c7f37179-0dc1-4d8b-93e9-898e02985c23', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-08-21 23:34:37.863' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'3cb496cf-9925-4324-9b4d-b2c3ad1301aa', CAST(N'2017-08-22 00:11:10.173' AS DateTime), N'Aptamil', 20, N'<p><span>We pride ourselves on always producing <g class="gr_ gr_15 gr-alert gr_spell gr_inline_cards gr_run_anim ContextualSpelling ins-del multiReplace" id="15" data-gr-id="15">high quality</g>, creative work. We set ourselves high standards and ensure all output is on-brief and on-brand.</span><br /><br /><span>We worked with Danon to understand the essence of their Aptamil brand, resulting in creative communication material that lifted their business to the next level. We created a campaign while focusing on building the association between Aptamil Character and baby&rsquo;s nutrition needs</span><br /><br /><span>Our responsibilities included:</span><br /><span>o Designing creative kids&rsquo; giveaways</span><br /><span>o Boosting kids&rsquo; interaction</span><br /><span>o Design and brochures printing</span></p>', 0, NULL, NULL, N'[]', 1025, N'6a4c0552-f8a2-4c9d-b61b-b5a9d07cf76f', NULL, N'/content/uploaded/0cf584ad-7a7a-428e-8170-665f559f2c6b.jpg', NULL, N'[
  {
    "Id": "0d60a0c7-73d0-4332-b65a-dc3033afb3a6",
    "Path": "/content/uploaded/fd9f2563-75bf-4ccf-b4f9-6994ff78ecaa.jpg"
  }
]', NULL, NULL, CAST(N'2017-08-21 21:11:10.163' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'ff880364-7b78-47fe-ae85-d1d6524f22cd', CAST(N'2017-08-22 02:34:01.600' AS DateTime), N'INNOVATIVE CONCEPTS', 20, NULL, 0, NULL, N'[]', N'[{"Key":"Count","Value":"198"}]', 1033, N'c7f37179-0dc1-4d8b-93e9-898e02985c23', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-08-21 23:34:01.590' AS DateTime), NULL)
INSERT [dbo].[Post] ([Id], [CreationDate], [Title], [Status], [Detail], [IsInitial], [ViewPath], [Attachments], [PostMetaValues], [PublicId], [PostTypeId], [ParentId], [Photo], [Author], [Gallery], [Widgets], [IsActive], [PublishedDate], [UrlKey]) VALUES (N'f32e3444-8bd7-441d-bd13-fa3336faf196', CAST(N'2017-08-22 02:33:23.597' AS DateTime), N'Happy Clients', 20, NULL, 0, NULL, N'[]', N'[{"Key":"Count","Value":"106"}]', 1032, N'c7f37179-0dc1-4d8b-93e9-898e02985c23', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-08-21 23:33:49.590' AS DateTime), NULL)
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

INSERT [dbo].[PostType] ([Id], [Title], [UrlKey], [ViewPath], [DisplayOrder], [EnableFeatureImage], [EnableTags], [EnableDescription], [EnableSummary], [EnableDateChoose], [EnableGallery], [CreationDate], [EnableCategories], [EnableWidgets], [EnableViewPath], [IsActive], [Icon], [IsSystem], [Status], [PublicId], [TermViewPath], [PostMetaFields], [PostMediaList], [TermTaxonomyList]) VALUES (N'caa69a2e-dc5b-4906-a0ed-06f8eafaf2ab', N'Banner', N'banner', NULL, 0, 1, 0, 1, 0, NULL, 0, CAST(N'2017-08-07 23:08:52.187' AS DateTime), 0, 0, 0, 1, NULL, 0, 0, 5, NULL, N'[]', N'[]', N'[]')
INSERT [dbo].[PostType] ([Id], [Title], [UrlKey], [ViewPath], [DisplayOrder], [EnableFeatureImage], [EnableTags], [EnableDescription], [EnableSummary], [EnableDateChoose], [EnableGallery], [CreationDate], [EnableCategories], [EnableWidgets], [EnableViewPath], [IsActive], [Icon], [IsSystem], [Status], [PublicId], [TermViewPath], [PostMetaFields], [PostMediaList], [TermTaxonomyList]) VALUES (N'da793770-7a47-4d06-80b6-4df908cd23d7', N'Clients', N'clients', N'clients', 0, 1, 0, 0, 0, NULL, 0, CAST(N'2017-08-22 02:09:47.167' AS DateTime), 0, 0, 0, 1, NULL, 0, 0, 7, NULL, N'[]', N'[]', N'[]')
INSERT [dbo].[PostType] ([Id], [Title], [UrlKey], [ViewPath], [DisplayOrder], [EnableFeatureImage], [EnableTags], [EnableDescription], [EnableSummary], [EnableDateChoose], [EnableGallery], [CreationDate], [EnableCategories], [EnableWidgets], [EnableViewPath], [IsActive], [Icon], [IsSystem], [Status], [PublicId], [TermViewPath], [PostMetaFields], [PostMediaList], [TermTaxonomyList]) VALUES (N'0004aef4-1380-40e1-bdbf-5c6dd6e7bddf', N'Pages', N'page', N'', 1, 1, 0, 1, 1, 1, 1, CAST(N'2017-10-10 00:00:00.000' AS DateTime), 0, 1, 1, 1, N'tag', 0, 0, 1, N'page', NULL, NULL, NULL)
INSERT [dbo].[PostType] ([Id], [Title], [UrlKey], [ViewPath], [DisplayOrder], [EnableFeatureImage], [EnableTags], [EnableDescription], [EnableSummary], [EnableDateChoose], [EnableGallery], [CreationDate], [EnableCategories], [EnableWidgets], [EnableViewPath], [IsActive], [Icon], [IsSystem], [Status], [PublicId], [TermViewPath], [PostMetaFields], [PostMediaList], [TermTaxonomyList]) VALUES (N'6142d69b-ed57-4ebd-9e66-66010c6ee0e8', N'Services', N'services', NULL, 0, 1, 0, 1, 1, NULL, 1, CAST(N'2017-08-07 23:04:30.067' AS DateTime), 0, 0, 0, 1, N'pencil', 0, 0, 4, NULL, N'[]', N'[]', N'[]')
INSERT [dbo].[PostType] ([Id], [Title], [UrlKey], [ViewPath], [DisplayOrder], [EnableFeatureImage], [EnableTags], [EnableDescription], [EnableSummary], [EnableDateChoose], [EnableGallery], [CreationDate], [EnableCategories], [EnableWidgets], [EnableViewPath], [IsActive], [Icon], [IsSystem], [Status], [PublicId], [TermViewPath], [PostMetaFields], [PostMediaList], [TermTaxonomyList]) VALUES (N'c7f37179-0dc1-4d8b-93e9-898e02985c23', N'funfacts', N'funfacts', N'funfacts', 0, 0, 0, 0, 0, NULL, 0, CAST(N'2017-08-22 02:21:09.540' AS DateTime), 0, 0, 0, 1, NULL, 0, 0, 8, NULL, N'[]', N'[]', N'[]')
INSERT [dbo].[PostType] ([Id], [Title], [UrlKey], [ViewPath], [DisplayOrder], [EnableFeatureImage], [EnableTags], [EnableDescription], [EnableSummary], [EnableDateChoose], [EnableGallery], [CreationDate], [EnableCategories], [EnableWidgets], [EnableViewPath], [IsActive], [Icon], [IsSystem], [Status], [PublicId], [TermViewPath], [PostMetaFields], [PostMediaList], [TermTaxonomyList]) VALUES (N'6a4c0552-f8a2-4c9d-b61b-b5a9d07cf76f', N'Work', N'work', N'WorkDetails', 0, 1, 0, 1, 0, NULL, 1, CAST(N'2017-08-21 23:58:30.570' AS DateTime), 0, 0, 0, 1, N'wrench', 0, 0, 6, NULL, N'[{"key":"SubTitle","type":"text"}]', N'[{"key":"Image1","type":"image"},{"key":"Image2","type":"image"},{"key":"Image3","type":"image"},{"key":"Image4","type":"image"},{"key":"Image5","type":"image"},{"key":"Image6","type":"image"},{"key":"Image7","type":"image"},{"key":"Image8","type":"image"},{"key":"Image9","type":"image"},{"key":"Image10","type":"image"},{"key":"Image11","type":"image"},{"key":"Image12","type":"image"}]', N'[]')
SET IDENTITY_INSERT [dbo].[PostType] OFF
SET IDENTITY_INSERT [dbo].[Setting] ON 

INSERT [dbo].[Setting] ([Id], [Name], [Value], [Status], [CreationDate], [PublicId], [Type]) VALUES (N'827c584b-1956-4cd9-b39a-1187fb6c286b', N'ThemeConfig', N'{"ThemeName":"Communi8"}', 10, CAST(N'2017-10-10 00:00:00.000' AS DateTime), 1, 10)
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

INSERT [dbo].[Widget] ([Id], [Title], [CreationDate], [SourceCategories], [SourceTags], [SourcePosts], [IsActive], [ViewPath], [PostCount], [PublicId], [Status], [ReturnTags], [ReturnCategories], [ReturnPosts], [PostType], [Tags], [Categories], [Posts]) VALUES (N'41f9251a-72bd-438b-8931-0ca54aa23cc9', N'ourwork', CAST(N'2017-08-08 00:52:35.213' AS DateTime), NULL, NULL, NULL, 1, N'ourwork', 9, 3, 0, 0, 0, 1, N'6a4c0552-f8a2-4c9d-b61b-b5a9d07cf76f', N'', N'', NULL)
INSERT [dbo].[Widget] ([Id], [Title], [CreationDate], [SourceCategories], [SourceTags], [SourcePosts], [IsActive], [ViewPath], [PostCount], [PublicId], [Status], [ReturnTags], [ReturnCategories], [ReturnPosts], [PostType], [Tags], [Categories], [Posts]) VALUES (N'497d5c86-ba95-426f-bfe8-3982fc0de330', N'main banner', CAST(N'2017-08-07 23:07:54.340' AS DateTime), NULL, NULL, NULL, 1, N'mainbanner', 0, 1, 0, 0, 0, 1, N'', N'', N'', N'bae208b0-a72d-4d88-8946-49a1f73be4cd')
INSERT [dbo].[Widget] ([Id], [Title], [CreationDate], [SourceCategories], [SourceTags], [SourcePosts], [IsActive], [ViewPath], [PostCount], [PublicId], [Status], [ReturnTags], [ReturnCategories], [ReturnPosts], [PostType], [Tags], [Categories], [Posts]) VALUES (N'2724569b-8b40-4e77-b179-4bba75978d6b', N'aboutus', CAST(N'2017-08-08 00:36:55.053' AS DateTime), NULL, NULL, NULL, 1, N'aboutus', 0, 2, 0, 0, 0, 1, N'', N'', N'', NULL)
INSERT [dbo].[Widget] ([Id], [Title], [CreationDate], [SourceCategories], [SourceTags], [SourcePosts], [IsActive], [ViewPath], [PostCount], [PublicId], [Status], [ReturnTags], [ReturnCategories], [ReturnPosts], [PostType], [Tags], [Categories], [Posts]) VALUES (N'd1e5d4b0-7bd3-4b97-af9e-53b755c5c39a', N'ourclients', CAST(N'2017-08-22 02:12:17.103' AS DateTime), NULL, NULL, NULL, 1, N'clients', 10, 5, 0, 0, 0, 1, N'da793770-7a47-4d06-80b6-4df908cd23d7', N'', N'', NULL)
INSERT [dbo].[Widget] ([Id], [Title], [CreationDate], [SourceCategories], [SourceTags], [SourcePosts], [IsActive], [ViewPath], [PostCount], [PublicId], [Status], [ReturnTags], [ReturnCategories], [ReturnPosts], [PostType], [Tags], [Categories], [Posts]) VALUES (N'1a821477-9fbe-4d3e-9519-667aab8439d4', N'getintouch', CAST(N'2017-08-22 03:00:28.413' AS DateTime), NULL, NULL, NULL, 1, N'getintouch', 0, 7, 0, 0, 0, 0, N'', N'', N'', NULL)
INSERT [dbo].[Widget] ([Id], [Title], [CreationDate], [SourceCategories], [SourceTags], [SourcePosts], [IsActive], [ViewPath], [PostCount], [PublicId], [Status], [ReturnTags], [ReturnCategories], [ReturnPosts], [PostType], [Tags], [Categories], [Posts]) VALUES (N'1c8c8ebd-8cf2-46bc-867d-a64a2ed9f23a', N'Our Services', CAST(N'2017-08-08 00:57:04.130' AS DateTime), NULL, NULL, NULL, 1, N'ourservices', 10, 4, 0, 0, 0, 1, N'6142d69b-ed57-4ebd-9e66-66010c6ee0e8', N'', N'', NULL)
INSERT [dbo].[Widget] ([Id], [Title], [CreationDate], [SourceCategories], [SourceTags], [SourcePosts], [IsActive], [ViewPath], [PostCount], [PublicId], [Status], [ReturnTags], [ReturnCategories], [ReturnPosts], [PostType], [Tags], [Categories], [Posts]) VALUES (N'64cd46e1-df05-4c6d-8db0-e5b67b6d5726', N'funfacts', CAST(N'2017-08-22 02:21:43.657' AS DateTime), NULL, NULL, NULL, 1, N'funfacts', 10, 6, 0, 0, 0, 1, N'c7f37179-0dc1-4d8b-93e9-898e02985c23', N'', N'', NULL)
SET IDENTITY_INSERT [dbo].[Widget] OFF
USE [master]
GO
ALTER DATABASE [Communi8DB] SET  READ_WRITE 
GO
