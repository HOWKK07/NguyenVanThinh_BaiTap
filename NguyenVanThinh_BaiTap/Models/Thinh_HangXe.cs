using System.ComponentModel.DataAnnotations;

namespace NguyenVanThinh_BaiTap.Models
{
    public class Thinh_HangXe
    {
        [Key]
        public int Thinh_HangXeID { get; set; }

        [Required]
        [StringLength(100)]
        public string Thinh_TenHang { get; set; }

        // Navigation property
        public virtual ICollection<Thinh_Xe> Thinh_Xes { get; set; }
    }
}
