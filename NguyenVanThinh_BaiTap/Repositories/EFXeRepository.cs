using NguyenVanThinh_BaiTap.Data;
using NguyenVanThinh_BaiTap.Models;
using System.Collections.Generic;
using System.Linq;

namespace NguyenVanThinh_BaiTap.Repositories
{
    public class EFXeRepository : IXeRepository
    {
        private readonly ApplicationDbContext _context;

        public EFXeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Thinh_Xe> GetAll() => _context.Thinh_Xe.ToList();

        public Thinh_Xe GetById(int id) => _context.Thinh_Xe.Find(id);

        public void Add(Thinh_Xe product)
        {
            _context.Thinh_Xe.Add(product);
            _context.SaveChanges();
        }

        public void Update(Thinh_Xe product)
        {
            _context.Thinh_Xe.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Thinh_Xe.Find(id);
            if (product != null)
            {
                _context.Thinh_Xe.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}