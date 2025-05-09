using AutoMapper;
using InclusiveCity.Application.Features.Commands.AddReview;
using InclusiveCity.Application.Features.Queries.GetStructures;
using InclusiveCity.Contracts.Dto;
using InclusiveCity.Domain.Entities;
using InclusiveCity.Domain.Enums;

namespace InclusiveCity.Application
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OsmReview, ReviewDto>()
                .ForMember(dest => dest.OsmType, opt => opt.MapFrom(src => (OsmType)src.OsmTypeId));

            CreateMap<AddReviewCommand, OsmReview>();

            CreateMap<GetStructuresQuery, OverpassRequestDto>();
        }
    }
}
