using Microsoft.EntityFrameworkCore;
using MWZX0D_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWZX0D_HFT_2021222.Repository.Database
{
    public class FormulaDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<EngineManufacturer> EngineManufacturers { get; set; }

        public FormulaDbContext()
        {
            this.Database.EnsureCreated(); // Create Db
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                // InMemory
                // builder.UseLazyLoadingProxies().UseInMemoryDatabase("formula_racing"); 

                // LocalDb
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\formula_racing.mdf;Integrated Security=True;MultipleActiveResultSets=true";
                builder.UseLazyLoadingProxies().UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationships

            // Team - Driver: one-to-many
            // One team can have multiple drivers.
            // One specific driver can only be in one team.
            // Example:
            // - Red Bull Racing:
            //   * Max Verstappen
            //   * Sergio "Checo" Perez
            modelBuilder.Entity<Driver>(driver => driver
                .HasOne(driver => driver.Team)
                .WithMany(team => team.Drivers)
                .HasForeignKey(driver => driver.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
            );

            // EngineManufacturer - Team: one-to-many
            // One engine manufacturer can have multiple teams.
            // One specific team can only have one engine manufacturer.
            // Example:
            // - Honda:
            //   * Red Bull Racing
            //   * AlphaTauri
            modelBuilder.Entity<Team>(team => team
                .HasOne(team => team.EngineManufacturer)
                .WithMany(engineManufacturer => engineManufacturer.Teams)
                .HasForeignKey(team => team.EngineManufacturerId)
                .OnDelete(DeleteBehavior.Cascade)
            );

            #region DataSeeds

            // Driver
            modelBuilder.Entity<Driver>().HasData(new Driver[]
            {
                new Driver("1$1$Lewis Hamilton$44$British$1985-01-07"),
                new Driver("2$1$George Russell$63$British$1998-02-15"),
                new Driver("3$2$Max Verstappen$1$Dutch$1997-09-03"),
                new Driver("4$2$Sergio Perez$11$Mexican$1990-01-26"),
                new Driver("5$3$Charles Leclerc$16$Monégasque$1997-10-16"),
                new Driver("6$3$Carlos Sainz$55$Spanish$1994-09-01"),
                new Driver("7$4$Daniel Ricciardo$3$Australian$1989-07-01"),
                new Driver("8$4$Lando Norris$4$British$1999-11-13"),
                new Driver("9$5$Fernando Alonso$14$Spanish$1981-07-29"),
                new Driver("10$5$Esteban Ocon$31$French$1996-09-17"),
                new Driver("11$6$Pierre Gasly$10$French$1996-02-07"),
                new Driver("12$6$Yuki Tsunoda$22$Japanase$2000-05-11"),
                new Driver("13$7$Sebastian Vettel$5$German$1987-07-03"),
                new Driver("14$7$Lance Stroll$18$Canadian$1998-10-29"),
                new Driver("15$8$Nicholas Latifi$6$Canadian$1995-06-29"),
                new Driver("16$8$Alexander Albon$23$Thai-British$1996-03-23"),
                new Driver("17$9$Zhou Guanyu$24$Chinese$1999-05-30"),
                new Driver("18$9$Valtteri Bottas$77$Finnish$1989-08-28"),
                new Driver("19$10$Kevin Magnussen$20$Danish$1992-10-05"),
                new Driver("20$10$Mick Schumacher$47$German$1999-03-22")
            });

            // Team
            modelBuilder.Entity<Team>().HasData(new Team[]
            {
                new Team("1$1$Mercedes$Germany$Toto Wolff"),
                new Team("2$2$Red Bull$Austria$Christian Horner"),
                new Team("3$3$Ferrari$Italy$Mattia Binotto"),
                new Team("4$1$McLaren$United Kingdom$Andreas Seidl"),
                new Team("5$4$Alpine$France$Otmar Szafnauer"),
                new Team("6$2$Alpha Tauri$Italy$Franz Tost"),
                new Team("7$1$Aston Martin$United Kingdom$Mike Krack"),
                new Team("8$1$Williams$United Kingdom$Jost Capito"),
                new Team("9$3$Alfa Romeo$Switzerland$Frederic Vassuer"),
                new Team("10$3$Haas$United States$Guenther Steiner"),
            });

            // EngineManufacturer
            modelBuilder.Entity<EngineManufacturer>().HasData(new EngineManufacturer[]
            {
                new EngineManufacturer("1$Mercedes"),
                new EngineManufacturer("2$Honda"),
                new EngineManufacturer("3$Ferrari"),
                new EngineManufacturer("4$Renault")
            });
            #endregion
        }
    }
}
