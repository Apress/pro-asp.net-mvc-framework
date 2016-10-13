using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace SiteMapsDemo.Helpers
{
    public static class SiteMapHelpers
    {
        public static void RenderNavMenu(this HtmlHelper html)
        {
            HtmlTextWriter writer = new HtmlTextWriter(html.ViewContext.HttpContext
                                                           .Response.Output);
            RenderRecursive(writer, SiteMap.RootNode);
        }

        private static void RenderRecursive(HtmlTextWriter writer, SiteMapNode node)
        {
            if (SiteMap.CurrentNode == node) // Highlight visitor's location
                writer.RenderBeginTag(HtmlTextWriterTag.B); // Render as bold text
            else
            {
                // Render as link
                writer.AddAttribute(HtmlTextWriterAttribute.Href, node.Url);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
            }
            writer.Write(node.Title);
            writer.RenderEndTag();

            // Render children
            if (node.ChildNodes.Count > 0)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                foreach (SiteMapNode child in node.ChildNodes)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    RenderRecursive(writer, child);
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
        }
    }

}