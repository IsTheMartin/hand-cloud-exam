using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedCarsAPI.Models;

namespace UsedCarsAPI.Services
{
    public interface IUsedCarService
    {
        IList<Car> GetUsedCars();
        Car GetUsedCar(int id);
        void AddUsedCar(Car car);
        bool DeleteUsedCar(int id);
        Car UpdateUsedCar(Car car);
        void SaveChanges();
    }
}
