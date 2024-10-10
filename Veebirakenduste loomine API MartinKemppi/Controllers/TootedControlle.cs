using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veebirakenduste_loomine_API_MartinKemppi.Models;

namespace Veebirakenduste_loomine_API_MartinKemppi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TootedController : ControllerBase
    {
        private static List<Toode> _tooted = new()
        {
            new Toode(1,"Koola", 1.5, true),
            new Toode(2,"Fanta", 1.0, false),
            new Toode(3,"Sprite", 1.7, true),
            new Toode(4,"Vichy", 2.0, true),
            new Toode(5,"Vitamin well", 2.5, true)
        };

        // GET https://localhost:4444/tooted
        [HttpGet]
        public List<Toode> Get()
        {
            return _tooted;
        }

        // DELETE https://localhost:4444/tooted/kustuta/0
        [HttpDelete("kustuta/{index}")]
        public List<Toode> Delete(int index)
        {
            _tooted.RemoveAt(index);
            return _tooted;
        }

        [HttpDelete("kustuta2/{index}")]
        public string Delete2(int index)
        {
            _tooted.RemoveAt(index);
            return "Kustutatud!";
        }

        // POST https://localhost:4444/tooted/lisa/1/Coca/1.5/true
        [HttpPost("lisa/{id}/{nimi}/{hind}/{aktiivne}")]
        public List<Toode> Add(int id, string nimi, double hind, bool aktiivne)
        {
            Toode toode = new Toode(id, nimi, hind, aktiivne);
            _tooted.Add(toode);
            return _tooted;
        }

        [HttpPost("lisa2")]
        public List<Toode> Add2(int id, string nimi, double hind, bool aktiivne)
        {
            Toode toode = new Toode(id, nimi, hind, aktiivne);
            _tooted.Add(toode);
            return _tooted;
        }

        // PATCH https://localhost:4444/tooted/hind-dollaritesse/1.5
        [HttpPatch("hind-dollaritesse/{kurss}")]
        public List<Toode> UpdatePrices(double kurss)
        {
            for (int i = 0; i < _tooted.Count; i++)
            {
                _tooted[i].Price = _tooted[i].Price * kurss;
            }
            return _tooted;
        }

        // või foreachina:

        [HttpGet("hind-dollaritesse2/{kurss}")] // GET /tooted/hind-dollaritesse2/1.5
        public List<Toode> Dollaritesse2(double kurss)
        {
            foreach (var t in _tooted)
            {
                t.Price = t.Price * kurss;
            }

            return _tooted;
        }

        [HttpPost("lisa")]
        public List<Toode> Add([FromBody] Toode toode)
        {
            _tooted.Add(toode);
            return _tooted;
        }


        [HttpGet("kustuta-tooded")] // GET /tooted/kustuta-tooded
        public List<Toode> Kustutakoik()
        {
            _tooted.Clear();
            return _tooted;
        }

        [HttpGet("aktiivsus-väär")] // GET /tooted/aktiivsus-väär
        public List<Toode> Aktiivsus_vaar()
        {
            foreach (var t in _tooted)
            {
                t.IsActive = false;
            }
            return _tooted;
        }

        [HttpGet("näita/{id}")] // GET /tooted/näita/id
        public ActionResult<Toode> Naita(int id)
        {
            var toode = _tooted.Find(t => t.Id == id);
            return toode != null ? toode : NotFound();
        }

        [HttpGet("max-hind")] // GET /tooted/max-hind
        public ActionResult<Toode> Maxhind()
        {
            var toode = _tooted.OrderByDescending(t => t.Price).First();
            return toode != null ? toode : NotFound();
        }

        // PUT https://localhost:port/tooted/uuenda/{id}
        [HttpPut("uuenda/{id}")]
        public ActionResult<Toode> Update(int id, [FromBody] Toode updatedToode)
        {
            var Toode = _tooted.Find(t => t.Id == id);
            if (Toode == null)
            {
                return NotFound();
            }

            Toode.Name = updatedToode.Name;
            Toode.Price = updatedToode.Price;
            Toode.IsActive = updatedToode.IsActive;

            return Toode;
        }

    }
}
