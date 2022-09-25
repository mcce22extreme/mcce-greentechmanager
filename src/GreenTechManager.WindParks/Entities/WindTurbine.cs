using System.ComponentModel.DataAnnotations;

namespace GreenTechManager.WindParks.Entities
{
    public class WindTurbine
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
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
