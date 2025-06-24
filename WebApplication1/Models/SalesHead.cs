using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models
{
    public class SalesHead
    {
        public SalesHead()
        {
            SalesChild = new List<SalesChild>();
           
        }

        public int? SrNo { get; set; }
        public string Prefix { get; set; }
        [Display(Name = "Sequence Number")]
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
