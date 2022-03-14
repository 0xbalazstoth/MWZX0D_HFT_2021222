using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWZX0D_HFT_2021222.Models
{
    public class EngineManufacturer
    {
        [Key] // Pk
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int EngineManufacturerId { get; set; }

        [StringLength(240)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public EngineManufacturer()
        {
            Teams = new HashSet<Team>();
        }

        public EngineManufacturer(string seed)
        {
            // ENGINEMANUFACTURERID$NAME
            string[] split = seed.Split('$');
            EngineManufacturerId = int.Parse(split[0]);
            Name = split[1];
            Teams = new HashSet<Team>();
        }
    }
}
