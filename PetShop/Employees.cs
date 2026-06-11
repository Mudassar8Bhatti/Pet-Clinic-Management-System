using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
namespace PetShop
{
    public partial class Employees : Form
    {
        private OracleConnection Con = new OracleConnection("User ID=PET;Password=pet123;Data Source=127.0.0.1:1521/XE;");

        public Employees()
        {
            InitializeComponent();
            DisplayEmployee();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void DisplayEmployee()
        {
            Con.Open();
            string query = "SELECT * FROM Employeetbl";
            OracleDataAdapter sda = new OracleDataAdapter(query, Con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            EmployeesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Clear()
        {
            EmpNameTb.Text = "";
            EmpAddTb.Text = "";
            EmpPhoneTb.Text = "";
            PasswordTb.Text = ""; 
        }
        int Key = 0;
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if(EmpNameTb.Text == "" || EmpAddTb.Text== "" || EmpPhoneTb.Text== "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "INSERT INTO EmployeeTbl (EmpNum, EmpName, EmpAddress, EmpDOB, EmpPhone, EmpPass) VALUES (EmpNum_seq.NEXTVAL, :EN, :EA, :ED, :EP, :EPa)"; 
                    OracleCommand cmd = new OracleCommand(query, Con);
                    cmd.Parameters.Add(":EN", OracleDbType.Varchar2).Value = EmpNameTb.Text;
                    cmd.Parameters.Add(":EA", OracleDbType.Varchar2).Value = EmpAddTb.Text;
                    cmd.Parameters.Add(":ED", OracleDbType.Date).Value = EmpDOB.Value.Date;
                    cmd.Parameters.Add(":EP", OracleDbType.Varchar2).Value = EmpPhoneTb.Text;
                    cmd.Parameters.Add(":EPa", OracleDbType.Varchar2).Value = PasswordTb.Text;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Added");
                    Con.Close();
                    DisplayEmployee();
                    Clear();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void EmployeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && EmployeesDGV.Rows[e.RowIndex].Cells[0].Value != null)
            {
                DataGridViewRow row = EmployeesDGV.Rows[e.RowIndex];

                EmpNameTb.Text = row.Cells[1].Value.ToString();
                EmpAddTb.Text = row.Cells[2].Value.ToString();
                EmpDOB.Text = row.Cells[3].Value.ToString();
                EmpPhoneTb.Text = row.Cells[4].Value.ToString();
                PasswordTb.Text = row.Cells[5].Value.ToString();

                Key = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "UPDATE EmployeeTbl SET EmpName = :EN, EmpAddress = :EA, EmpDOB = :ED, EmpPhone = :EP, EmpPass = :EPa WHERE EmpNum = :EKey";
                    OracleCommand cmd = new OracleCommand(query, Con);
                    cmd.Parameters.Add(":EN", OracleDbType.Varchar2).Value = EmpNameTb.Text;
                    cmd.Parameters.Add(":EA", OracleDbType.Varchar2).Value = EmpAddTb.Text;
                    cmd.Parameters.Add(":ED", OracleDbType.Date).Value = EmpDOB.Value.Date;
                    cmd.Parameters.Add(":EP", OracleDbType.Varchar2).Value = EmpPhoneTb.Text;
                    cmd.Parameters.Add(":EPa", OracleDbType.Varchar2).Value = PasswordTb.Text;
                    cmd.Parameters.Add(":EKey", OracleDbType.Int32).Value = Key;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated!");
                    Con.Close();
                    DisplayEmployee();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select an Employee");
            }
            else
            {
                try
                {
                    Con.Open();
                    OracleCommand cmd = new OracleCommand("delete from EmployeeTbl where EmpNum = :EKey", Con);
                    cmd.Parameters.Add(":EKey", OracleDbType.Int32).Value = Key;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted!!");
                    Con.Close();
                    DisplayEmployee();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            products Pr = new products();
            Pr.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Homes hr = new Homes();
            hr.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

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
