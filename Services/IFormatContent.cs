namespace MyBlog.Services
{
    /// <summary>
    /// Interface defines methods used to format post content
    /// </summary>
    public interface IFormatContent
    {
        /// <summary>
        /// Converts Environment.NewLine into the HTML new line tag
        /// </summary>
        /// <param name="content">Content to be formatted</param>
        /// <returns>Retrns formatted content where new lines have been replaced by HTML new line tag</returns>
        string FormatXMLBlocks(string content, string blockStart, string blockEnd);

        /// <summary>
        /// Method formats content to make XML tags visible in HTML
        /// </summary>
        /// <param name="content">Content to be formated</param>
        /// <param name="blockStart">String which represents XML block start</param>
        /// <param name="blockEnd">tring which represents XML block end</param>
        /// <returns>Returns contnet with XML tags visible in HTML</returns>
        string NewLineToHTML(string content);
    }
}