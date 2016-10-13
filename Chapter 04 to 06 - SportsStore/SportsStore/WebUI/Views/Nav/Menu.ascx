<%@ Control Language="C#" 
Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<WebUI.Controllers.NavLink>>" %>
<% foreach(var link in Model) { %>
    <a href="<%= Url.RouteUrl(link.RouteValues) %>"
       class="<%= link.IsSelected ? "selected" : "" %>"
    >
        <%= link.Text %>
    </a>
<% } %>