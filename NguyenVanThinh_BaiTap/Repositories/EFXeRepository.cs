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

        public IEnumerable<Thinh_Xe> GetAll() => _context.Thinh_Xes.ToList();

        public Thinh_Xe GetById(int id) => _context.Thinh_Xes.Find(id);

        public void Add(Thinh_Xe product)
        {
            _context.Thinh_Xes.Add(product);
            _context.SaveChanges();
        }

        public void Update(Thinh_Xe product)
        {
            _context.Thinh_Xes.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Thinh_Xes.Find(id);
            if (product != null)
            {
                _context.Thinh_Xes.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}