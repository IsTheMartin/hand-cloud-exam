using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UsedCarsAPI.Models;

namespace UsedCarsAPI.Services
{
    public class UsedCarService : IUsedCarService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string jsonPath;
        private IList<Car> _carCollection;

        public UsedCarService(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
            jsonPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "test.json");
            InitializeData();
        }

        public void InitializeData()
        {
            if (File.Exists(jsonPath))
            {
                using (StreamReader reader = new StreamReader(jsonPath))
                {
                    _carCollection = JsonConvert.DeserializeObject<IList<Car>>(reader.ReadToEnd());
                    if (_carCollection == null)
                    {
                        _carCollection = new List<Car>();
                    }
                }
            }
            else
            {
                try
                {
                    FileStream fs = File.Create(jsonPath);
                    fs.Dispose();
                    List<Car> lstCar = new List<Car>();
                    lstCar.Add(new Car { Id = 1, Model = "Elantra", Description = "Silver color", Year = 2018, Brand = "Hyundai", Kilometers = 20845, Price = 275000.5m });

                    string serialized = JsonConvert.SerializeObject(lstCar);
                    _carCollection = JsonConvert.DeserializeObject<IList<Car>>(serialized);
                    SaveChanges();
                } catch (JsonException jex)
                {
                    Console.WriteLine(jex.Message);
                }catch(IOException ioex)
                {
                    Console.WriteLine(ioex.Message);
                }
            }
        }

        public void AddUsedCar(Car car)
        {
            if (_carCollection != null && _carCollection.Count > 0)
            {
                car.Id = _carCollection.Max(carObj => carObj.Id) + 1;
                _carCollection.Add(car);
            }
            else
            {
                car.Id = 1;
                _carCollection.Add(car);
            }
        }

        public bool DeleteUsedCar(int id)
        {
            Car car = _carCollection.Where(carObj => carObj.Id == id).FirstOrDefault();
            if (car != null)
            {
                _carCollection.Remove(car);
                return true;
            }
            return false;
        }

        public Car GetUsedCar(int id)
        {
            return _carCollection.Where(carObj => carObj.Id == id).FirstOrDefault();
        }

        public IList<Car> GetUsedCars()
        {
            return _carCollection.ToList();
        }

        public void SaveChanges()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(jsonPath, false))
                {
                    writer.Write(JsonConvert.SerializeObject(_carCollection));
                }
            }
            catch (JsonException jex)
            {
                Console.WriteLine(jex.Message);
            }catch(IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }

        }

        public Car UpdateUsedCar(Car car)
        {
            Car currentCar = _carCollection.Where(carObj => carObj.Id == car.Id).FirstOrDefault();
            if(currentCar != null)
            {
                car.Id = currentCar.Id;
                currentCar.Brand = car.Brand;
                currentCar.Description = car.Description;
                currentCar.Kilometers = car.Kilometers;
                currentCar.Model = car.Model;
                currentCar.Price = car.Price;
                currentCar.Year = car.Year;
            }

            return currentCar;
        }
    }
}
