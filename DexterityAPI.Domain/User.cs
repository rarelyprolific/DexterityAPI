namespace DexterityAPI.Domain
{
    public class User
    {
        public int Id { get; set; }

        public string Forename { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }

        public bool IsEnabled { get; set; }
    }
}
