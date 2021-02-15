using Kursach.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Classes
{
    class PriceClass
    {
        public int Id { get; private set; }
        public string Class { get; private set; }
        public double Price { get; set; }
        /// <summary>
        /// Получить список актуальных цен и классов
        /// </summary>
        /// <returns></returns>
        static public PriceClass[] GetPrices()
        {
            using (var db = new Model1())
            {
                var list = db.PriceList.Where(c => c.Actual == true).Select(c => new PriceClass
                {
                    Id = c.Id,
                    Class = c.Class1.Name_class,
                    Price = c.Value
                }).ToArray();
                return list;
            }
        }
        /// <summary>
        /// Получить массив всех марок
        /// </summary>
        /// <returns>array of string</returns>
        static public string[] GetMarks()
        {
            using (var db = new Model1())
            {
                var list = db.marka.Select(c => c.name_marka).ToArray();
                return list;
            }
        }
        static public string[] GetBody()
        {
            using (var db = new Model1())
            {
                var list = db.Type_body.Select(c => c.Name_body).ToArray();
                return list;
            }
        }
        /// <summary>
        /// Получить массив моделей по заданной марке
        /// </summary>
        /// <param name="marka">Марка автомобиля</param>
        /// <returns>array of string</returns>
        static public string[] getModels(string marka)
        {
            using (var db = new Model1())
            {
                var list = db.Model.Where(c => c.marka.name_marka == marka).Select(c => c.name_model).ToArray();
                if (list.Length == 0)
                    throw new NotFoundExceptionMine("В таблице Model не найдено ни одной записи с маркой " + marka);
                return list;
            }
        }

        /// <summary>
        /// Не используй этот конструктор, он для выгрузки в массив
        /// </summary>
        public PriceClass() { }
        public PriceClass(string Class, double Price)
        {
            using (var db = new Model1())
            {
                var ClassRecord = db.Class.Where(c => c.Name_class == Class).FirstOrDefault();
                DataBase.PriceList PriceRecord;
                if(ClassRecord != null)
                {
                    PriceRecord = db.PriceList.Where(c => c.Class == ClassRecord.Code_class).FirstOrDefault();
                    if(PriceRecord != null)
                    {
                        PriceRecord.Actual = false;
                        db.SaveChanges();
                    }
                }
                else
                {
                    ClassRecord = new Class();
                    ClassRecord.Name_class = Class;
                    db.Class.Add(ClassRecord);
                    db.SaveChanges();
                }
                PriceRecord = new PriceList();
                PriceRecord.Actual = true;
                PriceRecord.DateStart = DateTime.Today;
                PriceRecord.Class1 = ClassRecord;
                PriceRecord.Value = Price;
                db.PriceList.Add(PriceRecord);
                db.SaveChanges();
            }
        }
        public PriceClass(string Class)
        {
            using (var db = new Model1())
            {
                var data = db.PriceList.Where(c => c.Actual == true && c.Class1.Name_class == Class).FirstOrDefault();
                if (data == null)
                    throw new NotFoundExceptionMine("Запись цены не найдена");
                Id = data.Id;
                Class = data.Class1.Name_class;
                Price = data.Value;
            }
        }
        public void Update()
        {
            using (var db = new Model1())
            {
                var upd = db.PriceList.Where(c => c.Id == Id).FirstOrDefault();
                if(upd != null)
                {
                    upd.Actual = false;
                    db.SaveChanges();
                }
                upd = new PriceList();
                upd.Actual = true;
                upd.Class1 = db.Class.Where(c => c.Name_class == Class).FirstOrDefault();
                upd.DateStart = DateTime.Today;
                upd.Value = Price;
                db.PriceList.Add(upd);
                db.SaveChanges();
            }
        }
    }
}
