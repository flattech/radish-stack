CREATE TABLE [dbo].[Media] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Title]        NVARCHAR (255)   NULL,
    [RelativePath] NVARCHAR (255)   NOT NULL,
    [Status]       INT              NOT NULL,
    [CreationDate] DATETIME         CONSTRAINT [DF_Media_CreationDate] DEFAULT (getutcdate()) NOT NULL,
    [LastModified] DATETIME         CONSTRAINT [DF_Media_LastModified] DEFAULT (getutcdate()) NOT NULL,
    [Type]         INT              CONSTRAINT [DF__Media__Type__25869641] DEFAULT ((0)) NOT NULL,
    [PublicId]     INT              IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED ([Id] ASC)
);

