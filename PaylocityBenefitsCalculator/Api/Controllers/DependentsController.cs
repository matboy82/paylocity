using Data.Dtos.Dependent;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Data.Controllers;

/// <summary>
/// Dependents Controller, normally would go through a Service and not direct access a repo, but logic is minimal and just in case
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IRepository<GetDependentDto> _repository;

	public DependentsController(IRepository<GetDependentDto> repository)
    {
		_repository = repository;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var dependent = await _repository.GetAsync(id);

		if (dependent == null)
		{
			return NotFound("Dependent with ID " + id + " not found");
		}
        return Ok(dependent);
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dependents = await _repository.GetAllAsync();
		return Ok(dependents);
    }
}
