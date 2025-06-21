using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenVanThinh_BaiTap.Models
{
    public class Thinh_Xe
    {
        [Key]
        public int Ten_XeID { get; set; }

        [Required]
        [StringLength(200)]
        public string Thinh_TenXe { get; set; }

        [Required]
        [ForeignKey("HangXe")]
        public int Thinh_HangXeID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Thinh_Gia { get; set; }

        [Required]
        public DateTime Thinh_NgaySanXuat { get; set; }


    }
}
