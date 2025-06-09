using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using TDGaming.Domain.Entities;

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
        public async Task Create_ReturnsCreated()
        {
            var newGame = new
            {
                title = "Elden Ring",
                genre = "RPG",
                price = 59.99M
            };

            var response = await _client.PostAsJsonAsync("/api/videogames", newGame);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }
    }
}
