using VirtualCardAPI.Models;
using VirtualCardAPI.Context;
using Microsoft.EntityFrameworkCore;    

namespace VirtualCardAPI.Repositories
{
    public class VirtualCardRepository : IVirtualCardRepository
    {
        private readonly VirtualCardDbContext _context;

        public VirtualCardRepository(VirtualCardDbContext context)
        {
            _context = context;
        }

        public IEnumerable<VirtualCard> GetAll()
        {
            return _context.VirtualCards.ToList();
        }
       

        public VirtualCard GetById(int id)
        {
            return _context.VirtualCards.FirstOrDefault(c => c.Id == id);
        }

        public void Add(VirtualCard card)
        {
            _context.VirtualCards.Add(card);
            _context.SaveChanges();
        }

        public void Update(VirtualCard card)
        {
            _context.VirtualCards.Update(card);
            _context.SaveChanges();
        }

        public void Delete(VirtualCard card)
        {
            _context.VirtualCards.Remove(card);
            _context.SaveChanges();
        }
    }
}
