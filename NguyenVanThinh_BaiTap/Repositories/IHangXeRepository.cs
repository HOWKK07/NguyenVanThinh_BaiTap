using NguyenVanThinh_BaiTap.Models;
using System.Collections.Generic;

namespace NguyenVanThinh_BaiTap.Repositories
{
    public interface IHangXeRepository
    {
        IEnumerable<Thinh_HangXe> GetAll();
        Thinh_HangXe GetById(int id);
        void Add(Thinh_HangXe category);
        void Update(Thinh_HangXe category);
        void Delete(int id);
    }
}