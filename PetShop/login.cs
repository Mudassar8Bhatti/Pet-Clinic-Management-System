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
    public partial class login : Form
    {
        private OracleConnection Con = new OracleConnection("User ID=PET;Password=pet123;Data Source=127.0.0.1:1521/XE;");
        public login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Reset()
        {
            UName.Text = "";
            Upassword.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UName.Text == "" || Upassword.Text == "")
            {
                MessageBox.Show("Please enter Username and Password.");
                return;
            }
            try
            {
                Con.Open();
                string query = "SELECT COUNT(*) FROM EmployeeTbl WHERE EmpName = :uname AND EmpPass = :upass";
                OracleCommand cmd = new OracleCommand(query, Con);
                cmd.Parameters.Add(":uname", OracleDbType.Varchar2).Value = UName.Text;
                cmd.Parameters.Add(":upass", OracleDbType.Varchar2).Value = Upassword.Text;

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Login Successful!");
                    Homes Hm = new Homes();
                    Hm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
