namespace MovableTypeParser.Models
{
    using System;

    /// <summary>
    /// Class representing a comment against a post.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Gets or sets the author of the comment.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the comment authors email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the comment authors website URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the comment authors IP address.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Gets or sets the date and time that the comment was posted.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the comment body text.
        /// </summary>
        public string Body { get; set; }
    }
}
