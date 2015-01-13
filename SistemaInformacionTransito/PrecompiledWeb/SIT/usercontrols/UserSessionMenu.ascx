<%@ control language="C#" autoeventwireup="true" inherits="usercontrols_UserSessionMenu, App_Web_usersessionmenu.ascx.6bb32623" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<div>
    <a><b><asp:Label Text="" ID="lblName" runat="server" style="padding: 5px 25px 8px 13px; margin-top:4px; cursor: pointer;" /></b></a>
    <cc1:DropDownExtender ID="ddeMenu" runat="server" TargetControlID="lblName" DropDownControlID="menuPanel"
     HighlightBackColor="#4498c2" HighlightBorderColor="#4a7eae">
    </cc1:DropDownExtender>
    <asp:Panel ID="menuPanel" runat="server" Style="display: none; visibility: hidden; background-color:#7dc3e6;" BorderColor="#505f76" BorderWidth="1">
        <table style="margin:0;">
            <tr onMouseover="this.bgColor='#b4e1f8'" onMouseout="this.bgColor='#7dc3e6'">
                <td style="padding:0; margin:0; width: 140px">
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/ChangePwd.aspx" runat="server">Modificar contraseña</asp:HyperLink>
                </td>
            </tr>
            <tr onMouseover="this.bgColor='#b4e1f8'" onMouseout="this.bgColor='#7dc3e6'">
                <td style="padding:0; margin:0; width: 140px">
                    <asp:LinkButton ID="lnkCloseSession" Style="display: block; margin: 0px;" 
                        runat="server" Text="Cerrar sesión" onclick="lnkCloseSession_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>

