using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Interfaces
{
    public interface ITournamentService
    {
        /// <summary>
        /// Seed the Tournament Team data
        /// </summary>
        void SeedTournamentData();

        /// <summary>
        /// Generate Teams
        /// Set Matches
        /// StartMatchRounds
        /// Get Match Results
        /// AdvanceTeam
        /// </summary>
        void PathToVictory();

        /// <summary>
        /// Populate the match and winning team details 
        /// </summary>
        /// <returns></returns>
        string GetTournamentWinner();
    }
}
