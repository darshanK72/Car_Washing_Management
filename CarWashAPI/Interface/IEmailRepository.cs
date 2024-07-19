namespace CarWashAPI.Interface
{
    public interface IEmailRepository
    {
        Task NotifyUserOnWashRequestResponse(string userEmail, string washerName, bool isAccepted);
        Task NotifyUserOnServiceUpdate(string userEmail, string washerName, string status);
        Task NotifyUserOnReceipt(string userEmail, int receiptId);
        Task NotifyWasherScheduledWash(string washerEmail, string orderDetails);
        Task NotifyWasherNewOrder(string washerEmail, int orderId);
        Task NotifyWasherPaymentSuccess(string washerEmail, int orderId);
    }
}
