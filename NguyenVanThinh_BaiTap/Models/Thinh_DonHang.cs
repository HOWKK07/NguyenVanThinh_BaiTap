using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NguyenVanThinh_BaiTap.Models
{
    public class Thinh_DonHang
    {
        [Key]
        public int Thinh_DonHangID { get; set; }

        [StringLength(20)]
        public string? MaDonHang { get; set; } // nullable

        public DateTime NgayDatHang { get; set; }

        [Required]
        [StringLength(100)]
        public string TenKhachHang { get; set; }

        [Required]
        [StringLength(15)]
        [Phone]
        public string SoDienThoai { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string DiaChi { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TongTien { get; set; }

        [StringLength(1000)]
        public string? GhiChu { get; set; }

        [StringLength(50)]
        public string? TrangThai { get; set; } // nullable

        public DateTime? NgayGiao { get; set; }

        [StringLength(50)]
        public string PhuongThucThanhToan { get; set; }

        [ValidateNever]
        public virtual ICollection<Thinh_ChiTietDonHang> Thinh_ChiTietDonHang { get; set; }
    }
}