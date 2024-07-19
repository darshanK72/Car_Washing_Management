using CarWashAPI.Interface;
using CarWashAPI.Model;
using CarWash2.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CarWash2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
        }

        private Review MapDtoToModel(ReviewDto reviewDto)
        {
            return new Review
            {
                ReviewId = reviewDto.ReviewId,
                UserId = reviewDto.UserId,
                WasherId = reviewDto.WasherId,
                OrderId = reviewDto.OrderId,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment
            };
        }

        private ReviewDto MapModelToDto(Review review)
        {
            return new ReviewDto
            {
                ReviewId = review.ReviewId,
                UserId = review.UserId,
                WasherId = review.WasherId,
                OrderId = review.OrderId,
                Rating = review.Rating,
                Comment = review.Comment
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewRepository.GetAllReviewsAsync();

                // Map Review to ReviewDto
                var reviewDtos = new List<ReviewDto>();
                foreach (var review in reviews)
                {
                    reviewDtos.Add(MapModelToDto(review));
                }

                return Ok(reviewDtos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReviewById(int id)
        {
            try
            {
                var review = await _reviewRepository.GetReviewByIdAsync(id);
                if (review == null)
                {
                    return NotFound();
                }

                // Map Review to ReviewDto
                var reviewDto = MapModelToDto(review);

                return Ok(reviewDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("user/{UserId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByUserId(int UserId)
        {
            try
            {
                var reviews = await _reviewRepository.GetReviewsByUserIdAsync(UserId);

                // Map Review to ReviewDto
                var reviewDtos = new List<ReviewDto>();
                foreach (var review in reviews)
                {
                    reviewDtos.Add(MapModelToDto(review));
                }

                return Ok(reviewDtos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("order/{OrderId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByOrderId(int OrderId)
        {
            try
            {
                var reviews = await _reviewRepository.GetReviewsByOrderIdAsync(OrderId);

                return Ok(MapModelToDto(reviews));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("washer/{washerId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByWasherId(int washerId)
        {
            try
            {
                var reviews = await _reviewRepository.GetReviewsByWasherIdAsync(washerId);

                // Map Review to ReviewDto
                var reviewDtos = new List<ReviewDto>();
                foreach (var review in reviews)
                {
                    reviewDtos.Add(MapModelToDto(review));
                }

                return Ok(reviewDtos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> AddReview(ReviewDto reviewDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var review = MapDtoToModel(reviewDto);
                var createdReview = await _reviewRepository.AddReviewAsync(review);
                var createdReviewDto = MapModelToDto(createdReview);

                return CreatedAtAction(nameof(GetReviewById), new { id = createdReview.ReviewId }, createdReviewDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReviewDto>> UpdateReview(int id, ReviewDto reviewDto)
        {
            if (id != reviewDto.ReviewId)
            {
                return BadRequest("Review ID mismatch");
            }

            try
            {
                var review = MapDtoToModel(reviewDto);
                var updatedReview = await _reviewRepository.UpdateReviewAsync(review);
                if (updatedReview == null)
                {
                    return NotFound();
                }

                // Map Review to ReviewDto
                var updatedReviewDto = MapModelToDto(updatedReview);

                return Ok(updatedReviewDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteReview(int id)
        {
            var result = await _reviewRepository.DeleteReviewAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
