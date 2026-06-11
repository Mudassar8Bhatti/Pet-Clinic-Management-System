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
    public partial class Customers : Form
    {
        private OracleConnection Con = new OracleConnection("User ID=PET;Password=pet123;Data Source=127.0.0.1:1521/XE;");
        public Customers()
        {
            InitializeComponent();
            DisplayCustomer();
        }
        private void DisplayCustomer()
        {
            Con.Open();
            string query = "SELECT * FROM CustomerTbl";
            OracleDataAdapter sda = new OracleDataAdapter(query, Con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Clear()
        {
            CustNameTb.Text = "";
            CustAddTb.Text = "";
            CustPhoneTb.Text = "";
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustAddTb.Text == "" || CustPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "INSERT INTO CustomerTbl (CustId, CustName, CustAddress, CustPhone) VALUES (cust_seq.NEXTVAL, :CN, :CA, :CP)";
                    OracleCommand cmd = new OracleCommand(query, Con);
                    cmd.Parameters.Add(":CN", OracleDbType.Varchar2).Value = CustNameTb.Text;
                    cmd.Parameters.Add(":CA", OracleDbType.Varchar2).Value = CustAddTb.Text;
                    cmd.Parameters.Add(":CP", OracleDbType.Varchar2).Value = CustPhoneTb.Text; 
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added");
                    Con.Close();
                    DisplayCustomer();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int Key = 0;
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && CustomerDGV.Rows[e.RowIndex].Cells[0].Value != null)
            {
                DataGridViewRow row = CustomerDGV.Rows[e.RowIndex];

                CustNameTb.Text = row.Cells[1].Value.ToString();
                CustAddTb.Text = row.Cells[2].Value.ToString();
                CustPhoneTb.Text = row.Cells[3].Value.ToString();
                Key = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select an Customer");
            }
            else
            {
                try
                {
                    Con.Open();
                    OracleCommand cmd = new OracleCommand("delete from CustomerTbl where CustId = :EKey", Con);
                    cmd.Parameters.Add(":EKey", OracleDbType.Int32).Value = Key;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted!!");
                    Con.Close();
                    DisplayCustomer();
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

        private void label2_Click(object sender, EventArgs e)
        {
            products hr = new products();
            hr.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees hr = new Employees();
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
