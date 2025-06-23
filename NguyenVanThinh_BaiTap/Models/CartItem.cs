using NguyenVanThinh_BaiTap.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem
{
    public int Thinh_XeID { get; set; }
    public Thinh_Xe Xe { get; set; }
    public int SoLuong { get; set; }
}