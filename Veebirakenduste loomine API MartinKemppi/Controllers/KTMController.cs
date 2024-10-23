using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veebirakenduste_loomine_API_MartinKemppi.Data;
using Veebirakenduste_loomine_API_MartinKemppi.Models;

namespace Veebirakenduste_loomine_API_MartinKemppi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KTMController : ControllerBase
    {
        private readonly DBContext _context;

        public KTMController(DBContext context)
        {
            _context = context;
        }

        // Toode
        [HttpGet("toode")]
        public List<Toode> GetToode()
        {
            return _context.Tooded.ToList();
        }

        [HttpPost("toode")]
        public List<Toode> PostToode([FromBody] Toode toode)
        {
            _context.Tooded.Add(toode);
            _context.SaveChanges();
            return _context.Tooded.ToList();
        }

        [HttpDelete("toode/{id}")]
        public List<Toode> DeleteToode(int id)
        {
            var toode = _context.Tooded.Find(id);
            if (toode != null)
            {
                _context.Tooded.Remove(toode);
                _context.SaveChanges();
            }
            return _context.Tooded.ToList();
        }

        [HttpGet("toode/{id}")]
        public ActionResult<Toode> GetToodeID(int id)
        {
            var toode = _context.Tooded.Find(id);
            if (toode == null)
            {
                return NotFound();
            }
            return toode;
        }

        [HttpPut("toode/{id}")]
        public ActionResult<List<Toode>> PutToode(int id, [FromBody] Toode updatedToode)
        {
            var toode = _context.Tooded.Find(id);
            if (toode == null)
            {
                return NotFound();
            }

            toode.Name = updatedToode.Name;
            toode.Price = updatedToode.Price;
            toode.IsActive = updatedToode.IsActive;

            _context.Tooded.Update(toode);
            _context.SaveChanges();

            return Ok(_context.Tooded);
        }

        // Kasutaja
        [HttpGet("kasutaja")]
        public List<Kasutaja> GetKasutaja()
        {
            return _context.Kasutajad.ToList();
        }

        [HttpPost("kasutaja")]
        public List<Kasutaja> PostKasutaja([FromBody] Kasutaja kasutaja)
        {
            _context.Kasutajad.Add(kasutaja);
            _context.SaveChanges();
            return _context.Kasutajad.ToList();
        }

        [HttpDelete("kasutaja/{id}")]
        public List<Kasutaja> DeleteKasutaja(int id)
        {
            var kasutaja = _context.Kasutajad.Find(id);
            if (kasutaja != null)
            {
                _context.Kasutajad.Remove(kasutaja);
                _context.SaveChanges();
            }
            return _context.Kasutajad.ToList();
        }

        [HttpGet("kasutaja/{id}")]
        public ActionResult<Kasutaja> GetKasutajaID(int id)
        {
            var kasutaja = _context.Kasutajad.Find(id);
            if (kasutaja == null)
            {
                return NotFound();
            }
            return kasutaja;
        }

        [HttpPut("kasutaja/{id}")]
        public ActionResult<List<Kasutaja>> PutKasutaja(int id, [FromBody] Kasutaja updatedKasutaja)
        {
            var kasutaja = _context.Kasutajad.Find(id);
            if (kasutaja == null)
            {
                return NotFound();
            }

            kasutaja.Username = updatedKasutaja.Username;
            kasutaja.Password = updatedKasutaja.Password;
            kasutaja.Firstname = updatedKasutaja.Firstname;
            kasutaja.Lastname = updatedKasutaja.Lastname;

            _context.Kasutajad.Update(kasutaja);
            _context.SaveChanges();

            return Ok(_context.Kasutajad);
        }

        // Tellimus
        [HttpGet("tellimus")]
        public List<Tellimus> GetTellimus()
        {
            return _context.Tellimused
            .Select(t => new Tellimus(
                t.Id,
                t.Kasutaja,
                t.Timestamp,
                t.TooteNimed,
                t.Kogused,
                (float)t.Hind))
            .ToList();
        }

        [HttpPost("tellimus")]
        public IActionResult PostTellimus([FromBody] Tellimus tellimus)
        {
            if (tellimus == null)
            {
                return BadRequest("Invalid order data.");
            }

            Console.WriteLine($"Kasutaja: {tellimus.Kasutaja}, Timestamp: {tellimus.Timestamp}, Hind: {tellimus.Hind}");

            _context.Tellimused.Add(tellimus);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok(_context.Tellimused.ToList());
        }


        [HttpDelete("tellimus/{id}")]
        public List<Tellimus> DeleteTellimus(int id)
        {
            var tellimus = _context.Tellimused.Find(id);
            if (tellimus != null)
            {
                _context.Tellimused.Remove(tellimus);
                _context.SaveChanges();
            }
            return _context.Tellimused.ToList();
        }

        [HttpGet("tellimus/{id}")]
        public ActionResult<Tellimus> GetTellimusID(int id)
        {
            var tellimus = _context.Tellimused.Find(id);
            if (tellimus == null)
            {
                return NotFound();
            }
            return tellimus;
        }

        [HttpPut("tellimus/{id}")]
        public ActionResult<List<Tellimus>> PutTellimus(int id, [FromBody] Tellimus updatedTellimus)
        {
            var tellimus = _context.Tellimused.Find(id);
            if (tellimus == null)
            {
                return NotFound();
            }

            tellimus.Kasutaja = updatedTellimus.Kasutaja;
            tellimus.TooteNimed = updatedTellimus.TooteNimed;
            tellimus.Kogused = updatedTellimus.Kogused;
            tellimus.Hind = updatedTellimus.Hind;

            _context.Tellimused.Update(tellimus);
            _context.SaveChanges();

            return Ok(_context.Tellimused);
        }
    }
}