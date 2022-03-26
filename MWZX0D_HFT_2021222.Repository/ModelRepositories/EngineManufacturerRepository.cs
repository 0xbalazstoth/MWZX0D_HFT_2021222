using MWZX0D_HFT_2021222.Models;
using MWZX0D_HFT_2021222.Repository.Database;
using MWZX0D_HFT_2021222.Repository.Generic_Repository;
using MWZX0D_HFT_2021222.Repository.Interfaces;
using System.Linq;

namespace MWZX0D_HFT_2021222.Repository.ModelRepositories
{
    public class EngineManufacturerRepository : Repository<EngineManufacturer>, IRepository<EngineManufacturer>
    {
        // The repository can only get DbContext dependecies as ctor parameters.
        public EngineManufacturerRepository(FormulaDbContext ctx) : base(ctx)
        {
        }
        
        // READ
        public override EngineManufacturer Read(int id)
        {
            return ctx.EngineManufacturers.FirstOrDefault(x => x.EngineManufacturerId == id);
        }

        // UPDATE
        public override void Update(EngineManufacturer item)
        {
            var old = Read(item.EngineManufacturerId);

            // Reflection
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }

            ctx.SaveChanges();
        }
    }
}
