using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Mvc;

namespace Tests
{
    using WebUI.HtmlHelpers; // The extension method will live in this namespace

    [TestFixture]
    public class PagingHelperTests
    {
        [Test]
        public void PageLinks_Method_Extends_HtmlHelper()
        {
            HtmlHelper html = null;
            html.PageLinks(0, 0, null);
        }

        [Test]
        public void PageLinks_Produces_Anchor_Tags()
        {
            // First parameter will be current page index
            // Second will be total number of pages
            // Third will be lambda method to map a page number to its URL
            string links = ((HtmlHelper)null).PageLinks(2, 3, i => "Page" + i);

            // This is how the tags should be formatted
            Assert.AreEqual(@"<a href=""Page1"">1</a>
<a class=""selected"" href=""Page2"">2</a>
<a href=""Page3"">3</a>
", links);
        }
    }
}
