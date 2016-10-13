<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" 
         Inherits="System.Web.Mvc.ViewPage<DomainModel.Entities.Product>" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
	<title>Admin : Edit <%= Model.Name %></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>Edit <%= Model.Name %></h1>

    <% using (Html.BeginForm("Edit", "Admin", FormMethod.Post, 
                             new { enctype = "multipart/form-data" })) { %>
        <%= Html.Hidden("ProductID") %>
        <p>
            Name: <%= Html.TextBox("Name") %>
            <div><%= Html.ValidationMessage("Name") %></div>
        </p>
        <p>
            Description: <%= Html.TextArea("Description", null, 4, 20, null) %>
            <div><%= Html.ValidationMessage("Description") %></div>
        </p>
        <p>
            Price: <%= Html.TextBox("Price") %>
            <div><%= Html.ValidationMessage("Price") %></div>
        </p>
<p>
    Category: <%= Html.TextBox("Category") %>
    <div><%= Html.ValidationMessage("Category") %></div>
</p>
<p>
    Image:
    <% if(Model.ImageData == null) { %>
        None
    <% } else { %>
        <img src="<%= Url.Action("GetImage", "Products", 
                                 new { Model.ProductID }) %>" />
    <% } %>
    <div>Upload new image: <input type="file" name="Image" /></div>                
</p>
        
<input type="submit" value="Save" /> &nbsp;&nbsp;
        <%=Html.ActionLink("Cancel and return to List", "Index") %>
    <% } %>
</asp:Content>

