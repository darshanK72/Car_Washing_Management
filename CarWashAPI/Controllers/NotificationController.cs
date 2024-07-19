using CarWashAPI.Interface;
using CarWashAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarWashAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly IEmailRepository _emailRepository;

        public NotificationsController(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        [HttpPost("notify-user-wash-request")]
        public async Task<IActionResult> NotifyUserOnWashRequestResponse(string userEmail, string washerName, bool isAccepted)
        {
            await _emailRepository.NotifyUserOnWashRequestResponse(userEmail, washerName, isAccepted);
            return Ok();
        }

        [HttpPost("notify-user-service-update")]
        public async Task<IActionResult> NotifyUserOnServiceUpdate(string userEmail, string washerName, string status)
        {
            await _emailRepository.NotifyUserOnServiceUpdate(userEmail, washerName, status);
            return Ok();
        }

        [HttpPost("notify-user-receipt")]
        public async Task<IActionResult> NotifyUserOnReceipt(string userEmail, int receiptId)
        {
            await _emailRepository.NotifyUserOnReceipt(userEmail, receiptId);
            return Ok();
        }

        [HttpPost("notify-washer-scheduled-wash")]
        public async Task<IActionResult> NotifyWasherScheduledWash(string washerEmail, string orderDetails)
        {
            await _emailRepository.NotifyWasherScheduledWash(washerEmail, orderDetails);
            return Ok();
        }

        [HttpPost("notify-washer-new-order")]
        public async Task<IActionResult> NotifyWasherNewOrder(string washerEmail, int orderId)
        {
            await _emailRepository.NotifyWasherNewOrder(washerEmail, orderId);
            return Ok();
        }

        [HttpPost("notify-washer-payment-success")]
        public async Task<IActionResult> NotifyWasherPaymentSuccess(string washerEmail, int orderDetails)
        {
            await _emailRepository.NotifyWasherPaymentSuccess(washerEmail, orderDetails);
            return Ok();
        }
    }
}
