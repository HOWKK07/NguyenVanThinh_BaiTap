using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenVanThinh_BaiTap.Data;
using NguyenVanThinh_BaiTap.Models;
using NguyenVanThinh_BaiTap.Repositories;

namespace NguyenVanThinh_BaiTap.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Thinh_XeController : Controller
    {
        private readonly IXeRepository _xeRepository;
        private readonly IHangXeRepository _hangXeRepository;
        private readonly ApplicationDbContext _context;

        public Thinh_XeController(IXeRepository xeRepository, IHangXeRepository hangXeRepository, ApplicationDbContext context)
        {
            _xeRepository = xeRepository;
            _hangXeRepository = hangXeRepository;
            _context = context;
        }

        // GET: Admin/Thinh_Xe
        public async Task<IActionResult> Index(string searchString)
        {
            var xes = _context.Thinh_Xe.Include(x => x.Thinh_HangXe).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                xes = xes.Where(x => x.Thinh_TenXe.Contains(searchString) ||
                                     x.Thinh_HangXe.Thinh_TenHang.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            return View(await xes.ToListAsync());
        }

        // GET: Admin/Thinh_Xe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Thinh_Xe
                .Include(x => x.Thinh_HangXe)
                .FirstOrDefaultAsync(m => m.Thinh_XeID == id);

            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // GET: Admin/Thinh_Xe/Create
        public IActionResult Create()
        {
            ViewData["Thinh_HangXeID"] = new SelectList(_hangXeRepository.GetAll(), "Thinh_HangXeID", "Thinh_TenHang");
            return View();
        }

        // POST: Admin/Thinh_Xe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Thinh_Xe xe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(xe);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm xe thành công!";
                return RedirectToAction(nameof(Index));
            }
            // Nếu lỗi, nạp lại danh sách hãng xe cho dropdown
            ViewData["Thinh_HangXeID"] = new SelectList(_context.Thinh_HangXe, "Thinh_HangXeID", "Thinh_TenHang", xe.Thinh_HangXeID);
            return View(xe);
        }

        // GET: Admin/Thinh_Xe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = _xeRepository.GetById(id.Value);
            if (xe == null)
            {
                return NotFound();
            }
            ViewData["Thinh_HangXeID"] = new SelectList(_hangXeRepository.GetAll(), "Thinh_HangXeID", "Thinh_TenHang", xe.Thinh_HangXeID);
            return View(xe);
        }

        // POST: Admin/Thinh_Xe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Thinh_XeID,Thinh_TenXe,Thinh_HangXeID,Thinh_Gia,Thinh_NgaySanXuat")] Thinh_Xe xe)
        {
            if (id != xe.Thinh_XeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _xeRepository.Update(xe);
                    TempData["SuccessMessage"] = "Cập nhật xe thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XeExists(xe.Thinh_XeID))
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
            ViewData["Thinh_HangXeID"] = new SelectList(_hangXeRepository.GetAll(), "Thinh_HangXeID", "Thinh_TenHang", xe.Thinh_HangXeID);
            return View(xe);
        }

        // GET: Admin/Thinh_Xe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Thinh_Xe
                .Include(x => x.Thinh_HangXe)
                .FirstOrDefaultAsync(m => m.Thinh_XeID == id);

            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // POST: Admin/Thinh_Xe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _xeRepository.Delete(id);
            TempData["SuccessMessage"] = "Xóa xe thành công!";
            return RedirectToAction(nameof(Index));
        }

        private bool XeExists(int id)
        {
            return _xeRepository.GetById(id) != null;
        }
    }
}