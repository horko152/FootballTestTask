using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTestTask.Models
{
	public class Match
	{
		public int Id { get; set; }
		public string Round { get; set; }
		public DateTime Date { get; set; }
		public string FirstTeam { get; set; }
		public string SecondTeam { get; set; }
		public Score Score { get; set; }
	}
}
