using MWZX0D_HFT_2021222.Logic.Exceptions;
using MWZX0D_HFT_2021222.Logic.Interfaces;
using MWZX0D_HFT_2021222.Models;
using MWZX0D_HFT_2021222.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MWZX0D_HFT_2021222.Logic.Classes
{
    public class TeamLogic : ITeamLogic
    {
        IRepository<Team> repo;

        // Logic can only receive Repository as a dependency via the interface as a constructor parameter
        // (Dependency Injection)!
        public TeamLogic(IRepository<Team> repo)
        {
            this.repo = repo;
        }

        public void Create(Team item)
        {
            if (item.Name == "") {
                throw new NameEmptyException()
                {
                    Msg = "Team's name cannot be empty!",
                };
            }

            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            try
            {
                this.repo.Delete(id);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("Id was not found.");
            }
        }

        public Team Read(int id)
        {
            var team = this.repo.Read(id);
            if (team == null)
            {
                throw new TeamIsNotExistsException()
                {
                    Msg = "Team is not exists!",
                };
            }

            return team;
        }

        public IQueryable<Team> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Team item)
        {
            if (Read(item.TeamId) != null)
            {
                this.repo.Update(item);
            }
        }

        // Non-crud methods
        #region If the team's principal's name contains specific letter, which engine they are using and what his/her name?
        public IEnumerable<PrincipalLetter> GetEngineManufacturerByPrincipalNameIfContainsSpecificLetter(char letter)
        {
            return from x in this.repo.ReadAll()
                   where x.Principal.Contains(letter.ToString())
                   select new PrincipalLetter()
                   { 
                        Name = x.Principal,
                        Engine = x.EngineManufacturer.Name
                   };
        }
        #endregion
    }
}
