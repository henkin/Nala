﻿using NUnit.Framework;

namespace Nala.Core.Tests
{
    [TestFixture]
    public class NewsServiceTests
    {
        [Test]
        public void GetLatestsNewsItems()
        {
            var newsService = new NewsService();
            var gazaNews = newsService.GetNewsContent("Gaza");
            Assert.IsNotEmpty(gazaNews, "No news?");
        }
    }
}