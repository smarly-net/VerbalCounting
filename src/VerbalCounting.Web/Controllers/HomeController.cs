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

		public IActionResult Easy(string op)
		{
			Example example = mathProvider.GetExample($"1 {op} 1");
			return View("Counting", example);
		}
		public IActionResult Normal(string op)
		{
			Example example = mathProvider.GetExample($"1-2 {op} 1-2");
			return View("Counting", example);
		}
		public IActionResult Hard(string op)
		{
			Example example = mathProvider.GetExample($"2-3 {op} 2-3");
			return View("Counting", example);
		}
	}
}