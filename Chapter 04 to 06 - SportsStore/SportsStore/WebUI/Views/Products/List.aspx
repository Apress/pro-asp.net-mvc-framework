<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<DomainModel.Entities.Product>>" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
	<title>
	    SportsStore : 
	    <%= string.IsNullOrEmpty((string)ViewData["CurrentCategory"])
	            ? "All Products"
	            : Html.Encode(ViewData["CurrentCategory"])   
	    %>
	</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <% foreach(var product in Model) { %>
        <% Html.RenderPartial("ProductSummary", product); %>
    <% } %>
    
    <div class="pager">
        Page:
        <%= Html.PageLinks((int)ViewData["CurrentPage"],
                           (int)ViewData["TotalPages"],
                           x => Url.Action("List", new { page = x, category = ViewData["CurrentCategory"] })) %>
    </div>    
</asp:Content>
