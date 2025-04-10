using VirtualCardAPI.Models;

namespace VirtualCardAPI.Repositories.Abstract
{
    public interface IVirtualCardRepository
    {
        IEnumerable<VirtualCard> GetAll();
        VirtualCard GetById(int id);
        VirtualCard GetByCardNumber(string cardNumber);
        void Add(VirtualCard card);
        void Update(VirtualCard card);
        void Delete(VirtualCard card);
    }
}
