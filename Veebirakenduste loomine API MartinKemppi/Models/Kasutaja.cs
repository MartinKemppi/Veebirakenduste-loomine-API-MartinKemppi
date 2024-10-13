namespace Veebirakenduste_loomine_API_MartinKemppi.Models
{
    public class Kasutaja
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public Kasutaja(int id, string username, string password, string firstname, string lastname)
        {
            Id = id;
            Username = username;
            Password = password;
            Firstname = firstname;
            Lastname = lastname;
        }
    }
}
