using Microsoft.AspNetCore.Mvc;
using NZWalk.Frontend.Models.DTO;

namespace NZWalk.Frontend.Controllers
{
    public class RegionController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        private readonly string ApiBase = "https://localhost:7225/api/region";
        public RegionController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync(ApiBase);
                httpResponseMessage.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());
                
            }
            catch (Exception e)
            {

                
            }
            return View(response);
        }
    }
}
