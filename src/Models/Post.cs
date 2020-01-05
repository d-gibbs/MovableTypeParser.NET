namespace MovableTypeParser.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class representing a complete post with comments and pings.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Post"/> class.
        /// </summary>
        public Post()
        {
            Tags = new List<string>();
            Categories = new List<string>();
            Comments = new List<Comment>();
            Pings = new List<Ping>();
        }

        /// <summary>
        /// Enum defining the post statuses.
        /// </summary>
        public enum Status
        {
            /// <summary>
            /// Undefined status - not set.
            /// </summary>
            Undefined,

            /// <summary>
            /// The post is in draft status.
            /// </summary>
            Draft,

            /// <summary>
            /// The post is published.
            /// </summary>
            Published
        }

        /// <summary>
        /// Gets or sets the post author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the post authors email address.
        /// </summary>
        public string AuthorEmail { get; set; }

        /// <summary>
        /// Gets or sets the title of the post.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the authored on or published date of the post.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the unique 'Base name' value for this post.
        /// </summary>
        public string Basename { get; set; }

        /// <summary>
        /// Gets or sets the post status.
        /// </summary>
        public Status PostStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether comments are allowed for this post.
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether pings are allowed for this post.
        /// </summary>
        public bool AllowPings { get; set; }

        /// <summary>
        /// Gets or sets the 'Convert breaks' value for this post.
        /// </summary>
        public string ConvertBreaks { get; set; }

        /// <summary>
        /// Gets or sets the 'No entry' value for this post.
        /// A special key used when importing data from a system where you have already imported all of the posts but not the comments.
        /// </summary>
        public string NoEntry { get; set; }

        /// <summary>
        /// Gets or sets the post body text.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the post extended body text.
        /// </summary>
        public string ExtendedBody { get; set; }

        /// <summary>
        /// Gets or sets the post excerpt.
        /// </summary>
        public string Excerpt { get; set; }

        /// <summary>
        /// Gets or sets the primary category assigned to this post.
        /// </summary>
        public string PrimaryCategory { get; set; }

        /// <summary>
        /// Gets or sets a collection of categories assigned to this post.
        /// </summary>
        public List<string> Categories { get; set; }

        /// <summary>
        /// Gets or sets a collection of comments made against this post.
        /// </summary>
        public List<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets a collection of pings made against this post.
        /// </summary>
        public List<Ping> Pings { get; set; }

        /// <summary>
        /// Gets or sets a collection of tags associated with this post.
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the posts unique absolute URL.
        /// </summary>
        public string UniqueUrl { get; set; }

        /// <summary>
        /// Gets or sets keywords associated with this post.
        /// </summary>
        public string Keywords { get; set; }
    }
}
