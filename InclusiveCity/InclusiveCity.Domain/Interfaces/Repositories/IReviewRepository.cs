using InclusiveCity.Domain.Entities;

namespace InclusiveCity.Domain.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        Task AddReview(OsmReview osmReview);
        Task DeleteReview(int reviewId);
        Task<OsmReview?> GetReviewById(int reviewId);
        Task<double> GetAverageRateFromReviews(long osmId);
        Task<IEnumerable<OsmReview>> GetReviewsByObjectId(long osmId);
        Task<IEnumerable<OsmReview>> GetReviewsByUserId(Guid userId);
        Task<Dictionary<long, List<OsmReview>>> GetReviewsForStructuresRange(IEnumerable<long> ids);
    }
}