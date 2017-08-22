CREATE TABLE [dbo].[MenuItem] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [CreationDate]    DATETIME         NOT NULL,
    [IsActive]        BIT              NOT NULL,
    [Title]           NVARCHAR (255)   NOT NULL,
    [EntityName]      NVARCHAR (255)   NULL,
    [Url]             NVARCHAR (500)   NULL,
    [DisplayOrder]    INT              CONSTRAINT [DF_MenuItem_DisplayOrder] DEFAULT ((0)) NOT NULL,
    [IsMega]          BIT              CONSTRAINT [DF_MenuItem_IsMega] DEFAULT ((0)) NOT NULL,
    [IncludeInHeader] BIT              CONSTRAINT [DF_MenuItem_IncludeInHeader] DEFAULT ((1)) NOT NULL,
    [IncludeInFooter] BIT              CONSTRAINT [DF_MenuItem_IncludeInFooter] DEFAULT ((0)) NOT NULL,
    [PublicId]        INT              IDENTITY (1, 1) NOT NULL,
    [Status]          INT              NOT NULL,
    [ParentId]        UNIQUEIDENTIFIER NULL,
    [EntityId]        UNIQUEIDENTIFIER NULL,
    [WidgetId]        UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_MenuItem_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

