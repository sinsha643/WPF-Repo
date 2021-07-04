using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common;
using log4net;

namespace Services
{
    public class HttpClientAdapter : IHttpClientAdapter, IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed;
        protected readonly ILog Log;


        public HttpClientAdapter(ILog log, IConfigProvider configProvider)
        {
            Log = log;

            _httpClient = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    UseCookies = false,
                })
                { BaseAddress = configProvider.ServerEndpoint, Timeout = TimeSpan.FromMinutes(1.5) };

        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => _httpClient.SendAsync(request);

        public Task<HttpResponseMessage> PostAsync(string urlExtension, StringContent content) => _httpClient.PostAsync(urlExtension, content);

        #region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Log.Info("Disposing rest client ");
            // Cleanup
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose pattern
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    _httpClient?.Dispose();
                }
            }
            _disposed = true;
        }

        #endregion
    }
}