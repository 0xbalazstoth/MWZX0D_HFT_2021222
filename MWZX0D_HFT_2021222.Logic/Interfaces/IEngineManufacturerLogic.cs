using MWZX0D_HFT_2021222.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MWZX0D_HFT_2021222.Logic.Interfaces
{
    public interface IEngineManufacturerLogic
    {
        void Create(EngineManufacturer item);
        void Delete(int id);
        EngineManufacturer Read(int id);
        IQueryable<EngineManufacturer> RealAll();
        void Update(EngineManufacturer item);
    }
}
