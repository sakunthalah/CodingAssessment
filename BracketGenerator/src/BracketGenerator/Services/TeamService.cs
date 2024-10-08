using BracketGenerator.Interfaces;
using BracketGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BracketGenerator.Services
{
    public class TeamService : ITeamService
    {
        /// <summary>
        /// data file path
        /// </summary>
        const string TEAMSDATAFILE = @"Data/teams.json";

        /// <summary>
        /// Load the tournament teams from data file
        /// </summary>
        /// <returns></returns>
        public List<Team> LoadTeams()
        {
            string jsonString = File.ReadAllText(TEAMSDATAFILE);
            var allTeams = JsonSerializer.Deserialize<List<Team>>(jsonString);
            if (allTeams == null)
                return new List<Team>();

            return allTeams;
        }
    }
}
