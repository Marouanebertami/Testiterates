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
        private readonly BeerController _controller;

        public BeerControllerTests()
        {
            _beerServices = A.Fake<IBeerServices>();
            _logger = A.Fake<ILogger<BeerController>>();
            _mapper = A.Fake<IMapper>();
            _controller = new BeerController(_beerServices, _logger, _mapper);
        }

        [Fact]
        public async Task GetBeer_Returns_Not_Null_And_Ok()
        {
            long? breweryId = 4;

            var result = await _controller.GetListBeersByIdBrewery(breweryId);

            if (breweryId.HasValue)
            {
                result.Should().NotBeNull();
                result.Should().BeOfType(typeof(OkObjectResult));
            }
            else
            {
                result.Should().BeOfType(typeof(BadRequestObjectResult));
            }
        }

        [Fact]
        public async Task Beer_Add_Return_Ok()
        { 
            SaveBeerDTO? saveItem = new SaveBeerDTO { Id_brewery = 3, Name = "Hello", Alcohol_Content = 5, Price = 12 };

            var result = await _controller.AddBeer(saveItem);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async Task Beer_delete_return_ok()
        {
            long? beerId = 2;

            var result = await _controller.DeleteBeer(beerId);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}