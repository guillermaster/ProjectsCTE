<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserInfo.aspx.cs" Inherits="UserInfo" Title="CTG - Información de usuario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="height: 10px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DisplayAfter="5">
            <ProgressTemplate>
                <div>
                    <img src="img/ajax-loader.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin: 10px;">
                <h3>
                    Datos de Usuario</h3>
                <div>
                    <asp:Label ID="lblUsername" runat="server" Text="Usuario:" Height="20px"></asp:Label>
                    <asp:TextBox ID="txtUsername" runat="server" ReadOnly="true" Height="20px" />
                </div>
                <div style="margin-top: 10px;">
                    <asp:LinkButton ID="btnChangePwd" runat="server" OnClick="btnChangePwd_Click">Cambiar contraseña</asp:LinkButton>
                </div>
                <ajaxToolkit:AnimationExtender ID="AnimationExtender1" TargetControlID="tblChangePwd"
                    runat="server">
                    <Animations>
                        <OnLoad>
                            <FadeIn Duration=".9" Fps="20" />
                        </OnLoad>
                    </Animations>
                </ajaxToolkit:AnimationExtender>
                <table id="tblChangePwd" runat="server" style="width: 463px" visible="false">
                    <tr>
                        <td style="width: 128px">
                            <asp:Label ID="lblContrasena" runat="server">Contraseña 
                    Actual:</asp:Label>
                        </td>
                        <td style="width: 110px">
                            <asp:TextBox ID="txtCurrPwd" runat="server" MaxLength="12" TextMode="Password" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="reqFieldVal1" runat="server" ControlToValidate="txtCurrPwd"
                                Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 128px">
                            <asp:Label ID="lblNuevaContrasena" runat="server">Nueva 
                    contraseña:</asp:Label>
                        </td>
                        <td style="width: 110px">
                            <asp:TextBox ID="txtNewPwd" runat="server" MaxLength="12"
                                TextMode="Password" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="reqFieldVal2" runat="server" ControlToValidate="txtNewPwd"
                                Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 128px; height: 40px;">
                            <asp:Label ID="lblConfContrasena" runat="server">Confirme 
                    contraseña:</asp:Label>
                        </td>
                        <td style="width: 110px; height: 40px;">
                            <asp:TextBox ID="txtNewPwdConf" runat="server" MaxLength="12"
                                TextMode="Password" />
                        </td>
                        <td style="height: 40px;">
                            <asp:RequiredFieldValidator ID="reqFieldVal3" runat="server" ControlToValidate="txtNewPwdConf"
                                Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="3">
                            <asp:Button ID="btnModificar" runat="server" CssClass="button" OnClick="btnModificar_Click"
                                Text="Modificar Contraseña" />
                            <asp:Button ID="btnCancelar" runat="server" CssClass="button" 
                                Text="Cancelar" onclick="btnCancelar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNewPwd"
                                CssClass="blackTitle2" Display="Dynamic" ErrorMessage="La nueva contraseña debe ser de al menos 8 caracteres de longitud, y tener al menos 1 caracter numérico"
                                ValidationExpression="(?=.{8,})[a-z]+[^a-z]+|[^a-z]+[a-z]+"></asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPwd"
                                ControlToValidate="txtNewPwdConf" CssClass="blackTitle2" Display="Dynamic" ErrorMessage="La confirmación de la nueva contraseña no coincide"></asp:CompareValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtNewPwd"
                                ControlToValidate="txtCurrPwd" CssClass="blackTitle2" Display="Dynamic" ErrorMessage="La nueva contraseña debe ser distinta a la actual"
                                Operator="NotEqual"></asp:CompareValidator>
                            <asp:Label ID="lblError" runat="server" CssClass="error" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hidAlert" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type='text/javascript'>
var _added = false;
var _alert;
function pageLoad(sender, args){
// register end request handler
if(!_added){
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
_added = true;
}
_alert = $get('ctl00_ContentPlaceHolder1_hidAlert');
}
function endRequest(sender, args) {
if(_alert.value.length > 0) {
alert(_alert.value);

_alert.value = "";

}
}

    </script>

</asp:Content>
