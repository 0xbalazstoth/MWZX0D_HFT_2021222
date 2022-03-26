using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MWZX0D_HFT_2021222.Models
{
    public class Team
    {
        [Key] // PK
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Auto-increment
        public int TeamId { get; set; }

        [StringLength(240)]
        [Required]
        public string Name { get; set; }

        [StringLength(240)]
        public string LicensedIn { get; set; }

        [StringLength(240)]
        public string Principal { get; set; }

        [ForeignKey(nameof(EngineManufacturer))]
        public int EngineManufacturerId { get; set; } // EngineManufacturer, Fk

        public virtual ICollection<Driver> Drivers { get; set; }

        public virtual EngineManufacturer EngineManufacturer { get; set; }

        public Team()
        {
            Drivers = new HashSet<Driver>();
        }

        public Team(string seed)
        {
            // TEAMID$ENGINEMANUFACTURERID$NAME$LICENSEDIN$PRINCIPAL
            string[] split = seed.Split('$');
            TeamId = int.Parse(split[0]);
            EngineManufacturerId = int.Parse(split[1]);
            Name = split[2];
            LicensedIn = split[3];
            Principal = split[4];
            Drivers = new HashSet<Driver>();
        }
    }
}
