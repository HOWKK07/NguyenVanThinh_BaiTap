using Microsoft.AspNetCore.Mvc;
using NguyenVanThinh_BaiTap.Data;
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
            var xe = _context.Thinh_Xes.Find(id);
            if (xe == null) return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<Thinh_Xe>>("Cart") ?? new List<Thinh_Xe>();
            cart.Add(xe);
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Thinh_Xe>>("Cart") ?? new List<Thinh_Xe>();
            return View(cart);
        }

        [HttpPost]
        public IActionResult CompleteOrder(Thinh_DonHang order)
        {
            // Lưu đơn hàng và chi tiết đơn hàng vào database
            // Xóa giỏ hàng khỏi session
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index", "Home");
        }
    }
}