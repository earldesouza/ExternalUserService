using System.Collections.Generic;
using System.Threading.Tasks;
using ExternalUserService.Clients;
using ExternalUserService.Models;
using ExternalUserService.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace ExternalUserService.Tests.Services
{
    public class ExternalUserServiceTests
    {
        private readonly Mock<IReqResApiClient> _apiClientMock;
        private readonly ExternalUserServiceImpl _service;

        public ExternalUserServiceTests()
        {
            _apiClientMock = new Mock<IReqResApiClient>();
            _service = new ExternalUserServiceImpl(_apiClientMock.Object);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsMappedUser()
        {
            // Arrange
            var userDto = new UserDto
            {
                Id = 1,
                Email = "test@example.com",
                First_Name = "John",
                Last_Name = "Doe"
            };

            _apiClientMock.Setup(x => x.GetUserByIdAsync(1))
                          .ReturnsAsync(userDto);

            // Act
            var user = await _service.GetUserByIdAsync(1);

            // Assert
            user.Should().NotBeNull();
            user.Id.Should().Be(1);
            user.Email.Should().Be("test@example.com");
            user.FirstName.Should().Be("John");
            user.LastName.Should().Be("Doe");
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            // Arrange
            var usersDto = new List<UserDto>
            {
                new UserDto { Id = 1, Email = "user1@example.com", First_Name = "User1", Last_Name = "Test1" },
                new UserDto { Id = 2, Email = "user2@example.com", First_Name = "User2", Last_Name = "Test2" }
            };

            _apiClientMock.Setup(x => x.GetAllUsersAsync())
                          .ReturnsAsync(usersDto);

            // Act
            var users = await _service.GetAllUsersAsync();

            // Assert
            users.Should().HaveCount(2);
            users.Should().Contain(u => u.Id == 1);
            users.Should().Contain(u => u.Id == 2);
        }

        [Fact]
        public async Task GetUserByIdAsync_ThrowsException_WhenClientThrows()
        {
            // Arrange
            _apiClientMock.Setup(x => x.GetUserByIdAsync(999))
                          .ThrowsAsync(new HttpRequestException());

            // Act
            Func<Task> act = async () => await _service.GetUserByIdAsync(999);

            // Assert
            await act.Should().ThrowAsync<HttpRequestException>();
        }
    }
}