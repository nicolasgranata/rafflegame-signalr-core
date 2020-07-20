using SignalRCore.DrawGame.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SignalRCore.DrawGame.UnitTests.Services
{
    public class DrawServiceTest
    {
        [Fact]
        public void Draw_ReturnsWinner()
        {
            // Arrange
            var drawService = new DrawService();
            drawService.AddParticipant("participant_one");
            drawService.AddParticipant("participant_two");
            drawService.AddParticipant("participant_three");

            // Act
            var winner = drawService.Draw();

            // Assert
            Assert.Contains(drawService.GetParticipants(), _ => _.Contains(winner));
        }

        [Fact]
        public void Draw_ReturnsLosers()
        {
            // Arrange
            var drawService = new DrawService();
            drawService.AddParticipant("participant_one");
            drawService.AddParticipant("participant_two");
            drawService.AddParticipant("participant_three");

            // Act
            var winner = drawService.Draw();
            drawService.RemoveParticipant(winner);

            // Assert
            Assert.Contains(drawService.GetParticipants(), _ => !_.Contains(winner));
        }

        [Fact]
        public void AddParticipant_SingleParticipant_ReturnsParticipantAdded()
        {
            // Arrange
            var drawService = new DrawService();

            // Act
            var expected = "participant_one";
            drawService.AddParticipant(expected);

            // Assert
            Assert.Equal(expected, drawService.GetParticipants()[0]);
        }

        [Fact]
        public void RemoveParticipant_SingleParticipant_ReturnsParticipantNotRemoved()
        {
            // Arrange
            var drawService = new DrawService();
            var toRemove = "participant_one";
            var expected = "participant_two";
            drawService.AddParticipant(toRemove);
            drawService.AddParticipant(expected);

            // Act
            drawService.RemoveParticipant(toRemove);

            // Assert
            Assert.Equal(expected, drawService.GetParticipants()[0]);
        }

        [Fact]
        public void RemoveAllParticipants_ReturnsNoParticipants()
        {
            // Arrange
            var drawService = new DrawService();
            drawService.AddParticipant("participant_one");
            drawService.AddParticipant("participant_two");
            drawService.AddParticipant("participant_three");

            // Act
            drawService.RemoveAllParticipants();

            // Assert
            Assert.Equal(0, drawService.GetParticipants().Count);
        }

        [Fact]
        public void GetParticipantsCount_AllParticipants_ReturnsCountOfParticipants()
        {
            // Arrange
            var drawService = new DrawService();
            drawService.AddParticipant("participant_one");
            drawService.AddParticipant("participant_two");
            drawService.AddParticipant("participant_three");

            // Act
            var result = drawService.GetParticipantsCount();

            // Assert
            Assert.Equal(3, result);
        }
    }
}
