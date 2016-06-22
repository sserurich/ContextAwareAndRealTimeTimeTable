CREATE TABLE [dbo].[StudentGroup]
(
	[StudentGroupId]  INT IDENTITY(1,1) NOT NULL,
	[StudentId]  INT NOT  NULL,	
	[GroupId]     INT NOT NULL,
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]   DATETIME NULL,
	CONSTRAINT [PK_dbo.StudentGroup] PRIMARY KEY CLUSTERED ([StudentGroupId],[StudentId],[GroupId] ASC),
	CONSTRAINT [Fk_dbo_StudentGroup_dbo_Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group]([GroupId]),
	CONSTRAINT [Fk_dbo_StudentGroup_dbo_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student]([StudentId])
) ON [PRIMARY]