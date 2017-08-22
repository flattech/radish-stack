CREATE TABLE [dbo].[Setting] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Name]         NVARCHAR (200)   NOT NULL,
    [Value]        NVARCHAR (2000)  NOT NULL,
    [Status]       INT              NOT NULL,
    [CreationDate] DATETIME         CONSTRAINT [DF_Config_CreationDate] DEFAULT (getutcdate()) NOT NULL,
    [PublicId]     INT              IDENTITY (1, 1) NOT NULL,
    [Type]         INT              NOT NULL,
    CONSTRAINT [PK_Config] PRIMARY KEY CLUSTERED ([Id] ASC)
);

