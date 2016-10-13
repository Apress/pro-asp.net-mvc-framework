<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Index</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Pledge Money to Our Campaign</h2>
    <p>With your help, we can eradicate the &lt;blink&gt; tag forever.<p>
    <% using(Html.BeginForm()) { %>    
        <div>
            Your name: <%= Html.TextBox("pledge.SupporterName") %>
        </div>
        <div>
            Your email address: <%= Html.TextBox("pledge.SupporterEmail")%>
        </div>
        <div>
            Amount to pledge: $<%= Html.TextBox("pledge.Amount") %>
        </div>
        <p><input type="submit" /></p>
    <% } %>
    
<script type="text/javascript">
    $(function() {
        $("form").validate({
            errorClass: "field-validation-error",
            rules: {
                "pledge.SupporterName": { required: true, maxlength: 50 },
                "pledge.SupporterEmail": { required: true, email: true },
                "pledge.Amount": { required: true, min: 10 }
            },
            messages: {
                "pledge.Amount": { min: "Come on, you can give at least $10.00!" }
            }
        })
    });
</script>
</asp:Content>
