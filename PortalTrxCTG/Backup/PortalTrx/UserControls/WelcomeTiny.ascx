<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WelcomeTiny.ascx.cs" Inherits="UserControls_WelcomeTiny" %>
<asp:Panel ID="wlcMainPanel" runat="server">
    <a><b><asp:Label Text="" ID="lblName" runat="server" style="padding: 0px 19px 8px 3px; margin-top:4px; cursor: pointer;" /></b></a>
    <asp:DropDownExtender ID="ddeMenu" runat="server" TargetControlID="lblName" DropDownControlID="menuPanel"
     HighlightBackColor="#4a7eae" HighlightBorderColor="#4a7eae">
    </asp:DropDownExtender>
    <asp:Panel ID="menuPanel" runat="server" Style="display: none; visibility: hidden; background-color:#4a7eae;" BorderColor="#505f76" BorderWidth="1">
        <table style="margin:0;">
            <tr onMouseover="this.bgColor='#85a4c1'" onMouseout="this.bgColor='#4a7eae'">
                <td style="padding:0; margin:0; width: 180px">
                    <asp:HyperLink ID="linkAccount" Style="display: block; margin: 5px;" NavigateUrl="~/DataUserAccount.aspx" runat="server">Información de mi cuenta</asp:HyperLink>
                </td>
            </tr>
            <tr onMouseover="this.bgColor='#85a4c1'" onMouseout="this.bgColor='#4a7eae'">
                <td style="padding:0; margin:0; width: 180px">
                    <asp:HyperLink ID="linkDataCTG" Style="display: block; margin: 5px;" NavigateUrl="~/DataUserCTG.aspx" runat="server">Mis datos personales en CTE</asp:HyperLink>
                </td>
            </tr>
            <tr onMouseover="this.bgColor='#85a4c1'" onMouseout="this.bgColor='#4a7eae'">
                <td style="padding:0; margin:0; width: 180px">
                    <asp:LinkButton ID="lnkCloseSession" Style="display: block; margin: 5px;" 
                        runat="server" Text="Cerrar sesión" onclick="lnkCloseSession_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
