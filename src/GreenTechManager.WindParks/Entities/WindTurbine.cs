using System.ComponentModel.DataAnnotations;
using GreenTechManager.WindParks.Constants;

namespace GreenTechManager.WindParks.Entities
{
    public class WindTurbine
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Type { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Location { get; set; }

        [Required]
        public int PowerOutput { get; set; }

        [Required]
        public int RotorDiameter { get; set; }

        [Required]
        public int HubHeight { get; set; }

        [Required]
        public int WindParkId { get; set; }

        public WindPark WindPark { get; }        
    }
}
