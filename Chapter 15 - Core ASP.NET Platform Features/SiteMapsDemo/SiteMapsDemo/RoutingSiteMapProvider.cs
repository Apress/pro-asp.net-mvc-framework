using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Hosting;
using System.Web.Routing;
using System.Xml;
using System.Linq;

namespace SiteMapsDemo
{
    public class RoutingSiteMapProvider : StaticSiteMapProvider
    {
        private SiteMapNode rootNode;

        public override void Initialize(string name, NameValueCollection attributes)
        {
            base.Initialize(name, attributes);

            // Load XML file, taking name from web.config or use Web.sitemap as default
            var xmlDoc = new XmlDocument();
            var siteMapFile = attributes["siteMapFile"] ?? "~/Web.sitemap";
            xmlDoc.Load(HostingEnvironment.MapPath(siteMapFile));
            var rootSiteMapNode = xmlDoc.DocumentElement["siteMapNode"];

            // Build the navigation structure 
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            var requestContext = new RequestContext(httpContext, new RouteData());
            rootNode = AddNodeRecursive(rootSiteMapNode, null, requestContext);
        }

        private static string[] reservedNames = new[] { "title","description","roles" };
        private SiteMapNode AddNodeRecursive(XmlNode xmlNode, SiteMapNode parent,
                                             RequestContext context)
        {
            // Generate this node's URL by querying RouteTable.Routes
            var routeValues = (from XmlNode attrib in xmlNode.Attributes
                               where !reservedNames.Contains(attrib.Name.ToLower())
                               select new { attrib.Name, attrib.Value })
                              .ToDictionary(x => x.Name, x => (object)x.Value);
            var routeDict = new RouteValueDictionary(routeValues);
            var url = RouteTable.Routes.GetVirtualPath(context, routeDict).VirtualPath;

            // Register this node and its children
            var title = xmlNode.Attributes["title"].Value;
            var node = new SiteMapNode(this, Guid.NewGuid().ToString(), url, title);
            node.Roles = GetRolesFromXmlNode(xmlNode);
            base.AddNode(node, parent);
            foreach (XmlNode childNode in xmlNode.ChildNodes)
                AddNodeRecursive(childNode, node, context);
            return node;
        }

        private static List<string> GetRolesFromXmlNode(XmlNode node)
        {
            var rolesAttribute = node.Attributes["roles"];
            if(rolesAttribute == null)
                return null;
            return (from token in rolesAttribute.Value.Split(',')
                    let trimmedToken = token.Trim()
                    where trimmedToken != string.Empty
                    select trimmedToken).ToList();
        }

        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            if (node == rootNode) return true; // Root node must always be accessible

            return (node.Roles == null) || (node.Roles.Count == 0) ||
                   (node.Roles.Cast<string>().Any(context.User.IsInRole));
        }

        // These methods are called by ASP.NET to fetch your site map data
        protected override SiteMapNode GetRootNodeCore() { return rootNode; }
        public override SiteMapNode BuildSiteMap() { return rootNode; }
    }

}