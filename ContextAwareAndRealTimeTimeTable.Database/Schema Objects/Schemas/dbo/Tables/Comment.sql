CREATE TABLE [dbo].[Comment]
(
	[CommentId] INT IDENTITY(1,1) NOT NULL,	
	[Description] VARCHAR(MAX) NOT NULL,
	[ActivityId] INT NOT NULL,
	[CreatedBy]   NVARCHAR(128) NOT NULL,--lecture who creates comment	
	[CreatedOn]   DATETIME  NOT NULL,
	[UpdatedOn]   DATETIME NULL,	
	[DeletedOn]    DATETIME NULL,
	CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED ([CommentId] ASC),	
	CONSTRAINT [Fk_dbo_Comment_dbo_AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers]([Id]),
	CONSTRAINT [Fk_dbo_Comment_dbo_Activity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activity]([ActivityId]),
) ON [PRIMARY]