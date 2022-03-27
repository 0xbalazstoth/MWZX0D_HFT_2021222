using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MWZX0D_HFT_2021222.Models
{
    public class Driver
    {
        [Key] // PK
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int DriverId { get; set; }

        [StringLength(240)]
        [Required]
        public string Name { get; set; }

        [Range(0, 99)]
        [Required]
        public int Number { get; set; }

        [StringLength(240)]
        public string Nationality { get; set; }

        public DateTime Born { get; set; }

        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; } // Team, Fk

        [NotMapped]
        public virtual Team Team { get; set; }

        public Driver()
        {

        }

        public Driver(string seed)
        {
            // DRIVERID$TEAMID$NAME$NUMBER$NATIONALITY$BORN
            string[] split = seed.Split('$');
            DriverId = int.Parse(split[0]);
            TeamId = int.Parse(split[1]);
            Name = split[2];
            Number = int.Parse(split[3]);
            Nationality = split[4];
            Born = DateTime.Parse(split[5]);
        }
    }
}
