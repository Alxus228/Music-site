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
    [RoutePrefix("genres")]
    public class GenresController : ApiController
    {
        private readonly IService<DomainModel.Genre> _genreService;

        public GenresController(IService<DomainModel.Genre> genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Get(int? id)
        {
            var domainGenre = await _genreService.Get(id);

            if (domainGenre == null)
            {
                return NotFound();
            }

            return Ok(await domainGenre.Include());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAll()
        {
            var domainGenres = await _genreService.GetAll();

            return Ok(await domainGenres.Include());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Create([FromBody] DomainModel.Genre domainGenre)
        {
            if (domainGenre == null || !domainGenre.IsValid())
            {
                return BadRequest();
            }

            try
            {
                await _genreService.Create(domainGenre);
            }
            catch
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Update([FromBody] DomainModel.Genre domainGenre)
        {
            if (domainGenre == null || await _genreService.Get(domainGenre.Id) == null)
            {
                return NotFound();
            }

            if (!domainGenre.IsValid())
            {
                return BadRequest();
            }

            try
            {
                await _genreService.Update(domainGenre);
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
            if (await _genreService.Get(id) == null)
            {
                return NotFound();
            }

            await _genreService.Delete(id);
            return Ok();
        }
    }
}
