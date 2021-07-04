using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Events;
using JetBrains.Annotations;
using log4net;
using Microsoft.Practices.Prism.Events;

namespace Services.RestClient
{
    public class RestClientImpl : IRestClient
    {
        #region fields

        private readonly ILog _log;
        private readonly IEventAggregator _eventAggregator;
        private readonly IHttpClientAdapter _httpClientAdapter;
        #endregion

        #region constructor

        /// <summary>
        /// RestClientImpl
        /// </summary>
        /// <param name="log"></param>
        /// <param name="eventAggregator"></param>
        /// <param name="httpClientAdapter"></param>
        public RestClientImpl(ILog log, IEventAggregator eventAggregator, IHttpClientAdapter httpClientAdapter)
        {
            _httpClientAdapter = httpClientAdapter;
            _log = log;
            _eventAggregator = eventAggregator;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="restParams"></param>
        /// <returns></returns>
        public async Task<string> GetAsync([NotNull] RestParams restParams)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, restParams.UrlWithParameters);
            request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            var response = new HttpResponseMessage();
            try
            {
                response = await _httpClientAdapter.SendAsync(request);
            }
            catch (TaskCanceledException ex)
            {
                _log.Error(ex.Message);
                // for intermittent connection issue findings
                _eventAggregator.GetEvent<ServerConnectivityLost>().Publish("Requested time out. Please try again.");
            }
            catch (Exception e)
            {
                _log.Error(e.InnerException);
                _eventAggregator.GetEvent<ServerConnectivityLost>().Publish("Could not establish server connection. Please try again.");
            }
            if (!response.IsSuccessStatusCode)
            {
                HandleRestfulException(response);
                return await Task.FromResult(string.Empty);
            }

            var data = string.Empty;
            //for intermittent connectivity issues, content 'll be null
            if (response.Content != null)
            { data = await response.Content.ReadAsStringAsync(); }

            return data;
        }

        /// <summary>
        /// PostAsync
        /// </summary>
        /// <param name="restParams"></param>
        /// <returns></returns>
        public async Task<string> PostAsync([NotNull] RestParams restParams)
        {
            var response = new HttpResponseMessage();
            try
            {
                var postContent = new StringContent(restParams.JsonContent, Encoding.UTF8, "application/json");
                response = await _httpClientAdapter.PostAsync(restParams.UrlWithParameters, postContent);
            }
            catch (TaskCanceledException ex)
            {
                _log.Error(ex.Message);
                // for intermittent connection issue findings
                _eventAggregator.GetEvent<ServerConnectivityLost>().Publish("Requested time out. Please try again.");
            }
            catch (Exception e)
            {
                _log.Error(e.InnerException);
                _eventAggregator.GetEvent<ServerConnectivityLost>().Publish("Could not establish server connection. Please try again.");
            }
            if (!response.IsSuccessStatusCode)
            {
                HandleRestfulException(response);
                return await Task.FromResult(string.Empty);
            }

            var data = string.Empty;
            //for intermittent connectivity issues, content 'll be null
            if (response.Content != null)
            { data = await response.Content.ReadAsStringAsync(); }

            return data;
        }


        #endregion

        #region private methods

        /// <summary>
        /// HandleRestfulException
        /// </summary>
        /// <param name="response"></param>
        private void HandleRestfulException(HttpResponseMessage response)
        {
            try
            {
                CheckAndNotifyServerCommError(response);
                _log.Error(response);
            }
            catch (ArgumentException ex)
            {
                if (response != null)
                {
                    LogException(ex, new Uri(response.RequestMessage?.RequestUri.OriginalString ?? throw new InvalidOperationException()));
                }
                if (response?.RequestMessage != null)
                {
                    throw new ArgumentException($"Error response received from {response.RequestMessage.RequestUri}");
                }
            }
        }

        /// <summary>
        /// Check And Notify Server Communication Error
        /// </summary>
        /// <param name="response"></param>
        private void CheckAndNotifyServerCommError(HttpResponseMessage response)
        {
            _eventAggregator.GetEvent<ServerConnectivityLost>().Publish(response?.ReasonPhrase);
        }

        /// <summary>
        /// LogException
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="url"></param>
        private void LogException(Exception ex, Uri url)
        {
            var exceptionsDetailsWithStacks =
            string.Join(Environment.NewLine, ex.Message, ex.StackTrace);
            _log.Error($"error while sending request {url} ,  {exceptionsDetailsWithStacks}");
        }

        #endregion
    }
}
