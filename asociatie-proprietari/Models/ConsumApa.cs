using System.ComponentModel.DataAnnotations;

namespace asociatie_proprietari.Models
{
    public class ConsumApa
    {
        public int Id { get; set; }
        public int? ConsumApaRece{ get; set; }
        public int? ConsumApaCalda { get; set; }

        [Range(1, 12)]
        public int? Luna { get; set; }

        public int? An { get; set; }

        public int ApartamentId { get; set; }
        public Apartament? Apartament { get; set; }
    }
}
