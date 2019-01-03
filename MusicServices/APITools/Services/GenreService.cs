using DomainModel;
using MusicRepository.Interfaces;
using MusicRepository.Repositories;
using MusicServices.APITools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicServices.APITools.Services
{
    public class GenreService : IService<DomainModel.Genre>
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task Create(DomainModel.Genre domainGenre)
        {
            if (domainGenre != null)
            {
                await _genreRepository.Create(domainGenre.ToSql());
            }
        }

        public async Task Delete(int? id)
        {
            if (await this.Get(id) != null)
            {
                await _genreRepository.DeleteById((int)id);
            }
        }

        public async Task<DomainModel.Genre> Get(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var sqlGenre = await _genreRepository.GetById((int)id);
            if (sqlGenre == null)
            {
                return null;
            }

            return sqlGenre.ToDomain();
        }

        public async Task<List<DomainModel.Genre>> GetAll()
        {
            var sqlGenres = await _genreRepository.GetAll();
            if (sqlGenres == null)
            {
                return null;
            }

            var domainGenres = sqlGenres.ToDomain();

            return domainGenres.ToList();
        }

        public async Task Update(DomainModel.Genre domainGenre)
        {
            if (domainGenre != null && await this.Get(domainGenre.Id) != null)
            {
                await _genreRepository.Update(domainGenre.ToSql());
            }
        }
    }
}
