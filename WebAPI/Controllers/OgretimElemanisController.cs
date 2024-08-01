
using Business.Handlers.OgretimElemanis.Commands;
using Business.Handlers.OgretimElemanis.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using Entities.Dtos;

namespace WebAPI.Controllers
{
    /// <summary>
    /// OgretimElemanis If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OgretimElemanisController : BaseApiController
    {
        ///<summary>
        ///List OgretimElemanis
        ///</summary>
        ///<remarks>OgretimElemanis</remarks>
        ///<return>List OgretimElemanis</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OgretimElemani>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetOgretimElemanisQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OzlukBilgileriDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getOgretimElemaniOzlukBilgileri")]
        public async Task<IActionResult> GetOgretimElemaniOzlukBilgileri(int id)
        {
            var result = await Mediator.Send(new GetOgretimElemaniOzlukBilgileriQuery {userId=id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MufredatDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getOgretimElemaniMufredat")]
        public async Task<IActionResult> GetOgretimElemaniMufredat(int id)
        {
            var result = await Mediator.Send(new GetOgretimElemaniMufredatQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OzlukBilgileriDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getOgretimElemaniVerilenDersler")]
        public async Task<IActionResult> GetOgretimElemaniVerilenDersler(int id)
        {
            var result = await Mediator.Send(new GetOgretimElemaniVerilenDerslerQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OgretimElemaniSınavlarDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getOgretimElemaniSinavlar")]
        public async Task<IActionResult> GetOgretimElemaniSinavlar(int id)
        {
            var result = await Mediator.Send(new GetOgretimElemaniSinavlarQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DegerlendirmeDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getOgrenciler")]
        public async Task<IActionResult> GetOgrenciler(int id)
        {
            var result = await Mediator.Send(new GetOgrencilerQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>OgretimElemanis</remarks>
        ///<return>OgretimElemanis List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OgretimElemani))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetOgretimElemaniQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add OgretimElemani.
        /// </summary>
        /// <param name="createOgretimElemani"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOgretimElemaniCommand createOgretimElemani)
        {
            var result = await Mediator.Send(createOgretimElemani);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update OgretimElemani.
        /// </summary>
        /// <param name="updateOgretimElemani"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOgretimElemaniCommand updateOgretimElemani)
        {
            var result = await Mediator.Send(updateOgretimElemani);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete OgretimElemani.
        /// </summary>
        /// <param name="deleteOgretimElemani"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteOgretimElemaniCommand deleteOgretimElemani)
        {
            var result = await Mediator.Send(deleteOgretimElemani);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
