using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace PetShop
{
    public partial class Billings : Form
    {
        private OracleConnection Con = new OracleConnection("User ID=PET;Password=pet123;Data Source=127.0.0.1:1521/XE;");
        int Key = 0, Stock = 0, n = 0, GrdTotal = 0, prodid, prodqty, prodprice, total, pos = 60;
        string prodname;

        public Billings()
        {
            InitializeComponent();
            GetCustomers();
            DisplayProduct();
            DisplayTransaction();
            GetCustName();
            SetupBillDGV();
            this.CustIdCb.SelectedIndexChanged += new EventHandler(CustIdCb_SelectedIndexChanged);
            this.ProductsDGV.CellClick += new DataGridViewCellEventHandler(this.ProductsDGV_CellContentClick);

        }
        private void CustIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustName();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int qty;
            int price;

            if (Key == 0 || Stock == 0)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            if (CustIdCb.Text == "" || CustNameTb.Text == "" || QtyTb.Text == "" || !int.TryParse(QtyTb.Text, out qty))
            {
                MessageBox.Show("Missing Information or Invalid Quantity");
                return;
            }

            if (qty > Stock)
            {
                MessageBox.Show("No Enough Stock");
                return;
            }

            if (!int.TryParse(PrPriceTb.Text, out price))
            {
                MessageBox.Show("Invalid Price");
                return;
            }

            total = qty * price;
            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(BillDGV);
            newRow.Cells[0].Value = n + 1;
            newRow.Cells[1].Value = PrNameTb.Text;
            newRow.Cells[2].Value = QtyTb.Text;
            newRow.Cells[3].Value = PrPriceTb.Text;
            newRow.Cells[4].Value = total;
            BillDGV.Rows.Add(newRow);
            GrdTotal += total;
            Totalbl.Text = "Rs " + GrdTotal;
            n++;
            UpdateStock();
            Reset();
        }
        private void SetupBillDGV()
        {
            BillDGV.Columns.Clear();
            BillDGV.Columns.Add("ID", "ID");
            BillDGV.Columns.Add("Product", "Product");
            BillDGV.Columns.Add("Qty", "Quantity");
            BillDGV.Columns.Add("Price", "Price");
            BillDGV.Columns.Add("Total", "Total");
        }

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && ProductsDGV.Rows[e.RowIndex].Cells[0].Value != null)
            {
                DataGridViewRow row = ProductsDGV.Rows[e.RowIndex];
                PrNameTb.Text = row.Cells[1].Value.ToString();
                Stock = Convert.ToInt32(row.Cells[3].Value);
                PrPriceTb.Text = row.Cells[4].Value.ToString();
                Key = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertBill(); 

            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custom", 850, 1100);
            printPreviewDialog1.Document = printDocument1;

            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Rachna Pet Shop", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID PRODUCT QTY PRICE TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));

            foreach (DataGridViewRow row in BillDGV.Rows)
            {
                if (row.IsNewRow) continue;  // Skip the placeholder row

                prodid = Convert.ToInt32(row.Cells[0].Value ?? 0);
                prodname = row.Cells[1].Value?.ToString() ?? "";
                prodqty = Convert.ToInt32(row.Cells[2].Value ?? 0);
                prodprice = Convert.ToInt32(row.Cells[3].Value ?? 0);
                total = Convert.ToInt32(row.Cells[4].Value ?? 0);

                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(60, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(130, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + total, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(220, pos));
                pos += 20;
            }

            e.Graphics.DrawString("Grand Total: Rs " + GrdTotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
            e.Graphics.DrawString("*******Pet Shop*******", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;
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

        private void InsertBill()
        {
            try
            {
                Con.Open();
                string query = "INSERT INTO BillTbl (BNum, BDate, CustId, CustName, Amt) VALUES (BillSeq.NEXTVAL, :BD, :CI, :CN, :Am)";
                OracleCommand cmd = new OracleCommand(query, Con);
                cmd.Parameters.Add(":BD", OracleDbType.Date).Value = DateTime.Today;
                cmd.Parameters.Add(":CI", OracleDbType.Int32).Value = Convert.ToInt32(CustIdCb.SelectedValue);
                cmd.Parameters.Add(":CN", OracleDbType.Varchar2).Value = CustNameTb.Text;
                cmd.Parameters.Add(":Am", OracleDbType.Int32).Value = GrdTotal;
                cmd.ExecuteNonQuery();
                Con.Close();
                MessageBox.Show("Bill Saved!");
                DisplayTransaction();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetCustomers()
        {
            try
            {
                Con.Open();
                OracleCommand cmd = new OracleCommand("SELECT * FROM Customertbl", Con);
                OracleDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("CustId", typeof(int));
                dt.Load(rdr);
                CustIdCb.ValueMember = "CustId";
                CustIdCb.DataSource = dt;
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DisplayProduct()
        {
            try
            {
                Con.Open();
                string query = "SELECT * FROM ProductTbl";
                OracleDataAdapter oda = new OracleDataAdapter(query, Con);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                ProductsDGV.DataSource = ds.Tables[0];
                ProductsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                ProductsDGV.MultiSelect = false;
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DisplayTransaction()
        {
            try
            {
                Con.Open();
                string query = "SELECT * FROM BillTbl";
                OracleDataAdapter oda = new OracleDataAdapter(query, Con);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                TransactionsDGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void GetCustName()
        {
            try
            {
                Con.Open();
                string query = "SELECT * FROM Customertbl WHERE CustId = :CustId";
                OracleCommand cmd = new OracleCommand(query, Con);
                cmd.Parameters.Add(":CustId", OracleDbType.Int32).Value = Convert.ToInt32(CustIdCb.SelectedValue);
                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    CustNameTb.Text = reader["CustName"].ToString();
                }
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UpdateStock()
        {
            try
            {
                int qty = Convert.ToInt32(QtyTb.Text);
                int NewQty = Stock - qty;
                Con.Open();
                OracleCommand cmd = new OracleCommand("UPDATE ProductTbl SET PrQty = :PQ WHERE PrId = :PKey", Con);
                cmd.Parameters.Add(":PQ", OracleDbType.Int32).Value = NewQty;
                cmd.Parameters.Add(":PKey", OracleDbType.Int32).Value = Key;
                cmd.ExecuteNonQuery();
                Con.Close();
                DisplayProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Reset()
        {
            PrNameTb.Text = "";
            PrPriceTb.Text = "";
            QtyTb.Text = "";
            Stock = 0;
            Key = 0;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}