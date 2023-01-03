using System.ComponentModel.DataAnnotations;
using GreenTechManager.Core.Models;
using Newtonsoft.Json;

namespace GreenTechManager.WindParks.Models
{
    public class OperatorModel : AuditableModelBase
    {
        /// <summary>
        /// The identifier of the operator.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the operator.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The address of the operator.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The city of the operator.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The zip code of the operator.
        /// </summary>
        public int Zip { get; set; }

        /// <summary>
        /// The country of the operator.
        /// </summary>
        public string Country { get; set; }
    }

    public class SaveOperatorModel
    {
        /// <summary>
        /// The name of the operator.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The address of the operator.
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// The city of the operator.
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// The zip code of the operator.
        /// </summary>
        [Required]
        public int Zip { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
