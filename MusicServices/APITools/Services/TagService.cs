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
    public class TagService : IService<DomainModel.Tag>
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task Create(DomainModel.Tag domainTag)
        {
            if (domainTag != null)
            {
                await _tagRepository.Create(domainTag.ToSql());
            }
        }

        public async Task Delete(int? id)
        {
            if (await this.Get(id) != null)
            {
                await _tagRepository.DeleteById((int)id);
            }
        }

        public async Task<DomainModel.Tag> Get(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var sqlTag = await _tagRepository.GetById((int)id);
            if (sqlTag == null)
            {
                return null;
            }

            return sqlTag.ToDomain();
        }

        public async Task<List<DomainModel.Tag>> GetAll()
        {
            var sqlTags = await _tagRepository.GetAll();
            if (sqlTags == null)
            {
                return null;
            }

            var domainTags = sqlTags.ToDomain();

            return domainTags.ToList();
        }

        public async Task Update(DomainModel.Tag domainTag)
        {
            if (domainTag != null && await this.Get(domainTag.Id) != null)
            {
                await _tagRepository.Update(domainTag.ToSql());
            }
        }
    }
}
