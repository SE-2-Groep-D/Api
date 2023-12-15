namespace Api.Models.Domain
{
    public class Hulpmiddel
    {
        public Guid Id { get; set; }
        public string Naam { get; set; }
        public List<Ervaringsdeskundige> Ervaringsdeskundigen { get; } = new();
    }
}
