CREATE TABLE [dbo].[StudentSubject]
(
	[StudentSubjectId]  INT IDENTITY(1,1) NOT NULL,
	[StudentId]  INT NOT  NULL,	
	[SubjectId]   INT NOT NULL,
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]   DATETIME NULL,
	CONSTRAINT [PK_dbo.StudentSubject] PRIMARY KEY CLUSTERED ([StudentSubjectId],[StudentId],[SubjectId]),
	CONSTRAINT [Fk_dbo_StudentSubject_dbo_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subject]([SubjectId]),
	CONSTRAINT [Fk_dbo_StudentSubject_dbo_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student]([StudentId])
) ON [PRIMARY]