CREATE TABLE [dbo].[StudentCourse]
(
	[StudentCourseId]  INT IDENTITY(1,1) NOT NULL,
	[StudentId]  INT NOT  NULL,	
	[CourseId]     INT NOT NULL,
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]   DATETIME NULL,
	CONSTRAINT [PK_dbo.StudentCourse] PRIMARY KEY CLUSTERED ([StudentCourseId],[StudentId],[CourseId] ASC),
	CONSTRAINT [Fk_dbo_StudentCourse_dbo_Course_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course]([CourseId]),
	CONSTRAINT [Fk_dbo_StudentCourse_dbo_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student]([StudentId])
) ON [PRIMARY]