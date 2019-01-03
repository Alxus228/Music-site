using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicRepository.Interfaces
{
    public interface ICommentRepository : IRepository<SqlModel.Comment>
    {
        Task<ICollection<SqlModel.Comment>> GetCommentsByUserWhoCreated(SqlModel.User sqlUser);
        Task<ICollection<SqlModel.Comment>> GetCommentsByTrack(SqlModel.Track sqlTrack);
    }
}
