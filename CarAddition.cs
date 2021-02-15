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
    public partial class CarAddition : Form
    {
        public CarAddition()
        {
            InitializeComponent();
            ClassCombobox.DataSource = PriceClass.GetPrices();
            ClassCombobox.DisplayMember = "Class";
            MarkCombobox.DataSource = PriceClass.GetMarks();
            BodyCombobox.DataSource = PriceClass.GetBody();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                var Class = ClassCombobox.SelectedItem as PriceClass;
                CarClass record = new CarClass(Class.Class, ColorTextbox.Text, dateTimePicker1.Value, BodyCombobox.Text,
                    ModelTextBox.Text, MarkCombobox.Text, VinTextBox.Text, GosTextBox.Text);
                MessageBox.Show("Автомобиль добавлен");
                this.Close();
            }
            catch(NotFoundExceptionMine ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(InsertErrorExceptionMine ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        
    }
}
