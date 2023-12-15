﻿namespace Api.Models.Domain
{
    public class Ervaringsdeskundige: Gebruiker
    {
        public string Postcode { get; set; }
        public bool ToestemmingBenadering { get; set; }
        public string Leeftijdscategorie { get; set; }

        public List<Voorkeurbenadering> Voorkeurbenaderingen { get; } = new();
        public List<Hulpmiddel> Hulpmiddelen { get; } = new();
        public List<TypeBeperking> TypeBeperkingen { get; } = new();

        public Guid? VoogdId { get; set; }
        public Voogd? Voogd { get; set; }


    }
}
