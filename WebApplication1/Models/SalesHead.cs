using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models
{
    public class SalesHead
    {
        public SalesHead()
        {
            SalesChild = new List<SalesChild>();
            PrefixList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Mr", Text = "Mr" },
                new SelectListItem { Value = "Mrs", Text = "Mrs" },
                new SelectListItem { Value = "Ms", Text = "Ms" },
                new SelectListItem { Value = "Dr", Text = "Dr" }
            };

        }

        public int? SrNo { get; set; }
        public string Prefix { get; set; }
        public List<SelectListItem>? PrefixList { get; set; }
        public int Seq { get; set; }
        public string CustomerName { get; set; }
        
        public string MobileNo { get; set; }
        public decimal Total { get; set; }
        public decimal DiscPer { get; set; }
        public decimal DiscRs { get; set; }
        public decimal NetAmount { get; set; }
        public string Remark { get; set; }
        public DateTime BillDt { get; set; } = DateTime.Now;
        public List<Item> ListItem { get; set; } = new List<Item>();

        public List<SalesChild>? SalesChild { get; set; }
    }

   
}
