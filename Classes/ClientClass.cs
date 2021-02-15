using Kursach.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Classes
{
    class ClientClass
    {
        public int Id { get; private set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public License License { get; set; }
        public DateTime BirthDay { get; set; }
        public string PassSerial { get; set; }
        public string PassNumber { get; set; }
        public bool ClassLimit { get; set; }
        public string Experience { get; set; }
        public string Phone { get; set; }

        /// <summary>
        /// Не используй этот конструктор, он для выгрузки в массив
        /// </summary>
        public ClientClass() { }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        public ClientClass(string login, string password)
        {
            using (var db = new Model1())
            {
                Client data = db.Client.Where(c => c.login == login && c.password == password).FirstOrDefault();
                if (data != null)
                {
                    Id = data.id;
                    LastName = data.Info_client.Lastname;
                    FirstName = data.Info_client.Firstname;
                    MiddleName = data.Info_client.Middlename;
                    License = data.Info_client.License;
                    BirthDay = DateTime.Parse(data.Info_client.date_of_birth.ToString());
                    PassSerial = data.Info_client.serial_pass;
                    PassNumber = data.Info_client.numer_pass;
                    ClassLimit = data.class_limit;
                    Experience = data.Exp;
                    Phone = data.Info_client.Phone;
                    Properties.Settings.Default.Client = Id;
                    Properties.Settings.Default.Save();
                }
                else
                    throw new NotFoundExceptionMine("Пользователь не найден");
            }
        }
        public ClientClass(int id)
        {
            using (var db = new Model1())
            {
                Client data = db.Client.Where(c => c.id == id).FirstOrDefault();
                if (data != null)
                {
                    Id = data.id;
                    LastName = data.Info_client.Lastname;
                    FirstName = data.Info_client.Firstname;
                    MiddleName = data.Info_client.Middlename;
                    License = data.Info_client.License;
                    Phone = data.Info_client.Phone;
                    BirthDay = DateTime.Parse(data.Info_client.date_of_birth.ToString());
                    PassSerial = data.Info_client.serial_pass;
                    PassNumber = data.Info_client.numer_pass;
                    ClassLimit = data.class_limit;
                    Experience = data.Exp;
                }
                else
                    throw new NotFoundExceptionMine("Пользователь не найден");
            }
        }
        public ClientClass(string lastname, string firstname, string middlename, int lic, string category,
            DateTime birthday, string passserial, string passnumber, string login, string password,string phone)
        {
            try
            {
                using (var db = new Model1())
                {
                    var check = db.License.Where(c => c.number_license == lic).FirstOrDefault();
                    if (check != null)
                        throw new InsertErrorExceptionMine("Данное Водительское удостоверение уже записано в базе");
                    else
                    {
                        Client record = new Client();
                        Info_client recordInfo = new Info_client();
                        License license = new License();
                        record.login = login;
                        record.password = password;
                        recordInfo.Lastname = lastname;
                        recordInfo.Firstname = firstname;
                        recordInfo.Middlename = middlename;
                        license.number_license = lic;
                        license.Category = category;
                        db.License.Add(license);
                        db.SaveChanges();
                        recordInfo.License = license;
                        recordInfo.date_of_birth = birthday;
                        recordInfo.numer_pass = passnumber;
                        recordInfo.serial_pass = passserial;
                        recordInfo.Phone = phone;
                        record.Info_client = recordInfo;
                        recordInfo.Client = record;
                        db.Info_client.Add(recordInfo);
                        record.class_limit = false;
                        db.Client.Add(record);
                        db.SaveChanges();

                        Id = record.id;
                        LastName = record.Info_client.Lastname;
                        FirstName = record.Info_client.Firstname;
                        MiddleName = record.Info_client.Middlename;
                        License = record.Info_client.License;
                        BirthDay = DateTime.Parse(record.Info_client.date_of_birth.ToString());
                        PassSerial = record.Info_client.serial_pass;
                        Phone = record.Info_client.Phone;
                        PassNumber = record.Info_client.numer_pass;
                        ClassLimit = record.class_limit;
                        Experience = record.Exp;
                    }
                    
                }
            }
            catch(Exception ex)
            {
                throw new InsertErrorExceptionMine("Ошибка при внесении нового пользователя в базу\nКод ошибки:\n" + ex.Message);
            }
                
        }
        /// <summary>
        /// Получить массив клиентов
        /// </summary>
        /// <returns></returns>
        static public ClientClass[] GetClients()
        {
            using (var db = new Model1())
            {
                var list = db.Client.Select(c => new ClientClass
                {
                    Id = c.id,
                    BirthDay = c.Info_client.date_of_birth,
                    ClassLimit = c.class_limit,
                    Experience = c.Exp,
                    FirstName = c.Info_client.Firstname,
                    LastName = c.Info_client.Lastname,
                    MiddleName = c.Info_client.Middlename,
                    License = c.Info_client.License,
                    PassNumber = c.Info_client.numer_pass,
                    PassSerial = c.Info_client.serial_pass,
                    Phone = c.Info_client.Phone
                }).ToArray();
                return list;
            }
        }

        static public ClientClass[] GetClients(bool ver)
        {
            using (var db = new Model1())
            {
                var list = db.Client.Where(c => c.class_limit == false).Select(c => new ClientClass
                {
                    Id = c.id,
                    BirthDay = c.Info_client.date_of_birth,
                    ClassLimit = c.class_limit,
                    Experience = c.Exp,
                    FirstName = c.Info_client.Firstname,
                    LastName = c.Info_client.Lastname,
                    MiddleName = c.Info_client.Middlename,
                    License = c.Info_client.License,
                    PassNumber = c.Info_client.numer_pass,
                    PassSerial = c.Info_client.serial_pass,
                    Phone = c.Info_client.Phone
                }).ToArray();
                return list;
            }
        }

        /// <summary>
        /// Обновить информацию о клиенте
        /// </summary>
        public void Update()
        {
            using (var db = new Model1())
            {
                var upd = db.Client.Where(c => c.id == Id).FirstOrDefault();
                upd.Exp = Experience;
                upd.Info_client.Phone = Phone;
                upd.Info_client.serial_pass = PassSerial;
                upd.Info_client.numer_pass = PassNumber;
                upd.Info_client.date_of_birth = BirthDay;
                upd.Info_client.Firstname = FirstName;
                upd.Info_client.Lastname = LastName;
                upd.Info_client.Middlename = MiddleName;
                upd.class_limit = ClassLimit;
                db.SaveChanges();
            }
        }
    }
}
