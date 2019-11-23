using System;
using System.Collections.Generic;
using System.Text;
using UsedCarsAPI.Models;

namespace xUnitTest
{
    public static class DataContext
    {
        public static void Seed(this UsedCarRepository carRepository)
        {
            carRepository.Add(new Car
            {
                Model = "Spark",
                Brand = "Chevrolet",
                Year = 2012,
                Kilometers = 125550,
                Price = 70500
            });

            carRepository.Add(new Car
            {
                Model = "Civic",
                Brand = "Honda",
                Year = 2006,
                Kilometers = 180000,
                Price = 80500
            });

            carRepository.Save();
        }
    }
}
