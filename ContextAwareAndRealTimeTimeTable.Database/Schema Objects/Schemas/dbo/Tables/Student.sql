CREATE TABLE [dbo].[Student]
(
	[StudentId] INT IDENTITY(1,1) NOT NULL,
	[StudentNumber] VARCHAR(50) NOT NULL,
	[RegistrationNumber] VARCHAR(50) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,--aspnet userId,its the accountId
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]    DATETIME NULL,
	CONSTRAINT [PK_dbo.Student] PRIMARY KEY CLUSTERED ([StudentId] ASC), 
	CONSTRAINT [Fk_dbo_Student_dbo_AspNetUsers_Id] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers](Id)
) ON [PRIMARY]
