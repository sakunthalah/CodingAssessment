using BracketGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Interfaces
{
    public interface ITeamService
    {
        /// <summary>
        /// Loads teams from data file
        /// </summary>
        /// <returns></returns>
        List<Team> LoadTeams();
    }
}
