USE Music

IF NOT (EXISTS (SELECT TOP 1 *
                       FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_SCHEMA = 'dbo'
        AND TABLE_NAME = 'Role'))
BEGIN
	CREATE TABLE [dbo].[Role]
	(
		[Id]        INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
		[Name]      NVARCHAR(30) NOT NULL,
		[IsDeleted] BIT          NOT NULL
	)
END

IF NOT (EXISTS (SELECT TOP 1 *
                       FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_SCHEMA = 'dbo'
        AND TABLE_NAME = 'User'))
BEGIN
	CREATE TABLE [dbo].[User]
	(
		[Id]                 INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
		[NickName]           NVARCHAR(30)   NOT NULL,
		[Email]              NVARCHAR(200)  NOT NULL,
		[Password]           NVARCHAR(30)   NOT NULL,
		[RoleId]             INT REFERENCES [dbo].[Role](Id), 
		[DateOfRegistration] DATETIME2      NOT NULL,
		[Avatar]             VARBINARY(MAX),
		[IsDeleted]          BIT            NOT NULL
	)
END

IF NOT (EXISTS (SELECT TOP 1 * 
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_SCHEMA = 'dbo' 
				AND TABLE_NAME = 'Genre'))
BEGIN
	CREATE TABLE [dbo].[Genre]
	(
		[Id]        INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
		[Name]      NVARCHAR(30) NOT NULL,
		[IsDeleted] BIT          NOT NULL
	)
END

IF NOT (EXISTS (SELECT TOP 1 * 
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_SCHEMA = 'dbo' 
				AND TABLE_NAME = 'Tag'))
BEGIN
	CREATE TABLE [dbo].[Tag]
	(
		[Id]        INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
		[Name]      NVARCHAR(30) NOT NULL,
		[IsDeleted] BIT NOT NULL
	)
END

IF NOT (EXISTS (SELECT TOP 1 * 
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_SCHEMA = 'dbo' 
				AND TABLE_NAME = 'Track'))
BEGIN
	CREATE TABLE [dbo].[Track]
	(
		[Id]                INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
		[Name]              NVARCHAR(100),
		[Author]            NVARCHAR(100),
		[GenreId]           INT REFERENCES [dbo].[Genre](Id),
		[Year]              INT,
		[Lyrics]            NVARCHAR(1000),
		[SizeInMB]	        FLOAT          NOT NULL,
	    [DurationInSeconds] INT            NOT NULL,
		[Quality]           NVARCHAR(10)   NOT NULL,
		[Icon]              VARBINARY(MAX),
		[AudioFile]		    VARBINARY(MAX) NOT NULL,
		[DateOfCreation]    DATETIME2      NOT NULL,
		[UserIdWhoCreated]  INT REFERENCES [dbo].[User](Id) ON DELETE SET NULL,
		[IsDeleted]         BIT            NOT NULL
	)
END

IF NOT (EXISTS (SELECT TOP 1 *
                       FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_SCHEMA = 'dbo'
        AND TABLE_NAME = 'Comment'))
BEGIN
	CREATE TABLE [dbo].[Comment]
	(
		[Id]               INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
		[Content]          NVARCHAR(100) NOT NULL,
		[DateOfCreation]   DATETIME2     NOT NULL,
		[UserIdWhoCreated] INT REFERENCES [dbo].[User](Id) ON DELETE CASCADE,
		[TrackId]          INT REFERENCES [dbo].[Track](Id) ON DELETE CASCADE,
		[IsDeleted]        BIT           NOT NULL
	)
END

IF NOT (EXISTS (SELECT TOP 1 *
                       FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_SCHEMA = 'dbo'
        AND TABLE_NAME = 'Tag_Track'))
BEGIN
	CREATE TABLE [dbo].[Tag_Track] -- For Tags
	(
		[TrackId]   INT NOT NULL,
		[TagId]     INT NOT NULL,
		PRIMARY KEY([TrackId], [TagId]),
		FOREIGN KEY([TrackId]) REFERENCES [dbo].[Track](Id),
		FOREIGN KEY([TagId])  REFERENCES [dbo].[Tag](Id)
	)
END

IF NOT (EXISTS (SELECT TOP 1 *
                       FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_SCHEMA = 'dbo'
        AND TABLE_NAME = 'Track_User'))
BEGIN
	CREATE TABLE [dbo].[Track_User] -- For Favorite music
	(
		[TrackId]   INT NOT NULL,
		[UserId]    INT NOT NULL,
		PRIMARY KEY([TrackId], [UserId]),
		FOREIGN KEY([TrackId]) REFERENCES [dbo].[Track](Id),
		FOREIGN KEY([UserId])  REFERENCES [dbo].[User](Id)
	)
END
