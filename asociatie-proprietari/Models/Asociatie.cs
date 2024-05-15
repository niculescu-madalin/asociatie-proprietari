namespace asociatie_proprietari.Models
{
    public class Asociatie
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Apartament>? Apartaments { get; set; }
        public ICollection<Angajat>? Angajats { get; set; }
    }
}
