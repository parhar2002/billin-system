using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class ItemRepository
    {
        private readonly string _connectionString;

        public ItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
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
        public Item GetItemById(int id)
        {
            Item Item = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetItemById", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@srno", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    Item = new Item
                    {
                        SrNo = Convert.ToInt32(rdr["srno"]),
                        ItemName = rdr["Item_name"].ToString(),
                        CompanySrNo = Convert.ToInt32(rdr["company_srno"]),
                        CompanyName = rdr["Company_name"].ToString(),
                        Mrp = Convert.ToDecimal(rdr["mrp"]),
                        Tax = Convert.ToDecimal(rdr["tax"]),
                        SalesRate = Convert.ToDecimal(rdr["sales_rate"])

                    };
                }
            }
            return Item;
        }

        public void SaveItem(Item Item)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SaveItem", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Action", Item.SrNo == null ? "i" : "u");
                cmd.Parameters.AddWithValue("@SrNo", (object?)Item.SrNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Item_name", Item.ItemName);
                cmd.Parameters.AddWithValue("@company_srno", Item.CompanySrNo);
                cmd.Parameters.AddWithValue("@mrp", Item.Mrp);
                cmd.Parameters.AddWithValue("@tax", Item.Tax);
                cmd.Parameters.AddWithValue("@sales_rate", Item.SalesRate);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteItem(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteItem", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SrNo", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Company> GetCompaniesList()
        {
            var Companys = new List<Company>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("ListCompanies", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Companys.Add(new Company
                    {
                        SrNo = Convert.ToInt32(rdr["srno"]),
                        CompanyName = rdr["Company_name"].ToString(),
                    });
                }
            }
            return Companys;
        }



    }
}
