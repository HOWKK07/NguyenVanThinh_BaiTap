using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenVanThinh_BaiTap.Data;
using NguyenVanThinh_BaiTap.Extensions;
using NguyenVanThinh_BaiTap.Models;

namespace NguyenVanThinh_BaiTap.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Thinh_Xe>>("Cart") ?? new List<Thinh_Xe>();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var xe = _context.Thinh_Xes
                .Include(x => x.Thinh_HangXe)
                .FirstOrDefault(x => x.Thinh_XeID == id);

            if (xe == null) return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<Thinh_Xe>>("Cart") ?? new List<Thinh_Xe>();
            cart.Add(xe);
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            TempData["SuccessMessage"] = "Đã thêm xe vào giỏ hàng!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Thinh_Xe>>("Cart") ?? new List<Thinh_Xe>();
            var item = cart.FirstOrDefault(x => x.Thinh_XeID == id);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Thinh_Xe>>("Cart") ?? new List<Thinh_Xe>();
            if (!cart.Any())
            {
                return RedirectToAction("Index");
            }

            var order = new Thinh_DonHang
            {
                MaDonHang = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                NgayDatHang = DateTime.Now,
                TongTien = cart.Sum(x => x.Thinh_Gia)
            };

            return View(order);
        }

        [HttpPost]
        public IActionResult CompleteOrder(Thinh_DonHang order)
        {
            if (ModelState.IsValid)
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<Thinh_Xe>>("Cart") ?? new List<Thinh_Xe>();

                order.MaDonHang = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss");
                order.NgayDatHang = DateTime.Now;
                order.TongTien = cart.Sum(x => x.Thinh_Gia);
                order.TrangThai = "Chờ xác nhận";

                _context.Thinh_DonHangs.Add(order);
                _context.SaveChanges();

                // Thêm chi tiết đơn hàng
                foreach (var xe in cart)
                {
                    var chiTiet = new Thinh_ChiTietDonHang
                    {
                        Thinh_DonHangID = order.Thinh_DonHangID,
                        Thinh_XeID = xe.Thinh_XeID,
                        SoLuong = 1,
                        DonGia = xe.Thinh_Gia
                    };
                    _context.Thinh_ChiTietDonHangs.Add(chiTiet);
                }

                _context.SaveChanges();

                // Xóa giỏ hàng
                HttpContext.Session.Remove("Cart");

                TempData["SuccessMessage"] = "Đặt hàng thành công!";
                return RedirectToAction("Index", "Home");
            }

            return View("Checkout", order);
        }
    }
}