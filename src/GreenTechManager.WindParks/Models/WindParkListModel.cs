namespace GreenTechManager.WindParks.Models
{
    public class WindParkListModel : WindParkModel
    {
        public int Id { get; set; }

        public int NumberOfTurbines { get; set; }

        public int MaxPowerOuput { get; set; }
    }
}
