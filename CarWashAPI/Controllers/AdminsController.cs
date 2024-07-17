using CarWashAPI.Interface;
using CarWashAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashAPI.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        public AdminsController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
        }

        [HttpGet("orders")]
        public async Task<ActionResult<string>> GetAllOrders()
        {
            try
            {
                var orders = await _adminRepository.GetAllOrdersAsync();

                // Transform the orders to the desired format
                var formattedOrders = orders.Select(order =>
                    $"OrderId: {order.OrderId}\n" +
                    $"UserId: {order.UserId}\n" +
                    $"WasherId: {order.WasherId}\n" +
                    $"CarId: {order.CarId}\n" +
                    $"PackageId: {order.PackageId}\n" +
                    $"Status: {order.Status}\n" +
                    $"ScheduledDate: {order.ScheduledDate}\n" +
                    $"ActualWashDate: {order.ActualWashDate}\n" +
                    $"TotalPrice: {order.TotalPrice}\n" +
                    $"ReceiptId: {order.ReceiptId}\n" +
                    $"Notes: {order.Notes}\n"
                );

                var response = string.Join("\n", formattedOrders);

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("report")]
        public async Task<ActionResult<string>> GenerateReport()
        {
            try
            {
                var report = await _adminRepository.GenerateReportAsync();
                return Ok(report);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}






//using CarWashAPI.Interface;
//using CarWashAPI.Model;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CarWashAPI.Controllers
//{
//    [Route("api/admins")]
//    [ApiController]
//    public class AdminsController : ControllerBase
//    {
//        private readonly IAdminRepository _adminRepository;

//        public AdminsController(IAdminRepository adminRepository)
//        {
//            _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
//        }

//        [HttpGet("orders")]
//        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
//        {
//            try
//            {
//                var orders = await _adminRepository.GetAllOrdersAsync();
//                return Ok(orders);
//            }
//            catch (Exception)
//            {
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpGet("report")]
//        public async Task<ActionResult<string>> GenerateReport()
//        {
//            try
//            {
//                var report = await _adminRepository.GenerateReportAsync();
//                return Ok(report);
//            }
//            catch (Exception)
//            {
//                return StatusCode(500, "Internal server error");
//            }
//        }
//    }
//}
