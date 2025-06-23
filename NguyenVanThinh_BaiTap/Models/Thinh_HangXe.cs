using System.ComponentModel.DataAnnotations;

namespace NguyenVanThinh_BaiTap.Models
{
    public class Thinh_HangXe
    {
        public Thinh_HangXe()
        {
            Thinh_Xe = new List<Thinh_Xe>();
        }

        public int Thinh_HangXeID { get; set; }

        [Required(ErrorMessage = "Tên hãng xe là bắt buộc")]
        [StringLength(100)]
        public string Thinh_TenHang { get; set; }

        // KHÔNG cần [Required] ở đây
        public virtual ICollection<Thinh_Xe> Thinh_Xe { get; set; }
    }
}
