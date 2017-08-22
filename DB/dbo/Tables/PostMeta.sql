CREATE TABLE [dbo].[PostMeta] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [PostId]       UNIQUEIDENTIFIER NOT NULL,
    [MetaKey]      NVARCHAR (255)   NOT NULL,
    [MetaValue]    NVARCHAR (500)   NOT NULL,
    [LastModified] DATETIME         NOT NULL,
    [CreationDate] DATETIME         NOT NULL,
    CONSTRAINT [PK_PostMeta] PRIMARY KEY CLUSTERED ([Id] ASC)
);

