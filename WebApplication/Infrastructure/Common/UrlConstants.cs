namespace WebApplication.Infrastructure.Common
{
    public static class UrlConstants
    {
        public static string ImagePostfix { get; private set; } = "-images";

        public static string AccessDeniedUri { get; private set; } = "/Account/AccessDenied";

        public static string InternalErrorUri { get; private set; } = "/Home/InternalErrorHandler";

        public static string HomeUri { get; private set; } = "/Home/Index";
    }
}