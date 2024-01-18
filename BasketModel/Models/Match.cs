namespace MAP_Ionescu_Serban_Andrei.Models
{
    public class Match
    {
        public int MatchID { get; set; }
        public string oppositeTeam { get; set; }
        public int minutesPlayed { get; set; }
        public int markedPoints { get; set; }

        public ICollection<Position> Positions { get; set; }
    }
}
