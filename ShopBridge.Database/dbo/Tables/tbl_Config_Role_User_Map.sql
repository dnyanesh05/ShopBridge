CREATE TABLE [dbo].[tbl_Config_Role_User_Map] (
    [RoleUserMapID] INT                                         IDENTITY (1, 1) NOT NULL,
    [RoleID]        INT                                         NOT NULL,
    [UserID]        INT                                         NOT NULL,
    [IsActive]      BIT                                         CONSTRAINT [DF_tbl_Master_Role_User_Map_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]     INT                                         NULL,
    [CreatedOn]     DATETIME                                    CONSTRAINT [DF_tbl_Master_Role_User_Map_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]    INT                                         NULL,
    [ModifiedOn]    DATETIME                                    CONSTRAINT [DF_tbl_Master_Role_User_Map_ModifiedOn] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_tbl_Master_Role_User_Map_RoleUserMapID] PRIMARY KEY CLUSTERED ([RoleUserMapID] ASC),
    CONSTRAINT [UQ_tbl_Master_Role_User_Map_RoleID_UserID] UNIQUE NONCLUSTERED ([RoleID] ASC, [UserID] ASC)
);



