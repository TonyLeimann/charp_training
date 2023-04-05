using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    internal class Square : Figure // Созданный класс
    {
        private int size; // Размер квадрата (поле)

        public Square(int size) // Конструктор с параметром
        {
            this.size = size; // В поле присваеваем значение,переданное в качестве параметра
        }

        public int Size // метод для информации о размере квадрата & Метод задания размера квадрата
        {
            get 
            { 
                return size; 
            
            }
            set 
            { 
                size = value;
            }
        }
        
            

    }
}
