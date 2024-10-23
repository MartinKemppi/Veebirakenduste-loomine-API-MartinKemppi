using System.Xml.Linq;

namespace Veebirakenduste_loomine_API_MartinKemppi.Models
{
    public class Tellimus
    {
        public int Id { get; set; }
        public string Kasutaja { get; set; }
        public string Timestamp { get; set; } 
        public List<string> TooteNimed { get; set; } 
        public List<int> Kogused { get; set; }
        public float Hind { get; set; }

        public Tellimus(int id, string kasutaja, string timestamp, List<string> tooteNimed, List<int> kogused, float hind)
        {
            Id = id;
            Kasutaja = kasutaja;
            Timestamp = timestamp;
            TooteNimed = tooteNimed;
            Kogused = kogused;
            Hind = hind;
        }
    }   
}
