using MWZX0D_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWZX0D_HFT_2021222.Logic.Exceptions
{
    public class EngineManufacturerNameIsTooShortException : BaseException
    {
        public EngineManufacturer EngineManufacturer { get; set; }
    }
}
