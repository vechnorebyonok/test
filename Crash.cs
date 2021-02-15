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
    public partial class Crash : Form
    {
        public Crash()
        {
            InitializeComponent();
            dataGridView1.DataSource = RentClass.GetHistory();
            dataGridView1.Columns["ClientSetter"].Visible = false;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Client"].Visible = false;
            dataGridView1.Columns["Car"].Visible = false;
            dataGridView1.Columns["CarSetter"].Visible = false;
            dataGridView1.Columns["pay"].Visible = false;
            dataGridView1.Columns["ClientLastName"].HeaderText = "Фамилия";
            dataGridView1.Columns["ClientFirstName"].HeaderText = "Имя";
            dataGridView1.Columns["ClientMiddleName"].HeaderText = "Отчество";
            dataGridView1.Columns["CarModel"].HeaderText = "Модель";
            dataGridView1.Columns["CarClass"].HeaderText = "Класс";
            dataGridView1.Columns["GOS"].HeaderText = "Гос номер";
            dataGridView1.Columns["Hour"].HeaderText = "Часы";
            dataGridView1.Columns["Date"].HeaderText = "Дата";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            RentClass data = dataGridView1.CurrentRow.DataBoundItem as RentClass;
            dogTextBox.Text = data.Id.ToString();
            dateTimePicker1.Value = data.date;
            PlaceTextBox.Text = "";
            obstaTextBox.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RentClass data = dataGridView1.CurrentRow.DataBoundItem as RentClass;
            data.AddCrash(dateTimePicker1.Value, PlaceTextBox.Text, obstaTextBox.Text);
            MessageBox.Show("Информация об аварии сохранена");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var db = new Model1())
            {
                RentClass data = dataGridView1.CurrentRow.DataBoundItem as RentClass;
                Word.Document doc = null;
                try
                {//подключаюсь к ворду
                    Word.Application wApp = new Word.Application();
                    wApp.Visible = true;
                    string str = $@"{ Directory.GetCurrentDirectory().ToString()}\ШаблонДТП.docx";//это путь, находится в папке с проектом bin/debug
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
                    var table2 = doc.Tables[2];
                    var info = RentClass.GetHistory(data.Client);
                    for (int i = 0; i < info.Length; i++)
                    {
                        table.Rows.Add();
                        table.Cell(2 + i, 1).Range.Text = info[i].Id.ToString();
                        table.Cell(2 + i, 2).Range.Text = info[i].date.ToString("dd.MM.yyyy");
                        table.Cell(2 + i, 3).Range.Text = info[i].Hour.ToString();
                        table.Cell(2 + i, 4).Range.Text = info[i].Car.Marka;
                        table.Cell(2 + i, 5).Range.Text = info[i].Car.Model;
                        table.Cell(2 + i, 6).Range.Text = info[i].Car.GOS;
                        table.Cell(2 + i, 7).Range.Text = info[i].Car.VIN;
                        int idtemp = info[i].Id;
                        var list = db.DTP.Where(c => c.contract_id == idtemp).ToArray();
                        if(list.Length > 0)
                        {
                            for (int j = 0; j < list.Length; j++)
                            {
                                table2.Rows.Add();
                                table2.Cell(2 + j, 1).Range.Text = list[j].contract_id.ToString();
                                table2.Cell(2 + j, 2).Range.Text = list[j].id.ToString();
                                table2.Cell(2 + j, 3).Range.Text = list[j].date;
                                table2.Cell(2 + j, 4).Range.Text = list[j].Description_DTP.Place;
                                table2.Cell(2 + j, 5).Range.Text = list[j].Description_DTP.Conditions;
                            }
                        }
                        
                    }
                    doc.SaveAs($@"{Directory.GetCurrentDirectory().ToString()}\Аренда ДТП Клиента " + data.Client.Id.ToString() + ".docx");//сохраняем в тот же путь
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
}
