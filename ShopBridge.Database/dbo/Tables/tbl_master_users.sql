CREATE TABLE [dbo].[tbl_Master_User] (
    [UserID]               INT            IDENTITY (1, 1) NOT NULL,
    [UserName]             NVARCHAR (640) NULL,
    [Email]                VARCHAR (320)  NOT NULL,
    [IsActive]             BIT            CONSTRAINT [DF_tbl_Master_User_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]            INT            NULL,
    [CreatedOn]            DATETIME       CONSTRAINT [DF_tbl_Master_User_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]           INT            NULL,
    [ModifiedOn]           DATETIME       CONSTRAINT [DF_tbl_Master_User_ModifiedOn] DEFAULT (getutcdate()) NOT NULL,    
    [Salutation]           NVARCHAR (40)  CONSTRAINT [DF_tbl_Master_User_Salutation] DEFAULT ('') NULL,
    [FirstName]            NVARCHAR (200) NOT NULL,
    [LastName]             NVARCHAR (200) NOT NULL,
    [HomePhoneNumber]      NVARCHAR (200) NULL,
    [AlternatePhoneNumber] NVARCHAR (200) NULL,
    [MobileNumber]         NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_tbl_Master_User_UserID] PRIMARY KEY CLUSTERED ([UserID] ASC)
);


GO


