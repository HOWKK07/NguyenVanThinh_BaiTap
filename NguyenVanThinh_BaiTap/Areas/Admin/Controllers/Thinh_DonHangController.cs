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
        public async Task<IActionResult> Index(string searchString)
        {
            var donHangs = _context.Thinh_DonHang.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                donHangs = donHangs.Where(d => d.MaDonHang.Contains(searchString) ||
                                               d.TenKhachHang.Contains(searchString) ||
                                               d.SoDienThoai.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            return View(await donHangs.OrderByDescending(d => d.NgayDatHang).ToListAsync());
        }

        // GET: Admin/Thinh_DonHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.Thinh_DonHang
                .Include(d => d.Thinh_ChiTietDonHang)
                    .ThenInclude(ct => ct.Thinh_Xe)
                        .ThenInclude(x => x.Thinh_HangXe)
                .FirstOrDefaultAsync(m => m.Thinh_DonHangID == id);

            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // GET: Admin/Thinh_DonHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.Thinh_DonHang.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }
            return View(donHang);
        }

        // POST: Admin/Thinh_DonHang/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Thinh_DonHangID,MaDonHang,NgayDatHang,TenKhachHang,SoDienThoai,Email,DiaChi,TongTien,GhiChu,TrangThai,NgayGiao,PhuongThucThanhToan")] Thinh_DonHang donHang)
        {
            if (id != donHang.Thinh_DonHangID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donHang);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật đơn hàng thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangExists(donHang.Thinh_DonHangID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(donHang);
        }

        // GET: Admin/Thinh_DonHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.Thinh_DonHang
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
            var donHang = await _context.Thinh_DonHang.FindAsync(id);
            if (donHang != null)
            {
                _context.Thinh_DonHang.Remove(donHang);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa đơn hàng thành công!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DonHangExists(int id)
        {
            return _context.Thinh_DonHang.Any(e => e.Thinh_DonHangID == id);
        }
    }
}