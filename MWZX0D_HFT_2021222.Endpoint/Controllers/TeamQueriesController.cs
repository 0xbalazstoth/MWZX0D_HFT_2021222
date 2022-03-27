using Microsoft.AspNetCore.Mvc;
using MWZX0D_HFT_2021222.Logic.Classes;
using MWZX0D_HFT_2021222.Logic.Interfaces;
using System.Collections.Generic;

namespace MWZX0D_HFT_2021222.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeamQueriesController : ControllerBase
    {
        ITeamLogic teamLogic;

        public TeamQueriesController(ITeamLogic teamLogic)
        {
            this.teamLogic = teamLogic;
        }

        [HttpGet]
        public IEnumerable<TeamLogic.PrincipalLetter> GetEngineManufacturerByPrincipalNameIfContainsSpecificLetter(char letter)
        {
            return this.teamLogic.GetEngineManufacturerByPrincipalNameIfContainsSpecificLetter(letter);
        }
    }
}
