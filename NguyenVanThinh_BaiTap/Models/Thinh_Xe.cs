using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NguyenVanThinh_BaiTap.Models
{
    public class Thinh_Xe
    {
        [Key]
        public int Thinh_XeID { get; set; }

        [Required(ErrorMessage = "Tên xe là bắt buộc")]
        [StringLength(200)]
        public string Thinh_TenXe { get; set; }

        [Required(ErrorMessage = "Mã hãng xe là bắt buộc")]
        [ForeignKey("Thinh_HangXe")]
        public int Thinh_HangXeID { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Thinh_Gia { get; set; }

        [Required(ErrorMessage = "Ngày sản xuất là bắt buộc")]
        public DateTime Thinh_NgaySanXuat { get; set; }

        [ValidateNever]
        public virtual Thinh_HangXe Thinh_HangXe { get; set; }

        [ValidateNever]
        public virtual ICollection<Thinh_ChiTietDonHang> Thinh_ChiTietDonHang { get; set; }
    }
}
