using System.ComponentModel.DataAnnotations;

namespace GreenTechManager.SolarParks.Entities
{
    public class Operator
    {
        public int Id { get; set; }

        [Required]
        public int ExternalId { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<SolarPark> SolarParks { get; }
    }
}
