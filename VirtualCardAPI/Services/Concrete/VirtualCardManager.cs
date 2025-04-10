using VirtualCardAPI.Context;
using VirtualCardAPI.Models;
using VirtualCardAPI.Repositories.Abstract;
using VirtualCardAPI.Services.Abstract;

namespace VirtualCardAPI.Services.Concrete
{
    public class VirtualCardManager : IVirtualCardService
    {
        private readonly IVirtualCardRepository _repository;

        public VirtualCardManager(IVirtualCardRepository repository)
        {
            _repository = repository;
        }


        public List<VirtualCard> GetAllActiveCards()
        {
            return _repository.GetAll().Where(c => c.IsActive).ToList();  
        }

        public decimal GetTotalBalance()
        {
            return _repository.GetAll().Sum(c => c.Balance);  
        }


        public VirtualCard GetCardByNumber(string cardNumber)
        {
            return _repository.GetAll().FirstOrDefault(c => c.CardNumber == cardNumber); 
        }

        public void AddCard(VirtualCard card)
        {
            _repository.Add(card);  
           
        }
    }
}
