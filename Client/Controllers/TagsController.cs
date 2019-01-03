using MusicServices.APITools.Interfaces;
using MusicServices.APITools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Client.Controllers
{
    [RoutePrefix("tags")]
    public class TagsController : ApiController
    {
        private readonly IService<DomainModel.Tag> _tagService;

        public TagsController(IService<DomainModel.Tag> tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Get(int? id)
        {
            var domainTag = await _tagService.Get(id);

            if (domainTag == null)
            {
                return NotFound();
            }

            return Ok(await domainTag.Include());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAll()
        {
            var domainTags = await _tagService.GetAll();

            return Ok(await domainTags.Include());
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IHttpActionResult> Create([FromBody] DomainModel.Tag domainTag)
        {
            if (domainTag == null || !domainTag.IsValid())
            {
                return BadRequest();
            }

            try
            {
                await _tagService.Create(domainTag);
            }
            catch
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IHttpActionResult> Update([FromBody] DomainModel.Tag domainTag)
        {
            if (domainTag == null || await _tagService.Get(domainTag.Id) == null)
            {
                return NotFound();
            }

            if (!domainTag.IsValid())
            {
                return BadRequest();
            }

            try
            {
                await _tagService.Update(domainTag);
            }
            catch
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IHttpActionResult> Delete(int? id)
        {
            if (await _tagService.Get(id) == null)
            {
                return NotFound();
            }

            await _tagService.Delete(id);
            return Ok();
        }
    }
}
