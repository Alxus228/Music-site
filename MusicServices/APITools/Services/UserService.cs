using DomainModel;
using MusicRepository;
using MusicRepository.Interfaces;
using MusicServices.APITools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicServices.APITools.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Create(DomainModel.User domainUser)
        {
            if(domainUser != null)
            {
                await _userRepository.Create(domainUser.ToSql());
            }
        }

        public async Task Delete(int? id)
        {
            if(await this.Get(id) != null)
            {
                await _userRepository.DeleteById((int)id);
            }
        }

        public async Task<List<DomainModel.User>> GetAll()
        {
            var sqlUsers = await _userRepository.GetAll();
            if(sqlUsers == null)
            {
                return null;
            }

            var domainUsers = sqlUsers.ToDomain();

            return domainUsers.ToList();
        }

        public async Task<DomainModel.User> Get(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var sqlUser = await _userRepository.GetById((int)id);
            if (sqlUser == null)
            {
                return null;
            }

            return sqlUser.ToDomain();
        }

        public async Task Update(DomainModel.User domainUser)
        {
            if(domainUser != null && await this.Get(domainUser.Id) != null)
            {
                await _userRepository.Update(domainUser.ToSql());
            }
        }

        public async Task AddFavoriteRelation(DomainModel.User domainUser, DomainModel.Track domainTrack)
        {
            if(domainUser != null && domainTrack != null && !(await IsExistFavoriteRelation(domainUser, domainTrack)))
            {
                await _userRepository.AddToFavoriteTracks(domainUser.ToSql(), domainTrack.ToSql());
            }
        }

        public async Task RemoveFavoriteRelation(DomainModel.User domainUser, DomainModel.Track domainTrack)
        {
            if (await IsExistFavoriteRelation(domainUser, domainTrack))
            {
                await _userRepository.RemoveFromFavoriteTracks(domainUser.ToSql(), domainTrack.ToSql());
            }
        }

        public async Task<bool> IsExistFavoriteRelation(DomainModel.User domainUser, DomainModel.Track domainTrack)
        {
            if(domainUser == null || domainTrack == null)
            {
                return false;
            }
            if(await _userRepository.CheckExistRelation(domainUser.ToSql(), domainTrack.ToSql()))
            {
                return true;
            }
            return false;
        }

        public Task<List<User>> GetBanList()
        {
            throw new NotImplementedException();
        }
    }
}
