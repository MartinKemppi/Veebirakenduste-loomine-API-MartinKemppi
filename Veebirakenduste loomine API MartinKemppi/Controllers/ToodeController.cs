using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veebirakenduste_loomine_API_MartinKemppi.Models;

namespace Veebirakenduste_loomine_API_MartinKemppi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToodeController : ControllerBase
    {
        private static Toode _toode = new Toode(1, "Koola", 1.5, true);

        // GET: toode
        [HttpGet]
        public Toode GetToode()
        {
            return _toode;
        }

        // GET: toode/suurenda-hinda
        [HttpGet("suurenda-hinda")]
        public Toode SuurendaHinda()
        {
            _toode.Price = _toode.Price + 1;
            return _toode;
        }

        // GET: toode/false-true-false
        [HttpGet("false-true-false")]
        public Toode FalseTrueFalse()
        {
            if(_toode.IsActive == true)
            {
                _toode.IsActive = _toode.IsActive = false;
            }
            else
            {
                _toode.IsActive = _toode.IsActive = true;
            }               
            return _toode;
        }

        // GET: toode/ümbernimetamine
        [HttpGet("ümbernimetamine")]
        public Toode FalseTrueFalse(string Name)
        {
            _toode.Name = Name;

            return _toode;
        }

        // GET: toode/uushind
        [HttpGet("uushind")]
        public Toode Uushind(float Xkorda)
        {
            _toode.Price *= Xkorda;

            return _toode;
        }
    }
}
