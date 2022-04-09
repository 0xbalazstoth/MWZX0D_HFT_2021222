using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWZX0D_HFT_2021222.Models
{
    public class DriversPerEngineManufacturer
    {
        public string EngineManufacturer { get; set; }
        public int Count { get; set; }

        public override bool Equals(object obj)
        {
            DriversPerEngineManufacturer b = obj as DriversPerEngineManufacturer;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.EngineManufacturer == b.EngineManufacturer && this.Count == b.Count;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.EngineManufacturer, this.Count);
        }
    }
}
