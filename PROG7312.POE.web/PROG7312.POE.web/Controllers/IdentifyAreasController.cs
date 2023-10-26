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
        public IActionResult Index()
        {
            //create and initialise new question
            QuestionData model = new QuestionData();
            // share new question with view
            return View(model);
        }

        [HttpGet]
        public JsonResult GetNewQuestion()
        {
            //create and initialise additional question
            QuestionData newQuestion = new QuestionData();
            // share additional question with view
            return Json(newQuestion);
        }

    }
}
