using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTestTask.Models
{
	public class League
	{
		public string Name { get; set; }
		public List<Match> Matches { get; set; }
	}
}
