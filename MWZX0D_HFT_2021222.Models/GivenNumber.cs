using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWZX0D_HFT_2021222.Models
{
    public class GivenNumber
    {
        public string TeamName { get; set; }
        public string DriverName { get; set; }
        public int Number { get; set; }

        public override bool Equals(object obj)
        {
            GivenNumber b = obj as GivenNumber;
            if (b == null)
            {
                return false;
            }
            else
            {
                return TeamName == b.TeamName && DriverName == b.DriverName && Number == b.Number;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.TeamName, this.DriverName, this.Number);
        }
    }
}
