namespace asociatie_proprietari.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime? DataInregistrare { get; set; }
        public DateTime? DataScadenta { get; set; }
        public int? SumaDePlata { get; set; }
        public int? SumaPlatita { get; set; }
        public string? status { get; set; }

        public int? ContractId { get; set; }
        public Contract? Contract { get; set; }

        public int? ApartamentId { get; set; }
        public Apartament? Apartament { get; set; }

    }
}
