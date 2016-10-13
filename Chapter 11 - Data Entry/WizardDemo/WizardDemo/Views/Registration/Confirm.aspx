<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RegistrationData>" %>
<%@ Import Namespace="WizardDemo.Models"%>
<%@ Import Namespace="WizardDemo"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Confirm</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Confirm</h2>
Please confirm that your details are correct.
<% using(Html.BeginForm()) { %>
    <%= Html.Hidden("regdata", SerializationUtils.Serialize(Model)) %>
    <div>Name: <b><%= Html.Encode(Model.Name) %></b></div>
    <div>E-mail: <b><%= Html.Encode(Model.Email)%></b></div>
    <div>Age: <b><%= Model.Age %></b></div>
    <div>Hobbies: <b><%= Html.Encode(Model.Hobbies)%></b></div>
    <p>
        <input type="submit" name="backButton" value="< Back" />
        <input type="submit" name="nextButton" value="Next >" />
    </p> 
<% } %>



</asp:Content>
