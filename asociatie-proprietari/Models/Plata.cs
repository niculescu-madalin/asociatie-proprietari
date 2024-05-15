namespace asociatie_proprietari.Models
{
    public class Plata
    {
        public int Id { get; set; }
        public string? NumarCard { get; set; }
        public string? NumeCard { get; set; }
        public string? CardCVV { get; set; }

        public int? SumaPlatita { get; set; }
        public string? Status { get; set; }
        public DateTime? Data {  get; set; }

        public int? FacturaId { get; set; }
        public Factura? Factura { get; set; }
    }
}
