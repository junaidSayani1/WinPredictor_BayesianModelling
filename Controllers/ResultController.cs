using Dota2WinPredictor.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dota2WinPredictor.Controllers
{
    [Route("Results")]

    public class ResultsController : Controller
    {
        [HttpPost("Store")]
        public IActionResult Store([FromBody] PredictResponse result)
        {
            TempData["Prediction"] = JsonConvert.SerializeObject(result);
            return Ok();
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var json = TempData.Peek("Prediction") as string;
            if (json == null)
                return Content("No prediction data found. Please run a prediction first.");

            var model = JsonConvert.DeserializeObject<PredictResponse>(json);
            return View("FinalView", model);
        }
    }

}
