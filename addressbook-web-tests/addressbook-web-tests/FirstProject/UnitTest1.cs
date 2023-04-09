using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace addressbook_web_tests.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodSquare()
        {
            Square s1 = new Square(5); // Объекты класса с параметром размера
            Square s2 = new Square(10);
            Square s3 = s1;


            Assert.AreEqual(s1.Size, 5);
            Assert.AreEqual(s2.Size, 10);
            Assert.AreEqual(s3.Size, 5);

            s3.Size = 88;

            Assert.AreEqual(s1.Size, 88);

            s2.Colored = true;

            Assert.AreEqual(s2.Colored, true);
            Assert.AreEqual(s3.Colored, false);
                      

        }
        [TestMethod]
        public void TestMethodCircle() 
        {
            Circle c1 = new Circle(5);
            Circle c2 = new Circle(10);
            Circle c3 = c1;

            Assert.AreEqual(c1.Radius, 5);
            Assert.AreEqual(c2.Radius, 10);

            c3.Radius = 89;

            Assert.AreEqual(c1.Radius, 89);

            

            Assert.AreEqual(c2.Colored, false); 



        }




    }
}
