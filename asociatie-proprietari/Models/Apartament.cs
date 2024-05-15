namespace asociatie_proprietari.Models
{
    public class Apartament
    {
        public int ApartamentId { get; set; }
        public int? NumarApartament {  get; set; }
        public int? Scara { get; set; }
        public int? NumarCamere { get; set; }
        public int? NumarPersoane { get; set; }

        public ICollection<ApartamentPropietar> ApartamentPropietars { get; set; }
        public ICollection<Factura> Facturas { get; set; } 
    }
}
