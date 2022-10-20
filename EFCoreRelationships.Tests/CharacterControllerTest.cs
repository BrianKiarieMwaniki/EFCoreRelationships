using EFCoreRelationships.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreRelationships.Tests
{
    public class CharacterControllerTest
    {
        private readonly HttpClient _httpClient;

        public CharacterControllerTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [Fact]
        public async void GetCharacterByUser()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Character>>("/api/character?userid=1");

            Assert.NotNull(response);

            var character = response?.FirstOrDefault();

            Assert.NotNull(character);

            Assert.NotEmpty(character?.Name);
            Assert.Equal(1, character?.UserId);
        }
    }
}
