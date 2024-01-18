namespace MAP_Ionescu_Serban_Andrei.Models
{
    public class Coach
    {
        public int CoachID { get; set; }
        public int? PersonalStatsID { get; set; }
        public PersonalStats? PersonalStats { get; set; }
        public string CoachName { get; set; }
        public string CoachCountry { get; set; }
        public int debutYear { get; set; }
        public ICollection<GamePlan>? GamePlans { get; set; }
    }
}
