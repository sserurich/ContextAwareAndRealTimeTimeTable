CREATE TABLE [dbo].[Subject]
(
	[SubjectId]	  INT IDENTITY(1,1) NOT NULL,
	[Name]		  VARCHAR(50) NOT NULL,	
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]    DATETIME NULL,
	CONSTRAINT [PK_dbo.Subject] PRIMARY KEY CLUSTERED ([SubjectId] ASC)	
) ON [PRIMARY]
