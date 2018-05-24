namespace MyBlog.Infrastructure
{
    /// <summary>
    /// Default settings defined in configuration file. This class is also used to gather data for not authenticated user
    /// </summary>
    public class DefaultUserData
    {
        public string Firstname {get; set;}

        public string Lastname {get; set;}

        public string Email {get; set;}

        public string Password {get; set;}

        public string AboutAuthor {get; set;}

        public string GithubProfile {get; set;}

        public string LinkedinProfile {get; set;}
    }
}