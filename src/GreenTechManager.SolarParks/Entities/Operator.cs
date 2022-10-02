using System.ComponentModel.DataAnnotations;
using GreenTechManager.Core.Constants;
using GreenTechManager.Core.Entities;

namespace GreenTechManager.SolarParks.Entities
{
    public class Operator : EntityBase
    {
        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public int ExternalId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Name { get; set; }

        public IList<SolarPark> SolarParks { get; }
    }
}
