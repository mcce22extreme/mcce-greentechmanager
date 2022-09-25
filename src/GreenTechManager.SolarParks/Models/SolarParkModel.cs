using System.ComponentModel.DataAnnotations;

namespace GreenTechManager.SolarParks.Models
{
    public class SolarParkModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int OperatorId { get; set; }

        public DateTime? StartOfOperation { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
