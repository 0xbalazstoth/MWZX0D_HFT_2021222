using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWZX0D_HFT_2021222.Models
{
    public class PrincipalLetter
    {
        public string Name { get; set; }
        public string Engine { get; set; }

        public override bool Equals(object obj)
        {
            PrincipalLetter b = obj as PrincipalLetter;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.Name == b.Name && this.Engine == b.Engine;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.Engine);
        }
    }
}
