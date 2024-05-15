namespace asociatie_proprietari.Models
{
    public class Angajat
    {
        public int AngajatId { get; set; }
        public string? Nume { get; set; }
        public string? Prenume { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
        public string? Functie { get; set; }

        public int Salariu { get; set; }
        public int Bonus { get; set; }
    }
}
