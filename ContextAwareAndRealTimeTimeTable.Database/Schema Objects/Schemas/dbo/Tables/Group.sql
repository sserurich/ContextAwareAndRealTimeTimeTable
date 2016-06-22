CREATE TABLE [dbo].[Group]
(
	[GroupId]	  INT IDENTITY(1,1) NOT NULL,
	[Name]		  VARCHAR(50) NOT NULL,
	[YearId]	  INT	NOT NULL,
	[CourseId]    INT   NULL,
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]   DATETIME NULL,
	CONSTRAINT [PK_dbo.Group] PRIMARY KEY CLUSTERED ([GroupId] ASC)	,
	CONSTRAINT [Fk_dbo_Group_dbo_Year_YearId] FOREIGN KEY ([YearId]) REFERENCES [dbo].[Year]([YearId]),
	CONSTRAINT [Fk_dbo_Group_dbo_Course_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course]([CourseId]),
) ON [PRIMARY]
