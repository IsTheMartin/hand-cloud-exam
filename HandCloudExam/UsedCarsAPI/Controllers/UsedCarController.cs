using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsedCarsAPI.Models;
using UsedCarsAPI.Services;

namespace UsedCarsAPI.Controllers
{
    [Route("hc/[controller]")]
    [ApiController]
    public class UsedCarController : ControllerBase
    {
        private readonly IUsedCarService usedCarService;

        public UsedCarController(IUsedCarService usedCarService)
        {
            this.usedCarService = usedCarService;
        }

        [HttpGet("GetUsedCars")]
        public ActionResult<IList<Car>> GetAllUsedCars()
        {
            IList<Car> carList = usedCarService.GetUsedCars();
            if (carList.Count > 0)
                return carList.Select(x => x).ToList();
            return NoContent();
        }

        [HttpGet("GetUsedCar")]
        public ActionResult<Car> GetUsedCar(int id)
        {
            Car car = usedCarService.GetUsedCar(id);
            if (car != null)
                return car;
            return NotFound();
        }

        [HttpPost("AddUsedCar")]
        public void AddUsedCar([FromBody] Car car)
        {
            usedCarService.AddUsedCar(car);
            usedCarService.SaveChanges();
        }

        [HttpDelete("DeleteUsedCar")]
        public ActionResult DeleteUsedCar(int id)
        {
            if (usedCarService.DeleteUsedCar(id))
            {
                usedCarService.SaveChanges();
                return Ok();
            }
            else
                return NotFound(id);
        }

        [HttpPut("UpdateUsedCar")]
        public ActionResult<Car> UpdateUsedCar([FromBody] Car car)
        {
            if (usedCarService.UpdateUsedCar(car) != null)
            {
                usedCarService.SaveChanges();
                return car;
            }
            return NotFound();
        }
    }
}