using CarWashAPI.Interface;
using CarWashAPI.Model;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Linq;
using System.Text;

namespace CarWashAPI.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public EmailRepository(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        private async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration["SmtpSettings:SenderName"], _configuration["SmtpSettings:SenderEmail"]));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_configuration["SmtpSettings:Server"], int.Parse(_configuration["SmtpSettings:Port"]), false);
                client.Authenticate(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]);
                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }

        public async Task NotifyUserOnWashRequestResponse(string userEmail, string washerName, bool isAccepted)
        {
            string subject = isAccepted ? "Wash Request Accepted" : "Wash Request Rejected";
            string body = $"<p>Dear User,</p><p>Your wash request has been by {washerName}.</p><p>Thank you for using our service!</p>";
            if(!isAccepted)
            {
                body += "<p> Do not wory, We will assign new washer for your request. Wait for our future response";
            }
            await SendEmailAsync(userEmail, subject, body);
        }

        public async Task NotifyUserOnServiceUpdate(string userEmail, string washerName, string status)
        {
            string subject = $"Wash Service {status}";
            string body = $"<p>Dear User,</p><p>Your wash service has {status.ToLower()} by {washerName}.</p><p>Thank you for using our service!</p>";
            await SendEmailAsync(userEmail, subject, body);
        }

        public async Task NotifyUserOnReceipt(string userEmail, int receiptId)
        {
            var receipt = await _context.Receipts.FirstOrDefaultAsync(r => r.ReceiptId == receiptId);
            var payment = await _context.Payments.FindAsync(receipt.ReceiptId);

            if (receipt == null)
            {
                throw new Exception("Receipt not found");
            }

            if (payment == null)
            {
                throw new Exception("Payment Details not found");
            }

            string subject = "Receipt Received For Your Car Wash";
            string body = GenerateReceiptEmailBody(receipt,payment);
            await SendEmailAsync(userEmail, subject, body);
        }

        private string GenerateReceiptEmailBody(Receipt receipt,Payment payment)
        {
            var sb = new StringBuilder();
            sb.Append("<p>Dear User,</p>");
            sb.Append("<p>Your receipt has been generated:</p>");
            sb.Append("<table border='1' style='width: 100%; border-collapse: collapse;'>");
            sb.Append("<tr><th>Receipt ID</th><td>").Append(receipt.ReceiptId).Append("</td></tr>");
            sb.Append("<tr><th>Washing Date</th><td>").Append(receipt.WashingDate.ToString("yyyy-MM-dd HH:mm")).Append("</td></tr>");
            sb.Append("<tr><th>Amount</th><td>").Append(receipt.Amount.ToString("C")).Append("</td></tr>");
            sb.Append("<tr><th>Payment Method</th><td>").Append(receipt.PaymentMethod ?? "N/A").Append("</td></tr>");
            sb.Append("<tr><th>Transaction ID</th><td>").Append(receipt.TransactionId).Append("</td></tr>");
            sb.Append("<tr><th>Status</th><td>").Append(receipt.Status).Append("</td></tr>");

            if (payment != null)
            {
                sb.Append("<tr><th>Payment ID</th><td>").Append(payment.PaymentId).Append("</td></tr>");
                sb.Append("<tr><th>Total Amount</th><td>").Append(payment.TotalAmount.ToString("C")).Append("</td></tr>");
                sb.Append("<tr><th>Payment Time</th><td>").Append(payment.PaymentTime.ToString("yyyy-MM-dd HH:mm")).Append("</td></tr>");
            }

            sb.Append("</table>");
            sb.Append("<p>Thank you for using our service!</p>");
            return sb.ToString();
        }


        public async Task NotifyWasherScheduledWash(string washerEmail, string orderDetails)
        {
            string subject = "Upcoming Scheduled Wash";
            string body = $"<p>Dear Washer,</p><p>You have a scheduled wash upcoming in 2 hours:</p><p>{orderDetails}</p><p>Please be prepared.</p>";
            await SendEmailAsync(washerEmail, subject, body);
        }

        public async Task NotifyWasherNewOrder(string washerEmail, int orderId)
        {
            var order = await _context.Orders
                                      .Include(o => o.User)
                                      .Include(o => o.Car)
                                      .Include(o => o.Package)
                                      .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            string subject = "New Wash Order";
            string body = GenerateOrderEmailBody(order);
            await SendEmailAsync(washerEmail, subject, body);
        }

        private string GenerateOrderEmailBody(Order order)
        {
            var sb = new StringBuilder();
            sb.Append("<p>Dear Washer,</p>");
            sb.Append("<p>You have received a new wash order:</p>");
            sb.Append("<table border='1' style='width: 100%; border-collapse: collapse;'>");
            sb.Append("<tr><th>Order ID</th><td>").Append(order.OrderId).Append("</td></tr>");
            sb.Append("<tr><th>Status</th><td>").Append(order.Status).Append("</td></tr>");
            sb.Append("<tr><th>Scheduled Date</th><td>").Append(order.ScheduledDate?.ToString("yyyy-MM-dd HH:mm")).Append("</td></tr>");
            sb.Append("<tr><th>Actual Wash Date</th><td>").Append(order.ActualWashDate?.ToString("yyyy-MM-dd HH:mm")).Append("</td></tr>");
            sb.Append("<tr><th>Total Price</th><td>").Append(order.TotalPrice.ToString("C")).Append("</td></tr>");
            sb.Append("<tr><th>Notes</th><td>").Append(order.Notes ?? "N/A").Append("</td></tr>");

            if (order.User != null)
            {
                sb.Append("<tr><th>User Name</th><td>").Append(order.User.Name).Append("</td></tr>");
                sb.Append("<tr><th>User Email</th><td>").Append(order.User.Email).Append("</td></tr>");
                sb.Append("<tr><th>User Phone</th><td>").Append(order.User.PhoneNumber).Append("</td></tr>");
                sb.Append("<tr><th>User Address</th><td>").Append(order.User.Address ?? "N/A").Append("</td></tr>");
            }

            if (order.Car != null)
            {
                sb.Append("<tr><th>Car ID</th><td>").Append(order.Car.CarId).Append("</td></tr>");
                sb.Append("<tr><th>Car Model</th><td>").Append(order.Car.Model).Append("</td></tr>");
            }

            if (order.Package != null)
            {
                sb.Append("<tr><th>Package ID</th><td>").Append(order.Package.PackageId).Append("</td></tr>");
                sb.Append("<tr><th>Package Name</th><td>").Append(order.Package.Name).Append("</td></tr>");
            }

            sb.Append("</table>");
            sb.Append("<p>Please review and accept the order.</p>");
            return sb.ToString();
        }

        public async Task NotifyWasherPaymentSuccess(string washerEmail, int orderId)
        {
            string subject = "Successful Payment";
            string body = $"<p>Dear Washer,</p><p>The user has successfully made a payment for the following order:</p><p>{orderId}</p><p>Thank you.</p>";
            await SendEmailAsync(washerEmail, subject, body);
        }
    }
}
