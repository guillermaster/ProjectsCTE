<%@ Page Language="C#" MasterPageFile="~/NotLoggedOnV2.master" AutoEventWireup="true"
    CodeFile="Logon.aspx.cs" Inherits="Logon" %>

<%@ Register Src="UserControls/BrowserNotSupported.ascx" TagName="BrowserNotSupported"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/VerisignLogo.ascx" TagName="VerisignLogo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightColumnContent" runat="Server">
    <div class="full">
        <!--<div style="background: url(images/pwdBg.gif) right bottom no-repeat; height: 178px; padding: 0 20px;">-->
        <div style="height: 178px; padding: 0 20px;">
            <h3>
                ¿No posee contraseña de acceso o no puede acceder?</h3>
            <ul style="margin-left: 15px; list-style-image: url('images/bulletDark.gif');">
                <li><a href="LoginExtra/UserRegistartion.aspx">Registre una nueva cuenta</a></li>
                <li><a href="LoginExtra/LostPassword.aspx">Recuerde su contraseña</a></li>
                <li><a href="LoginExtra/ResendActivationEmail.aspx">Solicite el reenvío e-mail de activación
                    de cuenta</a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="full" style="height: 178px; margin-left: 150px;">
        <asp:Login ID="Login1" runat="server" SkinID="SmallLogin" OnAuthenticate="Login1_Authenticate" DisplayRememberMe="False" />
        <div style="margin: 20px 0 0 400px;">
            <uc1:VerisignLogo ID="VerisignLogo1" runat="server" />
        </div>
    </div>
    <uc1:BrowserNotSupported ID="BrowserNotSupported1" runat="server" Visible="false" />
</asp:Content>
