using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class RestParams : IDisposable
    {
        private readonly ILog _log;

        /// <summary>
        /// RestParams
        /// </summary>
        /// <param name="log"></param>
        /// <param name="urlExtension"></param>
        /// <param name="jsonContent"></param>
        /// <param name="parameters"></param>
        public RestParams(ILog log, string urlExtension, string jsonContent, Dictionary<string, string> parameters = null)
        {
            CallInitiatedAt = DateTime.Now;
            _log = log;

            UrlExtension = urlExtension;
            JsonContent = jsonContent;
            UrlWithParameters = urlExtension;
            if (parameters != null && parameters.Any())
            {
                var queryParams = string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"));
                UrlWithParameters = $"{UrlWithParameters}?{queryParams}";
            }

            // log server call initiated at
            _log.Info($"{UrlExtension}. call initiated at : {CallInitiatedAt}");

        }

        public string UrlExtension { get; }
        public string UrlWithParameters { get; }
        public string JsonContent { get; }

        public DateTime CallInitiatedAt { get; }
        /// <summary>
        /// log service execution time lapse
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            var responseTime = (DateTime.Now - CallInitiatedAt).TotalMilliseconds;
            _log.Info($"{UrlExtension}. loading took: {responseTime:N0}ms");
        }
    }
}
