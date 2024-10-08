using BracketGenerator.Interfaces;
using BracketGenerator.Model;
using System.Text;
using Match = BracketGenerator.Model.Match;

namespace BracketGenerator.Services
{
    public class TournamentService : ITournamentService
    {
        ITeamService _teamService;
        StringBuilder matchRecords = new StringBuilder();
        private List<Team> tournamentTeamsList;
        private List<Match> tournamentRounds;
        private List<Team> winningTeamsList;
        public List<Match> tournamentHistory;
        int currentRound = 0;
        public TournamentService(ITeamService teamService)
        {
            _teamService = teamService;
            tournamentTeamsList = new List<Team>();
            winningTeamsList = new List<Team>();
            tournamentRounds = new List<Match>();
            tournamentHistory = new List<Match>();
        }

        /// <summary>
        /// Seed teams
        /// </summary>
        public void SeedTournamentData()
        {
            var teamsList = LoadTeams();
            matchRecords.AppendLine($"***************Tournament Teams***************");
            matchRecords.AppendLine($"\tSeed\tTeam");
            foreach (var team in teamsList.OrderBy(t => t.Group).ToList())
            {
                SeedTeam(team.Seed, team.Country);
            }

            matchRecords.AppendLine("");
        }

        /// <summary>
        /// Loads teams from TeamService
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        private List<Team> LoadTeams()
        {
            var teamsList = _teamService.LoadTeams();
            if (teamsList == null || teamsList.Count == 0)
                throw new ApplicationException("No Teams found");


            return teamsList;
        }

        /// <summary>
        /// Seed Teams
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="team"></param>
        public void SeedTeam(string seed, string team)
        {
            if (!string.IsNullOrEmpty(seed) && !string.IsNullOrEmpty(team))
            {
                var groupLetter = seed.ToCharArray().LastOrDefault().ToString();
                var newteam = new Team()
                {
                    Country = team,
                    Seed = seed,
                    Group = groupLetter.ToString().ToUpper(),
                };

                tournamentTeamsList.Add(newteam);
                matchRecords.AppendLine($"({tournamentTeamsList.Count}).\t{seed}\t{team}");


            }
        }
        /// <summary>
        /// Generate teams based on the group and seed
        /// </summary>
        /// <param name="teamsList"></param>
        public void GenerateBrackets(List<Team> teamsList)
        {
            matchRecords.AppendLine($"\n************Tournament Round {currentRound}************");
            //Teams group by each group
            var teamGroups = teamsList.GroupBy(t => t.Group).ToList();
            if (teamGroups != null)
            {
                List<Team> teams = new List<Team>();

                foreach (var group in teamGroups)
                {
                    //Get seeds within the group
                    var seedGroups = group.GroupBy(t => t.Seed).ToList();
                    for (int i = 0; i < seedGroups.Count; i++)
                    {
                        var teamsIntheGroup = seedGroups[i];

                        for (int j = 0; j < teamsIntheGroup.Count(i => i.Group == group.Key); j++)
                        {
                            teams.Add(teamsIntheGroup.ElementAt(j));
                            //Create brackets for the current round
                            if (teams.Count == 2)
                            {
                                //Set new match
                                SetNewMatch(teams[0], teams[1]);
                                teams = new List<Team>();
                            }

                        }

                    }
                }
            }
        }

        /// <summary>
        /// Generate Teams
        /// Set Matches
        /// StartMatchRounds
        /// Get Match Results
        /// AdvanceTeam
        /// </summary>
        public void PathToVictory()
        {
            //Load all teams
            var teamsList = _teamService.LoadTeams();

            while (winningTeamsList.Count != 1)
            {
                ++currentRound;
                //Initialized winningTeamList
                winningTeamsList = new List<Team>();
                //Initialized Tournament round
                tournamentRounds = new List<Match>();

                //Generate Brackets
                GenerateBrackets(teamsList);

                teamsList = new List<Team>();

                //StartMatchRounds
                StartMatchRounds();

                //Advance team for the next round
                AdvanceTeam();

                //Conitinue tournamant with the winning teams
                teamsList = winningTeamsList;
            }

            Console.WriteLine(matchRecords);

        }

        /// <summary>
        /// Create new match
        /// </summary>
        /// <param name="teamOne"></param>
        /// <param name="teamTwo"></param>
        private void SetNewMatch(Team teamOne, Team teamTwo)
        {
            Match match = new Match(teamOne, teamTwo);
            tournamentRounds.Add(match);
            matchRecords.AppendLine($"({tournamentRounds.Count}).\t{teamOne.Country} Vs {teamTwo.Country}");
        }

        /// <summary>
        /// Start match for each rounds in the tournament
        /// </summary>
        public void StartMatchRounds()
        {
            matchRecords.AppendLine($"\n***************Results for round {currentRound}***************");
            foreach (var match in tournamentRounds)
            {
                Random random = new Random();
                match.TeamOneScore = random.Next(10);
                match.TeamTwoScore = random.Next(10);
                if (match.TeamOneScore == match.TeamTwoScore)
                {
                    //if both scores are same
                    match.TeamOneScore++;
                }
                GetMatchResults(match);
            }
        }

        /// <summary>
        /// Get match result for the each team.
        /// </summary>
        /// <param name="match"></param>
        public void GetMatchResults(Match match)
        {

            if (match.TeamOneScore > match.TeamTwoScore)
            {
                match.WiningTeam = match.TeamOne;
            }
            else
            {
                match.WiningTeam = match.TeamTwo;
            }
          
            winningTeamsList.Add(match.WiningTeam);
            matchRecords.AppendLine($"Team 1: {match.TeamOne.Country},Score: {match.TeamOneScore}\nTeam 2: {match.TeamTwo.Country},Score: {match.TeamTwoScore}\n");
            tournamentHistory.Add(match);

        }

        /// <summary>
        /// Advance the team to the next round
        /// </summary>
        /// <param name="winningTeamsList"></param>
        /// <returns>List<Team></returns>
        private void AdvanceTeam()
        {
            if (winningTeamsList.Count != 1)
            {
                matchRecords.AppendLine($"\n**Teams qualified to the tournament round {currentRound + 1}**");

                foreach (var team in winningTeamsList)
                {
                    matchRecords.AppendLine($"({winningTeamsList.IndexOf(team) + 1}).{team.Country}");
                }
            }
        }

        /// <summary>
        /// Get Tournament Winner
        /// </summary>
        /// <returns></returns>
        public string GetTournamentWinner()
        {
            var resultsString = string.Empty;
            matchRecords.Clear();
            matchRecords.AppendLine("");
            matchRecords.AppendLine("------------------------GetTournamentWinner------------------------");
            matchRecords.AppendLine("");

            for (int j = 0; j < tournamentRounds.Count; j++)
            {
                var match = tournamentRounds[j];
                matchRecords.AppendLine($"\tTeam:{match.TeamOne.Country}, Score:{match.TeamOneScore}");
                matchRecords.AppendLine($"\tTeam:{match.TeamTwo.Country}, Score:{match.TeamTwoScore}");
                matchRecords.AppendLine("******************************************************************");
                matchRecords.AppendLine("******************************************************************");
                matchRecords.AppendLine($"*********** Soccer World Cup winner :{match.WiningTeam.Country} ***********");
                matchRecords.AppendLine("******************************************************************");
                matchRecords.AppendLine("******************************************************************");
            }


            matchRecords.AppendLine("");
            matchRecords.AppendLine("--GetTournamentWinner END------");
            Console.WriteLine(matchRecords);
            resultsString = matchRecords.ToString();
            matchRecords.Clear();
            return resultsString;
        }



        #region Test
        public List<Team> GetCurrentTeams()
        {
            return tournamentTeamsList;
        }

        public List<Match> GetTournamentRounds()
        {
            return tournamentRounds;
        }

        public List<Team> GetWiningTeamList()
        {
            return winningTeamsList;
        }
        #endregion

    }
}
