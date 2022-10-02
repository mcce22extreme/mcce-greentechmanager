using System.ComponentModel.DataAnnotations;
using GreenTechManager.Core.Constants;
using GreenTechManager.Core.Entities;

namespace GreenTechManager.WindParks.Entities
{
    public class WindPark : AuditableEntityBase
    {
        public override string EntityName
        {
            get { return Name; }
        }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Name { get; set; }

        [Required]
        public int OperatorId { get; set; }

        [Required]
        public int MaxPowerOutput { get; set; }

        [Required]
        public int NumberOfTurbines { get; set; }

        public DateTime? StartOfOperation { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Location { get; set; }

        public Operator Operator { get; }
    }
}
