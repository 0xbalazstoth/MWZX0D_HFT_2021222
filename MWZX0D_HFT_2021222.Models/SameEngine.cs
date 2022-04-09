using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWZX0D_HFT_2021222.Models
{
    public class SameEngine
    {
        public string Engine { get; set; }
        public double Avg { get; set; }

        public override bool Equals(object obj)
        {
            SameEngine b = obj as SameEngine;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.Engine == b.Engine && this.Avg == b.Avg;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Engine, this.Avg);
        }
    }
}
