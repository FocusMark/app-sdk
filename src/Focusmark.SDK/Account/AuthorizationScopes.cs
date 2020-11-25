namespace FocusMark.SDK.Account
{
    public class AuthorizationScopes
    {
        public const string OpenId = "openid";

        // Project
        public const string ApiProjectWrite = "app.focusmark.api.project/project.write";
        public const string ApiProjectRead = "app.focusmark.api.project/project.read";
        public const string ApiProjectDelete = "app.focusmark.api.project/project.delete";

        // Tasks
        public const string ApiTaskWrite = "app.focusmark.api.task/task.write";
        public const string ApiTaskRead = "app.focusmark.api.task/task.read";
        public const string ApiTaskDelete = "app.focusmark.api.task/task.delete";

        /// <summary>
        /// Creates an array of string values representing all scopes.
        /// </summary>
        /// <returns></returns>
        public static string[] ToArray()
        {
            return new string[]
            {
                ApiProjectDelete, ApiProjectWrite, ApiProjectRead,
                ApiTaskDelete, ApiTaskWrite, ApiTaskRead,
                OpenId
            };
        }
    }
}
