using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductCategory_Form
{
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source = (Localdb)\\MSSQLLocalDB; Initial Catalog = Application; Integrated Security = True");

        private void btn_Create_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Category_Insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@CategoryName", txt_Name.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Records are inserted successfully");
            txt_Name.Text = string.Empty;
            txt_Name.Focus();
        }
        private void Category_Load(object sender, EventArgs e)
        {

        }
    }
}
