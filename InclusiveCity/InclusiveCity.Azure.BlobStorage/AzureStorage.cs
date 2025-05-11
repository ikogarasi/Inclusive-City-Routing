using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using InclusiveCity.Azure.BlobStorage.Dto;
using InclusiveCity.Azure.BlobStorage.Enums;
using Microsoft.Extensions.Configuration;

namespace InclusiveCity.Azure.BlobStorage
{
    public class AzureStorage : IAzureStorage
    {
        private readonly string _storageConnectionString;
        private readonly string _storageStructureImagesContainerName;
        private readonly string _storageReviewsImagesContainerName;

        public AzureStorage(IConfiguration configuration)
        {
            _storageConnectionString = configuration["BlobConnectionString"]!.ToString();
            _storageStructureImagesContainerName = configuration["BlobStructureImagesContainerName"]!.ToString();
            _storageReviewsImagesContainerName = configuration["BlobReviewsImagesContainerName"]!.ToString();
        }

        public async Task<BlobResponseDto> UploadAsync(byte[] blob, string fileName, ContainerType containerType)
        {
            const string ContentType = "image/jpeg";

            var response = new BlobResponseDto();

            var storageContainer = _storageStructureImagesContainerName;

            if (containerType == ContainerType.Review)
            {
                storageContainer = _storageReviewsImagesContainerName;
            }

            var container = new BlobContainerClient(_storageConnectionString, storageContainer);

            try
            {
                var newFileName = Guid.NewGuid() + Path.GetExtension(fileName);

                var client = container.GetBlobClient(newFileName);

                var options = new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = ContentType
                    }
                };

                await using var data = new MemoryStream(blob);

                await client.UploadAsync(data, options);

                response.Status = $"File {fileName} Uploaded Successfully";
                response.Error = false;
                response.Blob.Uri = client.Uri.AbsoluteUri;
                response.Blob.Name = client.Name;
            }
            catch (RequestFailedException ex)
            {
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                response.Error = true;
                return response;
            }

            return response;
        }

        public async Task<BlobResponseDto> DeleteAsync(string blobFilename, ContainerType containerType)
        {
            var storageContainer = _storageStructureImagesContainerName;

            if (containerType == ContainerType.Review)
            {
                storageContainer = _storageReviewsImagesContainerName;
            }

            var client = new BlobContainerClient(_storageConnectionString, storageContainer);

            var file = client.GetBlobClient(blobFilename);

            try
            {
                await file.DeleteAsync();
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                return new() { Error = true, Status = $"File with name {blobFilename} not found." };
            }

            return new() { Error = false, Status = $"File: {blobFilename} has been successfully deleted." };
        }
    }
}