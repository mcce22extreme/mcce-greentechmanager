namespace GreenTechManager.Core.Models
{
    public class AuditableModelBase
    {
        /// <summary>
        /// The create date of the entity.
        /// </summary>
        public DateTime? CreatedUtc { get; set; }

        /// <summary>
        /// The creator of the entity.
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// The modified date of the entity.
        /// </summary>
        public DateTime? ModifiedUtc { get; set; }

        /// <summary>
        /// The modifier of the entity.
        /// </summary>
        public string Modifier { get; set; }
    }
}
