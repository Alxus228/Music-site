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
    [RoutePrefix("tagsInTracks")]
    [Authorize(Roles = "Admin,Moderator")]
    public class TagsInTracksController : ApiController
    {
        private readonly ITrackService _trackService;
        private readonly IService<DomainModel.Tag> _tagService;

        public TagsInTracksController(ITrackService trackService, IService<DomainModel.Tag> tagService)
        {
            _trackService = trackService;
            _tagService = tagService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IHttpActionResult> AddRelation([FromUri]int? id, [FromBody]int? tagId)
        {
            var domainTrack = await _trackService.Get(id);
            var domainTag = await _tagService.Get(tagId);

            if (domainTag == null || domainTrack == null || await _trackService.IsExistTagTrackRelation(domainTrack, domainTag))
            {
                return BadRequest();
            }

            await _trackService.AddTagTrackRelation(domainTrack, domainTag);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IHttpActionResult> RemoveRelation([FromUri]int? id, [FromBody]int? tagId)
        {
            var domainTrack = await _trackService.Get(id);
            var domainTag = await _tagService.Get(tagId);

            if (domainTag == null || domainTrack == null || !(await _trackService.IsExistTagTrackRelation(domainTrack, domainTag)))
            {
                return NotFound();
            }

            await _trackService.RemoveTagTrackRelation(domainTrack, domainTag);

            return Ok();
        }
    }
}
