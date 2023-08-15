namespace HelloWorldApplication
{
    class HelloWorld
    {
        static async Task Main(string[] args)
        {
            var options = new RestClientOptions("http://93.177.102.155")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/client-users", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer 9d16142b510e7f998ecb0821ccb68835d568bcdfd3085ca5290a52911a328aa5fa25fc227a9dbb11a7fbe7e84461ed479aed990ce2e7145c5666bbdbb299a61e045d000f098331a7325cf9333d5b56160dd4c50ddcb3d435d7ccfc4997b313e421e698dd5d7251c30023d00bcd04b294416493592eae75795a6559e914145a7a");
            
            
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
        }
    }
}

