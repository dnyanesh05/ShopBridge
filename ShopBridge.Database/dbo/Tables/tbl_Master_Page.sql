CREATE TABLE [dbo].[tbl_Master_Page] (
    [PageID]         INT            IDENTITY (1, 1) NOT NULL,
    [PageName]       NVARCHAR (100) NOT NULL,
    [AliasName]      NVARCHAR (100) NOT NULL,
    [DisplayOrder]   INT            NOT NULL,
    [IsActive]       BIT            CONSTRAINT [DF_tbl_Master_Page_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]      INT            NULL,
    [CreatedOn]      DATETIME       CONSTRAINT [DF_tbl_Master_Page_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]     INT            NULL,
    [ModifiedOn]     DATETIME       CONSTRAINT [DF_tbl_Master_Page_ModifiedOn] DEFAULT (getutcdate()) NOT NULL,
    [Isconfigurable] BIT            CONSTRAINT [DF_tbl_Master_Page_Isconfigurable] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tbl_Master_Page_PageID] PRIMARY KEY CLUSTERED ([PageID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_tbl_Master_Page_PageName]
    ON [dbo].[tbl_Master_Page]([PageName] ASC) WHERE ([IsActive]=(1));


