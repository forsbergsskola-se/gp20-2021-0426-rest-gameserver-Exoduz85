namespace GitHubExplorer {
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