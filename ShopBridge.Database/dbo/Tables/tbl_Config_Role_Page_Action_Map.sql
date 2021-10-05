﻿CREATE TABLE [dbo].[tbl_Config_Role_Page_Action_Map] (
    [RolePageActionMapID] INT      IDENTITY (1, 1) NOT NULL,
    [RoleID]              INT      NOT NULL,
    [PageActionMapID]     INT      NOT NULL,
    [IsActive]            BIT      CONSTRAINT [DF_tbl_Master_Role_Page_Action_Map_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]           INT      NULL,
    [CreatedOn]           DATETIME CONSTRAINT [DF_tbl_Master_Role_Page_Action_Map_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]          INT      NULL,
    [ModifiedOn]          DATETIME CONSTRAINT [DF_tbl_Master_Role_Page_Action_Map_ModifiedOn] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_tbl_Master_Role_Page_Action_Map_RolePageActionMapID] PRIMARY KEY CLUSTERED ([RolePageActionMapID] ASC),
    CONSTRAINT [FK_tbl_Master_Role_Page_Action_Map_tbl_Master_Page_Action_Map_PageActionMapID] FOREIGN KEY ([PageActionMapID]) REFERENCES [dbo].[tbl_Config_Page_Action_Map] ([PageActionMapID]),
    CONSTRAINT [FK_tbl_Master_Role_Page_Action_Map_tbl_Master_Role_RoleID] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[tbl_Master_Role] ([RoleID])
);

