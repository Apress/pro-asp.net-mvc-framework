using System.Web;
using System.Web.Routing;
using NUnit.Framework;
using WebUI;

namespace Tests
{
    [TestFixture]
    public class InboundRoutingTests
    {
        [Test]
        public void Slash_Goes_To_All_Products_Page_1()
        {
            TestRoute("~/", new { controller = "Products", action = "List",
                                  category = (string)null, page = 1 });
        }

        [Test]
        public void Page2_Goes_To_All_Products_Page_2()
        {
            TestRoute("~/Page2", new {
                controller = "Products", action = "List",
                category = (string)null, page = 2
            });
        }

        [Test]
        public void Football_Goes_To_Football_Page_1()
        {
            TestRoute("~/Football", new
            {
                controller = "Products", action = "List",
                category = "Football", page = 1
            });
        }

        [Test]
        public void Football_Slash_Page43_Goes_To_Football_Page_43()
        {
            TestRoute("~/Football/Page43", new
            {
                controller = "Products", action = "List",
                category = "Football", page = 43
            });
        }

        [Test]
        public void Anything_Slash_Else_Goes_To_Else_On_AnythingController()
        {
            TestRoute("~/Anything/Else", new {controller = "Anything",action = "Else"});
        }


        private void TestRoute(string url, object expectedValues)
        {
            // Arrange: Prepare the route collection and a mock request context
            RouteCollection routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);
            var mockHttpContext = new Moq.Mock<HttpContextBase>();
            var mockRequest = new Moq.Mock<HttpRequestBase>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockRequest.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(url);

            // Act: Get the mapped route
            RouteData routeData = routes.GetRouteData(mockHttpContext.Object);

            // Assert: Test the route values against expectations
            Assert.IsNotNull(routeData);
            var expectedDict = new RouteValueDictionary(expectedValues);
            foreach (var expectedVal in expectedDict)
            {
                if (expectedVal.Value == null)
                    Assert.IsNull(routeData.Values[expectedVal.Key]);
                else
                    Assert.AreEqual(expectedVal.Value.ToString(),
                                    routeData.Values[expectedVal.Key].ToString());
            }
        }

    }
}