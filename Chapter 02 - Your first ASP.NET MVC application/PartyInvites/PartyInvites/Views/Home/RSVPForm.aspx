<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <h1>RSVP</h1>

    <%= Html.ValidationSummary() %>

    <% using(Html.BeginForm()) { %>
        <p>Your name: <%= Html.TextBox("Name") %></p>
        <p>Your email: <%= Html.TextBox("Email")%></p>
        <p>Your phone: <%= Html.TextBox("Phone")%></p>           
        <p>
            Will you attend?
            <%= Html.DropDownList("WillAttend", new[] {
                new SelectListItem { Text = "Yes, I'll be there", 
                                     Value = bool.TrueString },
                new SelectListItem { Text = "No, I can't come", 
                                     Value = bool.FalseString }
            }, "Choose an option") %>
        </p>
        <input type="submit" value="Submit RSVP" />
    <% } %>
</body>

</html>
