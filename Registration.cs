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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text == RepeatPasswordTextBox.Text)
            {
                try
                {
                    ClientClass check = new ClientClass(LastNameTextBox.Text, FirstnameTextBox.Text, MiddleNameTextBox.Text,
                        Int32.Parse(NumberLicenseTextBox.Text), CategoryTextBox.Text, dateTimePicker1.Value,
                        PassSerialTextBox.Text, PassNumberTextBox.Text, LoginTextBox.Text, PasswordTextBox.Text, PhoneTextBox.Text);
                    MessageBox.Show("Пользователь зарегистрирован");
                }
                catch (InsertErrorExceptionMine ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Пароли должны совпадать");
            
        }
    }
}
