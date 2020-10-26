using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballTestTask.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FootballTestTask.Controllers
{
	public class EffectiveDayController : Controller
	{
		/// <summary>
		/// Reading data of leagues from json files
		/// </summary>
		public League league1 = JsonConvert.DeserializeObject<League>(System.IO.File.ReadAllText(@".\Data\en.1.json"));
		public League league2 = JsonConvert.DeserializeObject<League>(System.IO.File.ReadAllText(@".\Data\en.2.json"));
		public League league3 = JsonConvert.DeserializeObject<League>(System.IO.File.ReadAllText(@".\Data\en.3.json"));
		/// <summary>
		/// Global variables
		/// </summary>
		List<League> leagues = new List<League>();
		List<EffectiveDay> days = new List<EffectiveDay>();

		public IActionResult TheMostEffectiveDay()
		{
            InitializeLeagues();

            var stat = new Dictionary<string, int>();

            foreach (var league in leagues)
            {

                foreach (var match in league.Matches)
                {
                    var day = new EffectiveDay();

                    if (stat.ContainsKey(match.Date))
                    {
                        stat[match.Date] += match.Score.Ft[0] + match.Score.Ft[1];
                    }
                    else
                    {
                        stat.Add(match.Date, match.Score.Ft[0] + match.Score.Ft[1]);
                    }
                }

                foreach (var item in stat)
                {
                    var day = new EffectiveDay();
                    day.Date = item.Key;
                    day.Goals = item.Value;
                    days.Add(day);
                }
            }

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
    }
}
