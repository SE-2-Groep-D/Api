namespace Api.Models.Domain
{
    public class TypeBeperking
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Ervaringsdeskundige> Ervaringsdeskundigen { get; } = new();
    }
}
