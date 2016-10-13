<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" 
    Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
	<title>Admin : Login</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>Log in</h1>
    
    <% if((bool?)ViewData["lastLoginFailed"] == true) { %>
        <div class="Message">
            Sorry, your login attempt failed. Please try again.
        </div>
    <% } %>

    <p>Please log in to access the administrative area:</p>
    <% using(Html.BeginForm()) { %>
        <div>Login name: <%= Html.TextBox("name") %></div>
        <div>Password: <%= Html.Password("password") %></div>        
        <p><input type="submit" value="Log in" /></p>        
    <% } %>
</asp:Content>
