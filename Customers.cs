using Kursach.Classes;
using Kursach.DataBase;
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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            DataGrid.DataSource = ClientClass.GetClients();
            DataGrid.Columns["License"].Visible = false;
            DataGrid.Columns["BirthDay"].Visible = false;
            DataGrid.Columns["PassSerial"].Visible = false;
            DataGrid.Columns["PassNumber"].Visible = false;
            DataGrid.Columns["ClassLimit"].Visible = false;
            DataGrid.Columns["Experience"].Visible = false;
            DataGrid.Columns["Id"].Visible = false;
            DataGrid.Columns["LastName"].HeaderText = "Фамилия";
            DataGrid.Columns["FirstName"].HeaderText = "Имя";
            DataGrid.Columns["MiddleName"].HeaderText = "Отчество";
            DataGrid.Columns["Phone"].HeaderText = "Телефон";
        }

        private void DataGrid_SelectionChanged(object sender, EventArgs e)
        {
            ClientClass client = DataGrid.CurrentRow.DataBoundItem as ClientClass;
            SerialTextBox.Text = client.PassSerial;
            NumberPassTextBox.Text = client.PassNumber;
            DatePicker.Value = client.BirthDay;
            CategoryTextBox.Text = client.License.number_license.ToString();
            PhoneTextBox.Text = client.Phone;
            LastNameTextBox.Text = client.LastName;
            FirstnameTextBox.Text = client.FirstName;
            MiddleNameTextBox.Text = client.MiddleName;
            StaTextBox.Text = client.Experience;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ClientClass client = DataGrid.CurrentRow.DataBoundItem as ClientClass;
                client.PassSerial = SerialTextBox.Text;
                client.PassNumber = NumberPassTextBox.Text;
                client.BirthDay = DatePicker.Value;
                client.Phone = PhoneTextBox.Text;
                client.LastName = LastNameTextBox.Text;
                client.FirstName = FirstnameTextBox.Text;
                client.MiddleName = MiddleNameTextBox.Text;
                client.Experience = StaTextBox.Text;
                client.Update();
                MessageBox.Show("Данные обновлены");
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClientClass client = DataGrid.CurrentRow.DataBoundItem as ClientClass;



            Word.Document doc = null;
            try
            {//подключаюсь к ворду
                Word.Application wApp = new Word.Application();
                wApp.Visible = true;
                string str = $@"{ Directory.GetCurrentDirectory().ToString()}\ШаблонКлиент.docx";//это путь, находится в папке с проектом bin/debug
                doc = wApp.Documents.Open(str);
                doc.Activate();
                Word.Bookmarks wmark = doc.Bookmarks;
                //по названию закладки вставляем информацию
                wmark["LastName"].Range.Text = client.LastName;
                wmark["FirstName"].Range.Text = client.FirstName;
                wmark["MiddleName"].Range.Text = client.MiddleName;
                wmark["Category"].Range.Text = client.License.number_license.ToString();
                wmark["Phone"].Range.Text = client.Phone;



                
                doc.SaveAs($@"{Directory.GetCurrentDirectory().ToString()}\Клиент " + client.Id.ToString() + ".docx");//сохраняем в тот же путь
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
