CREATE TABLE [dbo].[Course]
(
	[CourseId]	  INT IDENTITY(1,1) NOT NULL,
	[Name]		  VARCHAR(50) NOT NULL,
	[Description] VARCHAR(50) NOT NULL,
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]   DATETIME NULL,
	CONSTRAINT [PK_dbo.Course] PRIMARY KEY CLUSTERED ([CourseId] ASC),
	
) ON [PRIMARY]
