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
        private IMeterReadingUploadService MeterReadingUploadService { get; set; }

        public MeterReadingUploadsController(IMeterReadingUploadService service)
        {
            this.MeterReadingUploadService = service;
        }

        [HttpPost()]
        public ActionResult Post(IFormFile file)
        {
            var result = this.MeterReadingUploadService.ProcessUpload(file);
            return this.Ok($"{result.NumberOfSuccesses} rows successfully processed, {result.NumberOfFailures} rows failed.");
        }
    }
}