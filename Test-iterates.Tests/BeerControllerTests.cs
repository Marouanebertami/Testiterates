using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Test_iterates.Controllers;
using Test_iterates.Entities;
using Test_iterates.Models.BeerDTO;
using Test_iterates.Services.BeerServices;

namespace Test_iterates.Tests
{
    public class BeerControllerTests
    {
        private readonly IBeerServices _beerServices;
        private readonly ILogger<BeerController> _logger;
        private readonly IMapper _mapper;

        public BeerControllerTests()
        {
            _beerServices = A.Fake<IBeerServices>();
            _logger = A.Fake<ILogger<BeerController>>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task GetBeer_Returns_Not_Null_And_Ok()
        {
            var beers = A.Fake<ICollection<Beer>>();
            var beersList = A.Fake<List<BeerItem>>();
            A.CallTo(() => _mapper.Map<List<BeerItem>>(beers)).Returns(beersList);

            BeerController controller = new BeerController(_beerServices, _logger, _mapper);

            var result = await controller.GetListBeersByIdBrewery(1);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async Task Beer_Add_Return_Ok()
        {
            SaveBeerDTO saveItem = new SaveBeerDTO { Id_brewery = 1, Name = "Hello", Alcohol_Content = 5, Price = 12 };

            BeerController controller = new BeerController(_beerServices, _logger, _mapper);
            var result = await controller.AddBeer(saveItem);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}