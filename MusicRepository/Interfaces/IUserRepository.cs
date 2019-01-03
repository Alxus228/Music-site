using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicRepository.Interfaces
{
    public interface IUserRepository : IRepository<SqlModel.User>
    {
        Task<ICollection<SqlModel.User>> GetBanList();
        Task<SqlModel.User> GetUserWhoCreatedComment(SqlModel.Comment sqlComment);
        Task<SqlModel.User> GetUserWhoCreatedTrack(SqlModel.Track sqlTrack);
        Task<int> GetCountWhoFavoriteTrack(SqlModel.Track sqlTrack);
        Task AddToFavoriteTracks(SqlModel.User sqlUser, SqlModel.Track sqlTrack);
        Task RemoveFromFavoriteTracks(SqlModel.User sqlUser, SqlModel.Track sqlTrack);
        Task<bool> CheckExistRelation(SqlModel.User sqlUser, SqlModel.Track sqlTrack);
    }
}
