using DotnetToolset.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace DotnetToolset.Services
{
	public interface IRestService<TResponse> : IHttpVerbs<TResponse>
    {
        /// <summary>
        /// Base URL for REST client requests
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Gets a XML from a REST resource
        /// </summary>
        /// <param name="resource">URI for the GET request</param>
        /// <param name="segments">Key-value pairs for each segment in the resource</param>
        /// <param name="parameters">Key-value pairs for each parameter in the querystring</param>
        /// <param name="headers">Key-value pairs for each header in the querystring</param>
        /// <returns>XML object</returns>
        XDocument GetXml(string resource, 
            Dictionary<string, string> segments = null, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null);

        /// <summary>
        /// Downloads a resource from a REST resource
        /// </summary>
        /// <param name="resource">URI for the Download resource</param>
        /// <param name="segments">Key-value pairs for each segment in the resource</param>
        /// <param name="parameters">Key-value pairs for each parameter in the querystring</param>
        /// <param name="headers">Key-value pairs for each header in the querystring</param>
        /// <returns>MemoryStream of the data Downloaded</returns>
        MemoryStream Download(string resource,
            Dictionary<string, string> segments = null, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null);
    }
}
