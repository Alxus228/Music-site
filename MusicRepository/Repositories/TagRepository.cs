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
    public class TagRepository : ITagRepository
    {
        public async Task Create(SqlModel.Tag sqlTag)
        {
            string createTag = $"INSERT INTO [dbo].[Tag] VALUES (@Name, 0)";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(createTag, sqlTag);
            }
        }

        public async Task DeleteById(int id)
        {
            string deleteTagById = $"UPDATE [dbo].[Tag] SET [IsDeleted] = 1 WHERE [Id] = {id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(deleteTagById);
            }
        }

        public async Task<ICollection<SqlModel.Tag>> GetAll()
        {
            string getAllTags = "SELECT * FROM [dbo].[Tag] WHERE [IsDeleted] = 0";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var tags = await con.QueryAsync<SqlModel.Tag>(getAllTags);

                return tags.ToList();
            }
        }

        public async Task<SqlModel.Tag> GetById(int id)
        {
            string getTagById = $"SELECT * FROM [dbo].[Tag] WHERE [Id] = {id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var tag = (await con.QueryAsync<SqlModel.Tag>(getTagById)).FirstOrDefault();

                if (tag != null && !tag.IsDeleted)
                {
                    return tag;
                }
                return null;
            }
        }

        public async Task Update(SqlModel.Tag sqlTag)
        {
            string updateTag = $"UPDATE [dbo].[Tag] SET [Name] = @Name WHERE [Id] = {sqlTag.Id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(updateTag, sqlTag);
            }
        }

        public async Task<ICollection<Tag>> GetTagsByTrack(SqlModel.Track sqlTrack)
        {
            string getTagsByTrack = $"EXEC GetTagsByTrack {sqlTrack.Id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var tags = await con.QueryAsync<SqlModel.Tag>(getTagsByTrack);

                return tags.ToList();
            }
        }
    }
}
