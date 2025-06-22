using Microsoft.EntityFrameworkCore;
using NguyenVanThinh_BaiTap.Models;

namespace NguyenVanThinh_BaiTap.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Thinh_HangXe> Thinh_HangXe { get; set; }
        public DbSet<Thinh_Xe> Thinh_Xe { get; set; }
        public DbSet<Thinh_DonHang> Thinh_DonHang { get; set; }
        public DbSet<Thinh_ChiTietDonHang> Thinh_ChiTietDonHang { get; set; }

        }
    }