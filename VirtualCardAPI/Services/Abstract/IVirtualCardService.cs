using VirtualCardAPI.Models;

namespace VirtualCardAPI.Services.Abstract
{
    public interface  IVirtualCardService
    {
        List<VirtualCard> GetAllActiveCards();
        decimal GetTotalBalance();
        VirtualCard GetCardByNumber(string cardNumber);
        void AddCard(VirtualCard card);

    }
}
