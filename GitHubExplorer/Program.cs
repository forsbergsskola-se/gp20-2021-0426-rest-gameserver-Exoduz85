using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GitHubExplorer
{
    static class Program {
        static string userName;
        static void Main(string[] args) {
            while (true) {
                var client = new HttpClient
                {
                    DefaultRequestHeaders =
                    {
                        Accept = {new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")},
                        UserAgent = {ProductInfoHeaderValue.Parse("request")},
                        Authorization = new AuthenticationHeaderValue("token", Secret.token)
                    },
                    BaseAddress = new Uri("https://api.github.com/users/")
                };
                Console.WriteLine("Hello, what user would you like to browse?");
                userName = Console.ReadLine();
                
                var response = client.GetStringAsync(client.BaseAddress + userName);
                User user = JsonSerializer.Deserialize<User>(response.Result);
                Console.WriteLine($"{user.name}, {user.location}.\n");
                
                Console.WriteLine("What would you like to see next? \n(0) Followers\n(1) Organizations\n(2) Repositories\n(3) Quit\n");
                var userSelect = int.TryParse(Console.ReadLine(), out var selection);
                
                if (userSelect) {
                    switch (selection) {
                        case 0:
                            Console.WriteLine($"Number of followers: {user.followers}.");
                            break;
                        case 1:
                            Console.WriteLine($"{user.name} is in organizations: {user.organizations_url}.");
                            break;
                        case 2:
                            var repos = client.GetStringAsync(client.BaseAddress + userName + "/repos");
                            List<Repository> repositories = JsonSerializer.Deserialize<List<Repository>>(repos.Result);
                            foreach (var repo in repositories) {
                                Console.WriteLine($"Repository name: {repo.name}, id: {repo.id}");
                            }
                            break;
                        case 3:
                            return;
                    }
                }
                else {
                    client.Dispose();
                    Console.WriteLine("Not a valid input!");
                    return;
                }
            }
        }
    }
}