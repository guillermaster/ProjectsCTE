<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRegistartion.aspx.cs"
    Inherits="LoginExtra_UserRegistartion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_text.css" />

    <script type="text/javascript" src="../js/common.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="height: 15px;">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                DisplayAfter="5">
                <ProgressTemplate>
                    <div>
                        <img src="../img/ajax-loader.gif" />
                        Procesando datos...
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="divForm" runat="server">
                    <table class="contactform" id="tblForm">
                        <tr>
                            <td align="right" style="width: 600px">
                                <asp:Label ID="lblCedula" runat="server" Text="Número de Cédula/Pasaporte:"></asp:Label></td>
                            <td style="width: 228px">
                                <asp:TextBox ID="txtCedula" runat="server" CausesValidation="True" MaxLength="13"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNumCedula" runat="server" ErrorMessage="*" ControlToValidate="txtCedula"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 600px">
                                <asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label></td>
                            <td style="width: 228px">
                                <asp:TextBox ID="txtNombres" runat="server" CausesValidation="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombres" runat="server" ErrorMessage="*" ControlToValidate="txtNombres"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rgValNombres" runat="server" ControlToValidate="txtNombres"
                                    ErrorMessage="*" ValidationExpression="[a-zA-Z áéíóúüñäëïöüÁÉÍÓÚÑÄËÏÖÜ]{3,}"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 600px">
                                <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label></td>
                            <td style="width: 228px">
                                <asp:TextBox ID="txtApellidos" runat="server" CausesValidation="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvApellidos" runat="server" ErrorMessage="*" ControlToValidate="txtApellidos"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rgValApellidos" runat="server" ControlToValidate="txtApellidos"
                                    ErrorMessage="*" ValidationExpression="[a-zA-Z áéíóúüñ]{3,}"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 600px">
                                <asp:Label ID="lblContrasena" runat="server" Text="Contraseña:"></asp:Label></td>
                            <td style="width: 228px">
                                <asp:TextBox ID="txtContrasena" runat="server" CausesValidation="True" MaxLength="12"
                                    TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvContrasena" runat="server" ErrorMessage="*" ControlToValidate="txtContrasena"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 600px">
                                <asp:Label ID="lblConfContrasena" runat="server" Text="Confirmar Contraseña:"></asp:Label></td>
                            <td style="width: 228px">
                                <asp:TextBox ID="txtConfContrasena" runat="server" CausesValidation="True" MaxLength="12"
                                    TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvConfContrasena" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtConfContrasena"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right" style="height: 22px; width: 600px;">
                                <asp:Label ID="lblEmail" runat="server" Text="Correo Electrónico:"></asp:Label></td>
                            <td style="height: 22px; width: 228px;">
                                <asp:TextBox ID="txtEmail" runat="server" CausesValidation="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right" style="height: 22px; width: 600px;">
                                <asp:Label ID="lblConfEmail" runat="server" Text="Confirmar Correo Electrónico:"></asp:Label></td>
                            <td style="height: 22px; width: 228px;">
                                <asp:TextBox ID="txtConfEmail" runat="server" CausesValidation="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" style="height: 26px">
                                <asp:Label ID="lblEmailWarning" runat="server" Text="Advertencia: Ingrese un correo electrónico válido o no podrá terminar el proceso de registro."
                                    Font-Bold="True" ForeColor="MidnightBlue" Width="345px" Font-Italic="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 600px">
                                <asp:Label ID="lblCaptcha" runat="server">Código de Verificación:</asp:Label></td>
                            <td style="width: 228px">
                                <asp:Image ID="imgCaptcha" runat="server" Height="50px" ImageUrl="getCaptchaImage.aspx"
                                    Width="200px" /></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 600px">
                                <asp:Label ID="lblIngreseCaptchaCode" runat="server" Text="Ingrese el código que está en la imagen arriba mostrada:"></asp:Label></td>
                            <td style="width: 228px">
                                <asp:TextBox ID="txtCaptchaCode" runat="server" CausesValidation="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCaptchaCode" runat="server" ErrorMessage="*" ControlToValidate="txtCaptchaCode"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:CompareValidator ID="cvContrasenaConfirmacion" runat="server" ErrorMessage="La contraseña y la confirmación de contraseña deben ser iguales."
                                    ControlToCompare="txtConfContrasena" ControlToValidate="txtContrasena"></asp:CompareValidator>&nbsp;<br />
                                <asp:RegularExpressionValidator ID="regExpValPasswordLength" runat="server" ErrorMessage="La longitud de la contraseña debe ser de al menos 6 caracteres"
                                    ControlToValidate="txtContrasena" ValidationExpression=".{5,}."></asp:RegularExpressionValidator>
                                <asp:CompareValidator ID="cvEmailConfirmacion" runat="server" ErrorMessage="La correo electrónico y la confirmación de correo electrónico deben ser iguales."
                                    ControlToCompare="txtConfEmail" ControlToValidate="txtEmail" Width="459px"></asp:CompareValidator>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="El correo electrónico ingresado es inválido." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="rvCedula" runat="server" ErrorMessage="El número de Cedula/Pasaporte es inválido"
                                    ControlToValidate="txtCedula" ValidationExpression="[0-9A-Z]{5,}"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 600px">
                                <asp:Button ID="btnRegistrar" runat="server" Text="Crear usuario" CssClass="button"
                                    OnClick="btnRegistrar_Click" /></td>
                            <td align="left" style="width: 228px">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" CausesValidation="False"
                                    OnClick="btnCancelar_Click" /></td>
                        </tr>
                    </table>
                </div>
                <br />
                <div id="divConfirmation" visible="false" runat="server">
                    <table>
                    <tr>
                    <td>
                        <img src="../img/paperplaneCTG.jpg" />
                    </td>
                    <td>
                        <div style="width:70%">
                            <h2>Por favor revise su correo electrónico</h2>
                            <ul style="list-style-type:square">
                                <li>Usted debe <span style="background-color:Yellow">hacer clic en el enlace que se encuentra en el correo</span> que le acabamos
                                    de enviar, de tal forma que pueda finalizar el proceso de registro.</li>
                                <li>El correo fue enviado a <asp:Label ID="lblEmailSent" runat="server" Text=""></asp:Label></li>
                                <li><asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" OnClick="LinkButton1_Click">Continuar</asp:LinkButton></li>
                            </ul>
                            <hr style="margin: 15px 10px 15px 10px;" width="50%" />
                            <h3>¿No recibió el correo electrónico?</h3>
                            <ul class="reg-secondary">
                                <li>¿Han pasado menos de 10 minutos? Espere un momento &mdash; la recepción del correo puede tomar unos minutos.</li>
                                <li>Revise su carpeta de spam o correos no deseados en caso de.</li>
                                <li>Intente <a href="ResendActivationEmail.aspx">reenviando</a>
                                    el correo.</li>
                            </ul>
                          </div>
                        </td>
                        </tr>
                        </table>
                        <br style="clear: both;" />
                    </div>
                    <!-- end #wrapper -->
                
                <div id="divError" runat="server" visible="false">
                    <table class="error2" align="center" width="480">
                        <tr>
                            <td style="width: 54px">
                                <img src="../img/error.gif" /></td>
                            <td>
                                <b>Error:</b><br />
                                <asp:Label ID="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
