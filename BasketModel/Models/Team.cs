namespace MAP_Ionescu_Serban_Andrei.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string TeamCity { get; set; }
        public string Nickname { get; set; }

        public string FullName
        {
            get
            {
                return $"{TeamName} {Nickname}";
            }
        }

        public ICollection<Baller>? Ballers { get; set; }
    }
}
