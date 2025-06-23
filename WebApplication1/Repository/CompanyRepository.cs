using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class CompanyRepository
    {
        private readonly string _connectionString;

        public CompanyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Company> GetListCompanies()
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
        public Company GetCompanyById(int id)
        {
            Company Company = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetCompanyById", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@srno", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    Company = new Company
                    {
                        SrNo = Convert.ToInt32(rdr["srno"]),
                        CompanyName = rdr["Company_name"].ToString(),
                    };
                }
            }
            return Company;
        }

        public void SaveCompanies(Company company)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SaveCompany", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Action", company.SrNo == null ? "i" : "u");
                cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                cmd.Parameters.AddWithValue("@SrNo", (object?)company.SrNo ?? DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCompany(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteCompany", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SrNo", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }



    }
}
