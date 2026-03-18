using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using Domain.Types;
using Application.UseCases.Base.Interfaces;
using Api.Mappers;

namespace Api.Controllers.Base;

[ApiController]
public class CrudController<T, TDto> : ControllerBase where T : IEntity
{
    private readonly IGetAllUseCase<T> _getAll;
    private readonly IGetByIdUseCase<T> _getById;
    protected readonly ICreateUseCase<T> _create;
    protected readonly IUpdateUseCase<T> _update;
    private readonly IDeleteUseCase<T> _delete;
    protected readonly IMapper<TDto, T> _mapper;

    public CrudController(
        IGetAllUseCase<T> getAll,
        IGetByIdUseCase<T> getById,
        ICreateUseCase<T> create,
        IUpdateUseCase<T> update,
        IDeleteUseCase<T> delete,
        IMapper<TDto, T> mapper)
    {
        _getAll = getAll;
        _getById = getById;
        _create = create;
        _update = update;
        _delete = delete;
        _mapper = mapper;
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAll()
    {
        var result = await _getAll.Execute();
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error?.Message);
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetById(Guid id)
    {
        var result = await _getById.Execute(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error?.Message);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] TDto dto)
    {
        // Mapear DTO a Entidad usando el mapper inyectado
        var entity = _mapper.Map(dto);
        var result = await _create.Execute(entity);
        
        return result.IsSuccess 
            ? CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity) 
            : BadRequest(result.Error?.Message);
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(Guid id, [FromBody] TDto dto)
    {
        var entity = _mapper.Map(dto);
        entity.Id = id;
        
        var result = await _update.Execute(entity);
        return result.IsSuccess ? Ok(entity) : BadRequest(result.Error?.Message);
    }
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        var result = await _delete.Execute(id);
        return result.IsSuccess ? NoContent() : NotFound(result.Error?.Message);
    }
}
