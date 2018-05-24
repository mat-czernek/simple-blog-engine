namespace MyBlog.Infrastructure
{
    /// <summary>
    /// Describes various operations result
    /// </summary>
    public enum OperationResult
    {
        InvalidUserName,

        EmailNotConfirmed,

        InvalidUserNamePassword,

        Success,

        Failure,

        InvalidUserId,

        PasswordsDontMatch
    }
}