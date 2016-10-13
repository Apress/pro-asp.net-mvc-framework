<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Index</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>See also</h2>
    <ul>
        <li><%= Html.ActionLink("Compression demo", "CompressionDemo") %></li>
        <li><%= Html.ActionLink("Tracing demo", "TracingDemo") %></li>
        <li><%= Html.ActionLink("Performance monitor demo", "PerformanceMonitorDemo")%></li>
        <li><%= Html.ActionLink("LINQ to SQL monitoring demo", "Index", "LinqToSqlMonitoringDemo")%></li>
    </ul>
</asp:Content>
