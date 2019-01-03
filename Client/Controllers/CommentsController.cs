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
    [RoutePrefix("comments")]
    public class CommentsController : ApiController
    {
        private readonly IService<DomainModel.Comment> _commentService;
        private readonly IService<DomainModel.User> _userService;

        public CommentsController(IService<DomainModel.Comment> commentService, IService<DomainModel.User> userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Get(int? id)
        {
            var domainComment = await _commentService.Get(id);

            if(domainComment == null)
            {
                return NotFound();
            }

            return Ok(await domainComment.Include());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAll()
        {
            var domainComments = await _commentService.GetAll();

            return Ok(await domainComments.Include());
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Create([FromBody] DomainModel.Comment domainComment)
        {
            if(domainComment == null || !domainComment.IsValid())
            {
                return BadRequest();
            }

            var aunthEmail = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            domainComment.UserIdWhoCreated = (await _userService.GetAll()).FirstOrDefault(user => user.Email == aunthEmail).Id;

            try
            {
                await _commentService.Create(domainComment);
            }
            catch
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> Update([FromBody] DomainModel.Comment domainComment)
        {
            if (domainComment == null || !domainComment.IsValid())
            {
                return BadRequest();
            }

            var oldDomainComment = await _commentService.Get(domainComment.Id);

            var comUserEmail = (await oldDomainComment.Include()).UserWhoCreated.Email;
            var aunthEmail = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            if (aunthEmail != comUserEmail)
            {
                return Unauthorized();
            }

            if (oldDomainComment == null)
            {
                return NotFound();
            }

            try
            {
                await _commentService.Update(domainComment);
            }
            catch
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize]       
        public async Task<IHttpActionResult> Delete(int? id)
        {
            var domainComment = await _commentService.Get(id);

            if (domainComment == null)
            {
                return NotFound();
            }

            var aunthCliams = ((ClaimsIdentity)User.Identity).Claims;
            var aunthRole = aunthCliams.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (!(aunthRole == "Admin" || aunthRole == "Moderator"))
            {
                var comUserEmail = (await domainComment.Include()).UserWhoCreated.Email;
                var aunthEmail = aunthCliams.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                if (aunthEmail != comUserEmail)
                {
                    return Unauthorized();
                }
            }

            await _commentService.Delete(id);
            return Ok();
        }
    }
}
