using InclusiveCity.Azure.BlobStorage.Dto;
using InclusiveCity.Azure.BlobStorage.Enums;

namespace InclusiveCity.Azure.BlobStorage
{
    public interface IAzureStorage
    {
        Task<BlobResponseDto> DeleteAsync(string blobFilename, ContainerType containerType);
        Task<BlobResponseDto> UploadAsync(byte[] blob, string fileName, ContainerType containerType);
    }
}