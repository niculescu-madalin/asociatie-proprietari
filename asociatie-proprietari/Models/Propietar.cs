using Microsoft.AspNetCore.Identity;

namespace asociatie_proprietari.Models
{
    public class Propietar : IdentityUser
    {
        [PersonalData]
        public string? Nume { get; set; }
        [PersonalData]
        public string? Prenume { get; set; }
        [PersonalData]
        public byte? ImagineProfil { get; set; }

        public ICollection<ApartamentPropietar> ApartamentPropietars { get; set; }

    }
}
