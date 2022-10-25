using DeveloperClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeveloperClient.Controllers
{
    public class DeveloperController : Controller
    {
        string baseUrl = "https://localhost:44347/api/Developer/";
        private readonly HttpClient httpClient = new HttpClient();

        public DeveloperController()
        {
            httpClient.DefaultRequestHeaders.Add("SecurityId", "1");
            httpClient.DefaultRequestHeaders.Add("SecurityKey", "1]H*lr4w%7Na3fr");
        }

        public async Task<IActionResult> Index()
        {
            var devs = new List<Developer>();

            var response = await httpClient.GetAsync(baseUrl);
            if (response.IsSuccessStatusCode)
            {
                var bodyResponse = await response.Content.ReadAsStringAsync();
                devs = JsonSerializer.Deserialize<List<Developer>>(bodyResponse,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
            }

            return View(devs);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Developer developer)
        {
            try
            {
                var res = await httpClient.PostAsJsonAsync(baseUrl, developer);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("");
                }

            }
            catch (Exception)
            {

                return View(developer);
            }
        }

        public async Task<IActionResult> Detalles(int id)
        {
            Developer developer = new Developer();

            var response = await httpClient.GetAsync(baseUrl + id);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                developer = JsonSerializer.Deserialize<Developer>(body, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return View(developer);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            Developer developer = new Developer();

            var response = await httpClient.GetAsync(baseUrl + id);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                developer = JsonSerializer.Deserialize<Developer>(body, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return View(developer);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id, int _)
        {
            var response = await httpClient.DeleteAsync(baseUrl + id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            } else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            Developer developer = new Developer();

            var response = await httpClient.GetAsync(baseUrl + id);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                developer = JsonSerializer.Deserialize<Developer>(body, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return View(developer);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Developer dev)
        {
            if(id == dev.DeveloperId)
            {
                var response = await httpClient.PutAsJsonAsync(baseUrl + id, dev);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                } else
                {
                    return RedirectToAction("Editar");
                }
            } else
            {
                return RedirectToAction("Editar");
            }
        }
    }
}
