using Microsoft.AspNetCore.Mvc;
using VerbalCounting.Web.Providers;

namespace VerbalCounting.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly MathProvider mathProvider;

		public HomeController(MathProvider mathProvider)
		{
			this.mathProvider = mathProvider ?? throw new System.ArgumentNullException(nameof(mathProvider));
		}

		public IActionResult Index()
		{
			return View("Index");
		}

		[HttpGet]
		public IActionResult Easy(string op)
		{
			return Counting($"1 {op} 1");
		}

		[HttpGet]
		public IActionResult Normal(string op)
		{
			return Counting($"1-2 {op} 1-2");
		}

		[HttpGet]
		public IActionResult Hard(string op)
		{
			return Counting($"2-3 {op} 2-3");
		}




		private IActionResult Counting(string template)
		{
			Example example = mathProvider.GetExample(template);
			return View("Counting", example);
		}
	}
}