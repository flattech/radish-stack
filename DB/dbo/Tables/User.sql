CREATE TABLE [dbo].[User] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [PublicId]     INT              IDENTITY (1, 1) NOT NULL,
    [CreationDate] DATETIME         NOT NULL,
    [Name]         NVARCHAR (255)   NOT NULL,
    [Username]     NVARCHAR (255)   NOT NULL,
    [Email]        NVARCHAR (255)   NOT NULL,
    [Role]         INT              NOT NULL,
    [Status]       INT              NOT NULL,
    [PasswordHash] VARCHAR (256)    NOT NULL,
    [PasswordSalt] VARCHAR (256)    NOT NULL,
    [Photo]        VARCHAR (256)    NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

