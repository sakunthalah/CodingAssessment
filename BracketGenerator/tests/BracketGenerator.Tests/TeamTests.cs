using BracketGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BracketGenerator.Tests
{
    public class TeamTests
    {
        [Fact]
        public void Team_ShouldNotBeNull()
        {
            // Arrange
            var team = new Team();

            // Act

            // Assert
            Assert.NotNull(team);
        }

        [Fact]
        public void Team_ShouldBeComparableByCountry()
        {
            // Arrange
            var teamA = new Team { Country = "Team A" };
            var teamB = new Team { Country = "Team B" };
            var teamC = new Team { Country = "Team C" };

            // Act
            bool comparison1 = teamA.Equals(teamB);
            bool comparison2 = teamB.Equals(teamA);
            bool comparison3 = teamB.Equals(teamC);
            bool comparison4 = teamA.Equals(teamA);

            // Assert
            Assert.False(comparison1);
            Assert.False(comparison2);
            Assert.False(comparison3);
            Assert.True(comparison4);
        }

    }
}
