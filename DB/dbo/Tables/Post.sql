CREATE TABLE [dbo].[Post] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [CreationDate]   DATETIME         NOT NULL,
    [Title]          NVARCHAR (255)   NOT NULL,
    [Status]         INT              NOT NULL,
    [Detail]         NVARCHAR (MAX)   NULL,
    [IsInitial]      BIT              NOT NULL,
    [ViewPath]       NVARCHAR (255)   NULL,
    [Attachments]    NVARCHAR (MAX)   NULL,
    [PostMetaValues] NVARCHAR (255)   NULL,
    [PublicId]       INT              IDENTITY (1, 1) NOT NULL,
    [PostTypeId]     UNIQUEIDENTIFIER NOT NULL,
    [ParentId]       UNIQUEIDENTIFIER NULL,
    [Photo]          NVARCHAR (255)   NULL,
    [Author]         NVARCHAR (255)   NULL,
    [Gallery]        NVARCHAR (MAX)   NULL,
    [Widgets]        NVARCHAR (MAX)   NULL,
    [IsActive]       BIT              NULL,
    [PublishedDate]  DATETIME         NULL,
    [UrlKey]         NVARCHAR (255)   NULL,
    CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED ([Id] ASC)
);

