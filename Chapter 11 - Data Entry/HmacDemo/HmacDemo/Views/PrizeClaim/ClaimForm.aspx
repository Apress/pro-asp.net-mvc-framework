<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>ClaimForm</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h1>Congratulations, you've won <%= ViewData.Eval("PrizeWon") %>!</h1>
<p>Claim it before it expires.</p>

<% using(Html.BeginForm("SubmitClaim", "PrizeClaim")) { %>
    <%= Html.Hidden("PrizeHash") %>
    
    <div>Prize: <%= Html.TextBox("PrizeWon") %></div>
    <div>Your address: <%= Html.TextArea("Address", null, 4, 15, null) %></div>
    
    <p align="center"><input type="submit" value="Submit prize claim" /></p>
<% } %>


</asp:Content>
