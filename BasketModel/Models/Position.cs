namespace MAP_Ionescu_Serban_Andrei.Models
{
    public class Position
    {
        public int MatchID { get; set; }
        public int BallerID { get; set; }
        public Match Match { get; set; }
        public Baller Baller { get; set; }
    }
}
