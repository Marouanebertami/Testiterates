using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_iterates.Models.RequestDTO;
using Test_iterates.Services.ClientServices;

namespace Test_iterates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientServices _clientServices;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientServices clientServices, ILogger<ClientController> logger)
        {
            _clientServices = clientServices;
            _logger = logger;
        }

        #region FR6

        [HttpPost(Name = "CreateRequest")]
        public async Task<IActionResult> CreateRequest(SaveRequestDTO model)
        {
            try
            {
                RequestReturnDTO request = await _clientServices.CreateRequest(model);

                if (!string.IsNullOrEmpty(request.messageError))
                {
                    return BadRequest(request.messageError);
                }

                return Ok(request);
            }
            catch (Exception e)
            {
                _logger.LogError("Error ClientController/CreateRequest : " + e.Message);
                return StatusCode(500, e.Message);
            }
        }

        #endregion
    }
}
