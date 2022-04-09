using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWZX0D_HFT_2021222.Models
{
    public class SumNumberEngine
    {
        public string HeadQuarter { get; set; }
        public int Sum { get; set; }

        public override bool Equals(object obj)
        {
            SumNumberEngine b = obj as SumNumberEngine;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.HeadQuarter == b.HeadQuarter && this.Sum == b.Sum;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.HeadQuarter, this.Sum);
        }
    }
}
