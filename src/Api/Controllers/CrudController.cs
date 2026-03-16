using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Services;
using Domain.Types;

[ApiController]
[Route("api/[controller]")]
public class CrudController<TEntity, TDto> : ControllerBase where TEntity : IEntity
{
    protected readonly IService<TEntity> _service;

    public CrudController(IService<TEntity> service)
    {
        _service = service;
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAll();

        if (!result.IsSuccess)
            return BadRequest(result.Error?.Message);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> Get(Guid id)
    {
        var result = await _service.Get(id);

        if (!result.IsSuccess)
            return NotFound(result.Error?.Message);

        return Ok(result.Value);
    }

    [HttpPost]
    [Authorize]
    public virtual async Task<IActionResult> Create([FromBody] TEntity entity)
    {
        var result = await _service.Create(entity);

        if (!result.IsSuccess)
            return BadRequest(result.Error?.Message);

        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize]
    public virtual async Task<IActionResult> Update(Guid id, [FromBody] TEntity entity)
    {
        var result = await _service.Update(entity);

        if (!result.IsSuccess)
            return BadRequest(result.Error?.Message);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        var result = await _service.Delete(id);

        if (!result.IsSuccess)
            return BadRequest(result.Error?.Message);

        return Ok();
    }
}