CREATE PROCEDURE [dbo].[CheckOnExistsTagTrack]
@TrackId INT, @TagId INT
AS
IF (EXISTS (SELECT * FROM [dbo].Tag_Track WHERE TrackId = @TrackId AND TagId = @TagId))
BEGIN SELECT 1 END
ELSE
BEGIN SELECT 0 END
