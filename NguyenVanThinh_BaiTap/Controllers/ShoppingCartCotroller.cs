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
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int soLuong = 1)
        {
            var xe = _context.Thinh_Xe
                .Include(x => x.Thinh_HangXe)
                .FirstOrDefault(x => x.Thinh_XeID == id);

            if (xe == null) return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(x => x.Thinh_XeID == id);
            if (item != null)
            {
                item.SoLuong += soLuong;
            }
            else
            {
                cart.Add(new CartItem { Thinh_XeID = id, Xe = xe, SoLuong = soLuong });
            }
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

        [HttpPost]
        public IActionResult UpdateCart(List<CartItem> cartItems)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            foreach (var item in cart)
            {
                var updated = cartItems.FirstOrDefault(x => x.Thinh_XeID == item.Thinh_XeID);
                if (updated != null)
                {
                    item.SoLuong = updated.SoLuong > 0 ? updated.SoLuong : 1;
                }
            }
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            TempData["SuccessMessage"] = "Cập nhật giỏ hàng thành công!";
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            if (!cart.Any())
            {
                return RedirectToAction("Index");
            }

            var order = new Thinh_DonHang
            {
                MaDonHang = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                NgayDatHang = DateTime.Now,
                TongTien = cart.Sum(x => x.Xe.Thinh_Gia * x.SoLuong)
            };

            return View(order);
        }

        [HttpPost]
        public IActionResult CompleteOrder(Thinh_DonHang order)
        {
            if (ModelState.IsValid)
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

                order.MaDonHang = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss");
                order.NgayDatHang = DateTime.Now;
                order.TongTien = cart.Sum(x => x.Xe.Thinh_Gia * x.SoLuong);
                order.TrangThai = "Chờ xác nhận";

                _context.Thinh_DonHang.Add(order);
                _context.SaveChanges();

                foreach (var item in cart)
                {
                    var chiTiet = new Thinh_ChiTietDonHang
                    {
                        Thinh_DonHangID = order.Thinh_DonHangID,
                        Thinh_XeID = item.Thinh_XeID,
                        SoLuong = item.SoLuong,
                        DonGia = item.Xe.Thinh_Gia,
                        ThanhTien = item.Xe.Thinh_Gia * item.SoLuong
                    };
                    _context.Thinh_ChiTietDonHang.Add(chiTiet);
                }

                _context.SaveChanges();
                HttpContext.Session.Remove("Cart");

                TempData["SuccessMessage"] = "Đặt hàng thành công!";
                return RedirectToAction("Index", "Home");
            }

            return View("Checkout", order);
        }
    }
}