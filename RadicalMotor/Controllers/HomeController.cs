using Microsoft.AspNetCore.Mvc;

namespace RadicalMotor.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
