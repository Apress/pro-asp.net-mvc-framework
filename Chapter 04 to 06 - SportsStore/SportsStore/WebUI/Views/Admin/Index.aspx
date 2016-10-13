<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" 
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<DomainModel.Entities.Product>>" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
	<title>Admin : All Products</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>All products</h1>
    <table class="Grid">
        <tr>
            <th>ID</th>
            <th>Name</th>
            <th class="NumericCol">Price</th>
            <th>Actions</th>
        </tr>
        <% foreach (var item in Model) { %>
            <tr>
                <td><%= item.ProductID %></td>
                <td><%= item.Name %></td>
                <td class="NumericCol"><%= item.Price.ToString("c") %></td>
                <td>
                    <%= Html.ActionLink("Edit", "Edit", new {item.ProductID}) %>
                    <%= Html.ActionLink("Delete", "Delete", new {item.ProductID})%>
                </td>
            </tr>
        <% } %>
    </table>
    <p><%= Html.ActionLink("Add a new product", "Create")%></p>
</asp:Content>

