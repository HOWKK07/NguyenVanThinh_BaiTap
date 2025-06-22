using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenVanThinh_BaiTap.Data;
using NguyenVanThinh_BaiTap.Models;

namespace NguyenVanThinh_BaiTap.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Thinh_DonHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Thinh_DonHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Thinh_DonHang
        public async Task<IActionResult> Index()
        {
            var donHangs = await _context.Thinh_DonHangs
                .OrderByDescending(d => d.NgayDatHang)
                .ToListAsync();
            return View(donHangs);
        }

        // GET: Admin/Thinh_DonHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.Thinh_DonHangs
                .Include(d => d.Thinh_ChiTietDonHangs)
                    .ThenInclude(ct => ct.Thinh_Xe)
                        .ThenInclude(x => x.Thinh_HangXe)
                .FirstOrDefaultAsync(m => m.Thinh_DonHangID == id);

            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // GET: Admin/Thinh_DonHang/UpdateStatus/5
        public async Task<IActionResult> UpdateStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.Thinh_DonHangs.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }

            ViewBag.TrangThaiList = new List<string>
            {
                "Chờ xác nhận",
                "Đã xác nhận",
                "Đang giao hàng",
                "Đã giao hàng",
                "Đã hủy"
            };

            return View(donHang);
        }

        // POST: Admin/Thinh_DonHang/UpdateStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string TrangThai)
        {
            var donHang = await _context.Thinh_DonHangs.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }

            donHang.TrangThai = TrangThai;

            if (TrangThai == "Đã giao hàng")
            {
                donHang.NgayGiao = DateTime.Now;
            }

            _context.Update(donHang);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Cập nhật trạng thái đơn hàng thành công!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Thinh_DonHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.Thinh_DonHangs
                .FirstOrDefaultAsync(m => m.Thinh_DonHangID == id);

            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // POST: Admin/Thinh_DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donHang = await _context.Thinh_DonHangs.FindAsync(id);
            if (donHang != null)
            {
                _context.Thinh_DonHangs.Remove(donHang);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa đơn hàng thành công!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}