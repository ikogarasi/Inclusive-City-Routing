using InclusiveCity.Application.Dto;
using MediatR;

namespace InclusiveCity.Application.Features.Commands.UploadStructureImages
{
    public class UploadStructureImagesCommand : IRequest
    {
        public long OsmId { get; set; }
        public List<ImageDto> Images { get; set; }
    }
}
