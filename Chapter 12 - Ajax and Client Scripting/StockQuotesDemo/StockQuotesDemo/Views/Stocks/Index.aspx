<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Index</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Stocks (JSON)</h2>
    <% using(Html.BeginForm("GetQuote", "Stocks")) { %>
        Symbol:
        <%= Html.TextBox("symbol") %>
        <input type="submit" />
        <span id="Span1"></span>
    <% } %>
    
    <table>
        <tr><td>Opening price:</td><td id="openingPrice"></td></tr>
        <tr><td>Closing price:</td><td id="closingPrice"></td></tr>
        <tr><td>Rating:</td><td id="stockRating"></td></tr>
    </table>
    
    <p><i>This page generated at <%= DateTime.Now.ToLongTimeString() %></i></p>
    
    <script type="text/javascript">
        $("form[action$='GetQuote']").submit(function() {
            $.getJSON($(this).attr("action"), $(this).serialize(), function(stockData) {
                $("#openingPrice").html(stockData.OpeningPrice).hide().fadeIn();
                $("#closingPrice").html(stockData.ClosingPrice).hide().fadeIn();
                $("#stockRating").html(stockData.Rating).hide().fadeIn();
            });
            return false;
        });
    </script>
    
    <hr />
    
    <h2>Stocks (XML)</h2>
    <% using(Html.BeginForm("GetQuoteXML", "Stocks")) { %>
        Symbol:
        <%= Html.TextBox("symbol") %>
        <input type="submit" />
        <span id="results"></span>
    <% } %>
    
    <table>
        <tr><td>Opening price:</td><td id="openingPriceXML"></td></tr>
        <tr><td>Closing price:</td><td id="closingPriceXML"></td></tr>
        <tr><td>Rating:</td><td id="stockRatingXML"></td></tr>
    </table>
    
    <p><i>This page generated at <%= DateTime.Now.ToLongTimeString() %></i></p>
    
    <script type="text/javascript">
        $("form[action$='GetQuoteXML']").submit(function() {
            $.ajax({
                url: $(this).attr("action"),
                type: "GET",
                data: $(this).serialize(),
                dataType: "xml", // Instruction to parse response as XMLDocument
                success: function(resultXml) {
                    // Extract data from XMLDocument using jQuery selectors
                    var opening = $("OpeningPrice", resultXml).text();
                    var closing = $("ClosingPrice", resultXml).text();
                    var rating = $("Rating", resultXml).text();
                    // Use that data to update DOM
                    $("#openingPriceXML").html(opening);
                    $("#closingPriceXML").html(closing);
                    $("#stockRatingXML").html(rating);
                }
            });
            return false;
        });
    </script>
</asp:Content>
