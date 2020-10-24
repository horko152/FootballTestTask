using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FootballTestTask.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FootballTestTask.Controllers
{
	public class HomeController : Controller
	{
		public League league1 = JsonConvert.DeserializeObject<League>(System.IO.File.ReadAllText(@".\Data\en.1.json"));
		public League league2 = JsonConvert.DeserializeObject<League>(System.IO.File.ReadAllText(@".\Data\en.2.json"));
		public League league3 = JsonConvert.DeserializeObject<League>(System.IO.File.ReadAllText(@".\Data\en.3.json"));
        List<League> leagues = new List<League>();
        List<Team> teams = new List<Team>();
        List<EffectiveDay> days = new List<EffectiveDay>();
        [HttpGet]
		public IActionResult Index()
		{
            ViewBag.Message = "Football Statictic";
            return View();
		}
        public IActionResult BestAttackingTeams()
		{
            InitializeLeagues();

            InitializeTeams("attacking");

            int[] theMostGoalsInLeague = new int[leagues.Count];
            int k = 0;

            foreach (var league in leagues)
            {
                theMostGoalsInLeague[k] = teams.Where(t => t.LeagueName == league.Name).Max(t => t.GoalsScored);
                k++;
            }

            List<Team> theBestTeams = new List<Team>();
            int j = 0;

            foreach(var league in leagues)
            {
                var bestAttackingTeamInLeague = teams.Where(x => x.LeagueName == league.Name && x.GoalsScored == theMostGoalsInLeague[j]).First();
                theBestTeams.Add(bestAttackingTeamInLeague);
                j++;

			}
            return View(theBestTeams);
        }
        public IActionResult BestDefensiveTeams()
		{
            InitializeLeagues();
            InitializeTeams("defensive");

			int[] theMinGoalsConcededInLeague = new int[leagues.Count];
			int k = 0;

			foreach (var league in leagues)
			{
				theMinGoalsConcededInLeague[k] = teams.Where(t => t.LeagueName == league.Name).Min(t => t.GoalsConceded);
				k++;
			}

            List<Team> theBestTeams = new List<Team>();
            int j = 0;

            foreach (var league in leagues)
            {
                var bestDefensiveTeamInLeague = teams.Where(x => x.LeagueName == league.Name && x.GoalsConceded == theMinGoalsConcededInLeague[j]).First();
                theBestTeams.Add(bestDefensiveTeamInLeague);
                j++;
            }
            return View(theBestTeams);
		}
        public IActionResult BestScoreDifferenceTeams()
		{
            InitializeLeagues();
            InitializeTeams("difference");
            int[] theMaxGoalsDifferenceInLeague = new int[leagues.Count];
            int k = 0;

            foreach (var league in leagues)
            {
                theMaxGoalsDifferenceInLeague[k] = teams.Where(t => t.LeagueName == league.Name).Max(t => t.GoalsDifference);
                k++;
            }

            List<Team> theBestTeams = new List<Team>();
            int j = 0;

			foreach (var league in leagues)
			{
				var bestScoreDifferenceTeamInLeague = teams.Where(x => x.LeagueName == league.Name && x.GoalsDifference == theMaxGoalsDifferenceInLeague[j]);
				
                if (bestScoreDifferenceTeamInLeague.Count() > 1)
				{
                    InitializeTeams("attacking");
                    var bestScoreDifference = bestScoreDifferenceTeamInLeague
                        .Where(x => bestScoreDifferenceTeamInLeague.First().GoalsScored > bestScoreDifferenceTeamInLeague.Last().GoalsScored)
                        .FirstOrDefault();

                    theBestTeams.Add(bestScoreDifference);
				}
				else
				{
					theBestTeams.Add(bestScoreDifferenceTeamInLeague.First());
				}
				j++;
			}

			return View(theBestTeams);
		}

        public IActionResult TheMostEffectiveDay()
		{
            InitializeLeagues();
            InitializeTeams("effectiveday");

            int theMostGoalsInLeaguesInADay = 0;

            foreach (var league in leagues)
            {
                theMostGoalsInLeaguesInADay = days.Max(t => t.Goals);
            }

            EffectiveDay theMostEffectiveDay = new EffectiveDay();
            theMostEffectiveDay.Date = days.Where(x => x.Goals == theMostGoalsInLeaguesInADay).First().Date;
            theMostEffectiveDay.Goals = theMostGoalsInLeaguesInADay;

            return View(theMostEffectiveDay);
		}

        private void InitializeLeagues()
		{
            leagues.Add(league1);
            leagues.Add(league2);
            leagues.Add(league3);
        }
        private void InitializeTeams(string whichTeams)
        {
            var stat = new Dictionary<string, int>();

            foreach (var league in leagues)
            {
                foreach (var match in league.Matches)
                {
                    if (whichTeams == "attacking")
                    {
                        if (stat.ContainsKey(match.Team1))
                        {
                            stat[match.Team1] += match.Score.Ft[0];
                        }
                        else if (stat.ContainsKey(match.Team2))
                        {
                            stat[match.Team2] += match.Score.Ft[1];
                        }
                        else
                        {
                            stat.Add(match.Team1, match.Score.Ft[0]);
                            stat.Add(match.Team2, match.Score.Ft[1]);
                        }
                    }
                    else if(whichTeams == "defensive")
					{
                        if (stat.ContainsKey(match.Team1))
                        {
                            stat[match.Team1] += match.Score.Ft[1];
                        }
                        else if (stat.ContainsKey(match.Team2))
                        {
                            stat[match.Team2] += match.Score.Ft[0];
                        }
                        else
                        {
                            stat.Add(match.Team1, match.Score.Ft[1]);
                            stat.Add(match.Team2, match.Score.Ft[0]);
                        }
                    }
					else if(whichTeams == "difference")
					{
                        int firstDifference = match.Score.Ft[0] - match.Score.Ft[1];
                        int secondDifference = match.Score.Ft[1] - match.Score.Ft[0];
                        if (stat.ContainsKey(match.Team1))
                        {
                            stat[match.Team1] += firstDifference;
                        }
                        else if (stat.ContainsKey(match.Team2))
                        {
                            stat[match.Team2] += secondDifference;
                        }
                        else
                        {
                            stat.Add(match.Team1, firstDifference);
                            stat.Add(match.Team2, secondDifference);
                        }
                    }
                    else
					{
                        if (stat.ContainsKey(match.Date))
                        {
                            stat[match.Date] += match.Score.Ft[0] + match.Score.Ft[1]; 
                        }
                        else
                        {
                            stat.Add(match.Date, match.Score.Ft[0] + match.Score.Ft[1]);
                        }
                    }

                }

                foreach (var item in stat)
                {
                    if (whichTeams == "attacking")
                    {
                        var team = new Team();
                        team.TeamName = item.Key;
                        team.GoalsScored = item.Value;
                        team.LeagueName = league.Name;
                        teams.Add(team);
                    }
                    else if(whichTeams == "defensive")
					{
                        var team = new Team();
                        team.TeamName = item.Key;
                        team.GoalsConceded = item.Value;
                        team.LeagueName = league.Name;
                        teams.Add(team);
                    }
					else if(whichTeams == "difference")
					{
                        var team = new Team();
                        team.TeamName = item.Key;
                        team.GoalsDifference = item.Value;
                        team.LeagueName = league.Name;
                        teams.Add(team);
                    }
					else
					{
                        var day = new EffectiveDay();
                        day.Date = item.Key;
                        day.Goals = item.Value;
                        days.Add(day);
					}
                }

                stat.Clear();
            }
        }
    }
}
