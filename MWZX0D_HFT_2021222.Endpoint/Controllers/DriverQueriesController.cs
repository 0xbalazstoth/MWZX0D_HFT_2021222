using Microsoft.AspNetCore.Mvc;
using MWZX0D_HFT_2021222.Logic.Classes;
using MWZX0D_HFT_2021222.Logic.Interfaces;
using System.Collections.Generic;

namespace MWZX0D_HFT_2021222.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DriverQueriesController : ControllerBase
    {
        IDriverLogic driverLogic;

        public DriverQueriesController(IDriverLogic driverLogic)
        {
            this.driverLogic = driverLogic;
        }

        [HttpGet]
        public IEnumerable<DriverLogic.DriversPerEngineManufacturer> GetDriversPerEngineManufacturer()
        {
            return this.driverLogic.GetDriversPerEngineManufacturer();
        }

        [HttpGet]
        public IEnumerable<DriverLogic.GivenNumber> GetDriversWhosNumberIsBetweenSpecificRange(string aTeam, string bTeam, int fromNumber, int toNumber)
        {
            return this.driverLogic.GetDriversWhosNumberIsBetweenSpecificRange(aTeam, bTeam, fromNumber, toNumber);
        }

        [HttpGet]
        public IEnumerable<DriverLogic.SumNumberEngine> GetSumPerHeadquarterAtLeastGivenValue(int n)
        {
            return this.driverLogic.GetSumPerHeadquarterAtLeastGivenValue(n);
        }

        [HttpGet]
        public IEnumerable<DriverLogic.SameEngine> GetAvgDriversAgeByTheSameEngineBasedTeams()
        { 
            return this.driverLogic.GetAvgDriversAgeByTheSameEngineBasedTeams();
        }
    }
}
