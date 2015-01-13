<%@ Page Language="C#" MasterPageFile="OneColumnV2.master" AutoEventWireup="true"
    CodeFile="UserRegistartion.aspx.cs" Inherits="LoginExtra_UserRegistartion" %>
<%@ MasterType VirtualPath="OneColumnV2.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .regLeft
        {
            float: left;
            width: 48%;
            text-align: right;
        }
        .regRight
        {
            float: right;
            width: 48%;
            text-align: left;
        }
        .row
        {
            height: 32px;
        }
    </style>
    <script type="text/javascript" src="../js/jquery.min.js"></script>
    <script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../js/alertbox.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../js/style.css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
            <div class="full" runat="server" id="divForm" style="height: 390px">
                <div class="row">
                    <div class="regLeft">
                        <asp:Label ID="lblCedula" runat="server" Text="Número de Cédula/Pasaporte:" />
                    </div>
                    <div class="regRight">
                        <asp:TextBox ID="txtCedula" runat="server" CausesValidation="True" MaxLength="13"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNumCedula" runat="server" ErrorMessage="*" ControlToValidate="txtCedula"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rvCedula" runat="server" ErrorMessage="La identificación ingresada es incorrecta."
                            ControlToValidate="txtCedula" ValidationExpression="[0-9A-Z]{5,}" />
                    </div>
                </div>
                <div class="row">
                    <div class="regLeft">
                        <asp:Label ID="lblNombres" runat="server" Text="Nombres:" />
                    </div>
                    <div class="regRight">
                        <asp:TextBox ID="txtNombres" runat="server" CausesValidation="True" />
                        <asp:RequiredFieldValidator ID="rfvNombres" runat="server" ErrorMessage="*" ControlToValidate="txtNombres" /><br />
                        <asp:RegularExpressionValidator ID="rgValNombres" runat="server" ControlToValidate="txtNombres"
                            ErrorMessage="*" ValidationExpression="[a-zA-Z áéíóúüñ]{3,}" />
                    </div>
                </div>
                <div class="row">
                    <div class="regLeft">
                        <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:" />
                    </div>
                    <div class="regRight">
                        <asp:TextBox ID="txtApellidos" runat="server" CausesValidation="True" />
                        <asp:RequiredFieldValidator ID="rfvApellidos" runat="server" ErrorMessage="*" ControlToValidate="txtApellidos" />
                        <asp:RegularExpressionValidator ID="rgValApellidos" runat="server" ControlToValidate="txtApellidos"
                            ErrorMessage="*" ValidationExpression="[a-zA-Z áéíóúüñ]{3,}" />
                    </div>
                </div>
                <div class="row">
                    <div class="regLeft">
                        <asp:Label ID="lblContrasena" runat="server" Text="Contraseña:" />
                    </div>
                    <div class="regRight">
                        <asp:TextBox ID="txtContrasena" runat="server" CausesValidation="True" MaxLength="12"
                            TextMode="Password" />
                        <asp:RequiredFieldValidator ID="rfvContrasena" runat="server" ErrorMessage="*" ControlToValidate="txtContrasena" />
                        <asp:RegularExpressionValidator ID="regExpValPasswordLength" runat="server" ErrorMessage="La longitud de la contraseña debe ser de al menos 6 caracteres."
                            ControlToValidate="txtContrasena" ValidationExpression=".{5,}." />
                    </div>
                </div>
                <div class="row">
                    <div class="regLeft">
                        <asp:Label ID="lblConfContrasena" runat="server" Text="Confirmar Contraseña:" />
                    </div>
                    <div class="regRight">
                        <asp:TextBox ID="txtConfContrasena" runat="server" CausesValidation="True" MaxLength="12"
                            TextMode="Password" />
                        <asp:RequiredFieldValidator ID="rfvConfContrasena" runat="server" ErrorMessage="*"
                            ControlToValidate="txtConfContrasena" />
                        <asp:CompareValidator ID="cvContrasenaConfirmacion" runat="server" ErrorMessage="La contraseña y su confirmación deben ser iguales."
                            ControlToCompare="txtConfContrasena" ControlToValidate="txtContrasena" />
                    </div>
                </div>
                <div class="row">
                    <div class="regLeft">
                        <asp:Label ID="lblEmail" runat="server" Text="Correo Electrónico:" />
                    </div>
                    <div class="regRight">
                        <asp:TextBox ID="txtEmail" runat="server" CausesValidation="True" CssClass="lowercase" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmail" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="El correo electrónico ingresado es inválido." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </div>
                </div>
                <div class="row">
                    <div class="regLeft">
                        <asp:Label ID="lblConfEmail" runat="server" Text="Confirmar Correo Electrónico:" />
                    </div>
                    <div class="regRight">
                        <asp:TextBox ID="txtConfEmail" runat="server" CausesValidation="True" CssClass="lowercase" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            ControlToValidate="txtEmail" />
                        <asp:CompareValidator ID="cvEmailConfirmacion" runat="server" ErrorMessage="El correo y su confirmación deben ser iguales."
                            ControlToCompare="txtConfEmail" ControlToValidate="txtEmail" />
                    </div>
                </div>
                <div class="row" style="text-align: center">
                    <asp:Label ID="lblEmailWarning" runat="server" Text="Ingrese un correo electrónico válido o no podrá terminar el proceso de registro."
                        Font-Bold="True" ForeColor="MidnightBlue" Font-Italic="True" />
                </div>
                <div style="height: 60px">
                    <div class="regLeft">
                        <asp:Label ID="lblCaptcha" runat="server" Text="Código de Verificación:" />
                    </div>
                    <div class="regRight">
                        <asp:Image ID="imgCaptcha" runat="server" Height="50px" ImageUrl="getCaptchaImage.aspx"
                            Width="200px" />
                    </div>
                </div>
                <div class="row">
                    <div class="regLeft">
                        <asp:Label ID="lblIngreseCaptchaCode" runat="server" Text="Ingrese el código que está en la imagen arriba mostrada:" />
                    </div>
                    <div class="regRight">
                        <asp:TextBox ID="txtCaptchaCode" runat="server" CausesValidation="true" />
                        <asp:RequiredFieldValidator ID="rfvCaptchaCode" runat="server" ErrorMessage="*" ControlToValidate="txtCaptchaCode" />
                    </div>
                </div>
                <div>
                    <div class="regLeft">
                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar" OnClick="btnContinuar_Click" />
                    </div>
                    <div class="regRight">
                        <asp:Button ID="btnCancelar" runat="server" CausesValidation="false" 
                            Text="Cancelar" onclick="btnCancelar_Click" />
                    </div>
                </div>
            </div>
            <div id="divError" runat="server" visible="false">
            </div>
            <div class="full" runat="server" id="divConfirmation" visible="false">
                <table>
                    <tr>
                        <td style="height: 323px">
                            <img src="../images/paperplaneCTG.png" />
                        </td>
                        <td style="height: 323px">
                            <div style="width: 70%">
                                <h2>
                                    Por favor revise su correo electrónico</h2>
                                <ul style="list-style-type: square">
                                    <li>Usted debe <span style="background-color: Yellow">hacer clic en el enlace que se
                                        encuentra en el correo</span> que le acabamos de enviar, de tal forma que pueda
                                        finalizar el proceso de registro.</li><li>El correo fue enviado a
                                            <asp:Label ID="lblEmailSent" runat="server" Text=""></asp:Label></li><li>
                                                <asp:LinkButton ID="btnLnkCountinuar" runat="server" CausesValidation="False" OnClick="btnLnkCountinuar_Click">Continuar</asp:LinkButton></li></ul>
                                <hr style="margin: 15px 10px 15px 10px;" width="50%" />
                                <h3>
                                    ¿No recibió el correo electrónico?</h3>
                                <ul class="reg-secondary">
                                    <li>¿Han pasado menos de 10 minutos? Espere un momento &mdash; la recepción del correo
                                        puede tomar unos minutos.</li><li>Revise su carpeta de spam o correos no deseados en
                                            caso de.</li><li>Intente solicitando una vez más el <a href="ResendActivationEmail.aspx">
                                                envío del correo de confirmación</a>.</li></ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
</asp:Content>
