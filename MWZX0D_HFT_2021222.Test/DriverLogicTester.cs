using Moq;
using MWZX0D_HFT_2021222.Logic.Classes;
using MWZX0D_HFT_2021222.Models;
using MWZX0D_HFT_2021222.Repository.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MWZX0D_HFT_2021222.Test
{
    [TestFixture]
    public class DriverLogicTester
    {
        DriverLogic logic;
        Mock<IRepository<Driver>> mockDriverRepo;

        [SetUp]
        public void Init()
        {
            // Only reserved drivers
            mockDriverRepo = new Mock<IRepository<Driver>>();
            mockDriverRepo.Setup(x => x.ReadAll()).Returns(new List<Driver>()
            { 
                new Driver("1$1$Stoffel Vandoorne$2$Belgian$1992-03-26"),
                //new Driver("2$1$Nyck De Vries$17$Dutch$1995-02-06"),
                new Driver("3$2$Liam Lawson$5$New Zealander$2002-02-11"),
                new Driver("4$2$Sébastien Buemi$8$Swiss$1988-10-31"),
                new Driver("5$3$Antonio Giovinazzi$99$Italian$1993-12-14"),
                //new Driver("6$3$Mick Schumacher$47$German$1999-03-22"),
                new Driver("7$4$Paul di Resta$40$British-Scottish$1986-04-16"),
                new Driver("8$4$Oscar Piastri$20$Austrian$2001-04-06"),
                new Driver("9$5$Oscar Piastri$2$Austrian$2001-04-06"),
                //new Driver("10$6$Liam Lawson$5$New Zealander$2002-02-11"),
                new Driver("11$7$Nico Hulkenberg$27$German$1987-08-19"),
                new Driver("12$8$Jack Aitken$22$German$1995-09-23"),
                new Driver("13$9$Robert Kubica$88$Polish$1984-12-07"),
                new Driver("14$10$Pietro Fittipaldi$51$Brazilian$1996-06-25"),
            }.AsQueryable());

            logic = new DriverLogic(mockDriverRepo.Object);
        }

        [Test]
        public void CreateDriverTestWithCorrectAge()
        {
            var driver = new Driver()
            { 
                Born = DateTime.Parse("1950-05-13")
            };

            // ACT
            logic.Create(driver);

            // ASSERT
            mockDriverRepo.Verify(x => x.Create(driver), Times.Once);
        }

        [Test]
        public void CreateDriverTestWithIncorrectAge()
        {
            var driver = new Driver()
            {
                Born = DateTime.Parse("2022-12-12")
            };

            // ACT
            logic.Create(driver);

            // ASSERT
            //mockDriverRepo.Verify(x => x.Create(driver), )
        }
    }
}
