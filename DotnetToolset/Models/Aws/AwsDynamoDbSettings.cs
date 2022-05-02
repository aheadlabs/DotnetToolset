namespace DotnetToolset.Models.Aws
{
    /// <summary>
    /// AWS DynamoDB settings mappings
    /// </summary>
    public class AwsDynamoDbSettings
    {
        /// <summary>
        /// Represents the EndPoint to connect DynamoDb service. Must be indicated when the
        /// environment is a local development environment (typically a localhost port)
        /// </summary>
        public string EndPoint { get; set; }
    }
}
