namespace Api.Models.Domain
{
    public class Ervaringsdeskundige: Gebruiker
    {
        public string Postcode { get; set; }
        public bool ToestemmingBenadering { get; set; }
        public string Leeftijdscategorie { get; set; }
        
    }
}
