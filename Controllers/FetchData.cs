using Microsoft.AspNetCore.Mvc;
using Dota2WinPredictor.Services;

namespace Dota2WinPredictor.Controllers
{
    [Route("FetchData")]
    public class FetchData : Controller
    {
        private readonly IDataService _dataService;

        public FetchData(IDataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("CallFetchDataAsync")]
        public async Task<IActionResult> CallFetchDataAsync()
        {
            var result = await _dataService.GetDataAsync();
            return View("HeroesView", result);

        }
    }
}
