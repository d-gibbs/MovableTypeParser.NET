[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("MovableTypeParser.Tests")]

namespace MovableTypeParser.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Models;
    using Utils;

    /// <summary>
    /// Parses movable type import files.
    /// </summary>
    public class MTIFParser
    {
        private readonly string _filePath;
        private readonly ParserUtils _utils;

        /// <summary>
        /// Initializes a new instance of the <see cref="MTIFParser"/> class for reading movable type formatted files.
        /// </summary>
        /// <param name="filePath">Path to the file to open.</param>
        public MTIFParser(string filePath)
        {
            _filePath = filePath;
            _utils = new ParserUtils();
        }

        /// <summary>
        /// Parses the provided file into an <see cref="IEnumerable{Post}">IEnumerable&lt;Post&gt;</see>.
        /// </summary>
        /// <returns>A collection of posts parsed from the provided file.</returns>
        public IEnumerable<Post> Parse()
        {
            using (var stream = File.OpenText(_filePath))
            {
                var post = new Post();

                string currentLine;
                while ((currentLine = stream.ReadLine()) != null)
                {
                    if (_utils.IsPostLine(currentLine))
                    {
                        ParseSingleLine(post, currentLine);
                    }
                    else if (_utils.IsMultiLine(currentLine))
                    {
                        ParseMultiLine(post, stream, currentLine);
                    }
                    else if (_utils.IsEndOfPost(currentLine))
                    {
                        yield return post;
                        post = new Post();
                    }
                }
            }
        }

        /// <summary>
        /// Parses a single line key/value pair from the current line and updates the post.
        /// </summary>
        /// <param name="post">Instance of the post.</param>
        /// <param name="currentLine">The line to be parsed.</param>
        private void ParseSingleLine(Post post, string currentLine)
        {
            var kvp = GetLineKeyValuePair(currentLine);

            if (kvp != null)
            {
                switch (kvp.Item1)
                {
                    case Constants.Keys.Title:

                        post.Title = kvp.Item2;

                        break;

                    case Constants.Keys.Basename:

                        post.Basename = kvp.Item2;

                        break;

                    case Constants.Keys.Author:

                        post.Author = kvp.Item2;

                        break;

                    case Constants.Keys.AuthorEmail:

                        post.AuthorEmail = kvp.Item2;

                        break;

                    case Constants.Keys.Date:

                        if (DateTime.TryParseExact(kvp.Item2, new string[] { "MM/dd/yyyy hh:mm:ss tt", "MM/dd/yyyy hh:mm:ss" }, null, DateTimeStyles.None, out DateTime date))
                        {
                            post.Date = date;
                        }

                        break;

                    case Constants.Keys.Status:

                        if (kvp.Item2.ToLowerInvariant().Equals("publish"))
                        {
                            post.PostStatus = Post.Status.Published;
                        }
                        else if (kvp.Item2.ToLowerInvariant().Equals("draft"))
                        {
                            post.PostStatus = Post.Status.Draft;
                        }

                        break;

                    case Constants.Keys.AllowComments:

                        post.AllowComments = kvp.Item2.Equals("1");

                        break;

                    case Constants.Keys.AllowPings:

                        post.AllowPings = kvp.Item2.Equals("1");

                        break;

                    case Constants.Keys.ConvertBreaks:

                        post.ConvertBreaks = kvp.Item2;

                        break;

                    case Constants.Keys.NoEntry:

                        post.NoEntry = kvp.Item2;

                        break;

                    case Constants.Keys.UniqueUrl:

                        post.UniqueUrl = kvp.Item2;

                        break;

                    case Constants.Keys.Category:

                        post.Categories.Add(kvp.Item2);

                        break;

                    case Constants.Keys.PrimaryCategory:

                        post.PrimaryCategory = kvp.Item2;

                        break;

                    case Constants.Keys.Tags:

                        if (!string.IsNullOrEmpty(kvp.Item2))
                        {
                            var tags = ParseTags(kvp.Item2);

                            if (tags != null && tags.Any())
                            {
                                post.Tags.AddRange(tags);
                            }
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Parses a multiline section and updates the post, and advances the stream to the next section.
        /// </summary>
        /// <param name="post">Instance of the post.</param>
        /// <param name="stream">The current stream reader instance.</param>
        /// <param name="currentLine">The current line being read from the stream, used to determine the section type.</param>
        private void ParseMultiLine(Post post, StreamReader stream, string currentLine)
        {
            if (currentLine.StartsWith($"{Constants.Keys.Body}:", StringComparison.OrdinalIgnoreCase))
            {
                post.Body = ReadStringSection(stream).Trim();
            }
            else if (currentLine.StartsWith($"{Constants.Keys.ExtendedBody}:", StringComparison.OrdinalIgnoreCase))
            {
                post.ExtendedBody = ReadStringSection(stream).Trim();
            }
            else if (currentLine.StartsWith($"{Constants.Keys.Excerpt}:", StringComparison.OrdinalIgnoreCase))
            {
                post.Excerpt = ReadStringSection(stream).Trim();
            }
            else if (currentLine.StartsWith($"{Constants.Keys.Keywords}:", StringComparison.OrdinalIgnoreCase))
            {
                post.Keywords = ReadStringSection(stream).Trim();
            }
            else if (currentLine.StartsWith($"{Constants.Keys.Comment}:", StringComparison.OrdinalIgnoreCase))
            {
                var comment = ParseComment(stream);

                if (comment != null)
                {
                    post.Comments.Add(comment);
                }
            }
            else if (currentLine.StartsWith($"{Constants.Keys.Ping}:", StringComparison.OrdinalIgnoreCase))
            {
                var ping = ParsePing(stream);

                if (ping != null)
                {
                    post.Pings.Add(ping);
                }
            }
        }

        /// <summary>
        /// Parses a comment from the stream and advances the stream position to the next section.
        /// </summary>
        /// <param name="stream">The current stream reader instance.</param>
        /// <returns>A comment associated with the current post being parsed.</returns>
        private Comment ParseComment(StreamReader stream)
        {
            var comment = new Comment();

            string currentLine;
            while ((currentLine = stream.ReadLine()) != null)
            {
                if (_utils.IsCommentLine(currentLine))
                {
                    // Empty checks are performed because the comment body could feasibly start with one of the reserved keys
                    // Since there is no section delimiter for the comment body we dont want data from the body to overwrite existing values

                    var kvp = GetLineKeyValuePair(currentLine);

                    if (kvp != null)
                    {
                        switch (kvp.Item1)
                        {
                            case Constants.Keys.Author:

                                if (string.IsNullOrEmpty(comment.Author))
                                {
                                    comment.Author = kvp.Item2;
                                }

                                break;

                            case Constants.Keys.Email:

                                if (string.IsNullOrEmpty(comment.Email))
                                {
                                    comment.Email = kvp.Item2;
                                }

                                break;

                            case Constants.Keys.Url:

                                if (string.IsNullOrEmpty(comment.Url))
                                {
                                    comment.Url = kvp.Item2;
                                }

                                break;

                            case Constants.Keys.Ip:

                                if (string.IsNullOrEmpty(comment.Ip))
                                {
                                    comment.Ip = kvp.Item2;
                                }

                                break;

                            case Constants.Keys.Date:

                                if (comment.Date == default(DateTime))
                                {
                                    if (DateTime.TryParseExact(kvp.Item2, new string[] { "MM/dd/yyyy hh:mm:ss tt", "MM/dd/yyyy hh:mm:ss" }, null, DateTimeStyles.None, out DateTime date))
                                    {
                                        comment.Date = date;
                                    }
                                }

                                break;
                        }
                    }
                }
                else if (!currentLine.Equals(Constants.Delimiters.EndOfPost) && !currentLine.Equals(Constants.Delimiters.EndOfSection))
                {
                    // Maintain the current line being read
                    comment.Body = string.Concat(currentLine, Environment.NewLine);

                    // Read the rest to section end as comment body
                    comment.Body += ReadStringSection(stream);
                    comment.Body = comment.Body.Trim();

                    break;
                }
                else
                {
                    break;
                }
            }

            return comment;
        }

        /// <summary>
        /// Parses a ping from the stream and advances the stream position to the next section.
        /// </summary>
        /// <param name="stream">The current stream reader instance.</param>
        /// <returns>A ping associated with the current post being parsed.</returns>
        private Ping ParsePing(StreamReader stream)
        {
            var ping = new Ping();

            string currentLine;
            while ((currentLine = stream.ReadLine()) != null)
            {
                if (_utils.IsPingLine(currentLine))
                {
                    // Empty checks are performed because the ping excerpt could feasibly start with one of the reserved keys
                    // Since there is no section delimiter for the ping excerpt we dont want data from the body to overwrite existing values

                    var kvp = GetLineKeyValuePair(currentLine);

                    if (kvp != null)
                    {
                        switch (kvp.Item1)
                        {
                            case Constants.Keys.Title:

                                if (string.IsNullOrEmpty(ping.Title))
                                {
                                    ping.Title = kvp.Item2;
                                }

                                break;

                            case Constants.Keys.Url:

                                if (string.IsNullOrEmpty(ping.Url))
                                {
                                    ping.Url = kvp.Item2;
                                }

                                break;

                            case Constants.Keys.Ip:

                                if (string.IsNullOrEmpty(ping.Ip))
                                {
                                    ping.Ip = kvp.Item2;
                                }

                                break;

                            case Constants.Keys.BlogName:

                                if (string.IsNullOrEmpty(ping.BlogName))
                                {
                                    ping.BlogName = kvp.Item2;
                                }

                                break;

                            case Constants.Keys.Date:

                                if (ping.Date == default(DateTime))
                                {
                                    if (DateTime.TryParseExact(kvp.Item2, new string[] { "MM/dd/yyyy hh:mm:ss tt", "MM/dd/yyyy hh:mm:ss" }, null, DateTimeStyles.None, out DateTime date))
                                    {
                                        ping.Date = date;
                                    }
                                }

                                break;
                        }
                    }
                }
                else if (!currentLine.Equals(Constants.Delimiters.EndOfPost) && !currentLine.Equals(Constants.Delimiters.EndOfSection))
                {
                    // Maintain the current line being read
                    ping.Excerpt = string.Concat(currentLine, Environment.NewLine);

                    // Read the rest to section end as ping excerpt
                    ping.Excerpt += ReadStringSection(stream);
                    ping.Excerpt = ping.Excerpt.Trim();

                    break;
                }
                else
                {
                    break;
                }
            }

            return ping;
        }

        /// <summary>
        /// Gets a collection of tags parsed from the posts 'Tags' value.
        /// </summary>
        /// <param name="tagsValue">The posts tags value.</param>
        /// <returns>A collection of tags parsed from the posts 'Tags' value.</returns>
        private HashSet<string> ParseTags(string tagsValue)
        {
            var match = Constants.RegularExpressions.Tag.Match(tagsValue);
            var tags = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

            foreach (var capture in match.Groups[1].Captures)
            {
                string str = capture.ToString().Trim(new char[] { '\"' }).Trim();
                tags.Add(str);
            }

            return tags;
        }

        /// <summary>
        /// Gets a key/value pair from the current line being read.
        /// </summary>
        /// <param name="currentLine">The current line being read.</param>
        /// <returns>A key/value pair parsed from the current line.</returns>
        private Tuple<string, string> GetLineKeyValuePair(string currentLine)
        {
            if (currentLine.Contains(Constants.Delimiters.KeyValue))
            {
                var values = currentLine.Split(new string[] { Constants.Delimiters.KeyValue }, StringSplitOptions.RemoveEmptyEntries);

                if (values != null && values.Length > 1)
                {
                    string key = values[0].Trim();
                    string value = string.Join(Constants.Delimiters.KeyValue, values.Skip(1)).Trim();

                    return new Tuple<string, string>(key, value);
                }
            }

            return null;
        }

        /// <summary>
        /// Reads a multiline string section and advances the stream position to the next section.
        /// </summary>
        /// <param name="stream">The current stream reader instance.</param>
        /// <returns>A multiline string section.</returns>
        private string ReadStringSection(StreamReader stream)
        {
            var buffer = new StringBuilder();
            string currentLine;
            while ((currentLine = stream.ReadLine()) != null &&
                   !_utils.IsEndOfSection(currentLine))
            {
                buffer.AppendLine(currentLine);
            }

            return buffer.ToString();
        }
    }
}
