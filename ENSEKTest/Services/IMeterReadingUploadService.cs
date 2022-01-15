using ENSEKTest.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEKTest.Services
{
    public interface IMeterReadingUploadService
    {
        MeterReadingUploadResult ProcessUpload(IFormFile file);
    }
}
