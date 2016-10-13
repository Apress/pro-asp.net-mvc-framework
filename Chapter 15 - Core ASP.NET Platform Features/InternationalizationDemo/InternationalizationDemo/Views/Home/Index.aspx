<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.Threading"%>
<%@ Import Namespace="System.Globalization"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Index</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%= Thread.CurrentThread.CurrentUICulture.TextInfo.ToTitleCase(MyResources.Resources.Greeting) %>!</h1>

    Latest news:
    <ul>
        <li>Government plans to widen <%= MyResources.Resources.Sidewalk %></li>
        <li>Man's <%= MyResources.Resources.Pants %> caught in <%= MyResources.Resources.Elevator %> door</li>
        <li>Christmas is <%= (new DateTime(2008, 12, 25)).ToShortDateString() %></li>
        <li>One unit of currency is <%= string.Format("{0:c}", 1) %></li>
    </ul>
    
    With respect to <%= MyResources.Resources.TheRuler %>.
</asp:Content>
