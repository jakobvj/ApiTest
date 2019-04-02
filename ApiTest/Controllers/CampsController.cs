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
        public async Task<ActionResult<List<CampModel>>> Get(bool includeTalks = false)
        {
            try
            {
                List<CampModel> campList = new List<CampModel>();

                var tempResult = await _repository.GetAllCampsAsync(includeTalks);
                var result = MapHelper.MapCampModels(tempResult);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
            
        }
        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModel>> Get(string moniker)
        {
            try
            {
                var tempResult = await _repository.GetCampAsync(moniker);

                if (tempResult == null)
                {
                    return NotFound();
                }

                var camp = MapHelper.MapCampModel(tempResult);
                return camp;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
