using ENSEKTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENSEKTest.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MeterReadingUploadsController : ControllerBase
    {
        private readonly ILogger<MeterReadingUploadsController> _logger;

        private IMeterReadingUploadService MeterReadingUploadService { get; set; }

        public MeterReadingUploadsController(ILogger<MeterReadingUploadsController> logger, IMeterReadingUploadService service)
        {
            _logger = logger;
            this.MeterReadingUploadService = service;
        }

        [HttpPost()]
        public bool Post(IFormFile files)
        {
            var test = files;
            return false;
        }
    }
}