using CarWashAPI.Interface;
using CarWashAPI.Model;
using Microsoft.EntityFrameworkCore;
using MimeKit.Encodings;

namespace CarWashAPI.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            return await _context.Reviews.FindAsync(reviewId);
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(int UserId)
        {
            return await _context.Reviews.Where(r => r.UserId == UserId).ToListAsync();
        }

        public async Task<Review> GetReviewsByOrderIdAsync(int OrderId)
        {
            return await _context.Reviews.Where(r => r.OrderId == OrderId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByWasherIdAsync(int washerId)
        {
            return await _context.Reviews.Where(r => r.WasherId == washerId).ToListAsync();
        }

        public async Task<Review> AddReviewAsync(Review review)
        {

            var order = await _context.Orders.FindAsync(review.OrderId);
            var washer = await _context.Washers.FindAsync(order.WasherId);


            review.Order = order;
            review.Washer = washer;
            review.WasherId = washer.WasherId;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review> UpdateReviewAsync(Review review)
        {
            _context.Entry(review).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(review.ReviewId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return review;
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(e => e.ReviewId == reviewId);
        }
    }
}
