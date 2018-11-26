CREATE PROCEDURE [dbo].[CheckOnExistsTrackUser]
@TrackId INT, @UserId INT
AS
IF (EXISTS (SELECT * FROM [dbo].[Track_User] WHERE TrackId = @TrackId AND UserId = @UserId))
BEGIN SELECT 1 END
ELSE
BEGIN SELECT 0 END
