CREATE TABLE [dbo].[Verification]
(
	[VerificationId]  INT IDENTITY(1,1) NOT NULL,
	[RequestedBy]   NVARCHAR(128) NOT NULL,
	[Message]		  VARCHAR(250) NOT NULL,	
	[CreatedOn]   DATETIME	NOT NULL,
	[UpdatedOn]   DATETIME	NULL,	
	[DeletedOn]   DATETIME NULL,
	CONSTRAINT [PK_dbo.Verification] PRIMARY KEY CLUSTERED ([VerificationId] ASC)	,
	CONSTRAINT [Fk_dbo_Verification_dbo_AspNetUsers_RequestedBy] FOREIGN KEY ([RequestedBy]) REFERENCES [dbo].[AspNetUsers]([Id]),
) ON [PRIMARY]
