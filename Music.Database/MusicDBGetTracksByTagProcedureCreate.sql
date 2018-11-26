CREATE PROCEDURE [dbo].[GetTracksByTag]
@TagId INT
AS
	SELECT *
--  SELECT [Id], [Name], [Author], [GenreId], [Year], [Lyrics], [SizeInMB], [DurationInSeconds], [Quality], [Icon], [AudioFile], [DateOfCreation], [UserIdWhoCreated]
	FROM [dbo].[Track] AS t
	LEFT JOIN [dbo].[Tag_Track] AS tt
	ON tt.TrackId = t.Id
	WHERE tt.TagId = @TagId
