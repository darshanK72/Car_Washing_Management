using CarWashAPI.Interface;
using CarWashAPI.Model;
using CarWashAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarWashAPI.Repository;

namespace CarWash2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailRepository _emailRepository;

        public OrdersController(IOrderRepository orderRepository,IReviewRepository reviewRepository, IUserRepository userRepository,IEmailRepository emailRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _emailRepository = emailRepository;
        }

        private Order MapDtoToModel(OrderDTO orderDto)
        {
            return new Order
            {
                UserId = orderDto.UserId,
                WasherId = orderDto.WasherId,
                CarId = orderDto.CarId,
                PackageId = orderDto.PackageId,
                Status = orderDto.Status,
                ScheduledDate = orderDto.ScheduledDate,
                ActualWashDate = orderDto.ActualWashDate,
                TotalPrice = orderDto.TotalPrice,
                ReceiptId = orderDto.ReceiptId,
                Notes = orderDto.Notes
            };
        }

        [HttpPost("complete-payment")]
        public async Task<ActionResult<Order>> CompletePayment(PaymentDTO paymentDTO)
        {
            try
            {
                var result = await _orderRepository.CompletePaymentAsync(paymentDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            try
            {
                var orders = await _orderRepository.GetAllOrdersAsync();

                var orderDTOs = new List<OrderDTO>();
                foreach (var order in orders)
                {
                    var orderDto = new OrderDTO
                    {
                        OrderId = order.OrderId,
                        UserId = order.UserId,
                        WasherId = order.WasherId,
                        CarId = order.CarId,
                        PackageId = order.PackageId,
                        Status = order.Status,
                        ScheduledDate = order.ScheduledDate,
                        ActualWashDate = order.ActualWashDate,
                        TotalPrice = order.TotalPrice,
                        ReceiptId = order.ReceiptId,
                        Notes = order.Notes
                    };

                    var review = await _reviewRepository.GetReviewsByOrderIdAsync(order.OrderId);
                    if(review != null)
                    {
                        orderDto.ReviewId = review.ReviewId;
                    }
                    orderDTOs.Add(orderDto);
                }

                return Ok(orderDTOs);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("user/receipt/{UserId}")]
        public async Task<ActionResult<Car>> GetReceiptsByUserId(int UserId)
        {
            try
            {
                var car = await _orderRepository.GetReciptsByIdAsync(UserId);
                if (car == null)
                {
                    return NotFound();
                }
                return Ok(car);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("userOrder/{UserId}")]
        public async Task<ActionResult<Order>> GetOrdersByUserId(int UserId)
        {
            try
            {
                var order = await _orderRepository.GetOrdersByUserId(UserId);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("user/{OrderId}")]
        public async Task<ActionResult<Order>> GetUserByOrderId(int OrderId)
        {
            try
            {
                var user = await _userRepository.GetUserByOrderIdAsync(OrderId);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("leaderboard")]
        public async Task<ActionResult<List<User>>> GetUsersSortedByOrders()
        {
            var users = await _orderRepository.GetUsersSortedByOrdersAsync();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                // Map Order to OrderDTO
                var orderDTO = new OrderDTO
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    WasherId = order.WasherId,
                    CarId = order.CarId,
                    PackageId = order.PackageId,
                    Status = order.Status,
                    ScheduledDate = order.ScheduledDate,
                    ActualWashDate = order.ActualWashDate,
                    TotalPrice = order.TotalPrice,
                    ReceiptId = order.ReceiptId,
                    Notes = order.Notes
                };

                return Ok(orderDTO);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("place-order")]
        public async Task<ActionResult<OrderDTO>> PlaceOrder(PlaceOrderDTO placeOrderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var package = await _orderRepository.GetPackageByIdAsync(placeOrderDto.PackageId);
                if (package == null)
                {
                    return BadRequest("Invalid package ID");
                }

                var receipt = await _orderRepository.CreateReceiptAsync(package.Price);

                var createdOrder = await _orderRepository.PlaceOrderAsync(placeOrderDto, receipt.ReceiptId);

                var orderDto = new OrderDTO
                {
                    OrderId = createdOrder.OrderId,
                    UserId = createdOrder.UserId,
                    WasherId = createdOrder.WasherId,
                    CarId = createdOrder.CarId,
                    PackageId = createdOrder.PackageId,
                    Status = createdOrder.Status,
                    ScheduledDate = createdOrder.ScheduledDate,
                    ActualWashDate = createdOrder.ActualWashDate,
                    TotalPrice = createdOrder.TotalPrice,
                    ReceiptId = createdOrder.ReceiptId,
                    Notes = createdOrder.Notes
                };

                return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.OrderId }, orderDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDTO orderDto)
        {
            if (id != orderDto.OrderId)
            {
                return BadRequest();
            }

            try
            {
                var order = MapDtoToModel(orderDto);
                var updatedOrder = await _orderRepository.UpdateOrderAsync(order);
                if (updatedOrder == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var result = await _orderRepository.DeleteOrderAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
