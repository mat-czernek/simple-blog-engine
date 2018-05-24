
using System;

namespace MyBlog.Services
{
    /// <summary>
    /// Class defines methods used to format post content
    /// </summary>
    public class FormatContent : IFormatContent
    {
        /// <summary>
        /// Converts Environment.NewLine into the HTML new line tag
        /// </summary>
        /// <param name="content">Content to be formatted</param>
        /// <returns>Retrns formatted content where new lines have been replaced by HTML new line tag</returns>
        public string NewLineToHTML(string content)
        {
            return content.Replace(Environment.NewLine, "<br/>");
        }

        /// <summary>
        /// Method formats content to make XML tags visible in HTML
        /// </summary>
        /// <param name="content">Content to be formated</param>
        /// <param name="blockStart">String which represents XML block start</param>
        /// <param name="blockEnd">tring which represents XML block end</param>
        /// <returns>Returns contnet with XML tags visible in HTML</returns>
        public string FormatXMLBlocks(string content, string blockStart, string blockEnd)
        {
            bool stopProcessing = false;
            int indexStart = 0;
            int indexEnd = 0;
            string rawData = string.Empty;
            
            string formatedContent = string.Empty;

            while(!stopProcessing)
            {
                // XML code block begin
                indexStart = content.IndexOf(blockStart);

                // XML code block end
                indexEnd = content.IndexOf(blockEnd);

                if(indexStart != -1 && indexEnd != -1)
                {
                    // extract string between blocks
                    rawData = content.Substring(indexStart + blockStart.Length, indexEnd - indexStart - blockStart.Length);

                    // replace XML nodes brackets
                    formatedContent = rawData.Replace("<", "&lt;");
                    formatedContent = formatedContent.Replace(">", "&gt;");

                    // trim new line characters at the start and at the end of the block
                    formatedContent = formatedContent.TrimStart('\r', '\n');
                    formatedContent = formatedContent.TrimEnd('\r', '\n');

                    // remove original content
                    content = content.Remove(indexStart, rawData.Length + blockStart.Length + blockEnd.Length);

                    // insert formated XML block
                    content = content.Insert(indexStart, formatedContent);     
                }
                else
                {
                    // no more block to process, stop
                    stopProcessing = true;
                }
            }

            return content;
        }
    }
}