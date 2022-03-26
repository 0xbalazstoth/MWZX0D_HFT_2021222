﻿using MWZX0D_HFT_2021222.Logic.Exceptions;
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
            if (item.Name != "") {
                throw new NameEmptyException()
                {
                    Msg = "Team's name cannot be empty!",
                };
            }

            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
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

        public IQueryable<Team> RealAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Team item)
        {
            this.repo.Update(item);
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
        #endregion
    }
}
