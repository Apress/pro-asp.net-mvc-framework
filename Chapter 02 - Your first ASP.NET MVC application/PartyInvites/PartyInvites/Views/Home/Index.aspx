<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <h1>New Year's Party</h1>
    <p>
        <%= ViewData["greeting"] %>! We're going to have an exciting party. 
        (To do: sell it better. Add pictures or something.)
    </p>
    <%= Html.ActionLink("RSVP Now", "RSVPForm") %>
</body>

</html>
