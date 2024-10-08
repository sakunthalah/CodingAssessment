using BracketGenerator.Model;
using Xunit;
using Match = BracketGenerator.Model.Match;

namespace BracketGenerator.Tests;

public class MatchTests
{
    [Fact]
    public void Match_ShouldSetWinnerCorrectly_WhenTeamAWins()
    {
        // Arrange
        var team1 = new Team { Country = "Team A" };
        var team2 = new Team { Country = "Team B" };
        var match = new Match(team1, team2);
        match.TeamOneScore = 2;
        match.TeamTwoScore = 1;

        // Act
        match.WiningTeam = team1;

        // Assert
        Assert.Equal(team1, match.WiningTeam);
    }

    [Fact]
    public void Match_ShouldSetWinnerCorrectly_WhenTeamBWins()
    {
        // Arrange
        var team1 = new Team { Country = "Team A" };
        var team2 = new Team { Country = "Team B" };
        var match = new Match(team1, team2);
        match.TeamOneScore = 0;
        match.TeamTwoScore = 3;

        // Act
        match.WiningTeam = team2;

        // Assert
        Assert.Equal(team2, match.WiningTeam);
    }

    
}