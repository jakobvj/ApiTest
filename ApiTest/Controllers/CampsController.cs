using ApiTest.Data;
using ApiTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository _repository;
        private readonly LinkGenerator linkGenerator;

        public CampsController(ICampRepository repository, LinkGenerator linkGenerator)
        {
            _repository = repository;
            this.linkGenerator = linkGenerator;
        }
        /// <summary>
        /// Hent liste af alle camps
        /// </summary>
        /// <param name="includeTalks"></param>
        /// <returns></returns>
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
        [HttpGet("search")]
        public async Task<ActionResult<List<CampModel>>> SearchByDate(DateTime theDate, bool includeTalks = false)
        {
            try
            {
                List<CampModel> campList = new List<CampModel>();

                var tempResult = await _repository.GetAllCampsByEventDate(theDate, false);
                var result = MapHelper.MapCampModels(tempResult);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        [HttpPost]
        public async Task<ActionResult<CampModel>> Post(CampModel model)
        {
            try
            {
                var existing = await _repository.GetCampAsync(model.Moniker);
                if (existing != null)
                {
                    return BadRequest("Moniker in Use");
                }

                var location = linkGenerator.GetPathByAction("Get",
                  "Camps",
                  new { moniker = model.Moniker });

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current moniker");
                }

                var result = MapHelper.MapCampModelBack(model);
                _repository.Add(result);
                if (await _repository.SaveChangesAsync())
                {
                    return Created("$/api/camps/{camp.Moniker}", MapHelper.ReturnMap(result));
                }
            }
            catch (Exception e)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
            return BadRequest();
        }
        [HttpPut("{moniker}")]
        public async Task<ActionResult<CampModel>> Put(string moniker, CampModel model)
        {
            try
            {
                var oldCamp = await _repository.GetCampAsync(moniker);
                if (oldCamp == null) return NotFound($"Could not find camp with moniker of {moniker}");

                var result = MapHelper.MapForPUT(oldCamp, model);
               
                if (await _repository.SaveChangesAsync())
                {
                    return MapHelper.ReturnMap(result);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{moniker}")]
        public async Task<IActionResult> Delete(string moniker)
        {
            try
            {
                var oldCamp = await _repository.GetCampAsync(moniker);
                if (oldCamp == null) return NotFound();

                _repository.Delete(oldCamp);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the camp");
        }
    }
}
