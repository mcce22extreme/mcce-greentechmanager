using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace GreenTechManager.WindParks.Models
{
    public class OperatorModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int Zip { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
