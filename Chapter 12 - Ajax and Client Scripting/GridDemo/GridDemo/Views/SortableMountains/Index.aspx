<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<GridDemo.Models.MountainInfo>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Index</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <b>Quiz:</b> Can you put these mountains in order of height (tallest first)?

    <div id="summits">
        <% foreach(var mountain in Model) { %>
            <div class="mountain"><%= mountain.Name %></div>
        <% } %>
    </div>
    
    <% using(Html.BeginForm()) { %>
        <%= Html.Hidden("chosenOrder") %>
        <input type="submit" value="Submit your answer" />
    <% } %>

    <script>
        $(function() {
            $("#summits").sortable();
        });

        $("form").submit(function() {
            var currentOrder = "";
            $("#summits div.mountain").each(function() {
                currentOrder += $(this).text() + "|";
            });
            $("#chosenOrder").val(currentOrder);
        });        
    </script>
</asp:Content>
