namespace MovableTypeParser
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Movable Type Parser constants.
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Regular expressions used by the tool.
        /// </summary>
        public static class RegularExpressions
        {
            public static readonly Regex Tag = new Regex("^(?:[ ,]*(\"[^\"]*\"|[^ ,]+))*[ ,]*$", RegexOptions.Compiled);
        }

        /// <summary>
        /// Delimiter utility constants.
        /// </summary>
        public static class Delimiters
        {
            public const string EndOfPost = "--------";
            public const string EndOfSection = "-----";
            public const string KeyValue = ":";
        }

        /// <summary>
        /// Keys used by the file to denote a particular piece of data.
        /// </summary>
        public static class Keys
        {
            public const string Title = "TITLE";
            public const string Basename = "BASENAME";
            public const string Author = "AUTHOR";
            public const string AuthorEmail = "AUTHOR EMAIL";
            public const string Date = "DATE";
            public const string Status = "STATUS";
            public const string AllowComments = "ALLOW COMMENTS";
            public const string AllowPings = "ALLOW PINGS";
            public const string ConvertBreaks = "CONVERT BREAKS";
            public const string NoEntry = "NO ENTRY";
            public const string UniqueUrl = "UNIQUE URL";
            public const string Category = "CATEGORY";
            public const string PrimaryCategory = "PRIMARY CATEGORY";
            public const string Tags = "TAGS";
            public const string Body = "BODY";
            public const string ExtendedBody = "EXTENDED BODY";
            public const string Excerpt = "EXCERPT";
            public const string Keywords = "KEYWORDS";
            public const string Comment = "COMMENT";
            public const string Ping = "PING";
            public const string Email = "EMAIL";
            public const string Url = "URL";
            public const string Ip = "IP";
            public const string BlogName = "BLOG NAME";

            /// <summary>
            /// Valid keys for a 'Post'.
            /// </summary>
            public static string[] PostKeys = new string[]
            {
                Title,
                Basename,
                Author,
                AuthorEmail,
                Date,
                Status,
                AllowComments,
                AllowPings,
                ConvertBreaks,
                NoEntry,
                UniqueUrl,
                Category,
                PrimaryCategory,
                Tags
            };

            /// <summary>
            /// Valid keys for a 'Comment'.
            /// </summary>
            public static string[] CommentKeys = new string[]
            {
                Author,
                Email,
                Url,
                Ip,
                Date
            };

            /// <summary>
            /// Valid keys for a 'Ping'.
            /// </summary>
            public static string[] PingKeys = new string[]
            {
                Title,
                Url,
                Ip,
                BlogName,
                Date
            };

            /// <summary>
            /// Valid keys for data which spans multiple lines.
            /// </summary>
            public static string[] MultiLineKeys = new string[]
            {
                Body,
                ExtendedBody,
                Excerpt,
                Keywords,
                Comment,
                Ping
            };
        }
    }
}
