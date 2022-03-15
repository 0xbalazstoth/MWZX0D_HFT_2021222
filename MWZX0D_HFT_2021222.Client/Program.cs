using MWZX0D_HFT_2021222.Repository.Database;
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

            ;
        }
    }
}
