namespace asociatie_proprietari.Models
{
    public class ApartamentPropietar
    {
        public int id {  get; set; }

        public int ApartamentId { get; set; }
        public Apartament Apartament { get; set; }

        public string PropietarId { get; set; }
        public Propietar Propietar { get; set; }
    }
}
