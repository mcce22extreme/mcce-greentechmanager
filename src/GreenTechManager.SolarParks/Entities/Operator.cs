using System.ComponentModel.DataAnnotations;
using GreenTechManager.SolarParks.Constants;

namespace GreenTechManager.SolarParks.Entities
{
    public class Operator
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public int ExternalId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Name { get; set; }

        public IList<SolarPark> SolarParks { get; }
    }
}
