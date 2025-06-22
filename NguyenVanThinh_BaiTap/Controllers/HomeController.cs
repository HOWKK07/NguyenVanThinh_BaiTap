using Microsoft.AspNetCore.Mvc;

namespace NguyenVanThinh_BaiTap.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
