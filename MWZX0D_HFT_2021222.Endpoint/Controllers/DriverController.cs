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
    public class DriverController : ControllerBase
    {
        IDriverLogic logic;
        IHubContext<SignalRHub> hub;

        public DriverController(IDriverLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Driver> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Driver Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Driver value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("DriverCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Driver value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("DriverUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var driverToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("DriverDeleted", driverToDelete);
        }
    }
}