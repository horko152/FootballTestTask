﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTestTask.Models
{
	public class Match
	{ 
		public string Round { get; set; }
		public string Date { get; set; }
		public string Team1 { get; set; }
		public string Team2 { get; set; }
		public Score Score { get; set; }
	}
}
