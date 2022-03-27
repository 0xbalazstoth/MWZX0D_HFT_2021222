using Moq;
using MWZX0D_HFT_2021222.Logic.Classes;
using MWZX0D_HFT_2021222.Logic.Exceptions;
using MWZX0D_HFT_2021222.Logic.Interfaces;
using MWZX0D_HFT_2021222.Models;
using MWZX0D_HFT_2021222.Repository.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static MWZX0D_HFT_2021222.Logic.Classes.DriverLogic;
using static MWZX0D_HFT_2021222.Logic.Classes.TeamLogic;

namespace MWZX0D_HFT_2021222.Test
{
    [TestFixture]
    public class LogicTester
    {
        DriverLogic driverLogic;
        TeamLogic teamLogic;
        EngineManufacturerLogic engineManufacturerLogic;

        Mock<IRepository<Driver>> mockDriverRepo;
        Mock<IRepository<Team>> mockTeamRepo;
        Mock<IRepository<EngineManufacturer>> mockEngineManufacturerRepo;

        [SetUp]
        public void Init()
        {
            #region Repos
            mockDriverRepo = new Mock<IRepository<Driver>>();
            mockTeamRepo = new Mock<IRepository<Team>>();
            mockEngineManufacturerRepo = new Mock<IRepository<EngineManufacturer>>();
            #endregion
            #region Lists
            var engineManufacturers = new List<EngineManufacturer>()
            {
                new EngineManufacturer()
                { 
                    EngineManufacturerId = 1,
                    Name = "Mercedes"
                },
                new EngineManufacturer()
                {
                    EngineManufacturerId = 2,
                    Name = "Honda"
                },
                new EngineManufacturer()
                {
                    EngineManufacturerId = 3,
                    Name = "Ferrari"
                },
                new EngineManufacturer()
                {
                    EngineManufacturerId = 4,
                    Name = "Renault"
                },
            }.AsQueryable();
            var teams = new List<Team>()
            {
                new Team()
                { 
                    TeamId = 1,
                    EngineManufacturerId = 1,
                    Name = "Mercedes",
                    LicensedIn = "Germany",
                    EngineManufacturer = engineManufacturers.ElementAt(0),
                    Principal = "Toto Wolff"
                },
                new Team()
                {
                    TeamId = 2,
                    EngineManufacturerId = 2,
                    Name = "Red Bull",
                    LicensedIn = "Austria",
                    EngineManufacturer = engineManufacturers.ElementAt(1),
                    Principal = "Christian Horner"
                },
                new Team()
                {
                    TeamId = 3,
                    EngineManufacturerId = 3,
                    Name = "Ferrari",
                    LicensedIn = "Italy",
                    EngineManufacturer = engineManufacturers.ElementAt(2),
                    Principal = "Mattia Binotto"
                },
                new Team()
                {
                    TeamId = 4,
                    EngineManufacturerId = 1,
                    Name = "McLaren",
                    LicensedIn = "United Kingdom",
                    EngineManufacturer = engineManufacturers.ElementAt(0),
                    Principal = "Andreas Seidl"
                },
                new Team()
                {
                    TeamId = 5,
                    EngineManufacturerId = 4,
                    Name = "Alpine",
                    LicensedIn = "France",
                    EngineManufacturer = engineManufacturers.ElementAt(3),
                    Principal = "Otmar Szafnauer"
                },
                new Team()
                {
                    TeamId = 6,
                    EngineManufacturerId = 2,
                    Name = "Alpha Tauri",
                    LicensedIn = "Italy",
                    EngineManufacturer = engineManufacturers.ElementAt(1),
                    Principal = "Franz Tost"
                },
                new Team()
                {
                    TeamId = 7,
                    EngineManufacturerId = 1,
                    Name = "Aston Martin",
                    LicensedIn = "United Kingdom",
                    EngineManufacturer = engineManufacturers.ElementAt(0),
                    Principal = "Mike Krack"
                },
                new Team()
                {
                    TeamId = 8,
                    EngineManufacturerId = 1,
                    Name = "Williams",
                    LicensedIn = "United Kingdom",
                    EngineManufacturer = engineManufacturers.ElementAt(0),
                    Principal = "Jost Capito"
                },
                new Team()
                {
                    TeamId = 9,
                    EngineManufacturerId = 3,
                    Name = "Alfa Romeo",
                    LicensedIn = "Switzerland",
                    EngineManufacturer = engineManufacturers.ElementAt(3),
                    Principal = "Frederic Vassuer"
                },
                new Team()
                {
                    TeamId = 10,
                    EngineManufacturerId = 3,
                    Name = "Haas",
                    LicensedIn = "United States",
                    EngineManufacturer = engineManufacturers.ElementAt(2),
                    Principal = "Guenther Steiner"
                },
            }.AsQueryable();
            var drivers = new List<Driver>()
            {
                new Driver()
                {
                    DriverId = 1,
                    Team = teams.ElementAt(0),
                    TeamId = 1,
                    Name = "Stoffel Vandoorne",
                    Number = 2,
                    Nationality = "Belgian",
                    Born = DateTime.Parse("1992-03-26")
                },
                new Driver()
                {
                    DriverId = 2,
                    Team = teams.ElementAt(0),
                    TeamId = 1,
                    Name = "Nyck De Vries",
                    Number = 17,
                    Nationality = "Dutch",
                    Born = DateTime.Parse("1995-02-06")
                },
                new Driver()
                {
                    DriverId = 3,
                    Team = teams.ElementAt(1),
                    TeamId = 2,
                    Name = "Sébastien Buemi",
                    Number = 8,
                    Nationality = "Swiss",
                    Born = DateTime.Parse("1988-10-31")
                },
                new Driver()
                {
                    DriverId = 4,
                    Team = teams.ElementAt(2),
                    TeamId = 3,
                    Name = "Antonio Giovinazzi",
                    Number = 99,
                    Nationality = "Italian",
                    Born = DateTime.Parse("1993-12-14")
                },
                new Driver()
                {
                    DriverId = 5,
                    Team = teams.ElementAt(2),
                    TeamId = 3,
                    Name = "Mick Schumacher",
                    Number = 47,
                    Nationality = "German",
                    Born = DateTime.Parse("1999-03-22")
                },
                new Driver()
                {
                    DriverId = 6,
                    Team = teams.ElementAt(3),
                    TeamId = 4,
                    Name = "Paul di Resta",
                    Number = 40,
                    Nationality = "British-Scottish",
                    Born = DateTime.Parse("1986-04-16")
                },
                new Driver()
                {
                    DriverId = 7,
                    Team = teams.ElementAt(4),
                    TeamId = 5,
                    Name = "Oscar Piastri",
                    Number = 20,
                    Nationality = "Austrian",
                    Born = DateTime.Parse("2001-04-06")
                },
                new Driver()
                {
                    DriverId = 8,
                    Team = teams.ElementAt(5),
                    TeamId= 6,
                    Name = "Liam Lawson",
                    Number = 5,
                    Nationality = "New Zealander",
                    Born = DateTime.Parse("2002-02-11")
                },
                new Driver()
                {
                    DriverId = 9,
                    Team = teams.ElementAt(6),
                    TeamId = 7,
                    Name = "Nico Hulkenberg",
                    Number = 27,
                    Nationality = "German",
                    Born = DateTime.Parse("1987-08-19")
                },
                new Driver()
                {
                    DriverId = 10,
                    Team = teams.ElementAt(7),
                    TeamId = 8,
                    Name = "Jack Aitken",
                    Number = 22,
                    Nationality = "British",
                    Born = DateTime.Parse("1995-09-23")
                },
                new Driver()
                {
                    DriverId = 11,
                    Team = teams.ElementAt(8),
                    TeamId = 9,
                    Name = "Robert Kubica",
                    Number = 88,
                    Nationality = "Polish",
                    Born = DateTime.Parse("1984-12-07")
                },
                new Driver()
                {
                    DriverId = 12,
                    Team = teams.ElementAt(9),
                    TeamId = 10,
                    Name = "Pietro Fittipaldi",
                    Number = 51,
                    Nationality = "Brazilian",
                    Born = DateTime.Parse("1996-06-25")
                },
            }.AsQueryable();

            #region Add Teams to Engine manufacturer
            // Mercedes engine based teams
            engineManufacturers.ElementAt(0).Teams.Add(teams.ElementAt(0));
            engineManufacturers.ElementAt(0).Teams.Add(teams.ElementAt(3));
            engineManufacturers.ElementAt(0).Teams.Add(teams.ElementAt(6));
            engineManufacturers.ElementAt(0).Teams.Add(teams.ElementAt(7));

            // Honda engine based teams
            engineManufacturers.ElementAt(1).Teams.Add(teams.ElementAt(1));
            engineManufacturers.ElementAt(1).Teams.Add(teams.ElementAt(5));

            // Ferrari engine based teams
            engineManufacturers.ElementAt(2).Teams.Add(teams.ElementAt(2));
            engineManufacturers.ElementAt(2).Teams.Add(teams.ElementAt(8));
            engineManufacturers.ElementAt(2).Teams.Add(teams.ElementAt(9));

            // Renault engine based teams
            engineManufacturers.ElementAt(3).Teams.Add(teams.ElementAt(4));
            #endregion
            #region Add Drivers to Team
            // Mercedes
            teams.ElementAt(0).Drivers.Add(drivers.ElementAt(0));
            teams.ElementAt(0).Drivers.Add(drivers.ElementAt(1));

            // Red Bull
            teams.ElementAt(1).Drivers.Add(drivers.ElementAt(2));

            // Ferrari
            teams.ElementAt(2).Drivers.Add(drivers.ElementAt(3));
            teams.ElementAt(2).Drivers.Add(drivers.ElementAt(4));

            // McLaren
            teams.ElementAt(3).Drivers.Add(drivers.ElementAt(5));

            // Alpine
            teams.ElementAt(4).Drivers.Add(drivers.ElementAt(6));

            // Alpha Tauri
            teams.ElementAt(5).Drivers.Add(drivers.ElementAt(7));

            // Aston Martin
            teams.ElementAt(6).Drivers.Add(drivers.ElementAt(8));

            // Williams
            teams.ElementAt(7).Drivers.Add(drivers.ElementAt(9));

            // Alfa Romeo
            teams.ElementAt(8).Drivers.Add(drivers.ElementAt(10));

            // Haas
            teams.ElementAt(9).Drivers.Add(drivers.ElementAt(11));
            #endregion

            #endregion
            #region Logics
            driverLogic = new DriverLogic(mockDriverRepo.Object, mockTeamRepo.Object, mockEngineManufacturerRepo.Object);
            teamLogic = new TeamLogic(mockTeamRepo.Object);
            engineManufacturerLogic = new EngineManufacturerLogic(mockEngineManufacturerRepo.Object);
            #endregion
            #region Repositories setup/init
            mockDriverRepo.Setup(x => x.ReadAll()).Returns(drivers);
            mockTeamRepo.Setup(x => x.ReadAll()).Returns(teams);
            mockEngineManufacturerRepo.Setup(x => x.ReadAll()).Returns(engineManufacturers);
            #endregion
        }

        #region Crud method tests
        [Test]
        public void GetAllDriversTest()
        {
            var actual = driverLogic.ReadAll();
            Assert.That(mockDriverRepo.Object.ReadAll().Count, Is.EqualTo(12));
        }

        [Test]
        public void GetAllTeamsTest()
        { 
            var actual = teamLogic.ReadAll();
            Assert.That(mockTeamRepo.Object.ReadAll().Count, Is.EqualTo(10));
        }

        [Test]
        public void GetAllEngineManufacturersTest()
        { 
            var actual = engineManufacturerLogic.ReadAll();
            Assert.That(mockEngineManufacturerRepo.Object.ReadAll().Count, Is.EqualTo(4));
        }

        [Test]
        public void CreateDriverTestWithCorrectAge()
        {
            var driver = new Driver()
            { 
                Born = DateTime.Parse("1950-05-13")
            };

            // ACT
            driverLogic.Create(driver);

            // ASSERT
            mockDriverRepo.Verify(x => x.Create(driver), Times.Once);
        }

        [Test]
        public void CreateDriverTestDriverIsTooYoungException()
        {
            var driver = new Driver()
            {
                Born = DateTime.Parse("2022-12-12")
            };

            // ACT & ASSERT
            Assert.Throws<DriverIsTooYoungException>(() => driverLogic.Create(driver));
            mockDriverRepo.Verify(x => x.Create(driver), Times.Never);
        }

        [Test]
        public void CreateTeamTestWithCorrectName()
        {
            var team = new Team()
            {
                Name = "BMW"
            };

            teamLogic.Create(team);

            mockTeamRepo.Verify(x => x.Create(team), Times.Once);
        }

        [Test]
        public void CreateTeamTestNameEmptyException()
        {
            var team = new Team()
            { 
                Name = ""
            };

            Assert.Throws<NameEmptyException>(() => teamLogic.Create(team));
            mockTeamRepo.Verify(x => x.Create(team), Times.Never);
        }

        [Test]
        public void CreateEngineManufacturerTestWithCorrectNameLength()
        {
            var engineManufacturer = new EngineManufacturer()
            { 
                Name = "Bugatti"
            };

            engineManufacturerLogic.Create(engineManufacturer);

            mockEngineManufacturerRepo.Verify(x => x.Create(engineManufacturer), Times.Once);
        }

        [Test]
        public void CreateEngineManufacturerTestEngineManufacturerNameIsTooShortException()
        {
            var engineManufacturer = new EngineManufacturer()
            { 
                Name = "BA"
            };

            Assert.Throws<EngineManufacturerNameIsTooShortException>(() => engineManufacturerLogic.Create(engineManufacturer));
            mockEngineManufacturerRepo.Verify(x => x.Create(engineManufacturer), Times.Never);
        }

        #endregion
        #region Non-crud method tests
        [Test]
        // Given two teams, is there any driver who's number is between specific range, who is it and what's his number?
        public void GetDriversWhosNumberIsBetweenSpecificRangeTest()
        {
            var actual = driverLogic.GetDriversWhosNumberIsBetweenSpecificRange("ferrari", "mercedes", 5, 49).ToList();
            var expected = new List<GivenNumber>()
            { 
                new GivenNumber()
                { 
                    DriverName = "Nyck De Vries",
                    Number = 17,
                    TeamName = "Mercedes"
                },
                new GivenNumber()
                {
                    DriverName = "Mick Schumacher",
                    Number = 47,
                    TeamName = "Ferrari"
                },
            };

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        // Calculate the average driver's age in the same engine based teams.
        public void GetAvgDriversAgeByTheSameEngineBasedTeamsTest()
        {
            var actual = driverLogic.GetAvgDriversAgeByTheSameEngineBasedTeams();
            var expected = new List<SameEngine>()
            { 
                new SameEngine()
                { 
                    Avg = 31,
                    Engine = "Mercedes"
                },
                new SameEngine()
                {
                    Avg = 27,
                    Engine = "Honda"
                },
                new SameEngine()
                {
                    Avg = 26,
                    Engine = "Ferrari"
                },
                new SameEngine()
                {
                    Avg = 29.5,
                    Engine = "Renault"
                },
            };

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        // What is the sum of the number of drivers per headquarter, which is at least the given value.
        public void GetSumPerHeadquarterAtLeastGivenValueTest()
        {
            var actual = driverLogic.GetSumPerHeadquarterAtLeastGivenValue(10);
            var expected = new List<SumNumberEngine>()
            { 
                new SumNumberEngine()
                { 
                    HeadQuarter = "Germany",
                    Sum = 19,
                },
                new SumNumberEngine()
                {
                    HeadQuarter = "Italy",
                    Sum = 151,
                },
                new SumNumberEngine()
                {
                    HeadQuarter = "United Kingdom",
                    Sum = 89,
                },
                new SumNumberEngine()
                {
                    HeadQuarter = "France",
                    Sum = 20,
                },
                new SumNumberEngine()
                {
                    HeadQuarter = "Switzerland",
                    Sum = 88,
                },
                new SumNumberEngine()
                {
                    HeadQuarter = "United States",
                    Sum = 51,
                },
            };

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        // If the team's principal's name contains specific letter, which engine they are using and what his/her name?
        public void GetEngineManufacturerByPrincipalNameIfContainsSpecificLetterTest()
        {
            var actual = teamLogic.GetEngineManufacturerByPrincipalNameIfContainsSpecificLetter('z');
            var expected = new List<PrincipalLetter>()
            { 
                new PrincipalLetter()
                { 
                    Engine = "Renault",
                    Name = "Otmar Szafnauer"
                },
                new PrincipalLetter()
                {
                    Engine = "Honda",
                    Name = "Franz Tost"
                },
            };

            Assert.That(actual, Is.EqualTo(expected));
        }
        #endregion
    }
}
