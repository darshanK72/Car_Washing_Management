using CarWashAPI.Model;

namespace CarWashAPI.Interface
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task<IEnumerable<Review>> GetReviewsByUserIdAsync(int UserId);
        Task<Review> GetReviewsByOrderIdAsync(int OrderID);
        Task<IEnumerable<Review>> GetReviewsByWasherIdAsync(int washerId);
        Task<Review> AddReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(int reviewId);
    }
}
