namespace MAP_Ionescu_Serban_Andrei.Models
{
    public class Baller
    {
        public int BallerID { get; set; }
        public string BallerName { get; set; }

        public int? TeamID { get; set; }
        public Team? Team { get; set; }
        public int TShirtNumber { get; set; }
        public ICollection<GamePlan>? GamePlanes { get; set; }
        public ICollection<Position>? Positions { get; set; }


    }
}
