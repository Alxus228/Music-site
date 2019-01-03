using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicServices.APITools.Interfaces
{
    public interface IUserService : IService<DomainModel.User>
    {
        Task<List<DomainModel.User>> GetBanList();
        Task AddFavoriteRelation(DomainModel.User domainUser, DomainModel.Track domainTrack);
        Task RemoveFavoriteRelation(DomainModel.User domainUser, DomainModel.Track domainTrack);
        Task<bool> IsExistFavoriteRelation(DomainModel.User domainUser, DomainModel.Track domainTrack);
    }
}
