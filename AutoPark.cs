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
    public partial class AutoPark : Form
    {
        public AutoPark()
        {
            InitializeComponent();
            updateData();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CarAddition page = new CarAddition();
            page.ShowDialog();
            updateData();

        }

        private void DataGrid_SelectionChanged(object sender, EventArgs e)
        {
            CarClass data = DataGrid.CurrentRow.DataBoundItem as CarClass;
            GosTextbox.Text = data.GOS;
            ColorTextBox.Text = data.Colour;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CarClass data = DataGrid.CurrentRow.DataBoundItem as CarClass;
            data.UpdateColour(ColorTextBox.Text);
            updateData();
        }
        private void updateData()
        {
            DataGrid.DataSource = CarClass.GetCars();
            DataGrid.Columns["Id"].Visible = false;
            DataGrid.Columns["DateRelease"].Visible = false;
            DataGrid.Columns["Class"].HeaderText = "Класс";
            DataGrid.Columns["Colour"].HeaderText = "Цвет";
            DataGrid.Columns["Body"].HeaderText = "Кузов";
            DataGrid.Columns["Model"].HeaderText = "Модель";
            DataGrid.Columns["Marka"].HeaderText = "Марка";
            DataGrid.Columns["Gos"].HeaderText = "Гос номер";
        }
    }
}
