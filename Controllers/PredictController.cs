using Microsoft.AspNetCore.Mvc;
using Dota2WinPredictor.Services;
using Dota2WinPredictor.Models;


namespace Dota2WinPredictor.Controllers
{
    [Route("PredictController")]
    public class PredictController : Controller
    {
        private readonly IDataService _dataService;

        public PredictController(IDataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Predict")]
        public IActionResult Predict(PredictRequest request)
        {
            //var result = _dataService.PredictBayes(request);
            return Json("");

        }
    }
}
