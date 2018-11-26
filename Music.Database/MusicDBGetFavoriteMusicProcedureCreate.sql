CREATE PROCEDURE [dbo].[GetFavoriteMusic]
@UserId INT
AS
	SELECT *
--  SELECT [Id], [Name], [Author], [GenreId], [Year], [Lyrics], [SizeInMB], [DurationInSeconds], [Quality], [Icon], [AudioFile], [DateOfCreation], [UserIdWhoCreated]
	FROM [dbo].[Track] AS t
	LEFT JOIN [dbo].[Track_User] AS tu
	ON tu.TrackId = t.Id
	WHERE tu.UserId = @UserId
