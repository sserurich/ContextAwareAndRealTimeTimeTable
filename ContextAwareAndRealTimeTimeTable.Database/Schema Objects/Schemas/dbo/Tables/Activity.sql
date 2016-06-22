CREATE TABLE [dbo].[Activity]
(
	[ActivityId]  INT IDENTITY(1,1) NOT NULL,
	[SubjectId]	  INT NOT NULL,	
	[GroupId]     INT NOT NULL,
	[LecturerId]  INT NOT NULL,
	[DayId]       INT NOT NULL,
	[RoomId]      INT NOT NULL,
	[Type]        VARCHAR NULL,
	[StartTime]   VARCHAR(50) NOT NULL,
	[EndTime]     VARCHAR(50) NOT NULL,
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]   DATETIME NULL,
	CONSTRAINT [PK_dbo.Activity] PRIMARY KEY CLUSTERED ([ActivityId] ASC),
	CONSTRAINT [Fk_dbo_Activity_dbo_Lecturer_LecturerId] FOREIGN KEY ([LecturerId]) REFERENCES [dbo].[Lecturer]([LecturerId]),
	CONSTRAINT [Fk_dbo_Activity_dbo_Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group]([GroupId]),
	CONSTRAINT [Fk_dbo_Activity_dbo_Room_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Room]([RoomId]),
	CONSTRAINT [Fk_dbo_Activity_dbo_Day_DayId] FOREIGN KEY ([DayId]) REFERENCES [dbo].[Day]([DayId]),
	CONSTRAINT [Fk_dbo_Activity_dbo_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subject]([SubjectId])
) ON [PRIMARY]


