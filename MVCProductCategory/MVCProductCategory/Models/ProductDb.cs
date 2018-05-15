using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCProductCategory.Models
{
    public class ProductDb
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constring);
        }

        public bool AddProduct(Product product)
        {
            connection();
            SqlCommand cmd = new SqlCommand("Product_Insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@ProductCode", product.ProductCode);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@Amount", product.Amount);
            cmd.Parameters.AddWithValue("@TaxPercent", product.TaxPercent);
            cmd.Parameters.AddWithValue("@TaxAmount", product.TaxAmount);
            cmd.Parameters.AddWithValue("@NetAmount", product.NetAmount);
            cmd.Parameters.AddWithValue("@Category", product.Category);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}