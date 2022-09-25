namespace GreenTechManager.WindParks.Models
{
    public class WindParkModel
    {
        public string Name { get; set; }

        public int OperatorId { get; set; }

        public DateTime? StartOfOperation { get; set; }

        public string Location { get; set; }        
    }
}
