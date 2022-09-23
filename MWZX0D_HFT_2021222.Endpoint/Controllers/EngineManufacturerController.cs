using Microsoft.AspNetCore.Mvc;
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
        }

        //[HttpPut("{id}")]
        [HttpPut]
        public void Update([FromBody] EngineManufacturer value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}