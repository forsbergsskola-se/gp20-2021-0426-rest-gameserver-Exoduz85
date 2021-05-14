using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GitHubExplorer
{
    public class User {
        public string login{get;set;}
        public int id {get; set;}
        public string node_id {get; set;}
        public string avatar_url {get; set;}
        public string gravatar_id {get; set;}
        public string url {get; set;}
        public string html_url {get; set;}
        public string followers_url {get; set;}
        public string following_url {get; set;}
        public string gists_url {get; set;}
        public string starred_url {get; set;}
        public string subscriptions_url {get; set;}
        public string organizations_url {get; set;}
        public string repos_url {get; set;} 
        public string events_url {get; set;}
        public string received_events_url {get; set;}
        public string type {get; set;}
        public bool site_admin {get; set;}
        public string name {get; set;}
        public string company {get; set;}
        public string blog {get; set;}
        public string location {get; set;}
        public string email {get; set;}
        public string bio {get; set;}
        public string twitter_username {get; set;}
        public int public_repos {get; set;}
        public int public_gists {get; set;}
        public int followers {get; set;}
        public int following {get; set;}
        public string created_at {get; set;}
        public string updated_at {get; set;}
    }
    static class Program {
        static string userName;
        const string baseUri = "https://api.github.com/users/";
        static void Main(string[] args) {
            while (true) {
                var client = new HttpClient
                {
                    DefaultRequestHeaders =
                    {
                        Accept = {new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")},
                        UserAgent = {ProductInfoHeaderValue.Parse("request")},
                        Authorization = new AuthenticationHeaderValue("token", Secret.token)
                    }
                };
                Console.WriteLine("Hello, what user would you like to browse?");
                userName = Console.ReadLine();
                client.BaseAddress = new Uri(baseUri + userName);
                var response = client.GetStringAsync(client.BaseAddress);
                User user = JsonSerializer.Deserialize<User>(response.Result);
                Console.WriteLine($"{user.name}, {user.location}.");
                
                Console.WriteLine("What would you like to see next? \n(0) Followers\n(1) Organizations\n(2) Repositories\n(3) Quit");
                var userSelect = int.TryParse(Console.ReadLine(), out var selection);
                if (userSelect) {
                    switch (selection) {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            return;
                    }
                }
                else {
                    client.Dispose();
                    Console.WriteLine("Not a valid input!");
                    break;
                }
            }
        }
    }
}
