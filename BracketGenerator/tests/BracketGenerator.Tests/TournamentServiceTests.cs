using BracketGenerator.Interfaces;
using BracketGenerator.Model;
using BracketGenerator.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Match = BracketGenerator.Model.Match;

namespace BracketGenerator.Tests
{

    public class TournamentServiceTests
    {
        [Fact]
        public void GetTeams_ShouldCreateCorrectNumberOfTeams()
        {
            // Arrange
            var teams = new List<Team>{
            new Team { Country = "Team A", Seed="1A"},
            new Team { Country = "Team B", Seed="1B"},
            new Team { Country = "Team C", Seed="1C"},
            new Team { Country = "Team D", Seed="1D"}
            };
            var teamService = new Mock<ITeamService>();
            var tournamentService = new TournamentService(teamService.Object);

            // Act
            foreach (var team in teams)
            {
                tournamentService.SeedTeam(team.Seed, team.Country);
            }

            // Assert
            Assert.Equal(4, tournamentService.GetCurrentTeams().Count);
        }
        [Fact]
        public void GetRounds_ShouldCreateCorrectNoOfRounds()
        {
            // Arrange
            var teams = new List<Team>{
            new Team { Country = "Team A", Seed="1A", Group="A"},
            new Team { Country = "Team B", Seed="1B", Group="B"},
            new Team { Country = "Team C", Seed="1C", Group="C"},
            new Team { Country = "Team D", Seed="1D", Group="D"},
            };
            var teamService = new Mock<ITeamService>();
            var tournamentService = new TournamentService(teamService.Object);

            // Act

            tournamentService.GenerateBrackets(teams);

            // Assert
            Assert.Equal(2, tournamentService.GetTournamentRounds().Count);
        }

        [Fact]
        public void GetRounds_ShouldCreateCorrectNoOfRoundsWhenTeamsCountIsOdd()
        {
            // Arrange
            var teams = new List<Team>{
            new Team { Country = "Team A", Seed="1A", Group="A"},
            new Team { Country = "Team B", Seed="1B", Group="B"},
            new Team { Country = "Team C", Seed="1C", Group="C"},
            new Team { Country = "Team D", Seed="1D", Group="D"},
            new Team { Country = "Team E", Seed="1E", Group="E"}
            };
            var teamService = new Mock<ITeamService>();
            var tournamentService = new TournamentService(teamService.Object);

            // Act

            tournamentService.GenerateBrackets(teams);

            // Assert
            Assert.Equal(2, tournamentService.GetTournamentRounds().Count);
        }

        [Fact]
        public void GetWinners_ShouldCreateCorrectNoOfWinners()
        {
            // Arrange
            var teams = new List<Team>{
            new Team { Country = "Team A", Seed="1A", Group="A"},
            new Team { Country = "Team B", Seed="1B", Group="B"},
            new Team { Country = "Team C", Seed="1C", Group="C"},
            new Team { Country = "Team D", Seed="1D", Group="D"},
            };
            var teamService = new Mock<ITeamService>();
            var tournamentService = new TournamentService(teamService.Object);

            // Act

            tournamentService.GenerateBrackets(teams);
            tournamentService.StartMatchRounds();

            // Assert
            Assert.Equal(2, tournamentService.GetWiningTeamList().Count);
        }

        [Fact]
        public void GetWinner_ShouldReturnCorrectMatchWinner()
        {
            var TeamOne = new Team()
            {
                Country = "Team A",
                Group = "A",
                Seed = "1A"
            };
            var TeamTwo = new Team()
            {
                Country = "Team B",
                Group = "B",
                Seed = "1B"
            };
            // Arrange
            Match newMatch = new Match(TeamOne, TeamTwo);
            newMatch.TeamOneScore = 4;
            newMatch.TeamTwoScore = 3;


            // Act
            var teamService = new Mock<ITeamService>();
            var tournamentService = new TournamentService(teamService.Object);
            tournamentService.GetMatchResults(newMatch);

            // Assert
            Assert.Equal(TeamOne.Country, newMatch.WiningTeam.Country);
        }

        [Fact]
        public void GetWinner_WinnerShouldNotBeNullWhenMatchDraw()
        {
            var TeamOne = new Team()
            {
                Country = "Team A",
                Group = "A",
                Seed = "1A"
            };
            var TeamTwo = new Team()
            {
                Country = "Team B",
                Group = "B",
                Seed = "1B"
            };
            // Arrange
            Match newMatch = new Match(TeamOne, TeamTwo);
            newMatch.TeamOneScore = 3;
            newMatch.TeamTwoScore = 3;


            // Act
            var teamService = new Mock<ITeamService>();
            var tournamentService = new TournamentService(teamService.Object);
            tournamentService.GetMatchResults(newMatch);

            // Assert
            Assert.NotNull(newMatch.WiningTeam.Country);
        }
    }
}

