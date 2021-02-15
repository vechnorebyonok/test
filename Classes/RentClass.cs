using Kursach.DataBase;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Classes
{
    class RentClass
    {
        public int Id { get; private set; }
        public DateTime date { get; set; }
        public Pay pay { get; set; }
        public ClientClass Client { get; set; }
        public string ClientLastName { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientMiddleName { get; set; }
        public int ClientSetter
        {
            get
            {
                return Client.Id;
            }
            set
            {
                Client = new ClientClass(value);
            }
        }
        public CarClass Car { get; set; }
        public string CarModel { get; set; }
        public string CarClass { get; set; }
        public string GOS { get; set; }
        public int CarSetter
        {
            get
            {
                return Car.Id;
            }
            set
            {
                Car = new CarClass(value);
            }
        }
        public int Hour { get; set; }
        /// <summary>
        /// Не используй этот конструктор
        /// </summary>
        public RentClass() { }
        public RentClass(ClientClass client, CarClass car, int hour, double price) 
        {
            using (var db = new Model1())
            {
                Contract recordContract = new Contract();
                recordContract.date = DateTime.Today;
                recordContract.client_id = client.Id;
                recordContract.veh_id = car.Id;
                recordContract.hours = hour;
                db.Contract.Add(recordContract);
                db.SaveChanges();
                Pay pay = new Pay();
                pay.Contract1 = recordContract;
                pay.Price = price;
                db.Pay.Add(pay);
                db.SaveChanges();
                Client = client;
                Car = car;
                Hour = hour;
                date = recordContract.date;
                this.pay = pay;
                Id = recordContract.id;
            }
        }
        /// <summary>
        /// Получить список истории
        /// </summary>
        /// <returns></returns>
        static public RentClass[] GetHistory()
        {
            using (var db = new Model1())
            {
                var list = db.Contract.Select(c => new RentClass
                {
                    Id = c.id,
                    date = c.date,
                    pay = c.Pay.FirstOrDefault(),
                    CarSetter = c.Vehicle.id,
                    CarClass = c.Vehicle.Class.Name_class,
                    CarModel = c.Vehicle.Model.name_model,
                    GOS = c.Vehicle.Gos_number,
                    ClientSetter = c.client_id,
                    ClientFirstName = c.Client.Info_client.Firstname,
                    ClientLastName = c.Client.Info_client.Lastname,
                    ClientMiddleName = c.Client.Info_client.Middlename,
                    Hour = c.hours
                }).ToArray();
                return list;
            }
        }

        /// <summary>
        /// Получить список истории клиента
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        static public RentClass[] GetHistory(ClientClass client)
        {
            using (var db = new Model1())
            {
                var list = db.Contract.Where(c => c.client_id == client.Id).Select(c => new RentClass
                {
                    Id = c.id,
                    date = c.date,
                    pay = c.Pay.FirstOrDefault(),
                    CarSetter = c.Vehicle.id,
                    CarClass = c.Vehicle.Class.Name_class,
                    CarModel = c.Vehicle.Model.name_model,
                    GOS = c.Vehicle.Gos_number,
                    ClientSetter = c.client_id,
                    ClientFirstName = c.Client.Info_client.Firstname,
                    ClientLastName = c.Client.Info_client.Lastname,
                    ClientMiddleName = c.Client.Info_client.Middlename,
                    Hour = c.hours
                }).ToArray();
                return list;
            }
        }
        public void AddCrash(DateTime date, string place, string condition)
        {
            using (var db = new Model1())
            {
                Description_DTP recordDesc = new Description_DTP();
                DTP recordDTP = new DTP();
                recordDesc.Place = place;
                recordDesc.Date = date.ToString();
                recordDesc.Conditions = condition;
                db.Description_DTP.Add(recordDesc);
                recordDTP.date = date.ToString();
                recordDTP.contract_id = Id;
                recordDTP.Description_DTP = recordDesc;
                db.DTP.Add(recordDTP);
                recordDesc.DTP = recordDTP;
                db.SaveChanges();
            }
        }
    }
}
