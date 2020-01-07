namespace MovableTypeParser.Tests.Unit
{
    using System;
    using NUnit.Framework;
    using MovableTypeParser.Utils;

    /// <summary>
    /// Tests functionality of the <see cref="ParserUtils"/> class.
    /// </summary>
    [TestFixture(Author = "Duane Gibbs", Category = "Unit tests", Description = "Tests functionality of the ParserUtils class", TestOf = typeof(ParserUtils))]
    public class ParserUtilsTests
    {
        private ParserUtils _utils;

        [SetUp]
        public void Setup()
        {
            _utils = new ParserUtils();
        }

        [Test(Description = "Verifies that the provided line is valid for a Post")]
        public void Should_Verify_Current_Line_Is_Valid_For_Post()
        {
            string titleLine = "TITLE: Foo ping title";
            string baseNameLine = "BASENAME: foo-base-name";
            string authorLine = "AUTHOR: Foo author";
            string authorEmailLine = "AUTHOR EMAIL: foo@bar.com";
            string dateLine = "DATE: 01/01/2020 09:24:59 AM";
            string statusLine = "STATUS: draft";
            string allowCommentsLine = "ALLOW COMMENTS: 1";
            string allowPingsLine = "ALLOW PINGS: 1";
            string convertBreaksLine = "CONVERT BREAKS: wysiwyg";
            string noEntryLine = "NO ENTRY: 1";
            string uniqueUrlLine = "UNIQUE URL: https://example.com/foo-bar-foobar.html";
            string categoryLine = "CATEGORY: Foo category";
            string primaryCategoryLine = "PRIMARY CATEGORY: Foo primary category";
            string tagsLine = "TAGS: Foo tag, Bar tag";
            string failingLine = "Foo failing line, lorem ipsum sit dolor : amet";

            bool validTitleLine = _utils.IsPostLine(titleLine);
            bool validBaseNameLine = _utils.IsPostLine(baseNameLine);
            bool validAuthorLine = _utils.IsPostLine(authorLine);
            bool validAuthorEmailLine = _utils.IsPostLine(authorEmailLine);
            bool validDateLine = _utils.IsPostLine(dateLine);
            bool validStatusLine = _utils.IsPostLine(statusLine);
            bool validAllowCommentsLine = _utils.IsPostLine(allowCommentsLine);
            bool validAllowPingsLine = _utils.IsPostLine(allowPingsLine);
            bool validConvertBreaksLine = _utils.IsPostLine(convertBreaksLine);
            bool validNoEntryLine = _utils.IsPostLine(noEntryLine);
            bool validUniqueUrlLine = _utils.IsPostLine(uniqueUrlLine);
            bool validCategoryLine = _utils.IsPostLine(categoryLine);
            bool validPrimaryCategoryLine = _utils.IsPostLine(primaryCategoryLine);
            bool validTagsLine = _utils.IsPostLine(tagsLine);
            bool lineFails = !_utils.IsPostLine(failingLine);

            Assert.IsTrue(validTitleLine);
            Assert.IsTrue(validBaseNameLine);
            Assert.IsTrue(validAuthorLine);
            Assert.IsTrue(validAuthorEmailLine);
            Assert.IsTrue(validDateLine);
            Assert.IsTrue(validStatusLine);
            Assert.IsTrue(validAllowCommentsLine);
            Assert.IsTrue(validAllowPingsLine);
            Assert.IsTrue(validConvertBreaksLine);
            Assert.IsTrue(validNoEntryLine);
            Assert.IsTrue(validUniqueUrlLine);
            Assert.IsTrue(validCategoryLine);
            Assert.IsTrue(validPrimaryCategoryLine);
            Assert.IsTrue(validTagsLine);
            Assert.IsTrue(lineFails);
        }

        [Test(Description = "Verifies that the provided line is valid for a Comment")]
        public void Should_Verify_Current_Line_Is_Valid_For_Comment()
        {
            string authorLine = "AUTHOR: Foo author";
            string urlLine = "URL: https://example.com";
            string ipLine = "IP: 127.0.0.1";
            string emailLine = "EMAIL: foo@bar.com";
            string dateLine = "DATE: 01/01/2020 09:24:59 AM";
            string failingLine = "Foo failing line, lorem ipsum sit dolor : amet";

            bool validAuthorLine = _utils.IsCommentLine(authorLine);
            bool validUrlLine = _utils.IsCommentLine(urlLine);
            bool validIpLine = _utils.IsCommentLine(ipLine);
            bool validEmailLine = _utils.IsCommentLine(emailLine);
            bool validDateLine = _utils.IsCommentLine(dateLine);
            bool lineFails = !_utils.IsCommentLine(failingLine);

            Assert.IsTrue(validAuthorLine);
            Assert.IsTrue(validUrlLine);
            Assert.IsTrue(validIpLine);
            Assert.IsTrue(validEmailLine);
            Assert.IsTrue(validDateLine);
            Assert.IsTrue(lineFails);
        }

        [Test(Description = "Verifies that the provided line is valid for a Ping")]
        public void Should_Verify_Current_Line_Is_Valid_For_Ping()
        {
            string titleLine = "TITLE: Foo ping title";
            string urlLine = "URL: https://example.com";
            string ipLine = "IP: 127.0.0.1";
            string blogNameLine = "BLOG NAME: Foo blog";
            string dateLine = "DATE: 01/01/2020 09:24:59 AM";
            string failingLine = "Foo failing line, lorem ipsum sit dolor : amet";

            bool validTitleLine = _utils.IsPingLine(titleLine);
            bool validUrlLine = _utils.IsPingLine(urlLine);
            bool validIpLine = _utils.IsPingLine(ipLine);
            bool validBlogNameLine = _utils.IsPingLine(blogNameLine);
            bool validDateLine = _utils.IsPingLine(dateLine);
            bool lineFails = !_utils.IsPingLine(failingLine);

            Assert.IsTrue(validTitleLine);
            Assert.IsTrue(validUrlLine);
            Assert.IsTrue(validIpLine);
            Assert.IsTrue(validBlogNameLine);
            Assert.IsTrue(validDateLine);
            Assert.IsTrue(lineFails);
        }

        [Test(Description = "Verifies that the provided line starts a multiline section")]
        public void Should_Verify_Current_Line_Is_Valid_For_Mulitine()
        {
            string bodyLine = $"BODY:{Environment.NewLine}<p>Foo body content</p>";
            string extBodyLine = $"EXTENDED BODY:{Environment.NewLine}<p>Foo extended body content</p>";
            string excerptLine = $"EXCERPT:{Environment.NewLine}Foo excerpt";
            string keywordsLine = $"KEYWORDS:{Environment.NewLine}Foo keywords";
            string commentLine = $"COMMENT:{Environment.NewLine}";
            string pingLine = $"PING:{Environment.NewLine}";
            string failingLine = "Foo failing line, lorem ipsum sit dolor : amet";

            bool validBodyLine = _utils.IsMultiLine(bodyLine);
            bool validExtBodyLine = _utils.IsMultiLine(extBodyLine);
            bool validExcerptLine = _utils.IsMultiLine(excerptLine);
            bool validKeywordsLine = _utils.IsMultiLine(keywordsLine);
            bool validCommentLine = _utils.IsMultiLine(commentLine);
            bool validPingLine = _utils.IsMultiLine(pingLine);
            bool lineFails = !_utils.IsMultiLine(failingLine);

            Assert.IsTrue(validBodyLine);
            Assert.IsTrue(validExtBodyLine);
            Assert.IsTrue(validExcerptLine);
            Assert.IsTrue(validKeywordsLine);
            Assert.IsTrue(validCommentLine);
            Assert.IsTrue(validPingLine);
            Assert.IsTrue(lineFails);
        }

        [Test(Description = "Verifies that the provided line matches the section ending delimiter")]
        public void Should_Verify_Current_Line_Is_End_Of_Section()
        {
            string currentLine = "-----";
            string failingLine = "Foo failing line, lorem ipsum sit dolor : amet";

            bool isValid = _utils.IsEndOfSection(currentLine);
            bool lineFails = !_utils.IsEndOfSection(failingLine);

            Assert.IsTrue(isValid);
            Assert.IsTrue(lineFails);
        }

        [Test(Description = "Verifies that the provided line matches the post ending delimiter")]
        public void Should_Verify_Current_Line_Is_End_Of_Post()
        {
            string currentLine = "--------";
            string failingLine = "Foo failing line, lorem ipsum sit dolor : amet";

            bool isValid = _utils.IsEndOfPost(currentLine);
            bool lineFails = !_utils.IsEndOfPost(failingLine);

            Assert.IsTrue(isValid);
            Assert.IsTrue(lineFails);
        }
    }
}
