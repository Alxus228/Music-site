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
    [RoutePrefix("api/Account")]
    public class AccountsController : ApiController
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register([FromBody]DomainModel.User domainUser)
        {
            domainUser.RoleType = DomainModel.Role.Regular;

            if (domainUser == null || 
               !domainUser.IsValid() ||
               (await _userService.GetAll()).FirstOrDefault(user => user.Email == domainUser.Email) != null) // MUST HAVE UNIQUE EMAIL
            {
                return BadRequest();
            }

            try
            {
                await _userService.Create(domainUser);
            }
            catch
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("DownCastUser/{id}")]
        public async Task<IHttpActionResult> DownCastUser([FromUri]int? id)
        {
            var domainUser = await _userService.Get(id);

            if(domainUser == null)
            {
                return NotFound();
            }

            if(domainUser.RoleType != DomainModel.Role.Regular && domainUser.RoleType != DomainModel.Role.Admin)
            {
                domainUser.RoleType = (DomainModel.Role)((int)domainUser.RoleType - 1);
            }
            else
            {
                return BadRequest();
            }
            
            try
            {
                await _userService.Update(domainUser);
            }
            catch
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("BoostUser/{id}")]
        public async Task<IHttpActionResult> BoostUser([FromUri]int? id)
        {
            var domainUser = await _userService.Get(id);

            if (domainUser == null)
            {
                return NotFound();
            }

            if (domainUser.RoleType == DomainModel.Role.Admin)
            {
                return BadRequest();
            }

            domainUser.RoleType = (DomainModel.Role)((int)domainUser.RoleType - 1);

            try
            {
                await _userService.Update(domainUser);
            }
            catch
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllUsers")]
        public async Task<IHttpActionResult> GetAll()
        {
            var domainUsers = await _userService.GetAll();

            return Ok(await domainUsers.Include());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetUserById/{id}")]
        public async Task<IHttpActionResult> Get(int? id)
        {
            var domainUser = await _userService.Get(id);

            if (domainUser == null)
            {
                return NotFound();
            }

            return Ok(await domainUser.Include());
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateUser")]
        public async Task<IHttpActionResult> Update([FromBody] DomainModel.User domainUser)
        {
            var oldDomainUser = await _userService.Get(domainUser.Id);

            if (domainUser == null || oldDomainUser == null)
            {
                return NotFound();
            }

            var aunthEmail = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            if (aunthEmail != oldDomainUser.Email)
            {
                return Unauthorized();
            }

            if (!domainUser.IsValid())
            {
                return BadRequest();
            }

            try
            {
                await _userService.Update(domainUser);
            }
            catch
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("BanUser/{id}")]
        public async Task<IHttpActionResult> BanUser(int? id)
        {
            var domainUser = await _userService.Get(id);
            if (domainUser == null)
            {
                return NotFound();
            }

            if(domainUser.RoleType != DomainModel.Role.Regular)
            {
                return BadRequest();
            }

            await _userService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetBanList")]
        public async Task<IHttpActionResult> GetBanList()
        {
            var domainBannedUsers = await _userService.GetBanList();

            return Ok(await domainBannedUsers.Include());
        }
    }
}