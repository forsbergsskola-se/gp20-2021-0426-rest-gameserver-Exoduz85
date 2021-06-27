using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GitHubExplorer
{
    static class Program {
        static string userName;
        static string host = "api.github.com";
        static int counter = 0;
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
                    BaseAddress = uri.Uri
                };
                Console.WriteLine("Hello, what user would you like to browse?");
                userName = Console.ReadLine();
                
                var response = client.GetStringAsync(client.BaseAddress + userName);
                User user = JsonSerializer.Deserialize<User>(response.Result);
                Console.WriteLine($"{user.name} from {user.location}.\n");
                
                continueBrowsingUser:
                
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
                                askAgain:
                                Console.WriteLine("Do you want to list the organizations members? \n(1) Yes\n(2) No\n");
                                var orgMemberDisplay = int.TryParse(Console.ReadLine(), out var select);
                                if (!orgMemberDisplay) {
                                    Console.WriteLine("Invalid input");
                                    goto askAgain;
                                }
                                if (select == 2) break;
                                var getMembers = client.GetStringAsync(organization.url + "/members");
                                List<Member> members = JsonSerializer.Deserialize<List<Member>>(getMembers.Result);
                                foreach (var member in members) {
                                    Console.WriteLine($"Member: {member.login}\nGithub html page: {member.html_url}\n");
                                }
                                //ask if user wants to visit any of the members.
                            }
                            break;
                        case 2: // users repositories, add possibility to open repo and add issue.
                            var repos = client.GetStringAsync(user.repos_url);
                            List<Repository> repositories = JsonSerializer.Deserialize<List<Repository>>(repos.Result);
                            Dictionary<int, Repository> asDic = new Dictionary<int, Repository>();
                            counter = 0;
                            foreach (var repo in repositories) {
                                asDic.Add(counter, repo);
                                Console.WriteLine($"[{counter++}] Repo: {repo.name}, id: {repo.id}\n");
                            }
                            Console.WriteLine("Which repository do you want to explore?");
                            if (int.TryParse(Console.ReadLine(), out var answer)) {
                                // load the repo here.
                                var repo = client.GetStringAsync(asDic[answer].full_name); // change to correct thing here..
                                Console.WriteLine(repo.Result);
                            }
                            
                            break;
                        case 3:
                            return;
                    }
                    invalidInput:
                    Console.WriteLine($"Do you want to continue browsing {user.name}?\n(1) Yes\n(2) No\n");
                    var continueBrowse = int.TryParse(Console.ReadLine(), out var browse);
                    if (!continueBrowse) {
                        Console.WriteLine("Invalid input");
                        goto invalidInput;
                    }
                    if(browse == 1) goto continueBrowsingUser;
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
}