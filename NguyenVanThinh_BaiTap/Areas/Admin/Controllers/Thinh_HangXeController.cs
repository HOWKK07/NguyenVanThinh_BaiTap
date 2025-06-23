using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenVanThinh_BaiTap.Data;
using NguyenVanThinh_BaiTap.Models;
using NguyenVanThinh_BaiTap.Repositories;

namespace NguyenVanThinh_BaiTap.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Thinh_HangXeController : Controller
    {
        private readonly IHangXeRepository _hangXeRepository;
        private readonly ApplicationDbContext _context;

        public Thinh_HangXeController(IHangXeRepository hangXeRepository, ApplicationDbContext context)
        {
            _hangXeRepository = hangXeRepository;
            _context = context;
        }

        // GET: Admin/Thinh_HangXe
        public async Task<IActionResult> Index(string searchString)
        {
            var hangXes = _context.Thinh_HangXe.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                hangXes = hangXes.Where(h => h.Thinh_TenHang.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            return View(await hangXes.ToListAsync());
        }

        // GET: Admin/Thinh_HangXe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangXe = await _context.Thinh_HangXe
                .Include(h => h.Thinh_Xe)
                .FirstOrDefaultAsync(m => m.Thinh_HangXeID == id);

            if (hangXe == null)
            {
                return NotFound();
            }

            return View(hangXe);
        }

        // GET: Admin/Thinh_HangXe/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Thinh_HangXe catagory)
        {
            if (ModelState.IsValid)
            {
                _context.Thinh_HangXe.Add(catagory);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm hãng xe thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(catagory);
        }

        // GET: Admin/Thinh_HangXe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catagoy = _hangXeRepository.GetById(id.Value);
            if (catagoy == null)
            {
                return NotFound();
            }
            return View(catagoy);
        }

        // POST: Admin/Thinh_HangXe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Thinh_HangXeID,Thinh_TenHang")] Thinh_HangXe catagory)
        {
            if (id != catagory.Thinh_HangXeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _hangXeRepository.Edit(catagory);
                    TempData["SuccessMessage"] = "Cập nhật hãng xe thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangXeExists(catagory.Thinh_HangXeID))
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
            return View(catagory);
        }

        // GET: Admin/Thinh_HangXe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Thinh_HangXe
                .Include(h => h.Thinh_Xe)
                .FirstOrDefaultAsync(m => m.Thinh_HangXeID == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Thinh_HangXe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Thinh_HangXe
                .Include(h => h.Thinh_Xe)
                .FirstOrDefaultAsync(h => h.Thinh_HangXeID == id);

            if (category.Thinh_Xe.Any())
            {
                TempData["ErrorMessage"] = "Không thể xóa hãng xe này vì còn xe thuộc hãng!";
                return RedirectToAction(nameof(Index));
            }

            _hangXeRepository.Delete(id);
            TempData["SuccessMessage"] = "Xóa hãng xe thành công!";
            return RedirectToAction(nameof(Index));
        }

        private bool HangXeExists(int id)
        {
            return _hangXeRepository.GetById(id) != null;
        }
    }
}