using Api.Controllers;
using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;
using Api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests;
public class OnderzoekTest {

  private readonly Mock<IOnderzoekRepository> _mockOnderzoekRepository;
  private readonly Mock<IMapper> _mockMapper;
  private readonly OnderzoekController _controller;
  
  public OnderzoekTest() {
    _mockOnderzoekRepository = new Mock<IOnderzoekRepository>();
    _mockMapper = new Mock<IMapper>();
    _controller = new OnderzoekController(_mockMapper.Object, _mockOnderzoekRepository.Object);
  }
  
         [Fact]
        public async Task Create_ReturnsCreatedResult_WithValidData()
        {
            // Arrange
            var addDto = new AddOnderzoekRequestDto
            {
                Titel = "Test Titel",
                AantalParticipanten = 10,
                WebsiteUrl = "http://example.com",
                StartDatum = DateTime.Now,
                Omschrijving = "Test Omschrijving",
                Vergoeding = 100.0,
                Locatie = "Test Locatie",
                Status = "open",
                BedrijfId = Guid.NewGuid()
            };

            var onderzoek = new Onderzoek
            {
              Id = Guid.NewGuid(),
              Titel = addDto.Titel,
              AantalParticipanten = addDto.AantalParticipanten,
              websiteUrl = addDto.WebsiteUrl,
              StartDatum = addDto.StartDatum,
              Omschrijving = addDto.Omschrijving,
              Vergoeding = addDto.Vergoeding,
              Locatie = addDto.Locatie,
              Status = (Status)Enum.Parse(typeof(Status), addDto.Status),
              BedrijfId = addDto.BedrijfId
            };

            var onderzoekDto = new OnderzoekDto
            {
              Id = onderzoek.Id,
              Titel = onderzoek.Titel,
              AantalParticipanten = onderzoek.AantalParticipanten,
              websiteUrl = onderzoek.websiteUrl,
              StartDatum = onderzoek.StartDatum,
              Omschrijving = onderzoek.Omschrijving,
              Vergoeding = onderzoek.Vergoeding,
              Locatie = onderzoek.Locatie,
              Status = onderzoek.Status.ToString(),
              BedrijfId = onderzoek.BedrijfId
            };

            _mockMapper.Setup(m => m.Map<Onderzoek>(It.IsAny<AddOnderzoekRequestDto>())).Returns(onderzoek);
            _mockOnderzoekRepository.Setup(r => r.CreateAsync(It.IsAny<Onderzoek>())).ReturnsAsync(onderzoek);
            _mockMapper.Setup(m => m.Map<OnderzoekDto>(It.IsAny<Onderzoek>())).Returns(onderzoekDto);

            // Act
            var result = await _controller.Create(addDto);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var resultDto = actionResult.Value as OnderzoekDto;
            Assert.Equal(onderzoekDto.Id, resultDto.Id);
          
            _mockOnderzoekRepository.Verify(r => r.CreateAsync(It.IsAny<Onderzoek>()), Times.Once);
          
        }
         
        [Fact]
        public async Task Delete_ReturnsOkResult_WhenDeletionIsSuccessful()
        {
          // Arrange
          var onderzoekId = Guid.NewGuid();
          _mockOnderzoekRepository.Setup(r => r.DeleteAsync(onderzoekId)).ReturnsAsync(true);

          // Act
          var result = await _controller.Delete(onderzoekId);

          // Assert
          var okResult = Assert.IsType<OkObjectResult>(result);
          Assert.Equal("Onderzoek is verwijderd.", okResult.Value);
          _mockOnderzoekRepository.Verify(r => r.DeleteAsync(onderzoekId), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenOnderzoekDoesNotExist()
        {
          // Arrange
          var onderzoekId = Guid.NewGuid();
          _mockOnderzoekRepository.Setup(r => r.DeleteAsync(onderzoekId)).ReturnsAsync(false);

          // Act
          var result = await _controller.Delete(onderzoekId);

          // Assert
          Assert.IsType<NotFoundResult>(result);
          _mockOnderzoekRepository.Verify(r => r.DeleteAsync(onderzoekId), Times.Once);
        }




}