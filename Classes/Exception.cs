using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Classes
{
    class NotFoundExceptionMine : Exception
    {
        public NotFoundExceptionMine() : base("Запись не найдена в базе данных") { }
        public NotFoundExceptionMine(string message) : base(message) { }

    }
    class InsertErrorExceptionMine :Exception
    {
        public InsertErrorExceptionMine() : base("Ошибка внесения данных в базу") { }
        public InsertErrorExceptionMine(string message) : base(message) { }
    }
}
