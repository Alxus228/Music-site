using MusicServices.APITools.Interfaces;
using MusicServices.APITools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Client.Controllers
{
    [RoutePrefix("favorite")]
    [Authorize]
    public class FavoriteController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IService<DomainModel.Track> _trackService;

        public FavoriteController(IUserService userService, IService<DomainModel.Track> trackService)
        {
            _userService = userService;
            _trackService = trackService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> AddRelation([FromUri]int? id, [FromBody]int? trackId)
        {
            var domainUser = await _userService.Get(id);

            var aunthEmail = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            if (aunthEmail != domainUser.Email)
            {
                return Unauthorized();
            }

            var domainTrack = await _trackService.Get(trackId);

            if(domainUser == null || domainTrack == null || await _userService.IsExistFavoriteRelation(domainUser, domainTrack))
            {
                return BadRequest();
            }

            await _userService.AddFavoriteRelation(domainUser, domainTrack);

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IHttpActionResult> RemoveRelation([FromUri]int? id, [FromBody]int? trackId)
        {
            var domainUser = await _userService.Get(id);

            var aunthEmail = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            if (aunthEmail != domainUser.Email)
            {
                return Unauthorized();
            }

            var domainTrack = await _trackService.Get(trackId);

            if (domainUser == null || domainTrack == null || !(await _userService.IsExistFavoriteRelation(domainUser, domainTrack)))
            {
                return NotFound();
            }

            await _userService.RemoveFavoriteRelation(domainUser, domainTrack);

            return Ok();
        }
    }
}
