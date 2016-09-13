<%@ Page
    Language="C#" AutoEventWireup="false"
    EnableViewState="true" ViewStateMode="Disabled"
    CodeFile="Postback.aspx.cs" Inherits="WebFormLearn.View.Postback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" ID="LabelMessage" ForeColor="red" EnableViewState="false"></asp:Label>
        </div>
        <div>
            <asp:Button
                runat="server"
                ID="Button1" Text="Button1"
                OnClick="Button1_Click"
                OnCommand="Button_Command" CommandArgument="Button1"
                EnableViewState="false" />

            <asp:Button runat="server" ID="Button2" Text="Button2"
                OnClick="Button2_Click" OnCommand="Button_Command" CommandArgument="Button2" UseSubmitBehavior="false"
                EnableViewState="false" />
            <asp:Button runat="server" ID="Button3" Text="Button3" OnClick="Button3_Click"
                OnCommand="Button_Command" CommandArgument="Button3" UseSubmitBehavior="false"
                EnableViewState="false"
                ViewStateMode="Disabled" />

        </div>
    </form>
</body>
</html>
