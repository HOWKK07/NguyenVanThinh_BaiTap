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

        public IEnumerable<Thinh_HangXe> GetAll() => _context.Thinh_HangXe.ToList();

        public Thinh_HangXe GetById(int id) => _context.Thinh_HangXe.Find(id);

        public void Create(Thinh_HangXe category)
        {
            _context.Thinh_HangXe.Add(category);
            _context.SaveChanges(); // Dòng này bắt buộc phải có!
        }

        public void Edit(Thinh_HangXe category)
        {
            _context.Thinh_HangXe.Update(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _context.Thinh_HangXe.Find(id);
            if (category != null)
            {
                _context.Thinh_HangXe.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}