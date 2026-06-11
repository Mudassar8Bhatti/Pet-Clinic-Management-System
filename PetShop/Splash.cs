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
    public partial class Splash : Form
    {
        private OracleConnection Con = new OracleConnection("User ID=PET;Password=pet123;Data Source=127.0.0.1:1521/XE;");
        public Splash()
        {
            InitializeComponent();
            timer1.Start();
        }
        int startP = 0;
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Splash_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startP += 1;
            Myprogress.Value = startP;
            percentagelbl.Text = startP + "%";
            if (Myprogress.Value == 100)
            {
                Myprogress.Value = 0;
                login obj = new login();
                obj.Show();
                this.Hide();
                timer1.Stop();
            }
        }
    }
}
