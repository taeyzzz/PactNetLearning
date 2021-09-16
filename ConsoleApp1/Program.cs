using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUri = "http://localhost:5000";
            
            var result = ConsumerApiClient.ValidateDateTimeUsingProviderApi(baseUri).GetAwaiter().GetResult();
            var resultContentText = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Console.WriteLine(resultContentText);
        }
    }
}