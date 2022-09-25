using System.ComponentModel.DataAnnotations;
using GreenTechManager.Operators.Constants;

namespace GreenTechManager.WindParks.Entities
{
    public class Operator
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Address { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string City { get; set; }

        [Required]        
        public int Zip { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Country { get; set; }
    }
}
