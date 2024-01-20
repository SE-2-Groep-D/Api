using Api.Controllers;
using Api.Models.Domain.User;
using Api.Models.DTO.Auth.request;
using Api.Repositories.IGebruikerRepository;
using Api.Services.ITokenService;
using Api.Services.IUserService;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Api.Models.DTO.Auth.response;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Tests;
public class RegisterTest {
  private readonly AuthController _controller;
  private readonly Mock<IGebruikerRepository> _mockGebruikerRepository;
  private readonly Mock<IUserService> _mockUserService;
  private readonly Mock<ITokenService> _mockTokenService;
  private readonly Mock<IMapper> _mockMapper;

  public RegisterTest() {

    var _usermanager = new FakeUserManager();
    _mockGebruikerRepository = new Mock<IGebruikerRepository>();
    _mockUserService = new Mock<IUserService>();
    _mockTokenService = new Mock<ITokenService>();
    _mockMapper = new Mock<IMapper>();

    _controller = new AuthController(_usermanager, _mockGebruikerRepository.Object, _mockUserService.Object, _mockTokenService.Object, _mockMapper.Object);
  }

  [Fact]
  public async Task Register_ReturnsOkResult_WhenRegistrationSucceeds() {
    // Arrange
    var registerRequestDto = new RegisterRequestDto {
      Voornaam = "Test",
      Achternaam = "User",
      GoogleAccount = false,
      Email = "testuser@example.com",
      Password = "Test@123",
      Roles = new string[] { "Beheerder" }
    };

    _mockUserService.Setup(service => service.Register(It.IsAny<Gebruiker>(), It.IsAny<string>(), It.IsAny<string[]>()))
        .ReturnsAsync(new RegisterResponseDto (true, "User was registerd! Please Login."));

    // Act
    var result = await _controller.Register(registerRequestDto);

    // Assert
    Assert.IsType<OkObjectResult>(result);
  }

  [Fact]
  public async Task Register_ReturnsBadResult_WhenRegistrationDoesNotSucceeds() {
    // Arrange
    var registerRequestDto = new RegisterRequestDto {
      Voornaam = "Test",
      Achternaam = "User",
      GoogleAccount = false,
      Email = "testuser@example.com",
      Password = "Test@123",
      Roles = new string[] { "Beheerder" }
    };

    _mockUserService.Setup(service => service.Register(It.IsAny<Gebruiker>(), It.IsAny<string>(), It.IsAny<string[]>()))
        .ReturnsAsync(new RegisterResponseDto(false, "Er ging iets mis!"));

    // Act
    var result = await _controller.Register(registerRequestDto);

    // Assert
    Assert.IsType<BadRequestObjectResult>(result);
  }
}

public class FakeUserManager : UserManager<Gebruiker> {
  public FakeUserManager()
      : base(new Mock<IUserStore<Gebruiker>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<Gebruiker>>().Object,
            new IUserValidator<Gebruiker>[0],
            new IPasswordValidator<Gebruiker>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<Gebruiker>>>().Object) { }
}

