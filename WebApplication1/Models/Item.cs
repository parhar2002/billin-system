namespace WebApplication1.Models
{
    public class Item
    {
        public int? SrNo { get; set; }
        public string ItemName { get; set; }
        public int CompanySrNo { get; set; }
        public string? CompanyName { get; set; }
        public decimal Mrp { get; set; } 
        public decimal Tax { get; set; }
        public decimal SalesRate { get; set; }

        public List<Company> Companies { get; set; } = new List<Company>();
    }
}
