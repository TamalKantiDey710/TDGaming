using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using TDGaming.Domain.Entities;
using TDGaming.API.DTO;

namespace TDGaming.Tests.Api
{
    public class VideoGamesApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public VideoGamesApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/videogames");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var games = await response.Content.ReadFromJsonAsync<List<VideoGame>>();
            games.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsNotFound()
        {
            var response = await _client.GetAsync("/api/videogames/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            var newGame = new VideoGame("Elden Ring", 59.99M);

            var response = await _client.PostAsJsonAsync("/api/videogames", newGame);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateGame_Invalid_ReturnsBadRequest()
        {
            var invalidGame = new VideoGameDto { Title = "", Price = -5 };

            var response = await _client.PostAsJsonAsync("/api/videogames", invalidGame);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateGame_InvalidId_ReturnsNotFound()
        {
            var update = new VideoGameDto { Title = "Updated Title", Price = 10 };

            var response = await _client.PutAsJsonAsync("/api/videogames/9999", update);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
