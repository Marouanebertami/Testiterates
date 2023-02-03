using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Test_iterates.Models.BeerDTO;
using Test_iterates.Services.BeerServices;
using System.Resources;
using System.Reflection;
using System.Globalization;

namespace Test_iterates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : Controller
    {
        private readonly IBeerServices _beerServices;
        private readonly ILogger<BeerController> _logger;
        private readonly IMapper _mapper;

        public BeerController(IBeerServices beerServices, ILogger<BeerController> logger, IMapper mapper)
        {
            _beerServices = beerServices;
            _logger = logger;
            _mapper = mapper;
        }

        #region FR1

        /// <summary>
        /// Function to get a list of Beers by Brewery
        /// </summary>
        /// <param name="id_brewery">the identifier of Brewery</param>
        /// <returns>List of Beers</returns>
        [HttpGet(Name = "GetListBeersByIdBrewery")]
        public async Task<IActionResult> GetListBeersByIdBrewery(long? id_brewery)
        {
            try
            {
                if (id_brewery.HasValue)
                {
                    var beers = await _beerServices.GetListBeerByBrewery(id_brewery.Value);
                    return Ok(_mapper.Map<List<BeerItem>>(beers));
                }
                
                return BadRequest("The ID is required");
            }
            catch(Exception e)
            {
                _logger.LogError("Error BeerController/GetListBeersByIdBrewery : " + e.Message);
                return StatusCode(500, e.Message);
            }
        }

        #endregion

        #region FR2

        /// <summary>
        /// Function to add a beer to DB
        /// </summary>
        /// <param name="model">content of values to add</param>
        /// <returns>the beer added</returns>
        [HttpPost(Name = "AddBeer")]
        public async Task<IActionResult> AddBeer(SaveBeerDTO? model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Model isn't valid");

                if(model.Id_brewery == 0)
                    return BadRequest("Model isn't valid");

                return Ok(await _beerServices.AddBeer(model));
            }
            catch (Exception e)
            {
                _logger.LogError("Error BeerController/AddBeer : " + e.Message);
                return StatusCode(500, e.Message);
            }
        }

        #endregion

        #region FR3

        /// <summary>
        /// Function to delete a beer by ID
        /// </summary>
        /// <param name="Id_Beer">id of beer to delete</param>
        /// <returns>a meesage if there's a error</returns>
        [HttpDelete(Name = "DeleteBeer")]
        public async Task<IActionResult> DeleteBeer(long? Id_Beer)
        {
            try
            {
                if (Id_Beer.HasValue)
                {
                    string? message = await _beerServices.DeleteBeer(Id_Beer.Value);

                    //if there's a error
                    if (!string.IsNullOrEmpty(message))
                    {
                        return BadRequest(message);
                    }

                    return Ok("The beer is deleted succes");
                }
                
                return BadRequest("Id Beer is not valid");
            }
            catch (Exception e)
            {
                _logger.LogError("Error BeerController/DeleteBeer : " + e.Message);
                return StatusCode(500, e.Message);
            }
        }

        #endregion
    }
}
