﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Veebirakenduste_loomine_API_MartinKemppi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParcelMachineController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ParcelMachineController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("omniva")]
        public async Task<IActionResult> GetParcelMachinesOmniva()
        {
            var response = await _httpClient.GetAsync("https://www.omniva.ee/locations.json");
            var responseBody = await response.Content.ReadAsStringAsync();
            return Content(responseBody, "application/json");
        }

        [HttpGet("smartpost")]
        public async Task<IActionResult> GetParcelMachinesSmartPost()
        {
            var response = await _httpClient.GetAsync("https://www.smartpost.ee/places.json");
            var responseBody = await response.Content.ReadAsStringAsync();
            return Content(responseBody, "application/json");
        }
    }
}
