using Microsoft.AspNetCore.Mvc;
using VirtualCardAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using VirtualCardAPI.Extensions;
using VirtualCardAPI.Validators;
using VirtualCardAPI.Repositories.Abstract;
using VirtualCardAPI.DTOs.VirtualCard;
using Microsoft.AspNetCore.Authorization;



namespace VirtualCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VirtualCardController : ControllerBase
    {
        private readonly IVirtualCardRepository _repository;

        public VirtualCardController(IVirtualCardRepository repository)
        {
            _repository = repository;
        }

      
        [HttpGet]
        public IActionResult GetAllCards()
        {
            var cards = _repository.GetAll()
                .Select(card => new VirtualCardResponse
                {
                    Id = card.Id,
                    CardHolder = card.CardHolder,
                    MaskedCardNumber = MaskCardNumber(card.CardNumber),
                    ExpirationDate = card.ExpirationDate,
                    Balance = card.Balance,
                    IsActive = card.IsActive
                });

            return Ok(cards);
        }


        [Authorize]
        [HttpPost]
        public IActionResult CreateCard([FromBody] VirtualCardCreateRequest request)
        {
            var card = new VirtualCard
            {
                CardHolder = request.CardHolder,
                CardNumber = request.CardNumber,
                ExpirationDate = request.ExpirationDate,
                Balance = request.Balance,
                IsActive = true
            };

            _repository.Add(card);

            var response = new VirtualCardResponse
            {
                Id = card.Id,
                CardHolder = card.CardHolder,
                MaskedCardNumber = MaskCardNumber(card.CardNumber),
                ExpirationDate = card.ExpirationDate,
                Balance = card.Balance,
                IsActive = card.IsActive
            };

            return CreatedAtAction(nameof(GetCardById), new { id = card.Id }, response);
        }

       

        [HttpGet("{id}")]
        public IActionResult GetCardById(int id)
        {
            var card = _repository.GetById(id);
            if (card == null)
                return NotFound();

            var response = new VirtualCardResponse
            {
                Id = card.Id,
                CardHolder = card.CardHolder,
                MaskedCardNumber = MaskCardNumber(card.CardNumber),
                ExpirationDate = card.ExpirationDate,
                Balance = card.Balance,
                IsActive = card.IsActive
            };

            return Ok(response);
        }


        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateCard(int id, [FromBody] VirtualCardUpdateRequest request)
        {
            var existingCard = _repository.GetById(id);
            if (existingCard == null)
                return NotFound();

            existingCard.CardHolder = request.CardHolder;
            existingCard.ExpirationDate = request.ExpirationDate;
            existingCard.Balance = request.Balance;
            existingCard.IsActive = request.IsActive;

            _repository.Update(existingCard);
            return NoContent();
        }


        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteCard(int id)
        {
            var card = _repository.GetById(id);
            if (card == null)
                return NotFound();

            _repository.Delete(card);
            return NoContent();
        }

        

        [HttpGet("by-number/{cardNumber}")]
        public IActionResult GetCardByNumber(string cardNumber)
        {
            var card = _repository.GetByCardNumber(cardNumber);
            if (card == null)
                return NotFound();

            var response = new VirtualCardResponse
            {
                Id = card.Id,
                CardHolder = card.CardHolder,
                MaskedCardNumber = MaskCardNumber(card.CardNumber),
                ExpirationDate = card.ExpirationDate,
                Balance = card.Balance,
                IsActive = card.IsActive
            };

            return Ok(response);
        }


        
        [HttpGet("list")]
        public IActionResult GetFilteredCards([FromQuery] string name, [FromQuery] string sortBy)
        {
            var cards = _repository.GetAll();

            if (!string.IsNullOrEmpty(name))
            {
                cards = cards.Where(c => c.CardHolder.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                cards = sortBy.ToLower() switch
                {
                    "balance" => cards.OrderBy(c => c.Balance),
                    "expirationdate" => cards.OrderBy(c => c.ExpirationDate),
                    _ => cards
                };
            }

            var response = cards.Select(card => new VirtualCardResponse
            {
                Id = card.Id,
                CardHolder = card.CardHolder,
                MaskedCardNumber = MaskCardNumber(card.CardNumber),
                ExpirationDate = card.ExpirationDate,
                Balance = card.Balance,
                IsActive = card.IsActive
            });

            return Ok(response);
        }


        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult UpdatePartialCard(int id, [FromBody] JsonPatchDocument<VirtualCard> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Patch document is null.");

            var existingCard = _repository.GetById(id);
            if (existingCard == null)
                return NotFound();

            patchDoc.ApplyTo(existingCard); 

            
            if (!TryValidateModel(existingCard)) 
                return BadRequest(ModelState);

            _repository.Update(existingCard);
            return NoContent();
        }




        private string MaskCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length < 4)
                return "****";

            return $"**** **** **** {cardNumber[^4..]}";
        }


    }
}


