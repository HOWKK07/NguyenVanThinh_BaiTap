using NguyenVanThinh_BaiTap.Models;
using System.Collections.Generic;

namespace NguyenVanThinh_BaiTap.Repositories
{
    public interface IXeRepository
    {
        IEnumerable<Thinh_Xe> GetAll();
        Thinh_Xe GetById(int id);
        void Add(Thinh_Xe product);
        void Update(Thinh_Xe product);
        void Delete(int id);
    }
}