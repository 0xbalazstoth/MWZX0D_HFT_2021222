using MWZX0D_HFT_2021222.Models;

namespace MWZX0D_HFT_2021222.Logic.Exceptions
{
    public class EngineManufacturerNameIsTooShortException : BaseException
    {
        public EngineManufacturer EngineManufacturer { get; set; }
    }
}
