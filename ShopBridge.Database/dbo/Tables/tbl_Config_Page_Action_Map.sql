CREATE TABLE [dbo].[tbl_Config_Page_Action_Map] (
    [PageActionMapID] INT      IDENTITY (1, 1) NOT NULL,
    [PageID]          INT      NOT NULL,
    [ActionID]        INT      NOT NULL,
    [IsActive]        BIT      CONSTRAINT [DF_tbl_Master_Page_Action_Map_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]       INT      NULL,
    [CreatedOn]       DATETIME CONSTRAINT [DF_tbl_Master_Page_Action_Map_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      INT      NULL,
    [ModifiedOn]      DATETIME CONSTRAINT [DF_tbl_Master_Page_Action_Map_ModifiedOn] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_tbl_Master_Page_Action_Map_PageActionMapID] PRIMARY KEY CLUSTERED ([PageActionMapID] ASC),
    CONSTRAINT [FK_tbl_Master_Page_Action_Map_tbl_Master_Action_ActionID] FOREIGN KEY ([ActionID]) REFERENCES [dbo].[tbl_Master_Action] ([ActionID]),
    CONSTRAINT [FK_tbl_Master_Page_Action_Map_tbl_Master_Page_PageID] FOREIGN KEY ([PageID]) REFERENCES [dbo].[tbl_Master_Page] ([PageID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_tbl_Config_Page_Action_Map_PageID_ActionID]
    ON [dbo].[tbl_Config_Page_Action_Map]([PageID] ASC, [ActionID] ASC);


