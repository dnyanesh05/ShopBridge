CREATE TABLE [dbo].[tbl_Master_Role] (
    [RoleID]                 INT          IDENTITY (1, 1) NOT NULL,
    [RoleName]               VARCHAR (50) NOT NULL,
	[RoleCode]               VARCHAR (50) NOT NULL,
    [IsActive]               BIT          CONSTRAINT [DF_tbl_Master_Role_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]              INT          NULL,
    [CreatedOn]              DATETIME     CONSTRAINT [DF_tbl_Master_Role_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]             INT          NULL,
    [ModifiedOn]             DATETIME     CONSTRAINT [DF_tbl_Master_Role_ModifiedOn] DEFAULT (getutcdate()) NOT NULL, 
    CONSTRAINT [PK_tbl_Master_Role_RoleID] PRIMARY KEY CLUSTERED ([RoleID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_tbl_Master_Role_RoleCode]
    ON [dbo].[tbl_Master_Role]([RoleCode] ASC) WHERE ([IsActive]=(1));


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_tbl_Master_Role_RoleName]
    ON [dbo].[tbl_Master_Role]([RoleName] ASC) WHERE ([IsActive]=(1));


