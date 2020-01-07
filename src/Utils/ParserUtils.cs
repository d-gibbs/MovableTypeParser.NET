[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("MovableTypeParser.Tests")]

namespace MovableTypeParser.Utils
{
    using System;
    using System.Linq;

    /// <summary>
    /// Parser utility class.
    /// </summary>
    internal class ParserUtils
    {
        /// <summary>
        /// Determines whether the current line being read is a valid 'Post' line.
        /// </summary>
        /// <param name="currentLine">The current line being read.</param>
        /// <returns>A value indicating whether the current line being read is valid for a 'Post'.</returns>
        public bool IsPostLine(string currentLine)
        {
            return Constants.Keys.PostKeys.Any(x => currentLine.StartsWith($"{x}:", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Determines whether the current line being read is a valid 'Comment' line.
        /// </summary>
        /// <param name="currentLine">The current line being read.</param>
        /// <returns>A value indicating whether the current line being read is valid for a 'Comment'.</returns>
        public bool IsCommentLine(string currentLine)
        {
            return Constants.Keys.CommentKeys.Any(x => currentLine.StartsWith($"{x}:", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Determines whether the current line being read is a valid 'Ping' line.
        /// </summary>
        /// <param name="currentLine">The current line being read.</param>
        /// <returns>A value indicating whether the current line being read is valid for a 'Ping'.</returns>
        public bool IsPingLine(string currentLine)
        {
            return Constants.Keys.PingKeys.Any(x => currentLine.StartsWith($"{x}:", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Determines whether the current line being read is a valid multiline section.
        /// </summary>
        /// <param name="currentLine">The current line being read.</param>
        /// <returns>A value indicating whether the current line being read is valid for a multiline section.</returns>
        public bool IsMultiLine(string currentLine)
        {
            return Constants.Keys.MultiLineKeys.Any(x => currentLine.StartsWith($"{x}:", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Determines whether the current line being read is an 'End of Section' delimiter.
        /// </summary>
        /// <param name="currentLine">The current line being read.</param>
        /// <returns>A value indicating whether the current line being read is an 'End of Section' delimiter..</returns>
        public bool IsEndOfSection(string currentLine)
        {
            return currentLine.Equals(Constants.Delimiters.EndOfSection, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines whether the current line being read is an 'End of Post' delimiter.
        /// </summary>
        /// <param name="currentLine">The current line being read.</param>
        /// <returns>A value indicating whether the current line being read is an 'End of Post' delimiter..</returns>
        public bool IsEndOfPost(string currentLine)
        {
            return currentLine.Equals(Constants.Delimiters.EndOfPost, StringComparison.OrdinalIgnoreCase);
        }
    }
}
