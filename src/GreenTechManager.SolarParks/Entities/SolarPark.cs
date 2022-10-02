using System.ComponentModel.DataAnnotations;
using GreenTechManager.Core.Constants;
using GreenTechManager.Core.Entities;

namespace GreenTechManager.SolarParks.Entities
{
    public class SolarPark : AuditableEntityBase
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
        public int NumberOfPanels { get; set; }

        public DateTime? StartOfOperation { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Location { get; set; }

        public Operator Operator { get; }

    }
}
