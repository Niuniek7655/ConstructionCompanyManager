namespace CCM.Constants
{
    public static class ConstantValues
    {
        public const string ControllerActionRoute = "[controller]/[action]";
        public const string ControllerRoute = "[controller]";
        public const string AccessMessageSection = "AccessMessage";
        public const string AccessClientName = "AccessClient";
        public const string HttpRequestBuilderDataSection = "HttpRequestBuilderData";
        public const string RequestBodyDeserializerDataSection = "RequestBodyDeserializerData";
        public const string BasicAccessSenderDataSection = "BasicAccessSenderData";
        public const string ErrorPath = "/Access/Error";
        public const string DefaultRouteName = "default";
        public const string DefaultRoutePattern = "{controller=Access}/{action=Start}/{id?}";

        public const string UserCookieName = "UserCookie";
        public const string AuthenticationPath = "/Access/Login";
        public const string AntiforgeryHeaderName = "__RequestVerificationToken";
        public const string AccessClientAddress = "https://localhost:44300/";
    }
}
