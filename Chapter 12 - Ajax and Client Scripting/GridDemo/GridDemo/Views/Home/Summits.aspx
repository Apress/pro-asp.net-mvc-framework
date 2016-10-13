<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<GridDemo.Models.MountainInfo>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Summits</title>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/SummitsGridBehaviors.js") %>"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>The Seven Summits</h2>
    <% Html.RenderPartial("SummitsGrid"); %>
    <p><i>This page generated at <%= DateTime.Now.ToLongTimeString() %></i></p>
</asp:Content>