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
    public class CommentRepository : ICommentRepository
    {
        public async Task Create(SqlModel.Comment sqlComment)
        {
            string createComment = "INSERT INTO [dbo].[Comment] VALUES (@Content, GETDATE(), @UserIdWhoCreated, @TrackId, 0)";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(createComment, sqlComment);
            }
        }

        public async Task DeleteById(int id)
        {
            string deleteCommentById = $"UPDATE [dbo].[Comment] SET [IsDeleted] = 1 WHERE [Id] = {id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(deleteCommentById);
            }
        }

        public async Task<ICollection<SqlModel.Comment>> GetAll()
        {
            string getAllComments = "SELECT * FROM [dbo].[Comment] WHERE [IsDeleted] = 0";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var comments = await con.QueryAsync<SqlModel.Comment>(getAllComments);

                return comments.ToList();
            }
        }

        public async Task<SqlModel.Comment> GetById(int id)
        {
            string getCommentById = $"SELECT * FROM [dbo].[Comment] WHERE [Id] = {id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var comment = (await con.QueryAsync<SqlModel.Comment>(getCommentById)).FirstOrDefault();

                if (comment != null && !comment.IsDeleted)
                {
                    return comment;
                }
                return null;
            }
        }

        public async Task Update(SqlModel.Comment sqlComment)
        {
            string updateComment = $"UPDATE [dbo].[Comment] SET [Content] = @Content WHERE [Id] = {sqlComment.Id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(updateComment, sqlComment);
            }
        }

        public async Task<ICollection<SqlModel.Comment>> GetCommentsByUserWhoCreated(SqlModel.User sqlUser)
        {
            string getCommentsByUserWhoCreated = $"SELECT * FROM [dbo].[Comment] WHERE [UserIdWhoCreated] = {sqlUser.Id} AND [IsDeleted] = 0";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var comments = await con.QueryAsync<SqlModel.Comment>(getCommentsByUserWhoCreated);

                return comments.ToList();
            }
        }

        public async Task<ICollection<Comment>> GetCommentsByTrack(Track sqlTrack)
        {
            string getCommentsByUserWhoCreated = $"SELECT * FROM [dbo].[Comment] WHERE [TrackId] = {sqlTrack.Id} AND [IsDeleted] = 0";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var comments = await con.QueryAsync<SqlModel.Comment>(getCommentsByUserWhoCreated);

                return comments.ToList();
            }
        }
    }
}
