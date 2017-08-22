CREATE TABLE [dbo].[PostTerm] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [DisplayOrder] INT              NOT NULL,
    [CreationDate] DATETIME         NOT NULL,
    [PostId]       UNIQUEIDENTIFIER NOT NULL,
    [TermId]       UNIQUEIDENTIFIER NOT NULL,
    [Status]       INT              NOT NULL,
    CONSTRAINT [PK_PostTerm_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

