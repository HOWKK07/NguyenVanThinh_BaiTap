using Microsoft.EntityFrameworkCore;
using NguyenVanThinh_BaiTap.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace NguyenVanThinh_BaiTap.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Thinh_HangXe> Thinh_HangXes { get; set; }
        public DbSet<Thinh_Xe> Thinh_Xes { get; set; }
        public DbSet<Thinh_DonHang> Thinh_DonHangs { get; set; }
        public DbSet<Thinh_ChiTietDonHang> Thinh_ChiTietDonHangs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ 1-nhiều giữa HangXe và Xe
            modelBuilder.Entity<Thinh_Xe>()
                .HasOne(x => x.Thinh_HangXe)
                .WithMany(h => h.Thinh_Xes)
                .HasForeignKey(x => x.Thinh_HangXeID);

            // Cấu hình quan hệ 1-nhiều giữa DonHang và ChiTietDonHang
            modelBuilder.Entity<Thinh_ChiTietDonHang>()
                .HasOne(ct => ct.Thinh_DonHang)
                .WithMany(dh => dh.Thinh_ChiTietDonHangs)
                .HasForeignKey(ct => ct.Thinh_DonHangID);

            // Cấu hình quan hệ 1-nhiều giữa Xe và ChiTietDonHang
            modelBuilder.Entity<Thinh_ChiTietDonHang>()
                .HasOne(ct => ct.Thinh_Xe)
                .WithMany(x => x.Thinh_ChiTietDonHangs)
                .HasForeignKey(ct => ct.Thinh_XeID);

            // Cấu hình computed column cho ThanhTien
            modelBuilder.Entity<Thinh_ChiTietDonHang>()
                .Property(ct => ct.ThanhTien)
                .HasComputedColumnSql("[SoLuong] * [DonGia]");

            // Seed data cho HangXe
            modelBuilder.Entity<Thinh_HangXe>().HasData(
                new Thinh_HangXe { Thinh_HangXeID = 1, Thinh_TenHang = "Toyota" },
                new Thinh_HangXe { Thinh_HangXeID = 2, Thinh_TenHang = "Honda" },
                new Thinh_HangXe { Thinh_HangXeID = 3, Thinh_TenHang = "Mazda" }
            );
        }
    }
}