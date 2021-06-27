using RestSharp;
using System.Collections.Generic;

namespace DotnetToolset.Interfaces
{
	public interface IHttpVerbs<T>
    {
        /// <summary>
        /// GET HTTP verb
        /// </summary>
        /// <param name="resource">URI for the GET request</param>
        /// <param name="segments">Key-value pairs for each segment in the resource</param>
        /// <param name="parameters">Key-value pairs for each parameter in the querystring</param>
        /// <param name="headers">Key-value pairs for each header in the querystring</param>
        /// <param name="dataFormat">Response data format, JSON by default</param>
        /// <returns></returns>
        IRestResponse Get(string resource,
            Dictionary<string, string> segments = null,
            Dictionary<string, string> parameters = null,
            Dictionary<string, string> headers = null,
            DataFormat dataFormat = DataFormat.Json);

        /// <summary>
        /// GET HTTP verb
        /// </summary>
        /// <typeparam name="T">Object to parse the result as</typeparam>
        /// <param name="resource">URI for the GET request</param>
        /// <param name="segments">Key-value pairs for each segment in the resource</param>
        /// <param name="parameters">Key-value pairs for each parameter in the querystring</param>
        /// <param name="headers">Key-value pairs for each header in the querystring</param>
        /// <param name="dataFormat">Response data format, JSON by default</param>
        /// <returns></returns>
        IRestResponse<T> GetT(string resource, 
            Dictionary<string, string> segments = null, 
            Dictionary<string, string> parameters = null, 
            Dictionary<string, string> headers = null,
            DataFormat dataFormat = DataFormat.Json);

        /// <summary>
        /// POST HTTP verb parsed as TResponse
        /// </summary>
        /// <typeparam name="T">Object to parse the result as</typeparam>
        /// <param name="resource">URI for the POST request</param>
        /// <param name="body">Body data to POST</param>
        /// <param name="segments">Key-value pairs for each segment in the resource</param>
        /// <param name="parameters">Key-value pairs for each parameter in the querystring</param>
        /// <param name="headers">Key-value pairs for each header in the querystring</param>
        IRestResponse<T> Post(string resource, object body,
            Dictionary<string, string> segments = null, 
            Dictionary<string, string> parameters = null, 
            Dictionary<string, string> headers = null);

        /// <summary>
        /// POST HTTP verb parsed as TResponse
        /// </summary>
        /// <typeparam name="T">Object to parse the result as</typeparam>
        /// <param name="resource">URI for the PUT request</param>
        /// <param name="body">Body data to PUT</param>
        /// <param name="segments">Key-value pairs for each segment in the resource</param>
        /// <param name="parameters">Key-value pairs for each parameter in the querystring</param>
        /// <param name="headers">Key-value pairs for each header in the querystring</param>
        IRestResponse<T> Put(string resource, object body,
            Dictionary<string, string> segments = null, 
            Dictionary<string, string> parameters = null, 
            Dictionary<string, string> headers = null);

        /// <summary>
        /// DELETE HTTP verb parsed as TResponse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource">URI for the DELETE request</param>
        /// <param name="body">Body data to DELETE</param>
        /// <param name="segments">Key-value pairs for each segment in the resource</param>
        /// <param name="parameters">Key-value pairs for each parameter in the querystring</param>
        /// <param name="headers">Key-value pairs for each header in the querystring</param>
        /// <returns></returns>
        IRestResponse<T> Delete(string resource, object body,
            Dictionary<string, string> segments = null, 
            Dictionary<string, string> parameters = null, 
            Dictionary<string, string> headers = null);
    }
}
