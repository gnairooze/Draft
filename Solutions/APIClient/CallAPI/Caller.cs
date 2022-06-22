using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CallAPI
{
    internal class Caller
    {
        private static readonly HttpClient client = new HttpClient();

        static Caller()
        {
            client.Timeout = TimeSpan.FromMinutes(2);
        }
        public static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var msg = await stringTask;
            Console.Write(msg);
        }

        public static async Task ProcessRepositories2()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");

            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

            if (repositories == null)
            {
                Console.WriteLine("no repos returned");
                return;
            }

            foreach (var repo in repositories)
            {
                Console.WriteLine(repo.Name);
            }
        }

        public static async Task<List<Repository>> ProcessRepositories3()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");

            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

            if (repositories == null)
            {
                return new List<Repository>();
            }

            return repositories;
        }

        public static async Task ProcessRepositories4()
        {
            var repositories = await ProcessRepositories3();

            if(repositories.Count == 0)
            {
                Console.WriteLine("no repos returned");
                return;
            }

            foreach (var repo in repositories)
            {
                Console.WriteLine(repo.Name);
                Console.WriteLine(repo.Description);
                Console.WriteLine(repo.GitHubHomeUrl);
                Console.WriteLine(repo.Homepage);
                Console.WriteLine(repo.Watchers);
                Console.WriteLine(repo.LastPush);
                Console.WriteLine();
            }
            
        }

        public static async Task ProcessPost1()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            
            var requestJson = @"{
    ""userId"": 1025,
    ""title"": ""testing post request - 2022062113120001"",
    ""body"": ""details of the request of 2022062113120001""
}";
            
            HttpContent httpContent = new StringContent(requestJson, Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://jsonplaceholder.typicode.com/posts", httpContent);

            Console.WriteLine($"IsSuccessStatusCode: {response.IsSuccessStatusCode}");
            Console.WriteLine($"StatusCode: {response.StatusCode}");
            Console.WriteLine($"RequestMessage: {response.RequestMessage?.Content?.ReadAsStringAsync().Result}");
            Console.WriteLine($"Content: {response.Content.ReadAsStringAsync().Result}");

        }
    }
}
