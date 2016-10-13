<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>SportsStore : Check Out</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Check out now</h2>
    Please enter your details, and we'll ship your goods right away!
    <%= Html.ValidationSummary() %>
    
    <% using(Html.BeginForm()) { %>
        <h3>Ship to</h3>
        <div>Name: <%= Html.TextBox("Name") %></div>
        <h3>Address</h3>
        <div>Line 1: <%= Html.TextBox("Line1") %></div>
        <div>Line 2: <%= Html.TextBox("Line2") %></div>
        <div>Line 3: <%= Html.TextBox("Line3") %></div>
        <div>City: <%= Html.TextBox("City") %></div>
        <div>State: <%= Html.TextBox("State") %></div>
        <div>Zip: <%= Html.TextBox("Zip") %></div>
        <div>Country: <%= Html.TextBox("Country") %></div>
        
        <h3>Options</h3>
        <%= Html.CheckBox("GiftWrap") %> Gift wrap these items
        
        <p align="center"><input type="submit" value="Complete order" /></p>
    <% } %>
</asp:Content>