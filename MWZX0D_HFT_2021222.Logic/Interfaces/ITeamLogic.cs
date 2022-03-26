using MWZX0D_HFT_2021222.Models;
using System.Linq;

namespace MWZX0D_HFT_2021222.Logic.Interfaces
{
    public interface ITeamLogic
    {
        void Create(Team item);
        void Delete(int id);
        Team Read(int id);
        IQueryable<Team> RealAll();
        void Update(Team item);
    }
}
