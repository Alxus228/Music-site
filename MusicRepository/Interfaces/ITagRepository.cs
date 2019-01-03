using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicRepository.Interfaces
{
    public interface ITagRepository : IRepository<SqlModel.Tag>
    {
        Task<ICollection<SqlModel.Tag>> GetTagsByTrack(SqlModel.Track sqlTrack);
    }
}
