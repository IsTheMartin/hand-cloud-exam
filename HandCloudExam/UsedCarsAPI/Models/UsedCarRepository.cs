using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedCarsAPI.Services;

namespace UsedCarsAPI.Models
{
    public class UsedCarRepository : IUsedCarRepository
    {
        private IUsedCarService usedCarService;
        public UsedCarRepository(IUsedCarService usedCarService)
        {
            this.usedCarService = usedCarService;
        }

        public void Add(Car car)
        {
            usedCarService.AddUsedCar(car);
        }

        public bool Delete(int id)
        {
            return usedCarService.DeleteUsedCar(id);
        }

        public Car Get(int id)
        {
            return usedCarService.GetUsedCar(id);
        }

        public IList<Car> GetAll()
        {
            return usedCarService.GetUsedCars();
        }

        public void Save()
        {
            usedCarService.SaveChanges();
        }

        public Car Update(Car car)
        {
            return usedCarService.UpdateUsedCar(car);
        }
    }
}
