using InclusiveCity.Contracts.Dto;
using MediatR;

namespace InclusiveCity.Application.Features.Commands.AddReview
{
    public class AddReviewCommand : IRequest
    {
        public int OsmId { get; set; }

        public string OsmType { get; set; }

        public string Comment { get; set; }

        public string? PhotoUrl { get; set; }

        public Guid CreatedBy { get; set; }

        public string Username { get; set; }
        
        public double Rate { get; set; }
    }
}
