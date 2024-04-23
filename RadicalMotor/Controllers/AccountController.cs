using Microsoft.AspNetCore.Mvc;

namespace RadicalMotor.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
