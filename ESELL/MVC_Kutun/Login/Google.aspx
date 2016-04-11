<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Google.aspx.cs" Inherits="MVC_Kutun.Login.Google" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div style="display:none"> <asp:Button ID="loginButton" runat="server" Text="Đăng nhập bằng tài khoản Google" OnClick="Google_Click" /></div>
        <br />
        <asp:Label ID="loginFailedLabel" runat="server" EnableViewState="False" Text="Login failed"
                Visible="False" />
        <asp:Label ID="loginCanceledLabel" runat="server" EnableViewState="False" Text="Login canceled"
                Visible="False" />
    </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    window.onload=function click() {
        var clickButton = document.getElementById("<%= loginButton.ClientID %>");
        clickButton.click();
    }
    
    </script>