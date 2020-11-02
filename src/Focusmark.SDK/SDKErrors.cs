namespace Focusmark.SDK
{

    public static class SDKErrors
    {
        // 401 errors related to account login and auth
        public class LoginFailed
        {
            public static int Code => 44011;
            public static string Message => "Account login failed.";
        }

        public class LoadTokensFailed
        {
            public static int Code => 44012;
            public static string Message => "Failed to resume previous user session";
        }

        // 500 errors related to unknown issues
        public class SaveTokenFailed
        {
            public static int Code => 45001;
            public static string Message => "Account login failed.";
        }

        public class DeleteTokenFailed
        {
            public static int Code => 45002;
            public static string Message => "Failed to log out of the user account cleanly";
        }
    }
}
