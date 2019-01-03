using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicRepository.Interfaces
{
    public interface ITrackRepository : IRepository<SqlModel.Track>
    {
        Task<SqlModel.Track> GetTrackByComment(SqlModel.Comment sqlComment);
        Task<ICollection<SqlModel.Track>> GetTracksByUserWhoCreated(SqlModel.User sqlUser);
        Task<ICollection<SqlModel.Track>> GetTracksByGenre(SqlModel.Genre sqlGenre);
        Task<ICollection<SqlModel.Track>> GetTracksByUserWhoFavorite(SqlModel.User sqlUser);
        Task<ICollection<SqlModel.Track>> GetTracksByTag(SqlModel.Tag sqlTag);
        Task AddToTrackTag(SqlModel.Track sqlTrack, SqlModel.Tag sqlTag);
        Task RemoveFromTrackTag(SqlModel.Track sqlTrack, SqlModel.Tag sqlTag);
        Task<bool> CheckExistRelation(SqlModel.Track sqlTrack, SqlModel.Tag sqlTag);
        Task CreateAudioFile(int trackId, byte[] bytes);
        Task<byte[]> GetAudioFile(int id);
        Task<int?> SearchAudioFileByTrackId(int trackId);
        Task DeleteAudioFile(int id);
        Task UpdateAudioFile(int id, int? trackId, byte[] bytes);
    }
}
