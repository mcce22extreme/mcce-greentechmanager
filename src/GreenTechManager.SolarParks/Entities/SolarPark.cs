using System.ComponentModel.DataAnnotations;
using GreenTechManager.SolarParks.Constants;

namespace GreenTechManager.SolarParks.Entities
{
    public class SolarPark
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Name { get; set; }

        [Required]
        public int OperatorId { get; set; }

        public DateTime? StartOfOperation { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Location { get; set; }

        public Operator Operator { get; }

        public IList<SolarArray> SolarArrays { get; }
    }
}
