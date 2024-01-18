namespace MAP_Ionescu_Serban_Andrei.Models.LibraryViewModels
{
    public class MatchIndexData
    {
        public IEnumerable<Match> Matches { get; set; }
        public IEnumerable<Baller> Ballers { get; set; }
        public IEnumerable<GamePlan> GamePlans { get; set; }
    }
}
