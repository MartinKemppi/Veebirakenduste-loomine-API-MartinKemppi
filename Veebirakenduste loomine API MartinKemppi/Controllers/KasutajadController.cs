using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veebirakenduste_loomine_API_MartinKemppi.Models;

namespace Veebirakenduste_loomine_API_MartinKemppi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KasutajadController : ControllerBase
    {
        private static List<Kasutaja> _kasutaja = new()
        {
            new Kasutaja(1,"martinkem", "meknitram", "Martin", "Kemppi"),
            new Kasutaja(2,"maksimtse", "estmiskam", "Maksim", "Tsepeleivits"),
            new Kasutaja(3,"darjamil", "limajrad", "Darja", "Miljukova"),
            new Kasutaja(4,"lucagluh", "hulgacul", "Luca", "Gluhhov"),
            new Kasutaja(5,"oleksboha", "ohabskelo", "Oleksandr", "Bohatyrov")
        };

        private static List<Toode> _toode = new()
        {
            new Toode(1,"Koola", 1.5, true),
            new Toode(2,"Fanta", 1.0, false),
            new Toode(3,"Sprite", 1.7, true),
            new Toode(4,"Vichy", 2.0, true),
            new Toode(5,"Vitamin well", 2.5, true)
        };

        // GET https://localhost:4444/api/Kasutajad
        [HttpGet]
        public List<Kasutaja> Get()
        {
            return _kasutaja;
        }

        // DELETE https://localhost:4444/api/kasutajad/kustuta/0
        [HttpDelete("kustuta/{index}")]
        public List<Kasutaja> Delete(int index)
        {
            _kasutaja.RemoveAt(index);
            return _kasutaja;
        }

        // POST https://localhost:4444/api/kasutajad/lisa/1/kasutaja/parool1/mina/tema
        [HttpPost("lisa/{id}/{kasutajanimi}/{salasona}/{eesnimi}/{perekonnanimi}")]
        public List<Kasutaja> Add(int id, string kasutajanimi, string salasona, string eesnimi, string perekonnanimi)
        {
            Kasutaja kasutaja = new Kasutaja(id, kasutajanimi, salasona, eesnimi, perekonnanimi);
            _kasutaja.Add(kasutaja);
            return _kasutaja;
        }

        [HttpPost("lisa2")]
        public List<Kasutaja> Add2(int id, string kasutajanimi, string salasona, string eesnimi, string perekonnanimi)
        {
            Kasutaja kasutaja = new Kasutaja(id, kasutajanimi, salasona, eesnimi, perekonnanimi);
            _kasutaja.Add(kasutaja);
            return _kasutaja;
        }

        [HttpPost("lisa")]
        public List<Kasutaja> Add([FromBody] Kasutaja kasutaja)
        {
            _kasutaja.Add(kasutaja);
            return _kasutaja;
        }

        [HttpGet("kustuta-kasutajad")] // GET /kasutajad/kustuta-tooded
        public List<Kasutaja> Kustutakoik()
        {
            _kasutaja.Clear();
            return _kasutaja;
        }

        [HttpGet("näita/{id}")] // GET /kasutajad/näita/id
        public ActionResult<Kasutaja> Naita(int id)
        {
            var kasutaja = _kasutaja.Find(t => t.Id == id);
            return kasutaja != null ? kasutaja : NotFound();
        }

        // PUT https://localhost:port/kasutajad/uuenda/{id}
        [HttpPut("uuenda/{id}")]
        public ActionResult<Kasutaja> Update(int id, [FromBody] Kasutaja uuendatudkasutaja)
        {
            var Kasutaja = _kasutaja.Find(t => t.Id == id);
            if (Kasutaja == null)
            {
                return NotFound();
            }

            Kasutaja.Username = uuendatudkasutaja.Username;
            Kasutaja.Password = uuendatudkasutaja.Password;
            Kasutaja.Firstname = uuendatudkasutaja.Firstname;
            Kasutaja.Lastname = uuendatudkasutaja.Lastname;

            return Kasutaja;
        }

        // GET https://localhost:7279/api/Kasutajad/kasutaja-toode
        [HttpGet("kasutaja-toode")]
        public ActionResult<List<object>> KomboData()
        {
            var combolist = new List<object>();

            for (int i = 0; i < _kasutaja.Count; i++)
            {
                var kasutaja = _kasutaja[i];
                var toode = i < _toode.Count ? _toode[i] : null;

                combolist.Add(new
                {
                    KasutajaId = kasutaja.Id,
                    Username = kasutaja.Username,
                    Firstname = kasutaja.Firstname,
                    Lastname = kasutaja.Lastname,
                    ToodeId = toode?.Id,
                    ToodeName = toode?.Name,
                    ToodePrice = toode?.Price,
                    ToodeIsActive = toode?.IsActive
                });
            }

            return combolist;
        }

        [HttpPost("uus-kasutaja-toode")]
        public ActionResult<List<object>> LooKasutajaToode(string username, string password, string firstname, string lastname, string toodeName, double toodePrice, bool toodeIsActive)
        {
            var kasutaja = new Kasutaja(
                _kasutaja.Max(k => k.Id) + 1,
                username,
                password,
                firstname,
                lastname
            );

            _kasutaja.Add(kasutaja);

            var toode = new Toode(
                _toode.Max(t => t.Id) + 1,
                toodeName,
                toodePrice,
                toodeIsActive
            );

            _toode.Add(toode);

            var combolist = new List<object>();

            for (int i = 0; i < _kasutaja.Count; i++)
            {
                var prgKasutaja = _kasutaja[i];
                var prgToode = i < _toode.Count ? _toode[i] : null;

                combolist.Add(new
                {
                    KasutajaId = prgKasutaja.Id,
                    Username = prgKasutaja.Username,
                    Firstname = prgKasutaja.Firstname,
                    Lastname = prgKasutaja.Lastname,
                    ToodeId = prgToode?.Id,
                    ToodeName = prgToode?.Name,
                    ToodePrice = prgToode?.Price,
                    ToodeIsActive = prgToode?.IsActive
                });
            }

            return combolist;
        }

        [HttpPost("uus-kasutaja-toode-json")]
        public ActionResult<List<object>> LooKasutajaToode([FromBody] dynamic model)
        {
            var kasutaja = new Kasutaja(
            _kasutaja.Max(k => k.Id) + 1,
            model.username,
            model.password,
            model.firstname,
            model.lastname
            );

            var toode = new Toode(
                _toode.Max(t => t.Id) + 1,
                model.toodeName,
                model.toodePrice,
                model.toodeIsActive
            );

            _toode.Add(toode);

            var combolist = new List<object>();

            for (int i = 0; i < _kasutaja.Count; i++)
            {
                var prgKasutaja = _kasutaja[i];
                var prgToode = i < _toode.Count ? _toode[i] : null;

                combolist.Add(new
                {
                    KasutajaId = prgKasutaja.Id,
                    Username = prgKasutaja.Username,
                    Firstname = prgKasutaja.Firstname,
                    Lastname = prgKasutaja.Lastname,
                    ToodeId = prgToode?.Id,
                    ToodeName = prgToode?.Name,
                    ToodePrice = prgToode?.Price,
                    ToodeIsActive = prgToode?.IsActive
                });
            }

            return combolist;
        }

    }
}
