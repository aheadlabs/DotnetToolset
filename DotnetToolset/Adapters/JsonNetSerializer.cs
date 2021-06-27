using RestSharp;
using RestSharp.Serialization;
using RestSharp.Serialization.Json;

namespace DotnetToolset.Adapters
{
	public class JsonNetSerializer: IRestSerializer
    {
        private JsonSerializer JsonSerializer { get; }

        public JsonNetSerializer()
        {
            JsonSerializer = new JsonSerializer();
        }
        public string ContentType { get; set; } = "application/json";

        public DataFormat DataFormat { get; } = DataFormat.Json;

        public string[] SupportedContentTypes { get; } = { "application/json", "text/json", "text/x-json", "text/javascript", "*+json" };

        public string Serialize(object @object) => JsonSerializer.Serialize(@object);

        public string Serialize(Parameter parameter) => JsonSerializer.Serialize(parameter.Value);

        public T Deserialize<T>(IRestResponse response) => JsonSerializer.Deserialize<T>(response);
    }
}
