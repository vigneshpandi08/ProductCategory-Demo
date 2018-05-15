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
    public partial class Form1 : Form
    {
        int ProductId = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlDataAdapter da = new SqlDataAdapter("SELECT CategoryId,CategoryName from ProductCategory", con))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow dr;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "---Select--" };
                dt.Rows.InsertAt(dr, 0);
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.DataSource = dt;
                con.Close();
                GetData();
            }

        }
        
        SqlConnection con = new SqlConnection("Data Source = (Localdb)\\MSSQLLocalDB; Initial Catalog = Application; Integrated Security = True");

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Product_Insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
            cmd.Parameters.AddWithValue("@ProductCode", txtProductCode.Text);
            cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
            cmd.Parameters.AddWithValue("@TaxPercent", txtTaxPercentage.Text);
            cmd.Parameters.AddWithValue("@TaxAmount", txtTaxAmount.Text);
            cmd.Parameters.AddWithValue("@NetAmount", txtNetAmount.Text);
            cmd.Parameters.AddWithValue("@Category", cmbCategory.SelectedValue);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Records are inserted successfully");
            GetData();
            Reset();
            txtProductName.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text != "" && txtProductCode.Text != "" && txtQuantity.Text != "" && txtAmount.Text != "" && txtTaxPercentage.Text != "" && txtTaxAmount.Text != "" && txtNetAmount.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Product_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                cmd.Parameters.AddWithValue("@ProductCode", txtProductCode.Text);
                cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                cmd.Parameters.AddWithValue("@TaxPercent", txtTaxPercentage.Text);
                cmd.Parameters.AddWithValue("@TaxAmount", txtTaxAmount.Text);
                cmd.Parameters.AddWithValue("@NetAmount", txtNetAmount.Text);
                cmd.Parameters.AddWithValue("@Category", cmbCategory.SelectedValue);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Records updated successfully");
                GetData();
                Reset();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ProductId != 0)
            {
                SqlCommand cmd = new SqlCommand("Product_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Records deleted successfully");
                GetData();
                Reset();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        public void GetData()
        {
            SqlCommand cmd = new SqlCommand("Product_Retrieve", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        public void Reset()
        {
            txtProductName.Text = "";
            txtProductCode.Text = "";
            ProductId = 0;
            txtQuantity.Text = "";
            txtAmount.Text = "";
            txtTaxPercentage.Text = "";
            txtTaxAmount.Text = "";
            txtNetAmount.Text = "";
            cmbCategory.SelectedValue = "";
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ProductId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtProductName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtProductCode.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtQuantity.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtTaxPercentage.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtTaxAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtNetAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            cmbCategory.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
