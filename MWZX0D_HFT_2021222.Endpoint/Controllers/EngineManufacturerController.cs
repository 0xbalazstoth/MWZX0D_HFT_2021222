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
    public class EngineManufacturerController : ControllerBase
    {
        IEngineManufacturerLogic logic;
        IHubContext<SignalRHub> hub;

        public EngineManufacturerController(IEngineManufacturerLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<EngineManufacturer> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public EngineManufacturer Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] EngineManufacturer value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("EngineManufacturerCreated", value);
        }

        //[HttpPut("{id}")]
        [HttpPut]
        public void Update([FromBody] EngineManufacturer value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("EngineManufacturerUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var engineManufacturerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("EngineManufacturerDeleted", engineManufacturerToDelete);
        }
    }
}