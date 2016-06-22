CREATE TABLE [dbo].[CourseGroupSubject]
(
	[CourseGroupSubjectId]  INT IDENTITY(1,1) NOT NULL,
	[CourseId]	  INT NOT  NULL,	
	[SubjectId]   INT NOT NULL,
	[GroupId]     INT NOT NULL,
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]   DATETIME NULL,
	CONSTRAINT [PK_dbo.CourseGroupSubject] PRIMARY KEY CLUSTERED ([CourseGroupSubjectId],[CourseId],[SubjectId],[GroupId]),
	CONSTRAINT [Fk_dbo_CourseGroupSubject_dbo_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subject]([SubjectId]),
	CONSTRAINT [Fk_dbo_CourseGroupSubject_dbo_Course_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course]([CourseId]),
	CONSTRAINT [Fk_dbo_CourseGroupSubject_dbo_Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group]([GroupId]),
) ON [PRIMARY]