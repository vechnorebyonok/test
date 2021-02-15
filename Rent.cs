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
    public partial class Rent : Form
    {
        public Rent()
        {
            InitializeComponent();
            
            ClassComboBox.DataSource = PriceClass.GetPrices();
            ClassComboBox.DisplayMember = "Class";
            MarkCombobox.DataSource = PriceClass.GetMarks();
            var Class = ClassComboBox.SelectedItem as PriceClass;
            CarsCombobox.DataSource = CarClass.GetCars(ModelCombobox.Text, Class.Class);
            CarsCombobox.DisplayMember = "GOS";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ClientClass client = new ClientClass(Properties.Settings.Default.Client);
                CarClass car = CarsCombobox.SelectedItem as CarClass;
                var cl = ClassComboBox.SelectedItem as PriceClass;
                var pr = new PriceClass(cl.Class);
                var price = pr.Price * Convert.ToInt32(TimehoursPicker.Value);
                RentClass recordRent = new RentClass(client, car, Convert.ToInt32(TimehoursPicker.Value),price);
                textBox3.Text = "Аренда оформлена";
            }
            catch(NotFoundExceptionMine ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("непредвиденная ошибка");
            }
            
        }

        private void MarkCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarsCombobox.DataSource = null;
            ModelCombobox.DataSource = PriceClass.getModels(MarkCombobox.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Price = ClassComboBox.SelectedItem as PriceClass;
            var price = Price.Price * Convert.ToInt32(TimehoursPicker.Value);
            FullPrice.Text = price.ToString();
        }

        private void ModelCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarsCombobox.DataSource = null;
            CarsCombobox.DataSource = CarClass.GetCars(ModelCombobox.Text, ClassComboBox.Text);
            CarsCombobox.DisplayMember = "GOS";
        }

        private void ClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarsCombobox.DataSource = null;
            if (ModelCombobox.Text != "")
            {
                CarsCombobox.DataSource = CarClass.GetCars(ModelCombobox.Text, ClassComboBox.Text);
                CarsCombobox.DisplayMember = "GOS";
            }

        }
    }
}
