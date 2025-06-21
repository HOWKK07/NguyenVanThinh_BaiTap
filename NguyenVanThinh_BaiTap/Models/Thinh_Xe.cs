using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenVanThinh_BaiTap.Models
{
    public class Thinh_Xe
    {
        [Key]
        public int Thinh_XeID { get; set; } // Đã sửa từ Ten_XeID

        [Required]
        [StringLength(200)]
        public string Thinh_TenXe { get; set; }

        [Required]
        [ForeignKey("Thinh_HangXe")]
        public int Thinh_HangXeID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Thinh_Gia { get; set; }

        [Required]
        public DateTime Thinh_NgaySanXuat { get; set; }

        // Navigation property
        public virtual Thinh_HangXe Thinh_HangXe { get; set; }
        public virtual ICollection<Thinh_ChiTietDonHang> Thinh_ChiTietDonHangs { get; set; }

    }
}
