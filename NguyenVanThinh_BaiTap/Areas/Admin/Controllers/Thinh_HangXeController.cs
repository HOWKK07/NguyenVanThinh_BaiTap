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
            var hangXes = _context.Thinh_HangXes.AsQueryable();

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

            var hangXe = await _context.Thinh_HangXes
                .Include(h => h.Thinh_Xes)
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

        // POST: Admin/Thinh_HangXe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Thinh_TenHang")] Thinh_HangXe hangXe)
        {
            if (ModelState.IsValid)
            {
                _hangXeRepository.Create(hangXe);
                TempData["SuccessMessage"] = "Thêm hãng xe thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(hangXe);
        }

        // GET: Admin/Thinh_HangXe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangXe = _hangXeRepository.GetById(id.Value);
            if (hangXe == null)
            {
                return NotFound();
            }
            return View(hangXe);
        }

        // POST: Admin/Thinh_HangXe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Thinh_HangXeID,Thinh_TenHang")] Thinh_HangXe hangXe)
        {
            if (id != hangXe.Thinh_HangXeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _hangXeRepository.Edit(hangXe);
                    TempData["SuccessMessage"] = "Cập nhật hãng xe thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangXeExists(hangXe.Thinh_HangXeID))
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
            return View(hangXe);
        }

        // GET: Admin/Thinh_HangXe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangXe = await _context.Thinh_HangXes
                .Include(h => h.Thinh_Xes)
                .FirstOrDefaultAsync(m => m.Thinh_HangXeID == id);

            if (hangXe == null)
            {
                return NotFound();
            }

            return View(hangXe);
        }

        // POST: Admin/Thinh_HangXe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hangXe = await _context.Thinh_HangXes
                .Include(h => h.Thinh_Xes)
                .FirstOrDefaultAsync(h => h.Thinh_HangXeID == id);

            if (hangXe.Thinh_Xes.Any())
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