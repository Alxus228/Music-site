using Dapper;
using MusicRepository.Interfaces;
using SqlModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRepository.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        public async Task Create(SqlModel.Track sqlTrack)
        {
            string createTrack = "INSERT INTO [dbo].[Track] VALUES " +
                                  "(@Name, @Author, @GenreId, @Year, @Lyrics, @SizeInMB, @DurationInSeconds, @Quality, @Icon, @AudioFileId, GETDATE(), @UserIdWhoCreated, 0)";
            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(createTrack, sqlTrack);
            }
        }

        public async Task DeleteById(int id)
        {
            string deleteTrackById = $"UPDATE [dbo].[Track] SET [IsDeleted] = 1 WHERE [Id] = {id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(deleteTrackById);
            }
        }

        public async Task<ICollection<SqlModel.Track>> GetAll()
        {
            string getAllTracks = "SELECT * FROM [dbo].[Track] WHERE [IsDeleted] = 0";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var tracks = await con.QueryAsync<SqlModel.Track>(getAllTracks);

                return tracks.ToList();
            }
        }

        public async Task<SqlModel.Track> GetById(int id)
        {
            string getTrackById = $"SELECT * FROM [dbo].[Track] WHERE [Id] = {id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var track = (await con.QueryAsync<SqlModel.Track>(getTrackById)).FirstOrDefault();

                if (track != null && !track.IsDeleted)
                {
                    return track;
                }
                return null;
            }
        }

        public async Task Update(SqlModel.Track sqlTrack)
        {
            string updateUser = "UPDATE [dbo].[Track] " +
                                 $"SET [Name] = @Name, [Author] = @Author, [GenreId] = @GenreId, [Year] = @Year, [Lyrics] = @Lyrics, [SizeInMB] = @SizeInMB, [DurationInSeconds] = @DurationInSeconds, [Quality] = @Quality, [Icon] = @Icon, [AudioFileId] = @AudioFileId WHERE [Id] = {sqlTrack.Id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(updateUser, sqlTrack);
            }
        }

        public async Task<SqlModel.Track> GetTrackByComment(SqlModel.Comment sqlComment)
        {
            string getTrackByComment = $"SELECT * FROM [dbo].[Track] WHERE [Id] = {sqlComment.TrackId}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var track = (await con.QueryAsync<SqlModel.Track>(getTrackByComment)).FirstOrDefault();

                if (track != null && !track.IsDeleted)
                {
                    return track;
                }
                return null;
            }
        }

        public async Task<ICollection<SqlModel.Track>> GetTracksByGenre(SqlModel.Genre sqlGenre)
        {
            string getTracksByGenre = $"SELECT * FROM [dbo].[Track] WHERE [GenreId] = {sqlGenre.Id} AND [IsDeleted] = 0";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var tracks = await con.QueryAsync<SqlModel.Track>(getTracksByGenre);

                return tracks.ToList();
            }
        }

        public async Task<ICollection<SqlModel.Track>> GetTracksByUserWhoCreated(SqlModel.User sqlUser)
        {
            string getTracksByUserWhoCreated = $"SELECT * FROM [dbo].[Track] WHERE [UserIdWhoCreated] = {sqlUser.Id} AND [IsDeleted] = 0";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var tracks = await con.QueryAsync<SqlModel.Track>(getTracksByUserWhoCreated);

                return tracks.ToList();
            }
        }

        public async Task<ICollection<Track>> GetTracksByUserWhoFavorite(SqlModel.User sqlUser)
        {
            string getTracksByUserWhoFavorite = $"EXEC GetFavoriteMusic {sqlUser.Id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var tracks = await con.QueryAsync<SqlModel.Track>(getTracksByUserWhoFavorite);

                return tracks.ToList();
            }
        }

        public async Task<ICollection<Track>> GetTracksByTag(Tag sqlTag)
        {
            string getTracksByTag = $"EXEC GetTracksByTag {sqlTag.Id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var tracks = await con.QueryAsync<SqlModel.Track>(getTracksByTag);

                return tracks.ToList();
            }
        }

        public async Task AddToTrackTag(SqlModel.Track sqlTrack, SqlModel.Tag sqlTag)
        {
            string addToTrackTag = $"INSERT INTO [dbo].[Tag_Track] VALUES ({sqlTrack.Id}, {sqlTag.Id})";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(addToTrackTag);
            }
        }

        public async Task RemoveFromTrackTag(SqlModel.Track sqlTrack, SqlModel.Tag sqlTag)
        {
            string removeFromTrackTag = $"DELETE FROM [dbo].[Tag_Track] WHERE [TrackId] = {sqlTrack.Id} AND [TagId] = {sqlTag.Id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(removeFromTrackTag);
            }
        }

        public async Task<bool> CheckExistRelation(SqlModel.Track sqlTrack, SqlModel.Tag sqlTag)
        {
            string checkExistRelation = $"EXEC CheckOnExistsTrackUser {sqlTrack.Id}, {sqlTag.Id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                return (await con.QueryAsync<bool>(checkExistRelation)).FirstOrDefault();
            }
        }

        public async Task CreateAudioFile(int trackId, byte[] bytes)
        {
            var data = new SqlAudioFile()
            {
                TrackId = trackId,
                File = bytes
            };

            string createTrack = $"INSERT INTO [dbo].[AudioFile] VALUES (@File, @TrackId, 0)";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(createTrack, data);
            }
        }

        public async Task<byte[]> GetAudioFile(int id)
        {
            string getAudioFileById = $"SELECT * FROM [dbo].[AudioFile] WHERE [Id] = {id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var audioFile = (await con.QueryAsync<SqlAudioFile>(getAudioFileById)).FirstOrDefault();

                if (audioFile != null && !audioFile.IsDeleted)
                {
                    return audioFile.File;
                }
                return null;
            }
        }

        public async Task DeleteAudioFile(int id)
        {
            string deleteAudioFileById = $"UPDATE [dbo].[AudioFile] SET [IsDeleted] = 1 WHERE [Id] = {id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(deleteAudioFileById);
            }
        }

        public async Task UpdateAudioFile(int id, int? trackId, byte[] bytes)
        {
            var data = new SqlAudioFile()
            {
                TrackId = trackId,
                File = bytes,
                IsDeleted = false
            };

            string updateUser = "UPDATE [dbo].[AudioFile] " +
                                 $"SET [TrackId] = @TrackId, [File] = @File, [IsDeleted] = @IsDeleted WHERE [Id] = {id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(updateUser, data);
            }
        }

        public async Task<int?> SearchAudioFileByTrackId(int trackId)
        {
            string searchAudioFileByTrackId = $"SELECT (Id) FROM [dbo].[AudioFile] WHERE [TrackId] = {trackId}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                return (await con.QueryAsync<int>(searchAudioFileByTrackId)).FirstOrDefault();
            }
        }
        private class SqlAudioFile
        {
            public int Id { get; set; }
            public int? TrackId { get; set; }
            public byte[] File { get; set; }
            public bool IsDeleted { get; set; }
        }
    }
}
