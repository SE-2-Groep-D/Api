﻿using Microsoft.AspNetCore.Identity;

namespace Api.Models.Domain
{
    public class Gebruiker: IdentityUser
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public bool GoogleAccount { get; set; } = false;
    }
}
