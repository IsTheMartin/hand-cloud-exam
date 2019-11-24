using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UsedCarsUI.Models;

namespace UsedCarsUI.Controllers
{
    public class HomeController : Controller
    {
        Helper _helper = new Helper();

        public async Task<IActionResult> Index()
        {
            List<Car> carList = new List<Car>();
            HttpClient client = _helper.Initial();
            HttpResponseMessage httpResponse = await client.GetAsync("hc/UsedCar/GetUsedCars");
            if (httpResponse.IsSuccessStatusCode)
            {
                var results = httpResponse.Content.ReadAsStringAsync().Result;
                carList = JsonConvert.DeserializeObject<List<Car>>(results);
            }
            return View(carList);
        }

        public async Task<IActionResult> Details(int id)
        {
            Car car = new Car();
            HttpClient client = _helper.Initial();
            HttpResponseMessage httpResponse = await client.GetAsync($"hc/UsedCar/GetUsedCar/?Id={id}");
            if(httpResponse.IsSuccessStatusCode)
            {
                var results = httpResponse.Content.ReadAsStringAsync().Result;
                car = JsonConvert.DeserializeObject<Car>(results);
            }
            return View(car);
        }

        public IActionResult Add(Car car)
        {
            HttpClient client = _helper.Initial();
            var post = client.PostAsJsonAsync<Car>("hc/UsedCar/AddUsedCar", car);
            if (post.Result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            Car car = new Car();
            HttpClient client = _helper.Initial();
            HttpResponseMessage httpResponse = await client.DeleteAsync($"hc/UsedCar/DeleteUsedCar?Id={id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Car car = new Car();
            HttpClient client = _helper.Initial();
            HttpResponseMessage httpResponse = await client.GetAsync($"hc/UsedCar/GetUsedCar/?Id={id}");
            if (httpResponse.IsSuccessStatusCode)
            {
                var results = httpResponse.Content.ReadAsStringAsync().Result;
                car = JsonConvert.DeserializeObject<Car>(results);
            }
            if(car != null)
                return View(car);
            return RedirectToAction("Index");
        }

        public IActionResult Update(Car car)
        {
            HttpClient client = _helper.Initial();
            var put = client.PutAsJsonAsync<Car>("hc/UsedCar/UpdateUsedCar", car);
            if (put.Result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
