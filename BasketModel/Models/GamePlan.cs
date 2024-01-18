namespace MAP_Ionescu_Serban_Andrei.Models
{
    public class GamePlan
    {
        public int GamePlanID { get; set; }
        public int BallerID { get; set; }
        public int CoachID { get; set; }
        public int PlanNumber;
        public string DescriptionStrategy;
        public int usedQuarter;

        public Baller Baller { get; set; }
        public Coach Coach { get; set; }
    }
}
