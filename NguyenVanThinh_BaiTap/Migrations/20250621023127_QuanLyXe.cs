using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NguyenVanThinh_BaiTap.Migrations
{
    /// <inheritdoc />
    public partial class QuanLyXe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Thinh_DonHangs",
                columns: table => new
                {
                    Thinh_DonHangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDonHang = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgayDatHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenKhachHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhuongThucThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thinh_DonHangs", x => x.Thinh_DonHangID);
                });

            migrationBuilder.CreateTable(
                name: "Thinh_HangXes",
                columns: table => new
                {
                    Thinh_HangXeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thinh_TenHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thinh_HangXes", x => x.Thinh_HangXeID);
                });

            migrationBuilder.CreateTable(
                name: "Thinh_Xes",
                columns: table => new
                {
                    Thinh_XeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thinh_TenXe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Thinh_HangXeID = table.Column<int>(type: "int", nullable: false),
                    Thinh_Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Thinh_NgaySanXuat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thinh_Xes", x => x.Thinh_XeID);
                    table.ForeignKey(
                        name: "FK_Thinh_Xes_Thinh_HangXes_Thinh_HangXeID",
                        column: x => x.Thinh_HangXeID,
                        principalTable: "Thinh_HangXes",
                        principalColumn: "Thinh_HangXeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Thinh_ChiTietDonHangs",
                columns: table => new
                {
                    Thinh_ChiTietDonHangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thinh_DonHangID = table.Column<int>(type: "int", nullable: false),
                    Thinh_XeID = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thinh_ChiTietDonHangs", x => x.Thinh_ChiTietDonHangID);
                    table.ForeignKey(
                        name: "FK_Thinh_ChiTietDonHangs_Thinh_DonHangs_Thinh_DonHangID",
                        column: x => x.Thinh_DonHangID,
                        principalTable: "Thinh_DonHangs",
                        principalColumn: "Thinh_DonHangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Thinh_ChiTietDonHangs_Thinh_Xes_Thinh_XeID",
                        column: x => x.Thinh_XeID,
                        principalTable: "Thinh_Xes",
                        principalColumn: "Thinh_XeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thinh_ChiTietDonHangs_Thinh_DonHangID",
                table: "Thinh_ChiTietDonHangs",
                column: "Thinh_DonHangID");

            migrationBuilder.CreateIndex(
                name: "IX_Thinh_ChiTietDonHangs_Thinh_XeID",
                table: "Thinh_ChiTietDonHangs",
                column: "Thinh_XeID");

            migrationBuilder.CreateIndex(
                name: "IX_Thinh_Xes_Thinh_HangXeID",
                table: "Thinh_Xes",
                column: "Thinh_HangXeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thinh_ChiTietDonHangs");

            migrationBuilder.DropTable(
                name: "Thinh_DonHangs");

            migrationBuilder.DropTable(
                name: "Thinh_Xes");

            migrationBuilder.DropTable(
                name: "Thinh_HangXes");
        }
    }
}
