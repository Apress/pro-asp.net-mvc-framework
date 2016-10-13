<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>TracingDemo</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>TracingDemo</h2>

    <p>
        To enable tracing, uncomment the line
        <tt>&lt;trace enabled="true" pageOutput="true"/></tt>
        in <tt>web.config</tt>, and then reload this page.
    </p>
</asp:Content>
