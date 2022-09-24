using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MWZX0D_HFT_2021222.Endpoint.Services;
using MWZX0D_HFT_2021222.Logic.Interfaces;
using MWZX0D_HFT_2021222.Models;
using System.Collections.Generic;

namespace MWZX0D_HFT_2021222.Endpoint.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        ITeamLogic logic;
        IHubContext<SignalRHub> hub;

        public TeamController(ITeamLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Team> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Team Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Team value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("TeamCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Team value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("TeamUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var teamToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("TeamDeleted", teamToDelete);
        }
    }
}