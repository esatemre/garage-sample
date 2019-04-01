namespace TheGarage.Sample.API.Controllers
{
    using Core.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    [Route("api/[controller]")]
    public class GarageController : BaseController
    {
        private readonly IGarageService _garageService;

        public GarageController(IGarageService garageService)
        {
            _garageService = garageService;
        }

        [HttpGet("Refresh")]
        public async Task<IActionResult> Refresh()
        {
            var result = await _garageService.RefreshGarage(GarageId);
            return new OkObjectResult(result);
        }

        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail()
        {
            var result = await _garageService.GetDetail(GarageId);
            return new OkObjectResult(result);
        }

    }
}