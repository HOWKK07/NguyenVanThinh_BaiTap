using NguyenVanThinh_BaiTap.Data;
using NguyenVanThinh_BaiTap.Models;
using System.Collections.Generic;
using System.Linq;

namespace NguyenVanThinh_BaiTap.Repositories
{
    public class EFHangXeRepository : IHangXeRepository
    {
        private readonly ApplicationDbContext _context;

        public EFHangXeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Thinh_HangXe> GetAll() => _context.Thinh_HangXes.ToList();

        public Thinh_HangXe GetById(int id) => _context.Thinh_HangXes.Find(id);

        public void Create(Thinh_HangXe category)
        {
            _context.Thinh_HangXes.Add(category);
            _context.SaveChanges();
        }

        public void Edit(Thinh_HangXe category)
        {
            _context.Thinh_HangXes.Update(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _context.Thinh_HangXes.Find(id);
            if (category != null)
            {
                _context.Thinh_HangXes.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}