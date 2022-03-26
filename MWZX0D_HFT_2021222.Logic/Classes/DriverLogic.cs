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
        #region How many drivers are included in one nationality, then sort in descending order and select only the first 3 nationalities.
        public IEnumerable<DriversPerNationality> GetDriversPerNationality()
        {
            return (from x in this.repo.ReadAll()
                   group x by x.Nationality into g
                   select new DriversPerNationality()
                   {
                        Nationality = g.Key,
                        Count = g.Count()
                   }).OrderByDescending(x => x.Count).Take(3);
        }

        public class DriversPerNationality
        {
            public string Nationality { get; set; }
            public int Count { get; set; }
        }
        #endregion
        #region Given two teams, is there any driver who's number is between specific range, who is it and what's his number?
        public IEnumerable<GivenNumber> GetDriversWhosNumberIsBetweenSpecificRange(string aTeam, string bTeam, int fromNumber, int toNumber)
        {
            return from x in this.repo.ReadAll()
                   where (x.Team.Name.ToUpper() == aTeam.ToUpper() || x.Team.Name.ToUpper() == bTeam.ToUpper()) && (x.Number >= fromNumber && x.Number <= toNumber)
                   select new GivenNumber()
                   { 
                       TeamName = x.Team.Name,
                       DriverName = x.Name,
                       Number = x.Number
                   };
        }

        public class GivenNumber
        {
            public string TeamName { get; set; }
            public string DriverName { get; set; }
            public int Number { get; set; }
        }
        #endregion
    }
}
