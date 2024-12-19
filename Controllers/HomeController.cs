using System.Diagnostics;
using System.Reflection.Emit;
using System.Runtime.ExceptionServices;
using _932220.titarenko.nikita.lab12.Models;
using Microsoft.AspNetCore.Mvc;

namespace _932220.titarenko.nikita.lab12.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManualParsingInSingleAction()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ManualParsingInSingleAction(IFormCollection data)
        {
            if (double.TryParse(data["a"], out double a) && double.TryParse(data["b"], out double b))
            {
                ViewBag.ans = Calc(a, b, data["operation"]);
                return View("Result");
            }

            else
            {
                return View();
            }
        }

        [HttpGet, ActionName("ManualParsingInSeparateActions")]
        public IActionResult ManualParsingInSeparateActionsGet()
        {
            return View();
        }

        [HttpPost, ActionName("ManualParsingInSeparateActions")]
        public IActionResult ManualParsingInSeparateActionsPost(IFormCollection data)
        {
            if (double.TryParse(data["a"], out double a) && double.TryParse(data["b"], out double b))
            {
                ViewBag.ans = Calc(a, b, data["operation"]);
                return View("Result");
            }

            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult ModelBindingInParameters()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ModelBindingInParameters(double a, double b, string operation)
        {
            ViewBag.ans = Calc(a, b, operation);
            return View("Result");
        }

        [HttpGet, ActionName("ModelBindingInSeparateModel")]
        public IActionResult ModelBindingInSeparateModelGet()
        {
            return View();
        }

        [HttpPost, ActionName("ModelBindingInSeparateModel")]
        public IActionResult ModelBindingInSeparateModelPost(CalcModel model)
        {
            ViewBag.ans = Calc(model.a, model.b, model.operation);
            return View("Result");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string Calc(double a, double b, string operation)
        {
            switch (operation)
            {
                case "+": return $"{a} + {b} = " + (a + b);
                case "-": return $"{a} - {b} = " + (a - b);
                case "*": return $"{a} * {b} = " + (a * b);
                case "/":
                {
                    if (b != 0) return $"{a} / {b} = " + (a / b);
                    return $"{a} / {b} = На ноль делить нельзя!";
                }
            }
            return "";
        }
    }
}