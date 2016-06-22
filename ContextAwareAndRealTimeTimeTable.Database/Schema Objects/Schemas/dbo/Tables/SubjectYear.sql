CREATE TABLE [dbo].[SubjectYear]
(
	[SubjectYearId]  INT IDENTITY(1,1) NOT NULL,
	[SubjectId]   INT NOT  NULL,	
	[YearId]      INT NOT NULL,
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]   DATETIME NULL,
	CONSTRAINT [PK_dbo.SubjectYear] PRIMARY KEY CLUSTERED ([SubjectYearId],[SubjectId],[YearId]),
	CONSTRAINT [Fk_dbo_SubjectYear_dbo_year_YearId] FOREIGN KEY ([YearId]) REFERENCES [dbo].[Year]([YearId]),
	CONSTRAINT [Fk_dbo_SubjectYear_dbo_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subject]([SubjectId])
) ON [PRIMARY]