namespace MyBlog.Infrastructure
{
    /// <summary>
    /// Interfece describes methods and properties required to seed database
    /// </summary>
    public interface ISeedDatabase
    {
        /// <summary>
        /// Method initalize database with default data
        /// </summary>
         void Initalize();
    }
}