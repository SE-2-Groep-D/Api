using Api.Controllers;
using Api.Models.Domain.Bericht;
using Api.Models.DTO.Bericht;
using Api.Repositories.IBerichtRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;

namespace Api.Tests.Chats {
  public class getBerichten {

    [Fact]
    public async Task GetBerichten_ReturnsMessagesBetweenUsers() {
      // Arrange
      var verzenderId = Guid.NewGuid();
      var ontvangerId = Guid.NewGuid();

      var predefinedMessages = new List<Bericht> {
                new Bericht {
                    Tekst = "Hello, how are you?",
                    VerzenderId = verzenderId,
                    OntvangerId = ontvangerId,
                    DatumTijd = DateTime.Now
                },
                new Bericht {
                    Tekst = "I'm fine, thanks!",
                    VerzenderId = ontvangerId,
                    OntvangerId = verzenderId,
                    DatumTijd = DateTime.Now
                }
            };

      var mockRepo = new Mock<IBerichtRepository>();
      mockRepo.Setup(repo => repo.GetBerichten(verzenderId, ontvangerId))
              .ReturnsAsync(predefinedMessages);
      var controller = new BerichtController(mockRepo.Object);

      // Act
      var result = await controller.GetBerichten(verzenderId, ontvangerId);

      // Assert
      var actionResult = Assert.IsType<OkObjectResult>(result);
      var returnValue = Assert.IsType<List<Bericht>>(actionResult.Value);
      Assert.Equal(predefinedMessages.Count, returnValue.Count);
    }

  }
}
