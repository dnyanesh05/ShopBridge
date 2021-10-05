CREATE TABLE [dbo].[tbl_Master_Item]
(
	[ItemID] INT      IDENTITY (1, 1) NOT NULL, 
    [ItemName] NVARCHAR(100) NOT NULL, 
    [ItemDescription] NVARCHAR(500) NULL, 
    [ItemCategoryID] INT NULL, 
    [Price] FLOAT NULL, 
    [Size] FLOAT NULL, 
    [Color] NVARCHAR(50) NULL, 
    [IsItemAvailable] BIT NULL DEFAULT 1, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreatedBy] INT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GetUTCDate(), 
    [ModifiedBy] INT NULL, 
    [ModifiedOn] DATETIME NULL DEFAULT GetUTCDate(), 
    CONSTRAINT [PK_tbl_Master_Item] PRIMARY KEY ([ItemID]),

)
