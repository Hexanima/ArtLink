using System.IO;
using Domain.Types;

namespace Domain.Services;
public interface IFileService
{
    Task<OperationResult<string>> GetPresignedUrl(string fileName, string contentType);
}