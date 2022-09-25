using System.ComponentModel.DataAnnotations;
using GreenTechManager.SolarParks.Constants;

namespace GreenTechManager.SolarParks.Entities
{
    public class SolarArray
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Type { get; set; }

        [Required]        
        public int NumberOfPanels { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public string Location { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public int PowerOutput { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public int Size { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DefaultMaxLength)]
        public int SolarParkId { get; set; }

        public SolarPark SolarPark { get; }
    }
}
