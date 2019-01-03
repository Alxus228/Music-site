using System.Threading.Tasks;

namespace MusicRepository.Interfaces
{
    public interface IGenreRepository : IRepository<SqlModel.Genre>
    {
        Task<SqlModel.Genre> GetGenreByTrack(SqlModel.Track sqlTrack);
    }
}
