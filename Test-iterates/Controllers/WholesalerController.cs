using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_iterates.Models.WholesalerBeerStockDTO;
using Test_iterates.Services.SaleServices;
using System.Resources;
using System.Reflection;

namespace Test_iterates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalerController : ControllerBase
    {
        private readonly ISaleServices _saleServices;
        private readonly ILogger<WholesalerController> _logger;

        public WholesalerController(ISaleServices saleServices, ILogger<WholesalerController> logger)
        {
            _saleServices = saleServices;
            _logger = logger;
        }

        #region FR4

        /// <summary>
        /// Function to add Stock
        /// </summary>
        /// <param name="model">Content of stock</param>
        /// <returns></returns>
        [HttpPost(Name = "AddSale")]
        public async Task<IActionResult> AddSale(SaveWholesalerBeerStockDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Model isn't valid");

                string message = await _saleServices.AddSale(model);

                if (!String.IsNullOrEmpty(message))
                    return BadRequest(message);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Error WholesalerController/AddSale : " + e.Message);
                return StatusCode(500, e.Message);
            }
        }

        #endregion

        #region FR5

        [HttpPut(Name = "UpdateStock")]
        public async Task<IActionResult> UpdateStock(SaveWholesalerBeerStockDTO model)
        {
            try
            {
                string message = await _saleServices.UpdateStock(model);

                if (!String.IsNullOrEmpty(message))
                    return BadRequest(message);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Error WholesalerController/UpdateStock : " + e.Message);
                return StatusCode(500, e.Message);
            }
        }

        #endregion
    }
}
