using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Model
{
    public class Match
    {
        public Team TeamOne { get; set; }
        public Team TeamTwo { get; set; }
        public Team WiningTeam { get; set; }
        public int TeamOneScore { get; set; }
        public int TeamTwoScore { get; set; }

        public Match(Team teamOne, Team teamTwo)
        {
            TeamOne = teamOne;
            TeamTwo = teamTwo;
        }
    }
}
