<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="WizardDemo"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>ExtraDetails</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Registration: Extra details</h2>
Just a bit more info please.

<% using(Html.BeginForm()) { %>
    <%= Html.Hidden("regData", SerializationUtils.Serialize(Model)) %>
    <%= Html.ValidationSummary() %>
    
    <p>Age: <%= Html.TextBox("Age")%></p>
    <p>
        Hobbies: 
        <%= Html.TextArea("Hobbies", null, 3, 20, null) %>
    </p>
    <p>
        <input type="submit" name="backButton" value="< Back" />
        <input type="submit" name="nextButton" value="Next >" />
    </p>        
<% } %>


</asp:Content>
