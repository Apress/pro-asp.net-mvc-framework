<%@ Control Language="C#" 
    Inherits="System.Web.Mvc.ViewUserControl<DomainModel.Entities.Cart>" %>
<% if(Model.Lines.Count > 0) { %>
    <div id="cart">
        <span class="caption">
            <b>Your cart:</b>
            <%= Model.Lines.Sum(x => x.Quantity) %> item(s),
            <%= Model.ComputeTotalValue().ToString("c") %>
        </span>
        <%= Html.ActionLink("Check out", "Index", "Cart", 
            new { returnUrl = Request.Url.PathAndQuery }, null)%>
    </div>
<% } %>