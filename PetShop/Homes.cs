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
using Oracle.ManagedDataAccess.Client;

namespace PetShop
{
    public partial class Homes : Form
    {
        private OracleConnection Con = new OracleConnection("User ID=PET;Password=pet123;Data Source=127.0.0.1:1521/XE;");
        public Homes()
        {
            InitializeComponent();
            CountDogs();
            CountBirds();
            CountCats();
            Finance();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void CountDogs()
        {
            string Cat = "Dog";
            Con.Open();
            string query = "SELECT COUNT(*) FROM ProductTbl WHERE PrCat = :category";
            OracleDataAdapter oda = new OracleDataAdapter(query, Con);
            oda.SelectCommand.Parameters.Add(":category", OracleDbType.Varchar2).Value = Cat;

            DataTable dt = new DataTable();
            oda.Fill(dt);
            Dogslbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountBirds()
        {
            string Cat = "Bird";
            Con.Open();
            string query = "SELECT COUNT(*) FROM ProductTbl WHERE PrCat = :category";
            OracleDataAdapter oda = new OracleDataAdapter(query, Con);
            oda.SelectCommand.Parameters.Add(":category", OracleDbType.Varchar2).Value = Cat;
            DataTable dt = new DataTable();
            oda.Fill(dt);
            Birdslbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountCats()
        {
            string Cat = "Bird";
            Con.Open();
            string query = "SELECT COUNT(*) FROM ProductTbl WHERE PrCat = :category";
            OracleDataAdapter oda = new OracleDataAdapter(query, Con);
            oda.SelectCommand.Parameters.Add(":category", OracleDbType.Varchar2).Value = Cat;
            DataTable dt = new DataTable();
            oda.Fill(dt);
            Catslbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void Finance()
        {
            
            Con.Open();
            string query = "SELECT Sum(Amt) FROM BillTbl";
            OracleDataAdapter oda = new OracleDataAdapter(query, Con);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            financelbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Birdslbl_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            login lg = new login();
            lg.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings obj = new Billings();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            products pr = new products();
            pr.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees emp = new Employees();
            emp.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers Cm = new Customers();
            Cm.Show();
            this.Hide();
        }
    }
}
