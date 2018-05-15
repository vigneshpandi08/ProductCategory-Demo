using MVCProductCategory;
using MVCProductCategory.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProductCategory.Controllers
{
    public class TestController : Controller
    {
        private SqlConnection con;
 
        public ActionResult AddProduct()
        {
            return View();
        }   
        [HttpPost]
        public ActionResult AddProduct(Product obj)
        {
            AddDetails(obj);

            return View();
        }
        
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constr);

        }    
        private void AddDetails(Product obj)
        {
            connection();
            SqlCommand cmd = new SqlCommand("Product_Insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
            cmd.Parameters.AddWithValue("@ProductCode", obj.ProductCode);
            cmd.Parameters.AddWithValue("@Quantity", obj.Quantity);
            cmd.Parameters.AddWithValue("@Amount", obj.Amount);
            cmd.Parameters.AddWithValue("@TaxPercent", obj.TaxPercent);
            cmd.Parameters.AddWithValue("@TaxAmount", obj.TaxAmount);
            cmd.Parameters.AddWithValue("@NetAmount", obj.NetAmount);
            cmd.Parameters.AddWithValue("@Category", obj.Category);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

    }
}