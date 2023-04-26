using API.Common;
using Application.Services.Pet.Adopt;
using Application.Services.Pet.Create;
using Application.Services.Pet.Delete;
using Application.Services.Pet.GetMany;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("pets")]
    public class PetController : BaseController
    {
        public PetController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// should create pet to adopt.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreatePetRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// should delete pet.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var request = new DeletePetRequest { Id = id, OwnerId = GetCurrentUserId() };
            var response = await _mediator.Send(request);
            return Ok(response);
        }


        /// <summary>
        /// should adopt pet.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Adopt([FromRoute] int id)
        {
            var request = new AdoptRequest { PetId = id, NewOwnerId = GetCurrentUserId() };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// should get pets.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetManyPetsRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
