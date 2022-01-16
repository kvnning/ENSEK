using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEKTest.Services
{
    /// <summary>
    /// Helper service for parsing types.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IParserService<TSource, TOutput>
    {
        /// <summary>
        /// Attempt to parse source and returns number of failed parses in numberOFFailures. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="numberOfFailures"></param>
        /// <returns></returns>
        TOutput Read(TSource source, out int numberOfFailures);
    }
}
