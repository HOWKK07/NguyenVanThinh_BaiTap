using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenVanThinh_BaiTap.Models
{
    public class Thinh_ChiTietDonHang
    {
        [Key]
        public int Thinh_ChiTietDonHangID { get; set; }

        [Required]
        [ForeignKey("Thinh_DonHang")]
        public int Thinh_DonHangID { get; set; }

        [Required]
        [ForeignKey("Thinh_Xe")]
        public int Thinh_XeID { get; set; }

        [Required]
        public int SoLuong { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGia { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ThanhTien
        {
            get { return SoLuong * DonGia; }
            private set { }
        }

        [StringLength(500)]
        public string? GhiChu { get; set; }

        // Navigation properties
        public virtual Thinh_DonHang? Thinh_DonHang { get; set; }
        public virtual Thinh_Xe? Thinh_Xe { get; set; }
    }
}