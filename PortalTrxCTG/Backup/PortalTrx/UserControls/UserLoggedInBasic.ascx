<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserLoggedInBasic.ascx.cs"
    Inherits="UserControls_UserLoggedInBasic" %>
<div style="margin: 10px 5px 3px 5px; text-align:left;">
    <div>
        <asp:Label ID="lblUserNombres" runat="server" />
        <asp:Label ID="lblUserApellidos" runat="server" />
    </div>
    <div>
        <asp:Label ID="lblUserIdent" runat="server" /></div>
    <div>
        <asp:Label ID="lblUserEmail" runat="server" /></div>
    <div style="text-align:center; margin-top:10px">
        <asp:ImageButton ID="btnAccount" runat="server" PostBackUrl="~/DataUserAccount.aspx" ImageUrl="~/images/btnAccount.png" />
        <asp:ImageButton ID="btnCtgData" PostBackUrl="~/DataUserCTG.aspx" ImageUrl="~/images/btnCtg.png" runat="server" />
        <asp:ImageButton ID="btnEndSession" ImageUrl="~/images/btnLogout.png" 
            runat="server" onclick="btnEndSession_Click" />
    </div>
</div>
