﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RunScript.aspx.cs" Inherits="WebServicesManager.RunScript" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="10" Width="459px"></asp:TextBox>
    </div>
    <div>
        <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label><br />
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"/>
        <asp:Button ID="Button2" runat="server" Text="Backup" OnClick="Button2_Click"/>
    </div>
    </form>
</body>
</html>
