using InclusiveCity.Domain.Entities;

namespace InclusiveCity.Domain.Repositories
{
    public interface IReviewRepository
    {
        Task AddReview(OsmReview osmReview);
        Task DeleteReview(int reviewId);
        Task<OsmReview?> GetReviewById(int reviewId);
        Task<double> GetAverageRateFromReviews(int osmId);
        Task<IEnumerable<OsmReview>> GetReviewsByObjectId(int osmId);
        Task<IEnumerable<OsmReview>> GetReviewsByUserId(Guid userId);
    }
}