using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace PetShop
{
    
    public partial class products : Form
    {
        private OracleConnection Con = new OracleConnection("User ID=PET;Password=pet123;Data Source=127.0.0.1:1521/XE;");
        public products()
        {
            InitializeComponent();
            DisplayProduct();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void DisplayProduct()
        {
            Con.Open();
            string query = "SELECT * FROM ProductTbl";
            OracleDataAdapter sda = new OracleDataAdapter(query, Con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            ProductDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Clear()
        {
            PrNameTb.Text = "";
            QtyTb.Text = "";
            PriceTb.Text = "";
            CatTb.SelectedIndex = 0;
        }
        int Key = 0;
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PrNameTb.Text == "" || CatTb.SelectedIndex == -1 || QtyTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "INSERT INTO ProductTbl (PrId, PrName, PrCat, PrQty, PrPrice) VALUES (product_seq.NEXTVAL, :PN, :PC, :PQ, :PP)";
                    OracleCommand cmd = new OracleCommand(query, Con);
                    cmd.Parameters.Add(":PN", OracleDbType.Varchar2).Value = PrNameTb.Text;
                    cmd.Parameters.Add(":PC", OracleDbType.Varchar2).Value = CatTb.SelectedItem.ToString();
                    cmd.Parameters.Add(":PQ", OracleDbType.Int32).Value = Convert.ToInt32(QtyTb.Text);
                    cmd.Parameters.Add(":PP", OracleDbType.Decimal).Value = Convert.ToDecimal(PriceTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Added");
                    Con.Close();
                    DisplayProduct();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && ProductDGV.Rows[e.RowIndex].Cells[0].Value != null)
            {
                DataGridViewRow row = ProductDGV.Rows[e.RowIndex];
                PrNameTb.Text = row.Cells[1].Value.ToString();
                CatTb.Text = row.Cells[2].Value.ToString();
                QtyTb.Text = row.Cells[3].Value.ToString();
                PriceTb.Text = row.Cells[4].Value.ToString();
                Key = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Product");
            }
            else
            {
                try
                {
                    Con.Open();
                    OracleCommand cmd = new OracleCommand("delete from ProductTbl where PrId = :EKey", Con);
                    cmd.Parameters.Add(":EKey", OracleDbType.Int32).Value = Key;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted!!");
                    Con.Close();
                    DisplayProduct();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PrNameTb.Text == "" || CatTb.SelectedIndex == -1 || QtyTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "UPDATE ProductTbl SET PrName = :PN, PrCat = :PC, PrQty = :PQ, PrPrice = :PP WHERE PrId = :EKey";
                    OracleCommand cmd = new OracleCommand(query, Con);
                    cmd.Parameters.Add(":PN", OracleDbType.Varchar2).Value = PrNameTb.Text;
                    cmd.Parameters.Add(":PC", OracleDbType.Varchar2).Value = CatTb.SelectedItem.ToString();
                    cmd.Parameters.Add(":PQ", OracleDbType.Int32).Value = Convert.ToInt32(QtyTb.Text);
                    cmd.Parameters.Add(":PP", OracleDbType.Decimal).Value = Convert.ToDecimal(PriceTb.Text);
                    cmd.Parameters.Add(":EKey", OracleDbType.Int32).Value = Key;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Edited");
                    Con.Close();
                    DisplayProduct();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Homes hr = new Homes();
            hr.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees hr = new Employees();
            hr.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers hr = new Customers();
            hr.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings hr = new Billings();
            hr.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            login hr = new login();
            hr.Show();
            this.Hide();
        }
    }
}
