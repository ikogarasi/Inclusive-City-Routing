using Asp.Versioning;
using InclusiveCity.Application.Features.Commands.AddReview;
using InclusiveCity.Application.Features.Queries.DeleteReview;
using InclusiveCity.Application.Features.Queries.GetObjectReviews;
using InclusiveCity.Application.Features.Queries.GetUsersReviews;
using InclusiveCity.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InclusiveCity.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ReviewController : ControllerApiBase
    {
        [HttpGet("osm/{osmId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetObjectReviews(long osmId)
        {
            return Ok(await Mediator.Send(new GetObjectReviewsQuery
            {
                OsmId = osmId
            }));
        }

        //[HttpGet("user/{userId}")]
        //public async Task<ActionResult<IEnumerable<ReviewDto>>> GetUsersReviews(Guid userId)
        //{
        //    return Ok(await Mediator.Send(new GetUsersReviewsQuery
        //    {
        //        UserId = userId
        //    }));
        //}

        [HttpPost]
        public async Task<ActionResult> AddReview([FromBody] AddReviewCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteReview(int reviewId)
        {
            await Mediator.Send(new DeleteReviewQuery
            {
                ReviewId = reviewId
            });

            return Ok();
        }
    }
}
