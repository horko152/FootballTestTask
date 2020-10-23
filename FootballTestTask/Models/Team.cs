using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTestTask.Models
{
	public class Team
	{
		public int Id { get; set; }
		public string TeamName { get; set; }
		public string LeagueName { get; set; }
		public int GoalsScored { get; set; }
		public int GoalsConceded { get; set; }
	}
}
