using System.ComponentModel.DataAnnotations;

namespace GreenTechManager.WindParks.Entities
{
    public class Operator
    {
        public int Id { get; set; }

        [Required]
        public int ExternalId { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<WindPark> WindParks { get; }
    }
}
