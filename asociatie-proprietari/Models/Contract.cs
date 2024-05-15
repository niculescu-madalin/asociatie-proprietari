namespace asociatie_proprietari.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string? Furnizor { get; set; }
        public DateTime? DataIncepere { get; set; }
        public DateTime? DataFinalizare { get; set; }

        public ICollection<Factura>? Factura { get; set; }
    }
}
