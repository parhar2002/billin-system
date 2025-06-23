using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class SalesHeadRepository
    {
        private readonly string _connectionString;

        public SalesHeadRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<SalesHead> GetSalesHeadList()
        {
            var SalesHeadList = new List<SalesHead>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("ListSalesHead", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    SalesHeadList.Add(new SalesHead
                    {
                        SrNo = Convert.ToInt32(rdr["srno"]),
                        Prefix = rdr["prefix"].ToString(),
                        Seq = Convert.ToInt32(rdr["seq"]),
                        CustomerName = rdr["customer_name"].ToString(),
                        MobileNo = rdr["mobile_no"].ToString(),
                        Total = Convert.ToDecimal(rdr["total"]),
                        DiscPer = Convert.ToDecimal(rdr["disc_per"]),
                        DiscRs = Convert.ToDecimal(rdr["disc_rs"]),
                        NetAmount = Convert.ToDecimal(rdr["net_amount"]),
                        Remark = rdr["remark"].ToString(),
                        BillDt = Convert.ToDateTime(rdr["bill_dt"]),
                    });
                }
            }
            return SalesHeadList;
        }
        public SalesHead GetSalesHeadById(int id)
        {
            SalesHead salesHead = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetSalesHeadById", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SrNo", id);

                con.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // First Result: SalesHead
                    if (rdr.Read())
                    {
                        salesHead = new SalesHead
                        {
                            SrNo = rdr["srno"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["srno"]),
                            Prefix = rdr["prefix"] == DBNull.Value ? null : rdr["prefix"].ToString(),
                            Seq = rdr["seq"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["seq"]),
                            CustomerName = rdr["customer_name"] == DBNull.Value ? null : rdr["customer_name"].ToString(),
                            MobileNo = rdr["mobile_no"] == DBNull.Value ? null : rdr["mobile_no"].ToString(),
                            Total = rdr["total"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["total"]),
                            DiscPer = rdr["disc_per"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["disc_per"]),
                            DiscRs = rdr["disc_rs"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["disc_rs"]),
                            NetAmount = rdr["net_amount"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["net_amount"]),
                            Remark = rdr["remark"] == DBNull.Value ? null : rdr["remark"].ToString(),
                            BillDt = rdr["bill_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(rdr["bill_dt"]),
                            SalesChild = new List<SalesChild>()
                        };
                    }

                    if (rdr.NextResult() && salesHead != null)
                    {
                        while (rdr.Read())
                        {
                            SalesChild child = new SalesChild
                            {
                                Srno = rdr["srno"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["srno"]),
                                ShSrno = rdr["sh_srno"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["sh_srno"]),
                                ItemSrno = rdr["item_srno"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["item_srno"]),
                                ItemName = rdr["Item_name"] == DBNull.Value ? null : rdr["Item_name"].ToString(),
                                Qty = rdr["qty"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["qty"]),
                                MRP = rdr["mrp"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["mrp"]),
                                Tax = rdr["tax"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["tax"]),
                                DiscPer = rdr["disc_per"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["disc_per"]),
                                SalesRate = rdr["sales_rate"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["sales_rate"]),
                                Total = 0 // will calculate below
                            };

                            // Calculate total safely
                            decimal qty = child.Qty;
                            decimal salesRate = child.SalesRate;
                            decimal discPer = child.DiscPer;

                            decimal total = qty * salesRate;
                            total -= total * discPer / 100;
                            child.Total = total;

                            salesHead.SalesChild.Add(child);
                        }
                    }
                }
            }

            return salesHead;
        }

        public void SaveSalesHead(SalesHead SalesHead)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SaveSalesHead", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@action", SalesHead.SrNo == null ? "i" : "u");
                cmd.Parameters.AddWithValue("@srno", (object?)SalesHead.SrNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@prefix", SalesHead.Prefix);
                cmd.Parameters.AddWithValue("@seq", SalesHead.Seq);
                cmd.Parameters.AddWithValue("@customer_name", SalesHead.CustomerName);
                cmd.Parameters.AddWithValue("@mobile_no", SalesHead.MobileNo);
                cmd.Parameters.AddWithValue("@total", SalesHead.Total);
                cmd.Parameters.AddWithValue("@disc_per", SalesHead.DiscPer);
                cmd.Parameters.AddWithValue("@disc_rs", SalesHead.DiscRs);
                cmd.Parameters.AddWithValue("@net_amount", SalesHead.NetAmount);
                cmd.Parameters.AddWithValue("@remark", SalesHead.Remark);
                cmd.Parameters.AddWithValue("@bill_dt", SalesHead.BillDt);

                string jsonData = JsonSerializer.Serialize(SalesHead.SalesChild);
                cmd.Parameters.AddWithValue("@jsonSalesChild", jsonData);

                con.Open();
                cmd.ExecuteNonQuery();


            }
        }

        public void DeleteSalesHead(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteSalesHead", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SrNo", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Item> GetItemList()
        {
            var itemList = new List<Item>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("ListItems", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    itemList.Add(new Item
                    {
                        SrNo = Convert.ToInt32(rdr["srno"]),
                        ItemName = rdr["Item_name"].ToString(),
                        CompanySrNo = Convert.ToInt32(rdr["company_srno"]),
                        CompanyName = rdr["Company_name"].ToString(),
                        Mrp = Convert.ToDecimal(rdr["mrp"]),
                        Tax = Convert.ToDecimal(rdr["tax"]),
                        SalesRate = Convert.ToDecimal(rdr["sales_rate"])
                    });
                }
            }
            return itemList;
        }


    }
}
