using Api.Controllers;
using Api.Models.DTO.Onderzoek.tracking;
using Api.Repositories.ITrackingRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests; 
public class TrackingTest {

  [Fact]
  public async Task if_submit_fails() {
    var mapperMock = new Mock<IMapper>();
    var repositoryMock = new Mock<ITrackingRepository>();
    var controller = new TrackingController(mapperMock.Object, repositoryMock.Object);
    var request = new SubmitTrackingResultsDto();

    repositoryMock.Setup(r => r.SubmitResults(request)).ReturnsAsync(false);

    // Act
    var result = await controller.SubmitResults(request);

    // Assert
    Assert.IsType<BadRequestResult>(result);
  }

}
