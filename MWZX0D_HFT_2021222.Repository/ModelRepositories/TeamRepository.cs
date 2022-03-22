using MWZX0D_HFT_2021222.Models;
using MWZX0D_HFT_2021222.Repository.Database;
using MWZX0D_HFT_2021222.Repository.Generic_Repository;
using MWZX0D_HFT_2021222.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWZX0D_HFT_2021222.Repository.ModelRepositories
{
    public class TeamRepository : Repository<Team>, IRepository<Team>
    {
        // The repository can only get DbContext dependecies as ctor parameters.
        public TeamRepository(FormulaDbContext ctx) : base(ctx)
        {
        }

        // READ
        public override Team Read(int id)
        {
            return ctx.Teams.FirstOrDefault(x => x.TeamId == id);
        }

        // UPDATE
        public override void Update(Team item)
        {
            var old = Read(item.TeamId);

            // Reflection
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }

            ctx.SaveChanges();
        }
    }
}
