using AutoMapper;
using InclusiveCity.Application.Dto;
using InclusiveCity.Application.Features.Commands.AddReview;
using InclusiveCity.Application.Features.Queries.GetInclusiveInfrastructure;
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
            CreateMap<ReviewDto, OsmReview>()
                .ForMember(dest => dest.OsmTypeId, opt => opt.MapFrom(src => (int)Enum.Parse(typeof(OsmType), src.OsmType)));
            CreateMap<AddReviewCommand, OsmReview>();

            CreateMap<OverpassResponseDto, GetStructuresDto>();
            CreateMap<OverpassElementDto, ElementDto>();
            CreateMap<GetStructuresQuery, OverpassRequestDto>();
            CreateMap<GetInclusiveInfrastructureQuery, InclusiveInfrastructureRequestDto>();
        }
    }
}
