using System.ComponentModel.DataAnnotations;

namespace GreenTechManager.SolarParks.Entities
{
    public class SolarPark
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int OperatorId { get; set; }

        public DateTime? StartOfOperation { get; set; }

        [Required]
        public string Location { get; set; }

        public Operator Operator { get; }

        public IList<SolarArray> SolarArrays { get; }
    }
}
