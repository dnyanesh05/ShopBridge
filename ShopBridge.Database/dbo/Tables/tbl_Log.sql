CREATE TABLE [dbo].[tbl_Log] (
    [LogID]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [Message]     VARCHAR (MAX)  CONSTRAINT [DF_tbl_Log_Message] DEFAULT ('Unknown') NOT NULL,
    [Source]      NVARCHAR (MAX) NULL,
    [ErrorLineNo] INT            NULL,
    [Type]        NVARCHAR (100) NULL,
    [CreatedBy]   INT            NULL,
    [CreatedOn]   DATETIME       CONSTRAINT [DF_tbl_Log_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_tbl_Log_LogID] PRIMARY KEY CLUSTERED ([LogID] ASC)
);

