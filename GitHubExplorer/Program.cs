using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GitHubExplorer
{
    static class Program {
        static string userName;
        static string host = "https://api.github.com";
        static void Main(string[] args) {
            while (true) {
                var uri = host.StringUri("users/");
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
                Console.WriteLine($"{user.name} from {user.location}.\n");
                
                Console.WriteLine("What would you like to see next? \n(0) Followers\n(1) Organizations\n(2) Repositories\n(3) Quit\n");
                var userSelect = int.TryParse(Console.ReadLine(), out var selection);
                
                if (userSelect) {
                    switch (selection) {
                        case 0:
                            Console.WriteLine($"Number of followers: {user.followers}.");
                            break;
                        case 1:
                            var org = client.GetStringAsync(user.organizations_url);
                            List<Organization> organizations = JsonSerializer.Deserialize<List<Organization>>(org.Result);
                            foreach (var organization in organizations) {
                                Console.WriteLine($"{user.name}'s organizations:\nOrg. gitname: {organization.login}.\nOrg. url: {organization.url}" +
                                                  $"\nDescription: {organization.description}\n");
                                var getMembers = client.GetStringAsync(organization.url + "/members");
                                List<Member> members = JsonSerializer.Deserialize<List<Member>>(getMembers.Result);
                                
                                // todo, ask if user wants to see all members.
                                foreach (var member in members) {
                                    Console.WriteLine($"Member: {member.login}\nGithub html page: {member.html_url}\n");
                                }
                            }
                            break;
                        case 2:
                            var repos = client.GetStringAsync(user.repos_url);
                            List<Repository> repositories = JsonSerializer.Deserialize<List<Repository>>(repos.Result);
                            foreach (var repo in repositories) {
                                Console.WriteLine($"Repository name: {repo.name}, id: {repo.id}\n");
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
        static UriBuilder StringUri(this string host, string toPath) {
            return new UriBuilder() {
                Host = host,
                Path = toPath,
                Scheme = "Https"
            };
        }
    }
    public class Member {
        public string login { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
    }
    public class Organization {
        public string login { get; set; }
        public int id { get; set; }
        public string url { get; set; }
        public string repos_url { get; set; }
        public string members_url { get; set; }
        public string public_members_url { set; get; }
        public string description { set; get; }
    }
}