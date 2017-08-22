CREATE TABLE [dbo].[Widget] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Title]            NVARCHAR (255)   NOT NULL,
    [CreationDate]     DATETIME         NOT NULL,
    [SourceCategories] NVARCHAR (255)   NULL,
    [SourceTags]       NVARCHAR (255)   NULL,
    [SourcePosts]      NVARCHAR (255)   NULL,
    [IsActive]         BIT              NOT NULL,
    [ViewPath]         NVARCHAR (255)   NULL,
    [PostCount]        INT              NOT NULL,
    [PublicId]         INT              IDENTITY (1, 1) NOT NULL,
    [Status]           INT              NOT NULL,
    [ReturnTags]       BIT              NOT NULL,
    [ReturnCategories] BIT              NOT NULL,
    [ReturnPosts]      BIT              NOT NULL,
    [PostType]         NVARCHAR (255)   NULL,
    [Tags]             NVARCHAR (MAX)   NULL,
    [Categories]       NVARCHAR (MAX)   NULL,
    [Posts]            NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Widget] PRIMARY KEY CLUSTERED ([Id] ASC)
);

