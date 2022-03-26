using MWZX0D_HFT_2021222.Models;

namespace MWZX0D_HFT_2021222.Logic.Exceptions
{
    public class DriverIsTooYoungException : BaseException
    {
        public Driver Driver { get; set; }
    }
}
