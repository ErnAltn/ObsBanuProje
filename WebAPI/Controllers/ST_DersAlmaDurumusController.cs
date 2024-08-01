
using Business.Handlers.ST_DersAlmaDurumus.Commands;
using Business.Handlers.ST_DersAlmaDurumus.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// ST_DersAlmaDurumus If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ST_DersAlmaDurumusController : BaseApiController
    {
        ///<summary>
        ///List ST_DersAlmaDurumus
        ///</summary>
        ///<remarks>ST_DersAlmaDurumus</remarks>
        ///<return>List ST_DersAlmaDurumus</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ST_DersAlmaDurumu>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetST_DersAlmaDurumusQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>ST_DersAlmaDurumus</remarks>
        ///<return>ST_DersAlmaDurumus List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ST_DersAlmaDurumu))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetST_DersAlmaDurumuQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add ST_DersAlmaDurumu.
        /// </summary>
        /// <param name="createST_DersAlmaDurumu"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateST_DersAlmaDurumuCommand createST_DersAlmaDurumu)
        {
            var result = await Mediator.Send(createST_DersAlmaDurumu);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update ST_DersAlmaDurumu.
        /// </summary>
        /// <param name="updateST_DersAlmaDurumu"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateST_DersAlmaDurumuCommand updateST_DersAlmaDurumu)
        {
            var result = await Mediator.Send(updateST_DersAlmaDurumu);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete ST_DersAlmaDurumu.
        /// </summary>
        /// <param name="deleteST_DersAlmaDurumu"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteST_DersAlmaDurumuCommand deleteST_DersAlmaDurumu)
        {
            var result = await Mediator.Send(deleteST_DersAlmaDurumu);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
