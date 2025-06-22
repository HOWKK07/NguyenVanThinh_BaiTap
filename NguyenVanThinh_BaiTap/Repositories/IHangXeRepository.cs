using NguyenVanThinh_BaiTap.Models;
using System.Collections.Generic;

namespace NguyenVanThinh_BaiTap.Repositories
{
    public interface IHangXeRepository
    {
        IEnumerable<Thinh_HangXe> GetAll();
        Thinh_HangXe GetById(int id);
        void Create(Thinh_HangXe category);
        void Edit(Thinh_HangXe category);
        void Delete(int id);
    }
}