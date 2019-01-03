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
    [RoutePrefix("tracks")]
    public class TracksController : ApiController
    {
        private readonly ITrackService _trackService;

        public TracksController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Get(int? id)
        {
            var domainTrack = await _trackService.Get(id);

            if (domainTrack == null)
            {
                return NotFound();
            }

            return Ok(await domainTrack.Include());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAll()
        {
            var domainTracks = await _trackService.GetAll();

            return Ok(domainTracks);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IHttpActionResult> Create([FromBody] DomainModel.Track domainTrack)
        {
            if (domainTrack == null || !domainTrack.IsValid())
            {
                return BadRequest();
            }

            if (await _trackService.IsDuplicate(domainTrack.Name, domainTrack.Author))
            {
                return BadRequest("Duplicate");
            }
            try
            {
                await _trackService.Create(domainTrack);
            }
            catch
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IHttpActionResult> Update([FromBody] DomainModel.Track domainTrack)
        {
            if (domainTrack == null || await _trackService.Get(domainTrack.Id) == null)
            {
                return NotFound();
            }

            if (!domainTrack.IsValid())
            {
                return BadRequest();
            }

            try
            {
                await _trackService.Update(domainTrack);
            }
            catch
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Delete(int? id)
        {
            if (await _trackService.Get(id) == null)
            {
                return NotFound();
            }

            await _trackService.Delete(id);
            return Ok();
        }
    }
}
