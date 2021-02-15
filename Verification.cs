using Kursach.Classes;
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
    public partial class Verification : Form
    {
        public Verification()
        {
            InitializeComponent();
            dataGridView1.DataSource = ClientClass.GetClients(true);
            dataGridView1.Columns["License"].Visible = false;
            dataGridView1.Columns["BirthDay"].Visible = false;
            dataGridView1.Columns["PassSerial"].Visible = false;
            dataGridView1.Columns["PassNumber"].Visible = false;
            dataGridView1.Columns["ClassLimit"].Visible = false;
            dataGridView1.Columns["Experience"].Visible = false;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["LastName"].HeaderText = "Фамилия";
            dataGridView1.Columns["FirstName"].HeaderText = "Имя";
            dataGridView1.Columns["MiddleName"].HeaderText = "Отчество";
            dataGridView1.Columns["Phone"].HeaderText = "Телефон";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                ClientClass client = dataGridView1.CurrentRow.DataBoundItem as ClientClass;
                if (client != null)
                {
                    client.ClassLimit = true;
                    client.Update();
                    dataGridView1.DataSource = ClientClass.GetClients(true);
                    dataGridView1.Columns["License"].Visible = false;
                    dataGridView1.Columns["BirthDay"].Visible = false;
                    dataGridView1.Columns["PassSerial"].Visible = false;
                    dataGridView1.Columns["PassNumber"].Visible = false;
                    dataGridView1.Columns["ClassLimit"].Visible = false;
                    dataGridView1.Columns["Experience"].Visible = false;
                    dataGridView1.Columns["Id"].Visible = false;
                    dataGridView1.Columns["LastName"].HeaderText = "Фамилия";
                    dataGridView1.Columns["FirstName"].HeaderText = "Имя";
                    dataGridView1.Columns["MiddleName"].HeaderText = "Отчество";
                    dataGridView1.Columns["Phone"].HeaderText = "Телефон";
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
