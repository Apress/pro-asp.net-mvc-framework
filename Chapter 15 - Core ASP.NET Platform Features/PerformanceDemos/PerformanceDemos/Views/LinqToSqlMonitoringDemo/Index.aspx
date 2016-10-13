<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<PerformanceDemos.Models.Product>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Index</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Now doing a Linq query...</h2>
    <p>There are <b><%= ViewData["numProducts"]%></b> products:</p>
    <ul>
        <% foreach(var product in Model) { %>
            <li><%= Html.Encode(product.Name) %> (<%= product.Price.ToString("c") %>)</li>
        <% } %>
    </ul>
    <p>The end.</p>
</asp:Content>
