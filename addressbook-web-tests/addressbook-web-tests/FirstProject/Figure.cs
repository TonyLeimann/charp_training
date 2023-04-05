using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    internal class Figure
    {
        private bool colored = false;

    
        public bool Colored // метод для информации о размере квадрата & Метод задания размера квадрата
       {
        get
        {
            return colored;

        }
        set
        {
            colored = value;
        }


       }


    }

}


