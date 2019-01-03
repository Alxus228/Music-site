using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicServices.APITools.Interfaces
{
    public interface ITrackService : IService<DomainModel.Track>
    {
        Task AddTagTrackRelation(DomainModel.Track domainTrack, DomainModel.Tag domainTag);
        Task RemoveTagTrackRelation(DomainModel.Track domainTrack, DomainModel.Tag domainTag);
        Task<bool> IsExistTagTrackRelation(DomainModel.Track domainTrack, DomainModel.Tag domainTag);
        Task<bool> IsDuplicate(string name, string author);
    }
}
