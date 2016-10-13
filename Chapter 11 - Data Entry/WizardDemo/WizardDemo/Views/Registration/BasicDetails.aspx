<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="WizardDemo"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>BasicDetails</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Registration: Basic details</h2>
    Please enter your details

    <% using(Html.BeginForm()) { %>
        <%= Html.Hidden("regData", SerializationUtils.Serialize(Model)) %>
        
        <%= Html.ValidationSummary() %>
        <p>Name: <%= Html.TextBox("Name")%></p>
        <p>E-mail: <%= Html.TextBox("Email")%></p>
        <p><input type="submit" name="nextButton" value="Next >" /></p>
    <% } %>
</asp:Content>
