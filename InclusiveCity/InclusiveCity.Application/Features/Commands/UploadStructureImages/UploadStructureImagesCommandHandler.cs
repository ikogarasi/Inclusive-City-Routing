using InclusiveCity.Azure.BlobStorage;
using InclusiveCity.Azure.BlobStorage.Dto;
using InclusiveCity.Azure.BlobStorage.Enums;
using InclusiveCity.Domain.Interfaces.Repositories;
using MediatR;

namespace InclusiveCity.Application.Features.Commands.UploadStructureImages
{
    public class UploadStructureImagesCommandHandler(IAzureStorage _azureStorage, IStructureImageRepository _structureImageRepository) : IRequestHandler<UploadStructureImagesCommand>
    {
        public async Task Handle(UploadStructureImagesCommand request, CancellationToken cancellationToken)
        {
            var files = new List<string>();

            for (int i = 0; i < request.Images.Count; ++i)
            {
                byte[] byteImage = Convert.FromBase64String(request.Images[i].ImageBase64);
                BlobResponseDto response = await _azureStorage.UploadAsync(byteImage, $"structure-{request.OsmId}-{i}", ContainerType.Structure);
            
                if (response.Error || response.Blob == null)
                {
                    throw new InvalidOperationException(response.Status);
                }

                files.Add(response.Blob.Uri);
            }

            await _structureImageRepository.AddStructureImage(request.OsmId, files);
        }
    }
}
