using System.ComponentModel.DataAnnotations;
using GreenTechManager.WindParks.Constants;

namespace GreenTechManager.WindParks.Entities
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

        public IList<WindPark> WindParks { get; }
    }
}
