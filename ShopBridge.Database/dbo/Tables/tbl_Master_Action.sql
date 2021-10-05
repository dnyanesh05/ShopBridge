CREATE TABLE [dbo].[tbl_Master_Action] (
    [ActionID]    INT            IDENTITY (1, 1) NOT NULL,
    [ActionName]  NVARCHAR (500) NULL,
    [ActionValue] NVARCHAR (500) NULL,
    [IsActive]    BIT            CONSTRAINT [DF_tbl_Master_Action_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]   INT            NULL,
    [CreatedOn]   DATETIME       CONSTRAINT [DF_tbl_Master_Action_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]  INT            NULL,
    [ModifiedOn]  DATETIME       CONSTRAINT [DF_tbl_Master_Action_ModifiedOn] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_tbl_Master_Action_ActionID] PRIMARY KEY CLUSTERED ([ActionID] ASC)
);

