using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEKTest.Services
{
    /// <summary>
    /// Service for uploading items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUploadService<T>
    {
        /// <summary>
        /// Check if item can be uploaded. Returns true if possible.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool CanUpload(T item);

        /// <summary>
        /// Attempts to upload a item. Returns true if successful.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Upload(T item);
    }
}
