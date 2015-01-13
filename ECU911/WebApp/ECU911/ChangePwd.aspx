<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePwd.aspx.cs" Inherits="ChangePwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <h2>Cambio de Contraseña</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel id="divForm" style="width: 455px; height:170px; margin: 0 0 5px 40px" align="center" runat="server">
                    <asp:Label ID="lblCurrentPwd" runat="server" Text="Contraseña actual:" />
                    <asp:RequiredFieldValidator id="reqValCurrPwd" ControlToValidate="txtCurrentPwd" runat="server" ErrorMessage="*" />
                    <asp:TextBox ID="txtCurrentPwd" TextMode="Password" Width="200px" runat="server" /><br /><br />
                    
                        <asp:Label ID="lblNewPwd" runat="server" Text="Nueva contraseña:" />
                        <asp:RequiredFieldValidator id="reqValNewPwd" ControlToValidate="txtNewPwd" runat="server" ErrorMessage="*" />
                        <asp:TextBox ID="txtNewPwd" TextMode="Password" Width="200px" runat="server" /><br /><br />
                    
                        <asp:Label ID="lblConfNewPwd" runat="server" Text="Confirme su nueva contraseña:" />
                        <asp:RequiredFieldValidator id="reqValConfNewPwd" ControlToValidate="txtConfNewPwd" runat="server" ErrorMessage="*" />
                        <asp:TextBox ID="txtConfNewPwd" TextMode="Password" Width="200px" runat="server" /><br /><br />
                    
                    <asp:CompareValidator id="cmpValNewPwd" ControlToCompare="txtNewPwd" ControlToValidate="txtConfNewPwd" runat="server" ErrorMessage="La nueva contraseña y su confirmación no coinciden"></asp:CompareValidator><br />
                    <asp:Button ID="btnChangePwd" runat="server" Text="Cambiar contraseña" 
                        onclick="btnChangePwd_Click1" />
                </asp:Panel>
                <asp:Panel ID="pnlWarning" runat="server" CssClass="warning" Visible="false">
        <asp:Label ID="lblWarning" runat="server"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlMsg" runat="server" CssClass="info2" Visible="false">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </asp:Panel>
</asp:Content>

