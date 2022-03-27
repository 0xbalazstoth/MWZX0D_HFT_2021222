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
            if ((DateTime.Now.Year - item.Born.Year) < 17)
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
            ;
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

        public IQueryable<Driver> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Driver item)
        {
            this.repo.Update(item);
        }

        // Non-crud methods
        #region How many drivers are included in one engine manufacturer, then sort in descending order by count.
        public IEnumerable<DriversPerEngineManufacturer> GetDriversPerEngineManufacturer()
        {
            return (from x in this.repo.ReadAll()
                   group x by x.Team.EngineManufacturer.Name into g
                   select new DriversPerEngineManufacturer()
                   { 
                        EngineManufacturer = g.Key,
                        Count = g.Count(),
                   }).OrderByDescending(x => x.Count);
        }

        public class DriversPerEngineManufacturer
        {
            public string EngineManufacturer { get; set; }
            public int Count { get; set; }

            public override bool Equals(object obj)
            {
                DriversPerEngineManufacturer b = obj as DriversPerEngineManufacturer;
                if (b == null)
                {
                    return false;
                }
                else
                { 
                    return this.EngineManufacturer == b.EngineManufacturer && this.Count == b.Count;
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.EngineManufacturer, this.Count);
            }
        }

        #endregion
        #region Given two teams, is there any driver who's number is between specific range, who is it and what's his number?
        public IEnumerable<GivenNumber> GetDriversWhosNumberIsBetweenSpecificRange(string aTeam, string bTeam, int fromNumber, int toNumber)
        {
            var res = from x in repo.ReadAll()
                   where (x.Team.Name.ToUpper() == aTeam.ToUpper() || x.Team.Name.ToUpper() == bTeam.ToUpper()) && (x.Number >= fromNumber && x.Number <= toNumber)
                   select new GivenNumber()
                   {
                       TeamName = x.Team.Name,
                       DriverName = x.Name,
                       Number = x.Number
                   };
            ;
            return res.AsEnumerable();
        }

        public class GivenNumber
        {
            public string TeamName { get; set; }
            public string DriverName { get; set; }
            public int Number { get; set; }

            public override bool Equals(object obj)
            {
                GivenNumber b = obj as GivenNumber;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return TeamName == b.TeamName && DriverName == b.DriverName && Number == b.Number;
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.TeamName, this.DriverName, this.Number);
            }
        }
        #endregion
        #region Calculate the average driver's age in the same engine based teams.
        public IEnumerable<SameEngine> GetAvgDriversAgeByTheSameEngineBasedTeams()
        {
            return from x in repo.ReadAll()
                   group x by x.Team.EngineManufacturer.Name into g
                   select new SameEngine()
                   {
                       Engine = g.Key,
                       Avg = g.Average(x => (DateTime.Now.Year - x.Born.Year))
                   };
        }

        public class SameEngine
        {
            public string Engine { get; set; }
            public double Avg { get; set; }

            public override bool Equals(object obj)
            {
                SameEngine b = obj as SameEngine;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return this.Engine == b.Engine && this.Avg == b.Avg;
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.Engine, this.Avg);
            }
        }
        #endregion
        #region What is the sum of the number of drivers per headquarter, which is at least the given value.
        public IEnumerable<SumNumberEngine> GetSumPerHeadquarterAtLeastGivenValue(int n)
        {
            return (from x in this.repo.ReadAll()
                   group x by x.Team.LicensedIn into g
                   select new SumNumberEngine()
                   {
                       HeadQuarter = g.Key,
                       Sum = g.Sum(x => x.Number)
                   }).Where(x => x.Sum >= n);
        }

        public class SumNumberEngine
        {
            public string HeadQuarter { get; set; }
            public int Sum { get; set; }

            public override bool Equals(object obj)
            {
                SumNumberEngine b = obj as SumNumberEngine;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return this.HeadQuarter == b.HeadQuarter && this.Sum == b.Sum;
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.HeadQuarter, this.Sum);
            }
        }
        #endregion
    }
}
