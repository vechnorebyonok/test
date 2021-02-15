using Kursach.Classes;
using Kursach.DataBase;
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
    public partial class Autorisation : Form
    {
        public Autorisation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(LoginTextBox.Text == "Admin" && PasswordTextBox.Text == "Admin")
            {
                Manager page = new Manager();
                Properties.Settings.Default.Role = "Admin";
                this.Hide();
                page.ShowDialog();
                this.Show();
            }
            else
            {
                try
                {
                    ClientClass client = new ClientClass(LoginTextBox.Text, PasswordTextBox.Text);
                    Properties.Settings.Default.Role = "Client";
                    Menu page = new Menu();
                    this.Hide();
                    page.ShowDialog();
                    this.Show();
                }
                catch (NotFoundExceptionMine ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Непредвиденная ошибка");
                }
            }
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registration page = new Registration();
            this.Hide();
            page.ShowDialog();
            this.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (PasswordTextBox.UseSystemPasswordChar)
                PasswordTextBox.UseSystemPasswordChar = false;
            else
                PasswordTextBox.UseSystemPasswordChar = true;

        }
    }
}
