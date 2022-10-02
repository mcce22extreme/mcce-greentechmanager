using System.ComponentModel.DataAnnotations;

namespace GreenTechManager.SolarParks.Models
{
    public class SolarArrayModel
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public int NumberOfPanels { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int PowerOutput { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        public int SolarParkId { get; set; }
    }
}
