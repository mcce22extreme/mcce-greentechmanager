namespace GreenTechManager.WindParks.Models
{
    public class WindTurbineModel
    {
        public string Type { get; set; }

        public string Location { get; set; }

        public int PowerOutput { get; set; }

        public int RotorDiameter { get; set; }

        public int HubHeight { get; set; }

        public int WindParkId { get; set; }        
    }
}
