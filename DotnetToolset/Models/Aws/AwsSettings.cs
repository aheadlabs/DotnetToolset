namespace DotnetToolset.Models.Aws
{
    /// <summary>
    /// AWS settings mappings
    /// </summary>
    public class AwsSettings
    {
        /// <summary>
        /// The region code used by default.
        /// </summary>
        /// <see>https://docs.aws.amazon.com/general/latest/gr/rande.html</see>
        public string DefaultRegion { get; set; }

        /// <summary>
        /// AWS S3 service settings
        /// </summary>
        public AwsS3Settings S3 { get; set; }

        /// <summary>
        /// AWS SES service settings
        /// </summary>
        public AwsSesSettings Ses { get; set; }

        /// <summary>
        /// Holds the DynamoDb settings
        /// </summary>
        public AwsDynamoDbSettings DynamoDb { get; set; }
    }
}
