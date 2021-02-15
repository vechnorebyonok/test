using Kursach.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Kursach
{
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
            if(Properties.Settings.Default.Role == "Admin")
                DataGrid.DataSource = RentClass.GetHistory();
            else
                DataGrid.DataSource = RentClass.GetHistory(new ClientClass(Properties.Settings.Default.Client));
            DataGrid.Columns["ClientSetter"].Visible = false;
            DataGrid.Columns["Id"].Visible = false;
            DataGrid.Columns["Client"].Visible = false;
            DataGrid.Columns["Car"].Visible = false;
            DataGrid.Columns["CarSetter"].Visible = false;
            DataGrid.Columns["pay"].Visible = false;
            DataGrid.Columns["ClientLastName"].HeaderText = "Фамилия";
            DataGrid.Columns["ClientFirstName"].HeaderText = "Имя";
            DataGrid.Columns["ClientMiddleName"].HeaderText = "Отчество";
            DataGrid.Columns["CarModel"].HeaderText = "Модель";
            DataGrid.Columns["CarClass"].HeaderText = "Класс";
            DataGrid.Columns["GOS"].HeaderText = "Гос номер";
            DataGrid.Columns["Hour"].HeaderText = "Часы";
            DataGrid.Columns["Date"].HeaderText = "Дата";



        }

        private void DataGrid_SelectionChanged(object sender, EventArgs e)
        {
            var ob = DataGrid.CurrentRow.DataBoundItem as RentClass;
            ClientTextBox.Text = String.Format("{0} {1}", ob.Client.LastName, ob.Client.FirstName);
            DateTextBox.Text = ob.date.ToString();
            HourTextBox.Text = ob.Hour.ToString();
            VINTextBox.Text = ob.Car.VIN.ToString();
            MarkTextBox.Text = ob.Car.Marka;
            ModelTextBox.Text = ob.Car.Model;
            GosTextBox.Text = ob.GOS;
            PriceTextBox.Text = ob.pay.Price.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RentClass data = DataGrid.CurrentRow.DataBoundItem as RentClass;

            Word.Document doc = null;
            try
            {//подключаюсь к ворду
                Word.Application wApp = new Word.Application();
                wApp.Visible = true;
                string str = $@"{ Directory.GetCurrentDirectory().ToString()}\ШаблонАренды.docx";//это путь, находится в папке с проектом bin/debug
                doc = wApp.Documents.Open(str);
                doc.Activate();
                Word.Bookmarks wmark = doc.Bookmarks;
                //по названию закладки вставляем информацию
                wmark["LastName"].Range.Text = data.Client.LastName;
                wmark["FirstName"].Range.Text = data.Client.FirstName;
                wmark["MiddleName"].Range.Text = data.Client.MiddleName;
                wmark["Category"].Range.Text = data.Client.License.number_license.ToString();
                wmark["Phone"].Range.Text = data.Client.Phone;
                var table = doc.Tables[1];
                var info = RentClass.GetHistory(data.Client);
                for(int i = 0;i< info.Length; i++)
                {
                    table.Rows.Add();
                    table.Cell(2 + i, 1).Range.Text = info[i].Id.ToString();
                    table.Cell(2 + i, 2).Range.Text = info[i].date.ToString("dd.MM.yyyy");
                    table.Cell(2 + i, 3).Range.Text = info[i].Hour.ToString();
                    table.Cell(2 + i, 4).Range.Text = info[i].Car.Marka;
                    table.Cell(2 + i, 5).Range.Text = info[i].Car.Model;
                    table.Cell(2 + i, 6).Range.Text = info[i].Car.GOS;
                    table.Cell(2 + i, 7).Range.Text = info[i].Car.VIN;
                }

                doc.SaveAs($@"{Directory.GetCurrentDirectory().ToString()}\Аренда Клиента " + data.Client.Id.ToString() + ".docx");//сохраняем в тот же путь
                doc.Close();
                doc = null;
                wApp.Quit();
            }
            catch (Exception ex)
            {
                doc.Close();
                doc = null;
                MessageBox.Show(ex.Message);
            }
        }
    }
}
