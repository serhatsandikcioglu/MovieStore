using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Data.Entities;
using MovieStore.Service;
using Newtonsoft.Json;

namespace MovieStore.Test
{
    public class ActorControllerTest : IClassFixture<CustomWebApplitactionFactory<Program>>
    {
        private readonly CustomWebApplitactionFactory<Program> _factory;

        public ActorControllerTest(CustomWebApplitactionFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetById_Returns_Ok()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/v1/actors/3");


            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var actor = JsonConvert.DeserializeObject<Actor>(content);

            Assert.NotNull(actor);
            Assert.Equal(3, actor.Id);
        }
    }
}
