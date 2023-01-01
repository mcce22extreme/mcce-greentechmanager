using System.ComponentModel.DataAnnotations;

namespace GreenTechManager.WindParks.Models
{
    public class WindParkModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int OperatorId { get; set; }

        public int NumberOfTurbines { get; set; }

        public int MaxPowerOutput { get; set; }

        public DateTime? StartOfOperation { get; set; }

        [Required]
        public string Location { get; set; }        
    }
}
