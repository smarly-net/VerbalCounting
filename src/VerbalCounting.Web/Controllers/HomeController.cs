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
			Example example = mathProvider.GetExample("2-3 ? 3-5");
			return View(example);
		}
	}
}