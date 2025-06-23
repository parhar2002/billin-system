using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models
{

    public class SalesChild
    {
        public int? Srno { get; set; }
        public int? ShSrno { get; set; }
        public int? ItemSrno { get; set; }
        public string? ItemName { get; set; }
        public int Qty { get; set; }
        public decimal? MRP { get; set; }
        public decimal? Tax { get; set; }
        public decimal DiscPer { get; set; }
        public decimal SalesRate { get; set; }
        public decimal? Total { get; set; }

        public string? ActionJson { get; set; }
    }
}
