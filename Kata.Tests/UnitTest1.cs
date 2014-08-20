using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit;

namespace Kata.Tests
{
    [TestClass]
    public class UnitTest1
    {

        private int _a = 50;
        private int _b = 30;
        private int _c = 20;
        private int _d = 15;
        private Dictionary<string, int> _prices;
        private Dictionary<string, int> _specials;



        private float CheckoutTest(IEnumerable<string> items)
        {
            //Arrange
            var _prices = new Dictionary<string, float>
                             {
                                 {"A", _a},
                                 {"B", _b},
                                 {"C", _c},
                                 {"D", _d}
                             };

            var _specials = new Dictionary<string[], float>
                            {
                                 {new [] {"A", "A", "A"}, 130},
                                 {new [] {"B", "B"}, 45},
                             };

            var checkout = new Checkout(_prices, _specials);

            if (items != null)
            {
                foreach (var item in items)
                {
                    checkout.Scan(item);
                }
            }

            //Act
            return checkout.Total();
        }

        [TestMethod]
        public void CheckNoScans()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(null).Equals(0));
        }

        [TestMethod]
        public void CheckA()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new [] {"A"}).Equals(_a));
        }

        [TestMethod]
        public void CheckB()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "B" }).Equals(_b));
        }

        [TestMethod]
        public void CheckC()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "C" }).Equals(_c));
        }

        [TestMethod]
        public void CheckD()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "D" }).Equals(_d));
        }

        [TestMethod]
        public void CheckAb()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "A", "B" }).Equals(_a + _b));
        }

        [TestMethod]
        public void CheckCdba()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "C", "D", "B", "A" }).Equals(_c + _d + _b + _a));
        }

        [TestMethod]
        public void CheckAa()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "A", "A" }).Equals(_a * 2));
        }

        [TestMethod]
        public void CheckAaaSpecialOffer()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "A", "A", "A" }).Equals(130));
        }

        [TestMethod]
        public void CheckAaaaSpecialOffer()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "A", "A", "A", "A" }).Equals(180));
        }

        [TestMethod]
        public void CheckAaaaaDoubleSpecialOffer()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "A", "A", "A", "A", "A" }).Equals(230));
        }

        [TestMethod]
        public void CheckAaaaaaDoubleSpecialOffer()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "A", "A", "A", "A", "A", "A" }).Equals(260));
        }

        [TestMethod]
        public void CheckAaab()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "A", "A", "A", "B" }).Equals(160));
        }

        [TestMethod]
        public void CheckAaabb()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "A", "A", "A", "B", "B" }).Equals(175));
        }

        [TestMethod]
        public void CheckAaabbd()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "A", "A", "A", "B", "B", "D" }).Equals(190));
        }

        [TestMethod]
        public void CheckDababa()
        {
            //Assert
            Assert.IsTrue(CheckoutTest(new[] { "D", "A", "B", "A", "B", "A" }).Equals(190));
        }

        [TestMethod]
        public void TestIncremental()
        {
            //Arrange
            var _prices = new Dictionary<string, float>
                             {
                                 {"A", _a},
                                 {"B", _b},
                                 {"C", _c},
                                 {"D", _d}
                             };

            var _specials = new Dictionary<string[], float>
                            {
                                 {new [] {"A", "A", "A"}, 130},
                                 {new [] {"B", "B"}, 45},
                             };

            //Arrange
            var checkout = new Checkout(_prices, _specials);

            //Act
            var total = checkout.Total();

            //Assert
            Assert.IsTrue(total.Equals(0));

            //Act
            checkout.Scan("A");
            total = checkout.Total();

            //Assert
            Assert.IsTrue(total.Equals(50));

            //Act
            checkout.Scan("B");
            total = checkout.Total();

            //Assert
            Assert.IsTrue(total.Equals(80));

            //Act
            checkout.Scan("A");
            total = checkout.Total();

            //Assert
            Assert.IsTrue(total.Equals(130));

            //Act
            checkout.Scan("A");
            total = checkout.Total();

            //Assert
            Assert.IsTrue(total.Equals(160));

            //Act
            checkout.Scan("B");
            total = checkout.Total();

            //Assert
            Assert.IsTrue(total.Equals(175));
        }
    }
}
