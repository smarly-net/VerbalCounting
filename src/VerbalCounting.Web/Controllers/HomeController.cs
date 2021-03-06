﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading.Tasks;
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

		[HttpPost]
		public async Task<IActionResult> Easy(Example val)
		{
			return await Counting(val);
		}

		[HttpPost]
		public async Task<IActionResult> Normal(Example val)
		{
			return await Counting(val);
		}

		[HttpPost]
		public async Task<IActionResult> Hard(Example val)
		{
			return await Counting(val);
		}

		private async Task<IActionResult> Counting(Example result)
		{
			string value = (await CSharpScript.EvaluateAsync($"{result.Left} {result.Operator} {result.Right}")).ToString();

			if (result.Result == value)
			{
				ViewData["Success"] = result;
				return View("Counting", mathProvider.GetExample(result.Template));
			}
			else
			{
				ViewData["Fail"] = result.Result;
				return View("Counting", result);
			}
		}

		private IActionResult Counting(string template)
		{
			Example example = mathProvider.GetExample(template);
			return View("Counting", example);
		}
	}
}