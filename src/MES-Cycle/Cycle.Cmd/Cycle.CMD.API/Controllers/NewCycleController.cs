using CQRS.Core.Infrastructure;
using Cycle.Cmd.Commands;
using Cycle.CMD.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Post.Common.DTOs;

namespace Cycle.CMD.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NewCycleController : ControllerBase
    {
        private readonly ILogger<NewCycleController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public NewCycleController(ILogger<NewCycleController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult> NewCycleAsync(NewCycleCommand command)
        {
            var id = Guid.NewGuid();
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);

                return StatusCode(StatusCodes.Status201Created, new NewResponse
                {
                    Id = id,
                    Message = "New Cycle creation request completed successfully!"
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Warning, ex, "Client made a bad request!");
                return BadRequest(new BaseResponse
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to create a new cycle!";
                _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new NewResponse
                {
                    Id = id,
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}