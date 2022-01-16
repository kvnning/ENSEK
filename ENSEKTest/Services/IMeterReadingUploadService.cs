using ENSEKTest.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEKTest.Services
{
    /// <summary>
    /// Service class for handling uploads for meter readings. 
    /// </summary>
    public interface IMeterReadingUploadService
    {
        /// <summary>
        /// Uploads meter readings and acquires number of successes and failures.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        MeterReadingUploadResult ProcessUpload(IFormFile file);
    }
}
