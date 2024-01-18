using MAP_Ionescu_Serban_Andrei.Models;
using Microsoft.EntityFrameworkCore;

namespace MAP_Ionescu_Serban_Andrei.Data
{
    public static class DbInitializer
    {
        public static void Initialize (IServiceProvider serviceProvider)
        {
            using (var context = new BasketballContext(serviceProvider.GetRequiredService<DbContextOptions<BasketballContext>>()))
            {
                if (context.Ballers.Any())
                {
                    return;
                }

                var personalStats = new PersonalStats[]
                {
                    new PersonalStats {Description="Antrenor foarte cunoscut",overallScore=78.3F},
                    new PersonalStats {Description="Antrenor cu multa experienta",overallScore=97.9F},
                    new PersonalStats {Description="Antrenor care a cucerit Euroliga",overallScore=99.7F},
                    new PersonalStats {Description="Antrenor incepator",overallScore=32.5F},
                    new PersonalStats {Description="Antrenor foarte impulsiv",overallScore=7.12F},
                };
                foreach (PersonalStats personalStats1 in personalStats)
                {
                    context.PersonalStats.Add(personalStats1);
                }

                context.SaveChanges();

                var teams = new Team[]
                {
                    new Team {TeamName="LAL",TeamCity="Los Angeles",Nickname="Lakers"},
                    new Team {TeamName="BKN",TeamCity="Brooklyn",Nickname="Nets"},
                    new Team {TeamName="SAS",TeamCity="San Antonio",Nickname="Spurs"},
                    new Team {TeamName="ATL",TeamCity="Atlanta",Nickname="Hawks"},
                    new Team {TeamName="OKC",TeamCity="Oklahoma",Nickname="Thunder"}
                };

                foreach (Team team in teams)
                {
                    context.Teams.Add(team);    
                }

                context.SaveChanges();

                var ballers = new Baller[]
                {
                    new Baller {BallerName="Lebron James",TShirtNumber=23,TeamID= teams[0].TeamID},
                    new Baller {BallerName="Mikal Bridges",TShirtNumber=0,TeamID= teams[1].TeamID},
                    new Baller {BallerName="Victor Wembanyama",TShirtNumber=1,TeamID= teams[2].TeamID},
                    new Baller {BallerName="Trae Young",TShirtNumber=11,TeamID= teams[3].TeamID},
                    new Baller {BallerName="Shai Gilgeous Alexander",TShirtNumber=2,TeamID= teams[4].TeamID},
                    new Baller {BallerName="Anthony Davis",TShirtNumber=3,TeamID= teams[0].TeamID},
                    new Baller {BallerName="Cam Thomas",TShirtNumber=24,TeamID= teams[1].TeamID},
                    new Baller {BallerName="Jeremy Sochan",TShirtNumber=10,TeamID= teams[2].TeamID},
                    new Baller {BallerName="Dejounte Murray",TShirtNumber=5,TeamID= teams[3].TeamID},
                    new Baller {BallerName="Josh Giddey",TShirtNumber=3,TeamID= teams[4].TeamID},
                };

                foreach (Baller baller in ballers)
                {
                    context.Ballers.Add(baller);
                }
                context.SaveChanges();

                var coaches = new Coach[]
                {
                    new Coach {CoachName="Ham",CoachCountry="USA",debutYear=2005},
                    new Coach {CoachName="Vaughn",CoachCountry="USA",debutYear=2008},
                    new Coach {CoachName="Gregg Popovich",CoachCountry="USA",debutYear=1994},
                    new Coach {CoachName="Snyder",CoachCountry="USA",debutYear=2002},
                    new Coach {CoachName="Daigneault",CoachCountry="USA",debutYear=2010},
                };

                foreach (Coach coach in coaches)
                {
                    context.Coaches.Add(coach);
                }

                context.SaveChanges();

                var gameplans = new GamePlan[]
                {
                    new GamePlan{BallerID=7,CoachID=2,PlanNumber=1,DescriptionStrategy="Pick and Go",usedQuarter=1},
                    new GamePlan{BallerID=5,CoachID=1,PlanNumber=2,DescriptionStrategy="Catch and Shoot",usedQuarter=2},
                    new GamePlan{BallerID=8,CoachID=4,PlanNumber=3,DescriptionStrategy="Horse",usedQuarter=3},
                    new GamePlan{BallerID=3,CoachID=3,PlanNumber=4,DescriptionStrategy="Iso",usedQuarter=4},
                    new GamePlan{BallerID=10,CoachID=5,PlanNumber=5,DescriptionStrategy="Pick and Roll",usedQuarter=1},
                    new GamePlan{BallerID=1,CoachID=1,PlanNumber=1,DescriptionStrategy="Zone Defense",usedQuarter=2},
                    new GamePlan{BallerID=9,CoachID=2,PlanNumber=2,DescriptionStrategy="Triangle Offense",usedQuarter=3},
                    new GamePlan{BallerID=4,CoachID=5,PlanNumber=3,DescriptionStrategy="Post Offense",usedQuarter=4},
                    new GamePlan{BallerID=6,CoachID=3,PlanNumber=4,DescriptionStrategy="Box and One Offense",usedQuarter=1},
                    new GamePlan{BallerID=2,CoachID=4,PlanNumber=5,DescriptionStrategy="Run and Gun Offense",usedQuarter=2},
                };

                foreach(GamePlan gameplan in gameplans)
                {
                    context.GamePlans.Add(gameplan);
                }

                context.SaveChanges();

                var matches = new Match[]
                {
                    new Match{oppositeTeam="Los Angeles Lakers",minutesPlayed=30,markedPoints=22 },
                    new Match{oppositeTeam="Milwaukee Bucks",minutesPlayed=27,markedPoints=14 },
                    new Match{oppositeTeam="Golden State Warriors",minutesPlayed=35,markedPoints=34 },
                    new Match{oppositeTeam="Brooklyn Nets",minutesPlayed=8,markedPoints=2 },
                    new Match{oppositeTeam="Denver Nuggets",minutesPlayed=16,markedPoints=10 },
                    new Match{oppositeTeam="Philadelphia 76ers",minutesPlayed=24,markedPoints=24 },
                    new Match{oppositeTeam="Utah Jazz",minutesPlayed=42,markedPoints=60 },
                    new Match{oppositeTeam="Boston Celtics",minutesPlayed=20,markedPoints=4 },
                    new Match{oppositeTeam="Phoenix Suns",minutesPlayed=38,markedPoints=30 },
                    new Match{oppositeTeam="Toronto Raptors",minutesPlayed=11,markedPoints=6 },
                };

                foreach (Match match in matches)
                {
                    context.Matches.Add(match);
                }

                context.SaveChanges();

                var positions = new Position[]
                {
                    new Position{BallerID = ballers[4].BallerID,MatchID = matches[7].MatchID},
                    new Position{BallerID = ballers[1].BallerID,MatchID = matches[8].MatchID},
                    new Position{BallerID = ballers[3].BallerID,MatchID = matches[6].MatchID},
                    new Position{BallerID = ballers[9].BallerID,MatchID = matches[0].MatchID},
                    new Position{BallerID = ballers[2].BallerID,MatchID = matches[5].MatchID},
                    new Position{BallerID = ballers[7].BallerID,MatchID = matches[1].MatchID},
                    new Position{BallerID = ballers[0].BallerID,MatchID = matches[4].MatchID},
                    new Position{BallerID = ballers[8].BallerID,MatchID = matches[2].MatchID},
                    new Position{BallerID = ballers[6].BallerID,MatchID = matches[3].MatchID},
                    new Position{BallerID = ballers[5].BallerID,MatchID = matches[9].MatchID},
                };

                foreach (Position position in positions)
                {
                    context.Positions.Add(position);
                }

                context.SaveChanges();
            }
        }
    }
}
