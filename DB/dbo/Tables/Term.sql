CREATE TABLE [dbo].[Term] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [CreationDate]     DATETIME         NOT NULL,
    [Title]            NVARCHAR (255)   NOT NULL,
    [TaxonomyId]       INT              NOT NULL,
    [Status]           INT              NOT NULL,
    [IncludeInTopMenu] BIT              NOT NULL,
    [DisplayOrder]     INT              NOT NULL,
    [IsPublic]         BIT              NOT NULL,
    [PublicId]         INT              IDENTITY (1, 1) NOT NULL,
    [PostTypeId]       UNIQUEIDENTIFIER NOT NULL,
    [ParentId]         UNIQUEIDENTIFIER NULL,
    [IsActive]         BIT              NULL,
    [TaxonomyType]     NVARCHAR (255)   NULL,
    [UrlKey]           NVARCHAR (255)   NULL,
    CONSTRAINT [PK_Term] PRIMARY KEY CLUSTERED ([Id] ASC)
);

