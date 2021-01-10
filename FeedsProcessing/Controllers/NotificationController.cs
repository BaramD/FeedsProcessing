using FeedsProcessing.Logic;
using FeedsProcessing.Models;
using FeedsProcessing.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FeedsProcessing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly INotificationModelParser _notificationModelParser;
        private readonly INotificationModelValidator _notificationModelValidator;
        private readonly INotificationManager _notificationManager;
        private readonly IRequestHandler _requestHandler;

        public NotificationController(
            INotificationModelParser notificationModelParser,
            INotificationModelValidator notificationModelValidator,
            INotificationManager notificationManager,
            IRequestHandler requestHandler, 
            ILogger<NotificationController> logger)
        {
            _notificationModelParser = notificationModelParser;
            _notificationModelValidator = notificationModelValidator;
            _notificationManager = notificationManager;
            _requestHandler = requestHandler;
            _logger = logger;
        }


        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] JsonElement notification)
        {
            _logger.LogInformation("About to process notification.");
            var modelResult = _notificationModelParser.Parse(notification);
            if (!modelResult.IsOk())
                return BadRequest(modelResult.Error);

            NotificationModel model = modelResult.Model;
  
            if (!_requestHandler.IsValid(model.Id))
                return BadRequest("Failed to process identical request!");

            _requestHandler.Save(model.Id);

            _logger.LogInformation("Request processing started.");
            var validationResult = _notificationModelValidator.Validate(modelResult.Model);
            if (validationResult != null)
                return BadRequest(validationResult.ErrorMessage);

            _notificationManager.Save(model.FromModel(), notification);
            return Ok();

        }

    }
}
