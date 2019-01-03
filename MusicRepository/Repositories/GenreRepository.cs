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
    public class GenreRepository : IGenreRepository
    {
        public async Task Create(SqlModel.Genre sqlGenre)
        {
            string createGenre = $"INSERT INTO [dbo].[Genre] VALUES (@Name, 0)";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(createGenre, sqlGenre);
            }
        }

        public async Task DeleteById(int id)
        {
            string deleteGenreById = $"UPDATE [dbo].[Genre] SET [IsDeleted] = 1 WHERE [Id] = {id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(deleteGenreById);
            }
        }

        public async Task<ICollection<SqlModel.Genre>> GetAll()
        {
            string getAllGenres = "SELECT * FROM [dbo].[Genre] WHERE [IsDeleted] = 0";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var genres = await con.QueryAsync<SqlModel.Genre>(getAllGenres);

                return genres.ToList();
            }
        }

        public async Task<SqlModel.Genre> GetById(int id)
        {
            string getGenreById = $"SELECT * FROM [dbo].[Genre] WHERE [Id] = {id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var genre = (await con.QueryAsync<SqlModel.Genre>(getGenreById)).FirstOrDefault();

                if (genre != null && !genre.IsDeleted)
                {
                    return genre;
                }
                return null;
            }
        }

        public async Task Update(SqlModel.Genre sqlGenre)
        {
            string updateGenre = $"UPDATE [dbo].[Genre] SET [Name] = @Name WHERE [Id] = {sqlGenre.Id}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                await con.QueryAsync(updateGenre, sqlGenre);
            }
        }

        public async Task<SqlModel.Genre> GetGenreByTrack(SqlModel.Track sqlTrack)
        {
            string getGenreByTrack = $"SELECT * FROM [dbo].[Genre] WHERE [Id] = {sqlTrack.GenreId}";

            using (var con = new SqlConnection(MusicRepository.Properties.Settings.Default.SqlRoot))
            {
                var genre = (await con.QueryAsync<SqlModel.Genre>(getGenreByTrack)).FirstOrDefault();

                if (genre != null && !genre.IsDeleted)
                {
                    return genre;
                }
                return null;
            }
        }
    }
}
