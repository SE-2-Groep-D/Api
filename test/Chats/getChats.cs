using Api.Controllers;
using Api.Models.DTO.Bericht;
using Api.Repositories.IBerichtRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Tests.Chats {
   public class getChats {

    [Fact]
    public async Task GetChats_ReturnsChatsForUser() {
      // Arrange

      var predefinedChatData = new List<ChatResponseDto>
{
    new ChatResponseDto
    {
        OtherUserId = Guid.NewGuid(), // Simulating another user's ID
        LastMessage = "Hello, how are you?",
        TotalMessages = 10,
        Naam = "John Doe"
    },
    new ChatResponseDto
    {
        OtherUserId = Guid.NewGuid(),
        LastMessage = "Are we still meeting tomorrow?",
        TotalMessages = 5,
        Naam = "Jane Smith"
    }
};

      var testUserId = Guid.NewGuid();

      var mockRepo = new Mock<IBerichtRepository>();
      mockRepo.Setup(repo => repo.GetChatsByUserId(testUserId))
              .ReturnsAsync(predefinedChatData);
      var controller = new BerichtController(mockRepo.Object);

      // Act
      var result = await controller.GetChats(testUserId);

      // Assert
      var actionResult = Assert.IsType<OkObjectResult>(result);
      var returnValue = Assert.IsType<List<ChatResponseDto>>(actionResult.Value);
      Assert.Equal(predefinedChatData.Count, returnValue.Count);
    }



  }
}
