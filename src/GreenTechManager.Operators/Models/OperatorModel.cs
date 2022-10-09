using System.ComponentModel.DataAnnotations;
using GreenTechManager.Core.Models;
using Newtonsoft.Json;

namespace GreenTechManager.WindParks.Models
{
    public class OperatorModel : AuditableModelBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int Zip { get; set; }

        public string Country { get; set; }
    }

    public class SaveOperatorModel
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
