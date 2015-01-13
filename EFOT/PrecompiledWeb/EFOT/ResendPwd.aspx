<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="ResendPwd, App_Web_resendpwd.aspx.cdcab7d2" theme="efot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div style="height: 15px; text-align: center">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
            DisplayAfter="5">
            <ProgressTemplate>
                <div>
                    <asp:Image ID="Image2" ImageUrl="~/images/ajax-loader.gif" runat="server" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <h2>
                Recuperación de Contraseña</h2>
            <asp:Panel ID="pnlForm" runat="server">
                <asp:Label ID="lblCedula" runat="server" Text="Número de cédula:" />
                <asp:TextBox ID="txtCedula" runat="server" /><br />
                <asp:RequiredFieldValidator ID="reqValCedula" runat="server" ControlToValidate="txtCedula"
                    ErrorMessage="Debe ingresar su número de cédula" />
                <asp:RegularExpressionValidator ID="regExpValCedula" runat="server" ControlToValidate="txtCedula"
                    ErrorMessage="El número de cédula ingresado es incorrecto" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                <br />
                <asp:Button ID="btnSendPwd" runat="server" Text="Enviar contraseña" OnClick="btnSendPwd_Click" />
            </asp:Panel>
            <div id="divError" runat="server" clientidmode="Static" style="visibility: hidden;
                background-color: #fbbcc8">
                <asp:Label ID="lblError" runat="server" Text="" SkinID="Large" />
                <asp:LinkButton ID="btnTryAgain" runat="server" onclick="btnTryAgain_Click">Intentar nuevamente</asp:LinkButton>
            </div>
            <div id="divSuccess" runat="server" clientidmode="Static" style="visibility: hidden;
                background-color: #cfe7f2">
                <asp:Label ID="lblSuccess" runat="server" Text="" SkinID="Large" />
                <a href="Default.aspx">Continuar</a>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
