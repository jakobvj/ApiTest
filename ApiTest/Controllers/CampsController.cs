using ApiTest.Data;
using ApiTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository _repository;

        public CampsController(ICampRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<List<CampModel>>> GetCamps()
        {
            try
            {
                List<CampModel> campList = new List<CampModel>();

                var tempResult = await _repository.GetAllCampsAsync();
                foreach (var item in tempResult)
                {
                    CampModel cm = new CampModel
                    {
                        Name = item.Name,
                        Moniker = item.Moniker,
                        Length = item.Length,
                        EventDate = item.EventDate
                    };

                    campList.Add(cm);
                }
                return Ok(campList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
            
        }
        [HttpGet("{moniker")]
        public async Task<ActionResult<CampModel>> Get(string moniker)
        {
            try
            {
                var tempResult = await _repository.GetCampAsync(moniker);

                if (tempResult == null)
                {
                    return NotFound();
                }

                CampModel cm = new CampModel
                {
                    Name = tempResult.Name,
                    Moniker = tempResult.Moniker,
                    Length = tempResult.Length,
                    EventDate = tempResult.EventDate
                };
                return cm;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
