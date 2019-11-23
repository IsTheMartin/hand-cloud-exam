using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsedCarsAPI.Models
{
    public interface IUsedCarRepository
    {
        IList<Car> GetAll();
        Car Get(int id);
        void Add(Car car);
        bool Delete(int id);
        Car Update(Car car);
        void Save();
    }
}
