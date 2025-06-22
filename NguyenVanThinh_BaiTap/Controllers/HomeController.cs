using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenVanThinh_BaiTap.Data;
using NguyenVanThinh_BaiTap.Models;

namespace NguyenVanThinh_BaiTap.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? hangXeId)
        {
            var xes = _context.Thinh_Xes.Include(x => x.Thinh_HangXe).AsQueryable();

            if (hangXeId.HasValue)
            {
                xes = xes.Where(x => x.Thinh_HangXeID == hangXeId.Value);
            }

            ViewBag.HangXes = await _context.Thinh_HangXes.ToListAsync();
            ViewBag.SelectedHangXeId = hangXeId;

            return View(await xes.ToListAsync());
        }

        public IActionResult Cart()
        {
            return RedirectToAction("Index", "ShoppingCart");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}