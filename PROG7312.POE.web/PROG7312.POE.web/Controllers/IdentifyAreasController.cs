using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PROG7312.POE.web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PROG7312.POE.web.Controllers
{
    public class IdentifyAreasController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            QuestionData model = new QuestionData();
            // Populate the model
            return View(model);
        }

        [HttpGet]
        public JsonResult GetNewQuestion()
        {
            QuestionData newQuestion = new QuestionData();
            return Json(newQuestion);
        }

    }
}

