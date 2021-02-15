using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach
{
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AutoPark page = new AutoPark();
            this.Hide();
            page.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            History page = new History();
            this.Hide();
            page.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PriceLIst page = new PriceLIst();
            this.Hide();
            page.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Customers page = new Customers();
            this.Hide();
            page.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Crash page = new Crash();
            this.Hide();
            page.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Verification page = new Verification();
            this.Hide();
            page.ShowDialog();
            this.Show();
        }
    }
}
