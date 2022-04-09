using MWZX0D_HFT_2021222.Models;
using MWZX0D_HFT_2021222.Repository.Database;
using MWZX0D_HFT_2021222.Repository.Generic_Repository;
using MWZX0D_HFT_2021222.Repository.Interfaces;
using System.Linq;

namespace MWZX0D_HFT_2021222.Repository.ModelRepositories
{
    public class DriverRepository : Repository<Driver>, IRepository<Driver>
    {
        // The repository can only get DbContext dependecies as ctor parameters.
        public DriverRepository(FormulaDbContext ctx) : base(ctx)
        {
        }

        // READ
        public override Driver Read(int id)
        {
            return ctx.Drivers.FirstOrDefault(x => x.DriverId == id);
        }

        // UPDATE
        public override void Update(Driver item)
        {
            var old = Read(item.DriverId);

            // Reflection
            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(x => x.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }

            ctx.SaveChanges();
        }
    }
}
