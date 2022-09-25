using System.ComponentModel.DataAnnotations;
using GreenTechManager.SolarParks.Entities;

namespace GreenTechManager.SolarParks.Entities
{
    public class SolarArray
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int NumberOfPanels { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int PowerRating { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        public int SolarParkId { get; set; }

        public SolarPark SolarPark { get; }
    }
}
