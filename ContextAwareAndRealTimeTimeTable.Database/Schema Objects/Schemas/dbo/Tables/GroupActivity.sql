CREATE TABLE [dbo].[GroupActivity]
(
	[GroupActivityId]  INT IDENTITY(1,1) NOT NULL,
	[ActivityId]  INT NOT  NULL,	
	[GroupId]     INT NOT NULL,
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]   DATETIME NULL,
	CONSTRAINT [PK_dbo.GroupActivity] PRIMARY KEY CLUSTERED ([GroupActivityId],[ActivityId],[GroupId] ASC),
	CONSTRAINT [Fk_dbo_GroupActivity_dbo_Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group]([GroupId]),
	CONSTRAINT [Fk_dbo_GroupActivity_dbo_Activity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activity]([ActivityId])
) ON [PRIMARY]
