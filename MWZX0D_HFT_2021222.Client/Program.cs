using MWZX0D_HFT_2021222.Logic.Classes;
using MWZX0D_HFT_2021222.Repository.Database;
using MWZX0D_HFT_2021222.Repository.ModelRepositories;
using System;
using System.Linq;

namespace MWZX0D_HFT_2021222.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // REPOSITORY DELETE REFERENCE | ONLY IN DEBUG
            FormulaDbContext ctx = new FormulaDbContext();
            var teams = ctx.Teams.ToArray();
            var drivers = ctx.Drivers.ToArray();
            var engineManufacturers = ctx.EngineManufacturers.ToArray();

            var driverRepo = new DriverRepository(ctx);
            var driverLogic = new DriverLogic(driverRepo);

            var teamRepo = new TeamRepository(ctx);
            var teamLogic = new TeamLogic(teamRepo);

            //var q1 = driverLogic.DriversOlderThan20AndTheyAreInHondaEngineBasedTeam();
            var q1 = driverLogic.GetDriversPerNationality();
            var q2 = teamLogic.GetEngineManufacturerByPrincipalNameIfContainsSpecificLetter('a');
            var q3 = driverLogic.GetDriversWhosNumberIsBetweenSpecificRange("ferrari", "mercedes", 5, 45);
            ;
        }
    }
}
