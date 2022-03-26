using MWZX0D_HFT_2021222.Logic.Classes;
using MWZX0D_HFT_2021222.Models;
using System.Collections.Generic;
using System.Linq;

namespace MWZX0D_HFT_2021222.Logic.Interfaces
{
    public interface IDriverLogic
    {
        void Create(Driver item);
        void Delete(int id);
        Driver Read(int id);
        IQueryable<Driver> RealAll();
        void Update(Driver item);
        IEnumerable<DriverLogic.DriversPerNationality> GetDriversPerNationality();
        IEnumerable<DriverLogic.GivenNumber> GetDriversWhosNumberIsBetweenSpecificRange(string aTeam, string bTeam, int fromNumber, int toNumber);
    }
}
