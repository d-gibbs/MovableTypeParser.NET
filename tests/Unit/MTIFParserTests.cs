namespace MovableTypeParser.Tests.Unit
{
    using System;
    using System.IO;
    using NUnit.Framework;
    using MovableTypeParser.Core;
    using MovableTypeParser.Models;
    using System.Linq;

    /// <summary>
    /// Tests functionality of the <see cref="MTIFParser"/> class.
    /// </summary>
    [TestFixture(Author = "Duane Gibbs", Category = "Unit tests", Description = "Tests functionality of the MTIFParser class", TestOf = typeof(MTIFParser))]
    public class MTIFParserTests
    {
        [Test(Description = "The MTIFParser should be able to parse a simple post")]
        public void Should_Parse_Simple_Post()
        {
            var parser = new MTIFParser(Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\SimplePost.txt"));

            var simplePost = parser.Parse().FirstOrDefault();

            Assert.IsNotNull(simplePost);
            Assert.AreEqual("Joe Bloggs", simplePost.Author);
            Assert.AreEqual("joe@bloggs.com", simplePost.AuthorEmail);
            Assert.AreEqual("Foo post title", simplePost.Title);
            Assert.AreEqual(Post.Status.Published, simplePost.PostStatus);
            Assert.AreEqual(true, simplePost.AllowComments);
            Assert.AreEqual(false, simplePost.AllowPings);
            Assert.AreEqual("wysiwyg", simplePost.ConvertBreaks);
            Assert.AreEqual("foo-post-basename", simplePost.Basename);
            Assert.AreEqual("1", simplePost.NoEntry);
            Assert.AreEqual("https://www.example.com/foo-bar.html", simplePost.UniqueUrl);
            Assert.AreEqual(new DateTime(2020, 1, 1, 09, 50, 0), simplePost.Date);
            Assert.AreEqual("<p>Foo body text</p>", simplePost.Body);
            Assert.AreEqual("<p>Foo extended body</p>", simplePost.ExtendedBody);
            Assert.AreEqual("Foo excerpt", simplePost.Excerpt);
            Assert.AreEqual("Foo keywords", simplePost.Keywords);
            Assert.AreEqual(3, simplePost.Categories.Count);
            Assert.IsTrue(simplePost.Categories[0] == "Foo category");
            Assert.IsTrue(simplePost.Categories[1] == "Bar category");
            Assert.IsTrue(simplePost.Categories[2] == "Foo bar category");
            Assert.AreEqual("Foo primary category", simplePost.PrimaryCategory);
            Assert.AreEqual(3, simplePost.Tags.Count);
            Assert.IsTrue(simplePost.Tags[0] == "Foo bar");
            Assert.IsTrue(simplePost.Tags[1] == "Foo");
            Assert.IsTrue(simplePost.Tags[2] == "Bar");
        }

        [Test(Description = "The MTIFParser should be able to parse a post with comments")]
        public void Should_Parse_Post_With_Comments()
        {
            var parser = new MTIFParser(Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PostWithComments.txt"));

            var postWithComments = parser.Parse().FirstOrDefault();

            var firstComment = postWithComments.Comments[0];
            var secondComment = postWithComments.Comments[1];

            Assert.IsNotNull(postWithComments);
            Assert.IsNotNull(firstComment);
            Assert.IsNotNull(secondComment);
            Assert.AreEqual(2, postWithComments.Comments.Count);

            Assert.AreEqual("Jane Doe", firstComment.Author);
            Assert.AreEqual("jane@doe.com", firstComment.Email);
            Assert.AreEqual("127.0.0.1", firstComment.Ip);
            Assert.AreEqual("https://example.com/foo", firstComment.Url);
            Assert.AreEqual(new DateTime(2019, 05, 18, 05, 0, 0), firstComment.Date);
            Assert.AreEqual("Thank you for the information.", firstComment.Body);

            Assert.AreEqual("Mr Smith", secondComment.Author);
            Assert.AreEqual("mrsmith@example.com", secondComment.Email);
            Assert.AreEqual("127.0.0.1", secondComment.Ip);
            Assert.AreEqual("https://example.com/foo/bar", secondComment.Url);
            Assert.AreEqual(new DateTime(2019, 05, 20, 05, 0, 0), secondComment.Date);
            Assert.AreEqual("Great article!", secondComment.Body);
        }

        [Test(Description = "The MTIFParser should be able to parse a post with pings")]
        public void Should_Parse_Post_With_Pings()
        {
            var parser = new MTIFParser(Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PostWithPings.txt"));

            var postWithPings = parser.Parse().FirstOrDefault();

            var firstPing = postWithPings.Pings[0];
            var secondPing = postWithPings.Pings[1];

            Assert.IsNotNull(postWithPings);
            Assert.IsNotNull(firstPing);
            Assert.IsNotNull(secondPing);
            Assert.AreEqual(2, postWithPings.Pings.Count);

            Assert.AreEqual("Foo ping 1", firstPing.Title);
            Assert.AreEqual("127.0.0.1", firstPing.Ip);
            Assert.AreEqual("http://example.com/ping/foo", firstPing.Url);
            Assert.AreEqual(new DateTime(2020, 01, 02, 09, 50, 0), firstPing.Date);
            Assert.AreEqual("Foo blog name 1", firstPing.BlogName);
            Assert.AreEqual("Ping 1 excerpt text", firstPing.Excerpt);

            Assert.AreEqual("Foo ping 2", secondPing.Title);
            Assert.AreEqual("127.0.0.1", secondPing.Ip);
            Assert.AreEqual("http://example.com/ping/foo/bar", secondPing.Url);
            Assert.AreEqual(new DateTime(2020, 01, 03, 09, 50, 0), secondPing.Date);
            Assert.AreEqual("Foo blog name 2", secondPing.BlogName);
            Assert.AreEqual("Ping 2 excerpt text", secondPing.Excerpt);
        }

        [Test(Description = "The MTIFParser should be able to parse a post with comments and pings")]
        public void Should_Parse_Post_With_Comments_And_Pings()
        {
            var parser = new MTIFParser(Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PostWithCommentsAndPings.txt"));

            var complexPost = parser.Parse().FirstOrDefault();

            var firstPing = complexPost.Pings[0];
            var secondPing = complexPost.Pings[1];

            Assert.IsNotNull(complexPost);
            Assert.IsNotNull(firstPing);
            Assert.IsNotNull(secondPing);
            Assert.AreEqual(2, complexPost.Pings.Count);

            Assert.AreEqual("Foo ping 1", firstPing.Title);
            Assert.AreEqual("127.0.0.1", firstPing.Ip);
            Assert.AreEqual("http://example.com/ping/foo", firstPing.Url);
            Assert.AreEqual(new DateTime(2020, 01, 02, 09, 50, 0), firstPing.Date);
            Assert.AreEqual("Foo blog name 1", firstPing.BlogName);
            Assert.AreEqual("Ping 1 excerpt text", firstPing.Excerpt);

            Assert.AreEqual("Foo ping 2", secondPing.Title);
            Assert.AreEqual("127.0.0.1", secondPing.Ip);
            Assert.AreEqual("http://example.com/ping/foo/bar", secondPing.Url);
            Assert.AreEqual(new DateTime(2020, 01, 03, 09, 50, 0), secondPing.Date);
            Assert.AreEqual("Foo blog name 2", secondPing.BlogName);
            Assert.AreEqual("Ping 2 excerpt text", secondPing.Excerpt);


            var firstComment = complexPost.Comments[0];
            var secondComment = complexPost.Comments[1];

            Assert.IsNotNull(complexPost);
            Assert.IsNotNull(firstComment);
            Assert.IsNotNull(secondComment);
            Assert.AreEqual(2, complexPost.Comments.Count);

            Assert.AreEqual("Jane Doe", firstComment.Author);
            Assert.AreEqual("jane@doe.com", firstComment.Email);
            Assert.AreEqual("127.0.0.1", firstComment.Ip);
            Assert.AreEqual("https://example.com/foo", firstComment.Url);
            Assert.AreEqual(new DateTime(2019, 05, 18, 05, 0, 0), firstComment.Date);
            Assert.AreEqual("Thank you for the information.", firstComment.Body);

            Assert.AreEqual("Mr Smith", secondComment.Author);
            Assert.AreEqual("mrsmith@example.com", secondComment.Email);
            Assert.AreEqual("127.0.0.1", secondComment.Ip);
            Assert.AreEqual("https://example.com/foo/bar", secondComment.Url);
            Assert.AreEqual(new DateTime(2019, 05, 20, 05, 0, 0), secondComment.Date);
            Assert.AreEqual("Great article!", secondComment.Body);
        }

        [Test(Description = "The MTIFParser should be able to parse multiple posts")]
        public void Should_Parse_Multiple_Posts()
        {
            var parser = new MTIFParser(Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\MultiplePosts.txt"));

            var multiplePosts = parser.Parse().ToList();

            Assert.IsNotNull(multiplePosts);
            Assert.AreEqual(3, multiplePosts.Count);
            Assert.AreEqual("Foo post title", multiplePosts[0].Title);
            Assert.AreEqual("Here is a 2nd entry", multiplePosts[1].Title);
            Assert.AreEqual("Here is a 3rd entry", multiplePosts[2].Title);
        }
    }
}
