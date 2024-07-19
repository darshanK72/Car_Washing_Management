using CarWash2.DTO;
using CarWashAPI.Interface;
using CarWashAPI.Model;
using Microsoft.AspNetCore.Mvc;

[Route("api/[Controller]")]
[ApiController]
public class AdminsController : ControllerBase
{
    private readonly IAdminRepository _adminRepository;

    public AdminsController(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
    }

    [HttpGet("orders")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        try
        {
            var orders = await _adminRepository.GetAllOrdersAsync();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error" + ex.Message);
        }
    }

    //[HttpGet("report")]
    //public async Task<ActionResult<string>> GenerateReport([FromQuery] string orderNumber, [FromQuery] string washerName, [FromQuery] string type, [FromQuery] string service, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    //{
    //    try
    //    {
    //        var report = await _adminRepository.G(orderNumber, washerName, type, service, startDate, endDate);
    //        return Ok(report);
    //    }
    //    catch (Exception)
    //    {
    //        return StatusCode(500, "Internal server error");
    //    }
    //}

    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        try
        {
            var users = await _adminRepository.GetUsersAsync();
            return Ok(users);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("users/{userId}/status")]
    public async Task<ActionResult> UpdateUserStatus(int userId, [FromBody] bool isActive)
    {
        try
        {
            var result = await _adminRepository.UpdateUserStatusAsync(userId, isActive);
            if (result)
                return Ok();
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("user/{userId}/orders")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllUserOrders(int userId)
    {
        try
        {
            var orders = await _adminRepository.GetAllUserOrders(userId);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error" + ex.Message);
        }
    }

    [HttpGet("washers")]
    public async Task<ActionResult<IEnumerable<Washer>>> GetWashers()
    {
        try
        {
            var washers = await _adminRepository.GetWashersAsync();
            return Ok(washers);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("washer/{washerId}/orders")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllWasherOrders(int washerId)
    {
        try
        {
            var orders = await _adminRepository.GetAllWasherOrders(washerId);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error" + ex.Message);
        }
    }


    [HttpPost("washers")]
    public async Task<ActionResult<Washer>> AddWasher([FromBody] Washer washer)
    {
        try
        {
            var newWasher = await _adminRepository.AddWasherAsync(washer);
            return CreatedAtAction(nameof(GetWashers), new { id = newWasher.WasherId }, newWasher);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("washers/{washerId}")]
    public async Task<ActionResult<Washer>> UpdateWasher(int washerId, [FromBody] Washer washer)
    {
        if (washerId != washer.WasherId)
            return BadRequest();

        try
        {
            var updatedWasher = await _adminRepository.UpdateWasherAsync(washer);
            return Ok(updatedWasher);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("washers/{washerId}/status")]
    public async Task<ActionResult> UpdateWasherStatus(int washerId, [FromBody] bool isActive)
    {
        try
        {
            var result = await _adminRepository.UpdateWasherStatusAsync(washerId, isActive);
            if (result)
                return Ok();
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("washers/{washerId}/reviews")]
    public async Task<ActionResult<IEnumerable<Review>>> GetWasherReviews(int washerId)
    {
        try
        {
            var reviews = await _adminRepository.GetWasherReviewsAsync(washerId);
            return Ok(reviews);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("user/{userId}/reviews")]
    public async Task<ActionResult<IEnumerable<Review>>> GetUserReviews(int userId)
    {
        try
        {
            var reviews = await _adminRepository.GetUserReviewsAsync(userId);
            return Ok(reviews);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("washers/{washerId}/report")]
    public async Task<ActionResult<string>> ExportWasherReport(int washerId)
    {
        try
        {
            var report = await _adminRepository.ExportWasherReportAsync(washerId);
            return Ok(report);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("orders/filtered")]
    public async Task<ActionResult<IEnumerable<Order>>> GetFilteredOrders([FromQuery] string status)
    {
        try
        {
            var orders = await _adminRepository.GetFilteredOrdersAsync(status);
            return Ok(orders);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("orders/{orderId}/payment")]
    public async Task<ActionResult<Payment>> GetPaymentDetails(int orderId)
    {
        try
        {
            var payment = await _adminRepository.GetPaymentByOrderIdAsync(orderId);
            if (payment == null) return NotFound();
            return Ok(payment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error" + ex.Message);
        }
    }

    [HttpGet("orders/{orderId}/receipt")]
    public async Task<ActionResult<Receipt>> GetReceiptDetails(int orderId)
    {
        try
        {
            var receipt = await _adminRepository.GetReceiptByOrderIdAsync(orderId);
            if (receipt == null) return NotFound();
            return Ok(receipt);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error" + ex.Message);
        }
    }

    [HttpGet("orders/{orderId}/review")]
    public async Task<ActionResult<Review>> GetReviewDetails(int orderId)
    {
        try
        {
            var review = await _adminRepository.GetReviewByOrderIdAsync(orderId);
            if (review == null) return NotFound();
            return Ok(review);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error" + ex.Message);
        }
    }

    [HttpGet("leaderboard")]
    public async Task<ActionResult<IEnumerable<Washer>>> GetLeaderboard()
    {
        try
        {
            var leaderboard = await _adminRepository.GetLeaderboardAsync();
            return Ok(leaderboard);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }


    [HttpGet("report")]
    public async Task<IActionResult> GetWasherReport( [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var report = await _adminRepository.GenerateReportsAsync(startDate, endDate);
        if (report == null)
        {
            return NotFound("Washer not found");
        }
        return Ok(report);
    }

    [HttpPut("orders/{orderId}/assign-washer/{washerId}")]
    public async Task<ActionResult> AssignWasherToOrder(int orderId, int washerId)
    {
        try
        {
            var result = await _adminRepository.AssignWasherToOrder(orderId, washerId);
            if (result)
                return Ok();
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error" + ex.Message);
        }
    }

}