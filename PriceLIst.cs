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
    public partial class PriceLIst : Form
    {
        public PriceLIst()
        {
            InitializeComponent();
            updateData();
        }
        private void updateData()
        {
            DataGrid.DataSource = PriceClass.GetPrices();
            DataGrid.Columns["Id"].Visible = false;
            ClassCombobox.DataSource = PriceClass.GetPrices();
            ClassCombobox.ValueMember = "Class";
        }

        private void DataGrid_SelectionChanged(object sender, EventArgs e)
        {
            int index = 0;
            if(ClassCombobox.Items.Count > 0)
            {
                foreach (var i in ClassCombobox.Items)
                {
                    var f = i as PriceClass;
                    var name = DataGrid.CurrentRow.DataBoundItem as PriceClass;
                    if (f.Class == name.Class)
                        break;
                    else
                        index++;
                }
                ClassCombobox.SelectedIndex = index;
            }
        }

        private void ClassCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = ClassCombobox.SelectedItem as PriceClass;
            PriceTextBox.Text = item.Price.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double price = double.Parse(PriceTextBox.Text);
            var Class = ClassCombobox.SelectedItem as PriceClass;
            if(Class != null)
            {
                Class.Price = price;
                Class.Update();
                updateData(); 
            }
            else
            {
                string ClassName = ClassCombobox.Text;
                PriceClass newPrice = new PriceClass(ClassName, price);
                updateData();
            }
        }
    }
}
