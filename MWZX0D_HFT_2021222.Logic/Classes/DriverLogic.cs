using MWZX0D_HFT_2021222.Logic.Exceptions;
using MWZX0D_HFT_2021222.Logic.Interfaces;
using MWZX0D_HFT_2021222.Models;
using MWZX0D_HFT_2021222.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MWZX0D_HFT_2021222.Logic.Classes
{
    public class DriverLogic : IDriverLogic
    {
        IRepository<Driver> repo;

        // Logic can only receive Repository as a dependency via the interface as a constructor parameter
        // (Dependency Injection)!
        public DriverLogic(IRepository<Driver> repo)
        {
            this.repo = repo;
        }

        public void Create(Driver item)
        {
            if ((DateTime.Now.Year - item.Born.Year) > 17)
            {
                throw new DriverIsTooYoungException()
                {
                    Msg = "Driver's age must be greater than 17!",
                    Driver = item
                };
            }

            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Driver Read(int id)
        {
            var driver = this.repo.Read(id);
            if (driver == null)
            {
                throw new DriverHasNoContractException()
                {
                    Msg = "Driver has no contract!"
                };
            }

            return driver;
        }

        public IQueryable<Driver> RealAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Driver item)
        {
            this.repo.Update(item);
        }

        // Non-crud methods
        public IEnumerable<OlderThan20AndHondaEngine> DriversOlderThan20AndTheyAreInHondaEngineBasedTeam()
        {
            return from x in this.repo.ReadAll()
                   where (DateTime.Now.Year - x.Born.Year) > 24 && x.Team.EngineManufacturer.Name == "Honda"
                   select new OlderThan20AndHondaEngine
                   { 
                        DriverName = x.Name,
                        TeamName = x.Team.Name,
                        Age = (DateTime.Now.Year - x.Born.Year)
                   };
        }

        public class OlderThan20AndHondaEngine
        {
            public string DriverName;
            public string TeamName;
            public int Age;
        }
    }
}
