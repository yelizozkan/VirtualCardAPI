using Microsoft.AspNetCore.Mvc;
using VirtualCardAPI.Models;
using VirtualCardAPI.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using VirtualCardAPI.Extensions;
using VirtualCardAPI.Validators;



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
            var cards = _repository.GetAll();
            return Ok(cards);
        }

        [HttpPost]
        public IActionResult CreateCard([FromBody] VirtualCard card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorDetails
                {
                    StatusCode = 400,
                    Message = "Invalid data sent."
                });
            }

            _repository.Add(card);
            return CreatedAtAction(nameof(GetCardById), new { id = card.Id }, card);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCard(int id, [FromBody] VirtualCard card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorDetails
                {
                    StatusCode = 400,
                    Message = "Invalid data sent."
                });
            }

            var existingCard = _repository.GetById(id);
            if (existingCard == null)
                return NotFound();

            existingCard.CardHolder = card.CardHolder;
            existingCard.ExpirationDate = card.ExpirationDate;
            existingCard.Balance = card.Balance;

            _repository.Update(existingCard);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCard(int id)
        {
            var card = _repository.GetById(id);
            if (card == null)
                return NotFound();

            _repository.Delete(card);
            return NoContent();
        }



        [HttpGet("{id}")]
        public IActionResult GetCardById(int id)
        {
            var card = _repository.GetById(id);
            if (card == null)
            {
                return NotFound(new ErrorDetails { StatusCode = 404, Message = "Card not found." });
            }

            return Ok(card);
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

            return Ok(cards);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePartialCard(int id, [FromBody] JsonPatchDocument<VirtualCard> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch document is null.");
            }

            var existingCard = _repository.GetById(id);
            if (existingCard == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(existingCard, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!TryValidateModel(existingCard))
            {
                return BadRequest(ModelState);
            }

            _repository.Update(existingCard);
            return NoContent();
        }

    }
}


