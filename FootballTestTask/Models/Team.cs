using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTestTask.Models
{
	public class Team
	{
		public string LeagueName { get; set; }
		public string TeamName { get; set; }
		public int GoalsScored { get; set; }
		public int GoalsConceded { get; set; }
		public int GoalsDifference { get; set; }
	}
}
