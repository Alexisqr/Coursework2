using MediatR;
using Microsoft.AspNetCore.Mvc;
using Practice.Application.Features.Commands.CreateAnimal;
using Practice.Application.Features.Commands.DeleteAnimal;
using Practice.Application.Features.Commands.UpdateAnimal;
using Practice.Application.Features.Queries.GetAllAnimal;
using Practice.Application.Features.Queries.GetByIdAnimal;
using Practice.Domain.Entities;
using System.Net;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnimalController : ControllerBase
    {

        private readonly IMediator _mediator;
        public AnimalController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }
        [HttpGet(Name = "GetAllAnimal")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAllAnimal()
        {
            var command = new GetAllAnimalQuery();
            var animals = await _mediator.Send(command);
            return Ok(animals);
        }
       
        [HttpGet("{id}", Name = "GetByIdAnimal")]
        public async Task<ActionResult<Animal>> GetAnimalById(Guid id)
        {
            var command = new GetByIdAnimalQuery() { ID = id };
            var animals = await _mediator.Send(command);
            return Ok(animals);
        }
        [HttpDelete("{id}", Name = "DeleteAnimal")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteAnimal(Guid id)
        {
            var command = new DeleteAnimalCommand() { ID = id };
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpPut(Name = "UpdateAnimal")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateAnimal([FromBody] UpdateAnimalCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        // testing purpose
        [HttpPost(Name = "CreateAnimal")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateAnimal([FromBody] CreateAnimalCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
