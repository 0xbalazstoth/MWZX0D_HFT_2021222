using MWZX0D_HFT_2021222.Logic.Exceptions;
using MWZX0D_HFT_2021222.Logic.Interfaces;
using MWZX0D_HFT_2021222.Models;
using MWZX0D_HFT_2021222.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWZX0D_HFT_2021222.Logic.Classes
{
    public class EngineManufacturerLogic : IEngineManufacturerLogic
    {
        IRepository<EngineManufacturer> repo;

        // Logic can only receive Repository as a dependency via the interface as a constructor parameter
        // (Dependency Injection)!
        public EngineManufacturerLogic(IRepository<EngineManufacturer> repo)
        {
            this.repo = repo;
        }

        public void Create(EngineManufacturer item)
        {
            if (item.Name.Length < 3)
            {
                throw new EngineManufacturerNameIsTooShortException()
                { 
                    Msg = "Engine manufacturer's name is too short!",
                    EngineManufacturer = item
                };
            }

            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public EngineManufacturer Read(int id)
        {
            var em = this.repo.Read(id);
            if (em == null)
            {
                throw new EngineManufacturerIsNotExistsException()
                {
                    Msg = "Engine manufacturer is not exists!"
                };
            }

            return em;
        }

        public IQueryable<EngineManufacturer> RealAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(EngineManufacturer item)
        {
            this.repo.Update(item);
        }
    }
}
