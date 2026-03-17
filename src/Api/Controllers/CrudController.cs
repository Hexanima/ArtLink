using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Services;
using Domain.Types;

[ApiController]
public class CrudController<T, TDto> : ControllerBase where T : IEntity
{
     private readonly IGetByIdUseCase<T> _getById;
     private readonly IDeleteUseCase<T> _delete;

    public CrudController(IGetByIdUseCase<T> getById, IDeleteUseCase<T> delete)
    {
        _getById = getById;
        _delete = delete;

    }
    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetById(Guid id)
    {
        var result = await _getById.Execute(id);

        if (!result.IsSuccess)
            return NotFound(result.Error?.Message);

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        var result = await _delete.Execute(id);

        if (!result.IsSuccess)
            return NotFound(result.Error?.Message);

        return Ok(result.Value);
    }
 
}