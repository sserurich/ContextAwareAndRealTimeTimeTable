CREATE TABLE [dbo].[Lecturer]
(
	[LecturerId] INT IDENTITY(1,1) NOT NULL,
	[EmployeeNumber] VARCHAR(50) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,--aspnet userId,its the account
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]    DATETIME NULL,
	CONSTRAINT [PK_dbo.Lecturer] PRIMARY KEY CLUSTERED ([LecturerId] ASC), 
	CONSTRAINT [Fk_dbo_Lecturer_dbo_AspNetUsers_Id] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers](Id),

) ON [PRIMARY]



