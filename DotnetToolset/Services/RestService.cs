using DotnetToolset.Adapters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Res = DotnetToolset.Resources.Literals;

namespace DotnetToolset.Services
{
	public class RestService<T> : IRestService<T> where T : new()
    {
        private IRestClient _restClient;
        private string _baseUrl;
        private static readonly string _textPlainMimeType = "text/plain";

        /// <inheritdoc />
        public string BaseUrl
        {
            get => _baseUrl;
            set
            {
                _baseUrl = value;

                // Create a REST client
                _restClient = new RestClient(_baseUrl);
            }
        }

        #region Configuration

        /// <summary>
        /// Checks if the REST client is ready to be called
        /// </summary>
        /// <returns>True if it is ready</returns>
        private void CheckIfClientIsReady()
        {
            if (_baseUrl == null || _restClient == null)
            {
                throw new InvalidOperationException(Res.b_RestClientNotReadySetBaseUrl);
            }
        }

        private void AddAcceptTextPlainToClient() => _restClient.DefaultParameters[0].Value += $", {_textPlainMimeType}";


        /// <summary>
        /// Creates a REST request
        /// </summary>
        /// <param name="resource">URI for the GET request</param>
        /// <param name="segments">Key-value pairs for each {param} in the resource</param>
        /// <param name="parameters">Key-value pairs for each ?param in the querystring</param>
        /// <param name="headers">Key-value pairs for each ?param in the querystring</param>
        /// <param name="dataFormat">Data format for the request</param>
        /// <param name="method">HTTP Method/Verb used on this request</param>
        /// <returns></returns>
        private IRestRequest CreateRestRequest(
            string resource,
            Dictionary<string, string> segments, 
            Dictionary<string, string> parameters, 
            Dictionary<string, string> headers,
            DataFormat dataFormat = DataFormat.Json,
            Method method = Method.GET )
        {
            CheckIfClientIsReady();
            AddAcceptTextPlainToClient();

            IRestRequest restRequest = new RestRequest(resource, method, dataFormat);

            // Add segments
            AddSegments(ref restRequest, segments);

            // Add parameters
            AddParameters(ref restRequest, parameters);

            // Add headers
            AddHeaders(ref restRequest, headers);

            return restRequest;
        }

        private void AddSegments(ref IRestRequest restRequest, Dictionary<string, string> segments)
        {
            if (segments != null)
            {
                foreach (KeyValuePair<string, string> segment in segments)
                {
                    restRequest.AddUrlSegment(segment.Key, segment.Value);
                }
            }
        }

        private void AddParameters(ref IRestRequest restRequest, Dictionary<string, string> parameters)
        {
            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    restRequest.AddParameter(parameter.Key, parameter.Value);
                }
            }
        }

        private void AddHeaders(ref IRestRequest restRequest, Dictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    restRequest.AddHeader(header.Key, header.Value);
                }
            }
        }

        private void AddBody(ref IRestRequest restRequest, object body)
        {
            if (body != null)
            {
                restRequest.AddParameter("text/plain", body, ParameterType.RequestBody);
            }
        }

        #endregion Configuration

        #region Download

        /// <inheritdoc />
        public MemoryStream Download(
            string resource,
            Dictionary<string, string> segments = null,
            Dictionary<string, string> parameters = null,
            Dictionary<string, string> headers = null)
        {
            MemoryStream response = new MemoryStream();
            IRestRequest request = CreateRestRequest(resource, null, null, null, DataFormat.None);
            request.ResponseWriter = responseStream => responseStream.CopyTo(response);
            _restClient.DownloadData(request); // This line is doing the download, memory will be incremented here!
            return response;
        }

        #endregion Download

        #region Get

        /// <inheritdoc />
        public XDocument GetXml(
            string resource, 
            Dictionary<string, string> segments = null, 
            Dictionary<string, string> parameters = null, 
            Dictionary<string, string> headers = null)
        {
            IRestResponse restResponse = Get(resource, segments, parameters, headers, DataFormat.Xml);
            return XDocument.Parse(restResponse.Content);
        }

        public IRestResponse Get(
            string resource,
            Dictionary<string, string> segments = null,
            Dictionary<string, string> parameters = null,
            Dictionary<string, string> headers = null,
            DataFormat dataFormat = DataFormat.Json)
        {
            // Create the request
            IRestRequest restRequest = CreateRestRequest(resource, segments, parameters, headers, dataFormat);

            // Execute the request and return
            IRestResponse restResponse = _restClient.Get(restRequest);

            return restResponse;
        }

        /// <inheritdoc />
        public IRestResponse<T> GetT(
            string resource, 
            Dictionary<string, string> segments = null, 
            Dictionary<string, string> parameters = null, 
            Dictionary<string, string> headers = null,
            DataFormat dataFormat = DataFormat.Json)
        {
            // Create the request
            IRestRequest restRequest = CreateRestRequest(resource, segments, parameters, headers, dataFormat);

            // Execute the request and return
            IRestResponse<T> restResponse = _restClient.Get<T>(restRequest);

            return restResponse;
        }

        #endregion Get

        #region Post

        /// <inheritdoc />
        public IRestResponse<T> Post(
            string resource, 
            object body,
            Dictionary<string, string> segments = null, 
            Dictionary<string, string> parameters = null, 
            Dictionary<string, string> headers = null)
        {
            IRestRequest request =
                CreateRestRequest(resource, segments, parameters, headers, DataFormat.Json, Method.POST);

            // Adding body to the request
            JsonNetSerializer jsonSerializer = new JsonNetSerializer();
            string jsonToSend = jsonSerializer.Serialize(body);
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);

            return _restClient.Execute<T>(request);
        }

        #endregion Post

        #region Put

        /// <inheritdoc />
        public IRestResponse<T> Put(
            string resource,
            object body,
            Dictionary<string, string> segments = null,
            Dictionary<string, string> parameters = null,
            Dictionary<string, string> headers = null)
        {
            IRestRequest request =
                CreateRestRequest(resource, segments, parameters, headers, DataFormat.Json, Method.PUT);
            
            // JSON to put
            JsonNetSerializer jsonSerializer = new JsonNetSerializer();
            string jsonToSend = jsonSerializer.Serialize(body);
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);

            return _restClient.Execute<T>(request);
        }

        #endregion Put

        #region Delete

        /// <inheritdoc />
        public IRestResponse<T> Delete(
            string resource, 
            object body, 
            Dictionary<string, string> segments = null,
            Dictionary<string, string> parameters = null,
            Dictionary<string, string> headers = null)
        {
            IRestRequest restRequest = CreateRestRequest(resource, segments, parameters, headers, DataFormat.Json, Method.DELETE);

            IRestResponse<T> restResponse = _restClient.Execute<T>(restRequest);
            return restResponse;
        }

        #endregion Delete
    }

    // TODO: Unit tests pending
}
