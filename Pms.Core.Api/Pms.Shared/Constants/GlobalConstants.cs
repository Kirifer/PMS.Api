namespace Pms.Shared.Constants
{
    public static class GlobalConstant
    {
        public const string HttpHeaderAuthorization = "Authorization";

        public const string DocumentVersion = "v1";

        public const string DocumentTitle = "ITSquarehub Performance Management System API 1.0";

        public const string DocumentDescription = "Documentation for Performance Management System API.";

        public const string DocumentAuthor = "ITSquarehub";

        public const string HttpSchemeBearer = "Bearer";

        #region DB Search Setup
        public const int DefaultPageIndex = 1;

        public const int DefaultPageSize = 25;

        public const int MaximumPageSize = 1000;
        #endregion
    }
}
