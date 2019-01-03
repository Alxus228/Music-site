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
    public class CommentService : IService<DomainModel.Comment>
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task Create(DomainModel.Comment domainComment)
        {
            if (domainComment != null)
            {
                await _commentRepository.Create(domainComment.ToSql());
            }
        }

        public async Task Delete(int? id)
        {
            if (await this.Get(id) != null)
            {
                await _commentRepository.DeleteById((int)id);
            }
        }

        public async Task<DomainModel.Comment> Get(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var sqlComment = await _commentRepository.GetById((int)id);
            if (sqlComment == null)
            {
                return null;
            }

            return sqlComment.ToDomain();
        }

        public async Task<List<DomainModel.Comment>> GetAll()
        {
            var sqlComments = await _commentRepository.GetAll();
            if (sqlComments == null)
            {
                return null;
            }

            var domainComments = sqlComments.ToDomain();

            return domainComments.ToList();
        }

        public async Task Update(DomainModel.Comment domainComment)
        {
            if (domainComment != null && await this.Get(domainComment.Id) != null)
            {
                await _commentRepository.Update(domainComment.ToSql());
            }
        }
    }
}
