using VirtualCardAPI.Models;

namespace VirtualCardAPI.Repositories
{
    public interface IVirtualCardRepository
    {
        IEnumerable<VirtualCard> GetAll();
        VirtualCard GetById(int id);
        void Add(VirtualCard card);
        void Update(VirtualCard card);
        void Delete(VirtualCard card);
    }
}
