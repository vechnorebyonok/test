using Kursach.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Classes
{
    class CarClass
    {
        public int Id { get; private set; }
        public string Class { get; set; }
        public string Colour { get; set; }
        public DateTime DateRelease { get; set; }
        public string Body { get; set; }
        public string Model { get; set; }
        public string Marka { get; set; }
        public string VIN { get; set; }
        public string GOS { get; set; }
        /// <summary>
        /// Получить массив всех машин из базы
        /// </summary>
        /// <returns>Id, Class, Colour, DateRelease, Body, Model, Model, Marka, VIN, GOS</returns>
        static public CarClass[] GetCars()
        {
            using (var db = new Model1())
            {
                var list = db.Vehicle.Select(c => new CarClass
                {
                    Id = c.id,
                    Class = c.Class.Name_class,
                    Colour = c.color,
                    DateRelease = c.date_release,
                    Body = c.Type_body.Name_body,
                    Model = c.Model.name_model,
                    Marka = c.Model.marka.name_marka,
                    VIN = c.VIN_number,
                    GOS = c.Gos_number
                }).ToArray();
                return list;
            }
        }
        /// <summary>
        /// Получить массив всех машин из базы по имени модели
        /// </summary>
        /// <param name="model">Модель</param>
        /// <returns></returns>
        static public CarClass[] GetCars(string model, string Class)
        {
            using (var db = new Model1())
            {
                var list = db.Vehicle.Where(c => c.Model.name_model == model && c.Class.Name_class == Class).Select(c => new CarClass
                {
                    Id = c.id,
                    Class = c.Class.Name_class,
                    Colour = c.color,
                    DateRelease = c.date_release,
                    Body = c.Type_body.Name_body,
                    Model = c.Model.name_model,
                    Marka = c.Model.marka.name_marka,
                    VIN = c.VIN_number,
                    GOS = c.Gos_number
                }).ToArray();
                return list;
            }
        }
        public CarClass(int id)
        {
            using (var db = new Model1())
            {
                var data = db.Vehicle.Where(c => c.id == id).FirstOrDefault();
                if (data == null)
                    throw new NotFoundExceptionMine("Запись автомобиля не найдена");
                Id = data.id;
                Class = data.Class.Name_class;
                Colour = data.color;
                DateRelease = data.date_release;
                Body = data.Type_body.Name_body;
                Model = data.Model.name_model;
                Marka = data.Model.marka.name_marka;
                VIN = data.VIN_number;
                GOS = data.Gos_number;
            }
        }
        public void UpdateColour(string colour)
        {
            using (var db = new Model1())
            {
                Vehicle upd = db.Vehicle.Where(c => c.id == Id).FirstOrDefault();
                if (upd != null)
                {
                    upd.color = colour;
                    db.SaveChanges();
                    Colour = colour;
                }
                else
                    throw new NotFoundExceptionMine("Ошибка обновления, запись не найдена");
            }
        }
        /// <summary>
        /// Не используй этот конструктор, он только для выгрузки в массив
        /// </summary>
        public CarClass() { }
        /// <summary>
        /// Добавление нового автомобиля в базу данных
        /// </summary>
        public CarClass(string Class, string colour, DateTime daterelease, string body, string model, string marka, string VIN, string GOS)
        {
            using (var db = new Model1())
            {
                DataBase.Class recordClass = db.Class.Where(c => c.Name_class == Class).FirstOrDefault();
                if(recordClass == null)
                {
                    recordClass = new DataBase.Class();
                    recordClass.Name_class = Class;
                    db.Class.Add(recordClass);
                    db.SaveChanges();
                }
                DataBase.Type_body recordBody = db.Type_body.Where(c => c.Name_body == body).FirstOrDefault();
                if(recordBody == null)
                {
                    recordBody = new Type_body();
                    recordBody.Name_body = body;
                    db.Type_body.Add(recordBody);
                    db.SaveChanges();
                }
                DataBase.marka recordMarka = db.marka.Where(c => c.name_marka == marka).FirstOrDefault();
                if(recordMarka == null)
                {
                    recordMarka = new marka();
                    recordMarka.name_marka = marka;
                    db.marka.Add(recordMarka);
                    db.SaveChanges();
                }
                DataBase.Model recordModel = db.Model.Where(c => c.name_model == model && c.marka.code_marka == recordMarka.code_marka).FirstOrDefault();
                if(recordModel == null)
                {
                    recordModel = new Model();
                    recordModel.name_model = model;
                    recordModel.marka = recordMarka;
                    db.Model.Add(recordModel);
                    db.SaveChanges();
                }
                Vehicle car = db.Vehicle.Where(c => c.Gos_number == GOS && c.VIN_number == VIN).FirstOrDefault();
                if (car != null)
                    throw new InsertErrorExceptionMine("Данный автомобиль уже добавлен в базу данных");
                else
                {
                    car = new Vehicle();
                    car.Class = recordClass;
                    car.color = colour;
                    car.date_release = daterelease;
                    car.Model = recordModel;
                    car.Type_body = recordBody;
                    car.VIN_number = VIN;
                    car.Gos_number = GOS;
                    db.Vehicle.Add(car);
                    db.SaveChanges();
                    Id = car.id;
                    Class = car.Class.Name_class;
                    Colour = car.color;
                    DateRelease = DateTime.Parse(car.date_release.ToString());
                    Body = car.Type_body.Name_body;
                    Model = car.Model.name_model;
                    Marka = car.Model.marka.name_marka;
                    VIN = car.VIN_number;
                    GOS = car.Gos_number;
                }


            }
        }
    }

}
