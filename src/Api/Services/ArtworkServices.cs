using Application.UseCases;
using Domain.Entities;
using Domain.Services;


public class ArtworkServices :IService<Artwork>
{
    private readonly IUnitOfWork _unitOfWork;

    public ArtworkServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Delete(Guid id)
    {
        try
        {
            var createdEntity = await _unitOfWork.ArtworkRepository.FindAsync(id);
            
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success(createdEntity);
        }
        catch (Exception ex)
        {
            return new OperationResult(ex);
        }
    }
}