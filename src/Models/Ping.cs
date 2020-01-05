namespace MovableTypeParser.Models
{
    using System;

    /// <summary>
    /// Class representing a trackback ping against a post.
    /// </summary>
    public class Ping
    {
        /// <summary>
        /// Gets or sets the ping title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL to the original entry.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the server that sent the ping.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Gets or sets the name of the blog from which the ping was sent.
        /// </summary>
        public string BlogName { get; set; }

        /// <summary>
        /// Gets or sets the date and time that the ping was sent.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the ping excerpt text.
        /// </summary>
        public string Excerpt { get; set; }
    }
}
