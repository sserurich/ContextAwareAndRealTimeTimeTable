CREATE TABLE [dbo].[StudentYear]
(
	[StudentYearId]  INT IDENTITY(1,1) NOT NULL,
	[StudentId]   INT NOT  NULL,	
	[YearId]      INT NOT NULL,
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]   DATETIME NULL,
	CONSTRAINT [PK_dbo.StudentYear] PRIMARY KEY CLUSTERED ([StudentYearId],[StudentId],[YearId]),
	CONSTRAINT [Fk_dbo_StudentYear_dbo_year_YearId] FOREIGN KEY ([YearId]) REFERENCES [dbo].[Year]([YearId]),
	CONSTRAINT [Fk_dbo_StudentYear_dbo_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student]([StudentId])
) ON [PRIMARY]