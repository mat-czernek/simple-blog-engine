namespace MyBlog.Infrastructure
{
    /// <summary>
    /// Class defines operation result, status and optional message
    /// </summary>
    public class OperationResultStatus
    {
        public OperationResult Result {get; set;}

        public string Message {get; set;}
    }

}