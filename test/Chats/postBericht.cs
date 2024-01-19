using Api.Controllers;
using Api.Models.Domain.Bericht;
using Api.Models.DTO.Bericht;
using Api.Repositories.IBerichtRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;


namespace Api.Tests.Chats {
  public class postBericht {

    [Fact]
    public async Task StuurBericht_CreatesBerichtAndReturnsIt() {
      
      // Arrange
      var requestDto = new StuurBerichtRequestDto {
        Tekst = "Test message",
        VerzenderId = Guid.NewGuid(),
        OntvangerId = Guid.NewGuid()
      };

      var expectedBericht = new Bericht {
        Tekst = requestDto.Tekst,
        DatumTijd = DateTime.Now,
        VerzenderId = requestDto.VerzenderId,
        OntvangerId = requestDto.OntvangerId
      };

      var mockRepo = new Mock<IBerichtRepository>();
      mockRepo.Setup(repo => repo.CreateBericht(It.IsAny<Bericht>()))
              .ReturnsAsync(expectedBericht);
      var controller = new BerichtController(mockRepo.Object);

      // Act
      var result = await controller.StuurBericht(requestDto);

      // Assert
      var actionResult = Assert.IsType<OkObjectResult>(result);
      var returnValue = Assert.IsType<Bericht>(actionResult.Value);
      Assert.Equal(expectedBericht.Tekst, returnValue.Tekst);
      Assert.Equal(expectedBericht.VerzenderId, returnValue.VerzenderId);
      Assert.Equal(expectedBericht.OntvangerId, returnValue.OntvangerId);
    }

  }
}
