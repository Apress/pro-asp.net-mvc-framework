using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace PerformanceDemos
{
    public class SqlPerformanceMonitorModule : IHttpModule
    {
        static string[] QuerySeparator = new string[] { Environment.NewLine + Environment.NewLine };

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += delegate(object sender, EventArgs e)
            {
                // Set up a new empty log
                HttpContext httpContext = ((HttpApplication)sender).Context;
                httpContext.Items["linqToSqlLog"] = new StringWriter();
            };

            context.PostRequestHandlerExecute += delegate(object sender, EventArgs e)
            {
                HttpContext httpContext = ((HttpApplication)sender).Context;
                HttpResponse response = httpContext.Response;

                // Don't interfere with non-HTML responses
                if (response.ContentType == "text/html")
                {
                    var log = (StringWriter)httpContext.Items["linqToSqlLog"];
                    var queries = log.ToString().Split(QuerySeparator, StringSplitOptions.RemoveEmptyEntries);
                    RenderQueriesToResponse(response, queries);
                }
            };
        }

        void RenderQueriesToResponse(HttpResponse response, string[] queries)
        {
            response.Write("<div class='PerformanceMonitor'>");
            response.Write(string.Format("<b>Executed {0} SQL {1}</b>",
                                         queries.Length,
                                         queries.Length == 1 ? "query" : "queries"));

            response.Write("<ol>");
            foreach (var entry in queries)
                response.Write(string.Format("<li>{0}</li>", Regex.Replace(entry, "(FROM|WHERE|--)", "<br/>$1")));
            response.Write("</ol>");
            response.Write("</div>");
        }

        public void Dispose() { /* Not needed */ }
    }
}