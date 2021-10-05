CREATE TABLE [dbo].[tbl_Master_Item_Category] (
    [ItemCategoryID] INT      IDENTITY (1, 1) NOT NULL,
    [CategoryName]              nvarchar(100)      NOT NULL,
    [CategoryDescription]     nvarchar(500)      NULL,
    [IsActive]            BIT      CONSTRAINT [DF_tbl_Master_Item_Category_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]           INT      NULL,
    [CreatedOn]           DATETIME CONSTRAINT [DF_tbl_Master_Item_Category_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]          INT      NULL,
    [ModifiedOn]          DATETIME CONSTRAINT [DF_tbl_Master_Item_Category_ModifiedOn] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_tbl_Master_Item_Category_ItemCategoryID] PRIMARY KEY CLUSTERED ([ItemCategoryID] ASC)
);

