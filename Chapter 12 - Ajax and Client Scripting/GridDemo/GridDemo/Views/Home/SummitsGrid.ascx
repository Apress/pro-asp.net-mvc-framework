<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<GridDemo.Models.MountainInfo>>" %>
<div>
<div id="summits">
    <table>
        <thead><tr>
            <td>Item</td> <td>Height (m)</td> <td>Actions</td>
        </tr></thead>
        <% foreach(var mountain in Model) { %>
            <tr>
                <td><%= mountain.Name %></td>
                <td><%= mountain.HeightInMeters %></td>
                <td>
                    <% using(Html.BeginForm("DeleteItem", "Home")) { %>
                        <%= Html.Hidden("item", mountain.Name) %>
                        <input type="submit" value="Delete" />
                    <% } %>
                </td>
            </tr>
        <% } %>
    </table>
    Page:    
    <%= Html.PageLinks((int)ViewData["currentPage"], 
                       (int)ViewData["totalPages"], 
                       i => Url.Action("Summits", new { page = i }) ) %>    
</div>
</div>