using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MusicRepository.Interfaces;
using SqlModel;

namespace MusicRepository
{
    public class UserRepository : IUserRepository
    {
        public async Task Create(SqlModel.User sqlUser)
        {
            string createUser = "INSERT INTO [dbo].[User] VALUES (@NickName, @Email, @Password, @RoleType, GETDATE(), @Avatar, 0)";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(createUser, sqlUser);
            }
        }

        public async Task DeleteById(int id)
        {
            string deleteUserById = $"UPDATE [dbo].[User] SET [IsDeleted] = 1 WHERE [Id] = {id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(deleteUserById);
            }
        }

        public async Task<ICollection<SqlModel.User>> GetAll()
        {
            string getAllUsers = "SELECT * FROM [dbo].[User] WHERE [IsDeleted] = 0";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var users = await con.QueryAsync<SqlModel.User>(getAllUsers);

                return users.ToList();
            }
        }

        public async Task<SqlModel.User> GetById(int id)
        {
            string getUserById = $"SELECT * FROM [dbo].[User] WHERE [Id] = {id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var user = (await con.QueryAsync<SqlModel.User>(getUserById)).FirstOrDefault();

                if (user != null && !user.IsDeleted)
                {
                    return user;
                }
                return null;
            }
        }

        public async Task Update(SqlModel.User sqlUser)
        {
            string updateUser = $"UPDATE [dbo].[User] SET [NickName] = @NickName, [Email] = @Email, [Password] = @Password, [RoleType] = @RoleType, [Avatar] = @Avatar WHERE [Id] = {sqlUser.Id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(updateUser, sqlUser);
            }
        }

        public async Task<SqlModel.User> GetUserWhoCreatedComment(SqlModel.Comment sqlComment)
        {
            string getUserByComment = $"SELECT * FROM [dbo].[User] WHERE [Id] = {sqlComment.UserIdWhoCreated}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var user = (await con.QueryAsync<SqlModel.User>(getUserByComment)).FirstOrDefault();

                if (user != null && !user.IsDeleted)
                {
                    return user;
                }
                return null;
            }
        }

        public async Task<SqlModel.User> GetUserWhoCreatedTrack(SqlModel.Track sqlTrack)
        {
            string getUserByTrack = $"SELECT * FROM [dbo].[User] WHERE [Id] = {sqlTrack.UserIdWhoCreated}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var user = (await con.QueryAsync<SqlModel.User>(getUserByTrack)).FirstOrDefault();

                if (user != null && !user.IsDeleted)
                {
                    return user;
                }
                return null;
            }
        }

        public async Task<int> GetCountWhoFavoriteTrack(SqlModel.Track sqlTrack)
        {
            string getCountWhoFavoriteTrack = $"SELECT COUNT(*) FROM [dbo].[Track_User] WHERE [TrackId] = {sqlTrack.Id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var usersCount = (await con.QueryAsync<int>(getCountWhoFavoriteTrack)).FirstOrDefault();

                return usersCount;
            }
        }

        public async Task AddToFavoriteTracks(SqlModel.User sqlUser, SqlModel.Track sqlTrack)
        {
            string addToFavoriteTrack = $"INSERT INTO [dbo].[Track_User] VALUES ({sqlTrack.Id}, {sqlUser.Id})";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(addToFavoriteTrack);
            }
        }

        public async Task RemoveFromFavoriteTracks(SqlModel.User sqlUser, SqlModel.Track sqlTrack)
        {
            string removeFromFavoriteTrack = $"DELETE FROM [dbo].[Track_User] WHERE [TrackId] = {sqlTrack.Id} AND [UserId] = {sqlUser.Id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(removeFromFavoriteTrack);
            }
        }

        public async Task<bool> CheckExistRelation(SqlModel.User sqlUser, SqlModel.Track sqlTrack)
        {
            string checkExistRelation = $"EXEC CheckOnExistsTrackUser {sqlTrack.Id}, {sqlUser.Id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                return (await con.QueryAsync<bool>(checkExistRelation)).FirstOrDefault();
            }
        }

        public async Task<ICollection<SqlModel.User>> GetBanList()
        {
            string getBanList = "SELECT * FROM [dbo].[User] WHERE [IsDeleted] = 1";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var users = await con.QueryAsync<SqlModel.User>(getBanList);

                return users.ToList();
            }
        }
    }
}
