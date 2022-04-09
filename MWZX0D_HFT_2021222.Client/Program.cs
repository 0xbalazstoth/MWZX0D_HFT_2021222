//using MWZX0D_HFT_2021222.Logic.Classes;
//using MWZX0D_HFT_2021222.Repository.Database;
//using MWZX0D_HFT_2021222.Repository.ModelRepositories;
using ConsoleTools;
using MWZX0D_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MWZX0D_HFT_2021222.Client
{
    internal class Program
    {
        static RestService rest;

        const string DRIVER_ENTITY = "Driver";
        const string DRIVER_ENDPOINT = "/api/driver";

        const string TEAM_ENTITY = "Team";
        const string TEAM_ENDPOINT = "/api/team";

        const string ENGINE_MANUFACTURER_ENTITY = "EngineManufacturer";
        const string ENGINE_MANUFACTURER_ENDPOINT = "/api/enginemanufacturer";

        #region CRUD
        static void List(string entity)
        {
            if (entity == DRIVER_ENTITY)
            {
                Console.Title = "List Driver";
                List<Driver> drivers = rest.Get<Driver>(DRIVER_ENDPOINT);
                foreach (var driver in drivers)
                {
                    Console.WriteLine($"Id: {driver.DriverId}: Name: {driver.Name}, Number: {driver.Number}, Nationality: {driver.Nationality}, Born: {driver.Born.ToString("yyyy-MM-dd")}, TeamId: {driver.TeamId}");
                }
            }
            else if (entity == TEAM_ENTITY)
            {
                Console.Title = "List Team";
                List<Team> teams = rest.Get<Team>(TEAM_ENDPOINT);
                foreach (var team in teams)
                {
                    Console.WriteLine($"Id: {team.TeamId}: Name: {team.Name}, Licensed in: {team.LicensedIn}, Principal: {team.Principal}");
                }
            }
            else if (entity == ENGINE_MANUFACTURER_ENTITY)
            {
                Console.Title = "List Engine Manufacturer";
                List<EngineManufacturer> ems = rest.Get<EngineManufacturer>(ENGINE_MANUFACTURER_ENDPOINT);
                foreach (var em in ems)
                {
                    Console.WriteLine($"Id: {em.EngineManufacturerId}: Name: {em.Name}");
                }
            }

            Console.Write("\n[PRESS ENTER TO GO BACK]");
            Console.ReadLine();
        }

        static void Read(string entity)
        {
            if (entity == DRIVER_ENTITY)
            {
                Console.Title = "Read Driver";

                Console.Write("Id: ");
                int driverId = int.Parse(Console.ReadLine());

                Driver driver = rest.Get<Driver>(DRIVER_ENDPOINT).Where(x => x.DriverId == driverId).FirstOrDefault();
                Console.WriteLine($"Name: {driver.Name}, Number: {driver.Number}, Nationality: {driver.Nationality}, Born: {driver.Born.ToString("yyyy-MM-dd")}, TeamId: {driver.TeamId}");
            }
            else if (entity == TEAM_ENTITY)
            {
                Console.Title = "Read Team";

                Console.Write("Id: ");
                int teamId = int.Parse(Console.ReadLine());

                Team team = rest.Get<Team>(TEAM_ENDPOINT).Where(x => x.TeamId == teamId).FirstOrDefault();
                Console.WriteLine($"Name: {team.Name}, Licensed in: {team.LicensedIn}, Principal: {team.Principal}");
            }
            else if (entity == ENGINE_MANUFACTURER_ENTITY)
            {
                Console.Title = "Read Engine Manufacturer";

                Console.Write("Id: ");
                int emId = int.Parse(Console.ReadLine());

                EngineManufacturer em = rest.Get<EngineManufacturer>(ENGINE_MANUFACTURER_ENDPOINT).Where(x => x.EngineManufacturerId == emId).FirstOrDefault();
                Console.WriteLine($"Name: {em.Name}");
            }

            Console.Write("\n[PRESS ENTER TO GO BACK]");
            Console.ReadLine();
        }

        static void Create(string entity)
        {
            if (entity == DRIVER_ENTITY)
            {
                Console.Title = "Create Driver";

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Number: ");
                int number = int.Parse(Console.ReadLine());

                Console.Write("Nationality: ");
                string nat = Console.ReadLine();

                Console.Write("Born (yyyy-MM-dd): ");
                DateTime born = DateTime.Parse(Console.ReadLine());

                rest.Post(new Driver()
                {
                    Name = name,
                    Number = number,
                    Nationality = nat,
                    Born = born
                }, DRIVER_ENDPOINT);

                Console.WriteLine("[+] New driver created!");
            }
            else if (entity == TEAM_ENTITY)
            {
                Console.Title = "Create Team";

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Licensed in: ");
                string licensedIn = Console.ReadLine();

                Console.Write("Principal: ");
                string principal = Console.ReadLine();

                rest.Post(new Team()
                {
                    Name = name,
                    LicensedIn = licensedIn,
                    Principal = principal
                }, TEAM_ENDPOINT);

                Console.WriteLine("[+] New team created!");
            }
            else if (entity == ENGINE_MANUFACTURER_ENTITY)
            {
                Console.Title = "Create Engine Manufacturer";

                Console.Write("Name: ");
                string name = Console.ReadLine();

                rest.Post(new Team()
                {
                    Name = name
                }, ENGINE_MANUFACTURER_ENDPOINT);

                Console.WriteLine("[+] New engine manufacturer created!");
            }
        }

        static void Update(string entity)
        {
            if (entity == DRIVER_ENTITY)
            {
                Console.Title = "Update Driver (Leave empty what you don't want to update except id)";

                Console.Write("Id: ");
                int driverId = int.Parse(Console.ReadLine());

                Driver driver = rest.Get<Driver>(driverId, DRIVER_ENDPOINT);

                Console.Write("Name: ");
                string name = Console.ReadLine();

                if (name == "" || name == null)
                {
                    Console.WriteLine("[-] Driver\'s name not updated!");
                }
                else
                {
                    driver.Name = name;
                    Console.WriteLine("[+] Driver\'s name updated!");
                }

                Console.Write("Number: ");
                string numberStr = Console.ReadLine();

                if (numberStr == "" || numberStr == null)
                {
                    Console.WriteLine("[-] Driver\'s number not updated!");
                }
                else
                {
                    if (Int32.TryParse(numberStr, out int number))
                    {
                        Console.WriteLine("[+] Driver\'s number updated!");
                        driver.Number = number;
                    }
                    else
                    {
                        Console.WriteLine("[-] Driver\'s number not updated because of invalid value given by user!");
                    }
                }

                Console.Write("Nationality: ");
                string nat = Console.ReadLine();

                if (nat == "" || nat == null)
                {
                    Console.WriteLine("[-] Driver\'s nationality not updated!");
                }
                else
                {
                    driver.Nationality = nat;
                    Console.WriteLine("[+] Driver\'s nationality updated!");
                }

                Console.Write("Born (yyyy-MM-dd): ");
                string bornStr = Console.ReadLine();

                if (bornStr == "" || bornStr == null)
                {
                    Console.WriteLine("[-] Driver\'s birthday not updated!");
                }
                else
                {
                    if (DateTime.TryParse(bornStr, out DateTime born))
                    {
                        Console.WriteLine("[+] Driver\'s birthday updated!");
                        driver.Born = born;
                    }
                    else
                    {
                        Console.WriteLine("[-] Driver\'s birthday not updated because of invalid value given by user!");
                    }
                }

                rest.Put(new Driver() { 
                    Name = driver.Name,
                    Born = driver.Born,
                    DriverId = driverId,
                    TeamId = driver.TeamId,
                    Nationality = driver.Nationality,
                    Number = driver.Number
                }, DRIVER_ENDPOINT + "/" + $"{driverId}");
            }
            else if (entity == TEAM_ENTITY)
            {
                Console.Title = "Update Team (Leave empty what you don't want to update except id)";

                Console.Write("Id: ");
                int teamId = int.Parse(Console.ReadLine());

                Team team = rest.Get<Team>(teamId, TEAM_ENDPOINT);

                Console.Write("Name: ");
                string name = Console.ReadLine();

                if (name == "" || name == null)
                {
                    Console.WriteLine("[-] Team\'s name not updated!");
                }
                else
                {
                    team.Name = name;
                    Console.WriteLine("[+] Team\'s name updated!");
                }

                Console.Write("Licensed in: ");
                string licensedIn = Console.ReadLine();

                if (licensedIn == "" || licensedIn == null)
                {
                    Console.WriteLine("[-] Team\'s location not updated!");
                }
                else
                {
                    team.LicensedIn = licensedIn;
                    Console.WriteLine("[+] Team\'s location updated!");
                }

                Console.Write("Principal: ");
                string principal = Console.ReadLine();

                if (principal == "" || principal == null)
                {
                    Console.WriteLine("[-] Team\'s principal not updated!");
                }
                else
                {
                    team.Principal = principal;
                    Console.WriteLine("[+] Team\'s principal updated!");
                }

                rest.Put(new Team()
                {
                    Name = team.Name,
                    Principal = team.Principal,
                    LicensedIn = team.LicensedIn,
                    TeamId = team.TeamId
                }, TEAM_ENDPOINT + "/" + $"{teamId}");
            }
            else if (entity == ENGINE_MANUFACTURER_ENTITY)
            {
                Console.Title = "Update Engine Manufacturer (Leave empty what you don't want to update except id)";

                Console.Write("Id: ");
                int emId = int.Parse(Console.ReadLine());

                EngineManufacturer em = rest.Get<EngineManufacturer>(emId, ENGINE_MANUFACTURER_ENDPOINT);

                Console.Write("Name: ");
                string name = Console.ReadLine();

                if (name == "" || name == null)
                {
                    Console.WriteLine("[-] Engine Manufacturer\'s name not updated!");
                }
                else
                {
                    em.Name = name;
                    Console.WriteLine("[+] Engine Manufacturer\'s name updated!");
                }

                rest.Put(new EngineManufacturer()
                {
                    Name = em.Name,
                    EngineManufacturerId = emId
                }, ENGINE_MANUFACTURER_ENDPOINT + "/" + $"{emId}");
            }
        }

        static void Delete(string entity)
        {
            if (entity == DRIVER_ENTITY)
            {
                Console.Title = "Delete Driver";
                Console.Write("Id: ");
                int driverId = int.Parse(Console.ReadLine());
                rest.Delete(driverId, DRIVER_ENDPOINT);
                Console.WriteLine("[+] Driver deleted!");
            }
            else if (entity == TEAM_ENTITY)
            {
                Console.Title = "Delete Team";
                Console.Write("Id: ");
                int teamId = int.Parse(Console.ReadLine());
                rest.Delete(teamId, TEAM_ENDPOINT);
                Console.WriteLine("[+] Team deleted!");
            }
            else if (entity == ENGINE_MANUFACTURER_ENTITY)
            {
                Console.Title = "Delete Engine Manufacturer";
                Console.Write("Id: ");
                int emId = int.Parse(Console.ReadLine());
                rest.Delete(emId, ENGINE_MANUFACTURER_ENDPOINT);
                Console.WriteLine("[+] Engine Manufacturer deleted!");
            }
        }
        #endregion
        #region Non-CRUD
        #endregion

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:59582/");

            var driverSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List(DRIVER_ENTITY))
                .Add("Read", () => Read(DRIVER_ENTITY))
                .Add("Create", () => Create(DRIVER_ENTITY))
                .Add("Update", () => Update(DRIVER_ENTITY))
                .Add("Delete", () => Delete(DRIVER_ENTITY))
                .Add("Back", ConsoleMenu.Close);

            var teamSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List(TEAM_ENTITY))
                .Add("Read", () => Read(TEAM_ENTITY))
                .Add("Create", () => Create(TEAM_ENTITY))
                .Add("Update", () => Update(TEAM_ENTITY))
                .Add("Delete", () => Delete(TEAM_ENTITY))
                .Add("Back", ConsoleMenu.Close);

            var emSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List(ENGINE_MANUFACTURER_ENTITY))
                .Add("Read", () => Read(ENGINE_MANUFACTURER_ENTITY))
                .Add("Create", () => Create(ENGINE_MANUFACTURER_ENTITY))
                .Add("Update", () => Update(ENGINE_MANUFACTURER_ENTITY))
                .Add("Delete", () => Delete(ENGINE_MANUFACTURER_ENTITY))
                .Add("Back", ConsoleMenu.Close);

            var queriesSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Back", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Drivers", () => driverSubMenu.Show())
                .Add("Teams", () => teamSubMenu.Show())
                .Add("Engine manufacturers", () => emSubMenu.Show())
                .Add("Queries", () => queriesSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }
    }
}
