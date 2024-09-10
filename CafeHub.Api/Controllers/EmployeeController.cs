using CafeHub.Application.Commands.Employee;
using CafeHub.Application.Queries.Employee;
using CafeHub.Application.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeHub.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator) => _mediator = mediator;

        // GET: api/Employee/{id}
        [HttpGet("Employee/{id}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployee(string id)
        {
            var query = new GetEmployeeQuery(id);
            var result = await _mediator.Send(query);

            return result == null ? NotFound() : Ok(result);
        }

        // GET: api/Employees
        [HttpGet("Employees")]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployees(string? cafe)
        {
            var query = new GetAllEmployeesQuery(cafe);
            var result = await _mediator.Send(query);

            return result == null ? NotFound() : Ok(result);
        }

        // POST: api/Employee
        [HttpPost("Employee")]
        public async Task<ActionResult<EmployeeModel>> PostEmployee(CreateEmployeeCommand command)
        {
            var insertedId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetEmployee), new { id = insertedId }, command);
        }

        // PUT: api/Employee/{id}
        [HttpPut("Employee/{id}")]
        public async Task<IActionResult> PutEmployee(string id, UpdateEmployeeCommand command)
        {
            if (id != command.Id) return BadRequest();

            await _mediator.Send(command);

            return NoContent();
        }


        // DELETE: api/Employee/{id}
        [HttpDelete("Employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var command = new DeleteEmployeeCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}