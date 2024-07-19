using CarWashAPI.Model;

namespace CarWashAPI.DTO
{

    public class Report
    {
        public OrderReport OrderReport { get; set; }
        public WashersReport WashersReport { get; set; }
    }
    public class OrderReport
    {
        public string TotalOrders { get; set; }
        public double TotalRevenue { get; set; }
        public List<Order> Orders { get; set; }
        public DateTime GeneratedDate { get; set; }
    }

    public class WashersReport
    {
        public double TotalWashersRevenue { get; set; }
        public List<IndividualWasherReport> WasherReports { get; set; }
        public DateTime GeneratedDate { get; set; }
    }

    public class IndividualWasherReport
    {
        public int? WasherId { get; set; }
        public int TotalOrders { get; set; }
        public double TotalRevenue { get; set; }
    }
}
