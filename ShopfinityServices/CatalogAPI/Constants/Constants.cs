using static System.Net.WebRequestMethods;

namespace CatalogAPI.Constants
{
    public static class Constants
    {
        public static class AWS
        {
            public static class SQS
            {
                public const string QueueUrl = "https://sqs.us-east-1.amazonaws.com/463470979568/create-order";
            }
        }
    }
}
