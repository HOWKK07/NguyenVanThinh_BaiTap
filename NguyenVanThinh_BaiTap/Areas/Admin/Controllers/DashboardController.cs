using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenVanThinh_BaiTap.Data;
using NguyenVanThinh_BaiTap.Models;

namespace NguyenVanThinh_BaiTap.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var totalCustomers = await _context.Thinh_DonHang
                .Select(o => o.Email).Distinct().CountAsync();

            var totalOrders = await _context.Thinh_DonHang.CountAsync();

            var totalRevenue = await _context.Thinh_DonHang.SumAsync(o => o.TongTien);

            ViewBag.TotalCustomers = totalCustomers;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalRevenue = totalRevenue;

            return View();
        }
        //Tr? v? data ?? v? chart
        [HttpGet]
        public async Task<JsonResult> GetChartData()
        {
            var today = DateTime.UtcNow.Date;
            var last30Days = today.AddDays(-29);

            var ordersData = await _context.Thinh_DonHang
                .Where(o =>
                    o.NgayDatHang.Year > last30Days.Year ||
                    (o.NgayDatHang.Year == last30Days.Year && o.NgayDatHang.Month > last30Days.Month) ||
                    (o.NgayDatHang.Year == last30Days.Year && o.NgayDatHang.Month == last30Days.Month && o.NgayDatHang.Day >= last30Days.Day)
                )
                .Where(o =>
                    o.NgayDatHang.Year < today.Year ||
                    (o.NgayDatHang.Year == today.Year && o.NgayDatHang.Month < today.Month) ||
                    (o.NgayDatHang.Year == today.Year && o.NgayDatHang.Month == today.Month && o.NgayDatHang.Day <= today.Day)
                )
                .GroupBy(o => new { o.NgayDatHang.Year, o.NgayDatHang.Month, o.NgayDatHang.Day })
                .Select(g => new
                {
                    Date = $"{g.Key.Year:D4}-{g.Key.Month:D2}-{g.Key.Day:D2}",
                    Orders = g.Count(),
                    Revenue = g.Sum(o => o.TongTien)
                })
                .OrderBy(g => g.Date)
                .ToListAsync();

            return Json(ordersData);
        }

    }
}