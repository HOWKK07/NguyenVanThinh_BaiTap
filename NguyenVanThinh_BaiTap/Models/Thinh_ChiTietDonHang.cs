using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenVanThinh_BaiTap.Models
{
    public class Thinh_ChiTietDonHang
    {
        public int Thinh_ChiTietDonHangID { get; set; }
        public int Thinh_DonHangID { get; set; }
        public int Thinh_XeID { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; } // Removed 'private set;' to make the setter accessible
        public string? GhiChu { get; set; }
        public virtual Thinh_DonHang? Thinh_DonHang { get; set; }
        public virtual Thinh_Xe? Thinh_Xe { get; set; }
    }
}