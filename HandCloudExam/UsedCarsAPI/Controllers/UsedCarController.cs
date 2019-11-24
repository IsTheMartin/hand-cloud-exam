using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsedCarsAPI.Models;
using UsedCarsAPI.Services;

namespace UsedCarsAPI.Controllers
{
    [Route("hc/[controller]")]
    [EnableCors("Policy")]
    [ApiController]
    public class UsedCarController : ControllerBase
    {
        private readonly IUsedCarRepository repo;

        public UsedCarController(IUsedCarRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("GetUsedCars")]
        public ActionResult<IList<Car>> GetAllUsedCars()
        {
            IList<Car> carList = repo.GetAll();
            if (carList.Count > 0)
                return carList.Select(x => x).ToList();
            return NoContent();
        }

        [HttpGet("GetUsedCar")]
        public ActionResult<Car> GetUsedCar(int id)
        {
            Car car = repo.Get(id);
            if (car != null)
                return car;
            return NotFound();
        }

        [HttpPost("AddUsedCar")]
        public ActionResult AddUsedCar([FromBody] Car car)
        {
            repo.Add(car);
            repo.Save();
            return Ok();
        }

        [HttpDelete("DeleteUsedCar")]
        public ActionResult DeleteUsedCar(int id)
        {
            if (repo.Delete(id))
            {
                repo.Save();
                return Ok();
            }
            else
                return NotFound(id);
        }

        [HttpPut("UpdateUsedCar")]
        public ActionResult<Car> UpdateUsedCar([FromBody] Car car)
        {
            if (repo.Update(car) != null)
            {
                repo.Save();
                return car;
            }
            return NotFound();
        }
    }
}