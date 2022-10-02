using System.ComponentModel.DataAnnotations;
using GreenTechManager.Core.Constants;
using GreenTechManager.Core.Entities;

namespace GreenTechManager.WindParks.Entities
{
    public class Operator : AuditableEntityBase
    {
        public override string EntityName
        {
            get { return Name; }
        }

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
