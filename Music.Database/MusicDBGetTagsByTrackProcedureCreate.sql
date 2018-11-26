CREATE PROCEDURE [dbo].[GetTagsByTrack]
@TrackId INT
AS
	SELECT *
--  SELECT [Id], [Name], [Author], [GenreId], [Year], [Lyrics], [SizeInMB], [DurationInSeconds], [Quality], [Icon], [AudioFile], [DateOfCreation], [UserIdWhoCreated]
	FROM [dbo].[Tag] AS t
	LEFT JOIN [dbo].[Tag_Track] AS tt
	ON tt.TagId = t.Id
	WHERE tt.TrackId = @TrackId
