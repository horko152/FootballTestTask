using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FootballTestTask.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FootballTestTask.Controllers
{
	public class HomeController : Controller
	{
		#region Controller methods
		[HttpGet]
		public IActionResult Index()
		{
            ViewBag.Message = "Football Statictic";
            return View();
		}
		#endregion
	}
}
