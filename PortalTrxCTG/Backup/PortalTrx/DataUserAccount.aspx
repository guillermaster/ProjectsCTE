<%@ Page Title="" Language="C#" MasterPageFile="~/OneColumnV2.master" AutoEventWireup="true"
    CodeFile="DataUserAccount.aspx.cs" Inherits="DataUserAccount" %>

<%@ MasterType VirtualPath="~/MainV2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="js/alertbox.js"></script>
    <script type="text/javascript">
        function ok(sender, e) {
            //$find('MPE').hide();
            //alert("tesfdft");
            __doPostBack('btnSaveNewPwd', e);

        }

        function cancel(sender, e) {
            //$find('MPE').hide();
            __doPostBack('btnSaveNewPwd', e);
        }
        function pageLoad() {
        }
    </script>
    <link rel="stylesheet" type="text/css" media="all" href="js/style.css" />
    <style type="text/css">
        table
        {
            text-align: center;
            width: 100%;
            border: 2;
            border-style: dashed;
            margin-left: 20px;
        }
        table th
        {
            text-align: right;
            padding: 10px;
            vertical-align: middle;
            width: 25%;
        }
        table td
        {
            text-align: left;
            padding: 10px;
            width: 75%;
        }
        
        .barIndicatorBorder
        {
            border: solid 1px #c0c0c0;
            width: 120px;
        }
        
        .barIndicator_poor
        {
            background-color: gray;
        }
        
        .barIndicator_weak
        {
            background-color: cyan;
        }
        
        .barIndicator_good
        {
            background-color: lightblue;
        }
        
        .barIndicator_strong
        {
            background-color: blue;
        }
        
        .barIndicator_excellent
        {
            background-color: navy;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divMain" runat="server">
        <div class="title">
            Información de mi cuenta
        </div>
        <asp:Panel runat="server" ID="pnlContent" class="full" style="width:80%">
            <table>
                <tr>
                    <th>
                        <asp:Label ID="lblIdentificacion" runat="server" Text="Identificación" />
                    </th>
                    <td>
                        <asp:TextBox ID="txtIdentificacion" ReadOnly="true" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Label ID="lblNombres" runat="server" Text="Nombres" />
                    </th>
                    <td>
                        <asp:HiddenField ID="hdnNombres" runat="server" />
                        <asp:TextBox ID="txtNombres" ReadOnly="true" runat="server" Width="280" />
                        <asp:ImageButton ID="btnEditNombres" ImageUrl="~/images/icoXsEditField.png" runat="server"
                            OnClick="btnEditNombres_Click" />
                        <asp:ImageButton ID="btnSaveNombres" ImageUrl="~/images/btnSmGuardar.png" runat="server"
                            Visible="false" OnClick="btnSaveNombres_Click" />
                        <asp:ImageButton ID="btnCancelNombres" ImageUrl="~/images/btnSmCancelar.png" runat="server"
                            Visible="false" OnClick="btnCancelNombres_Click" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Label ID="lblApellidos" runat="server" Text="Apellidos" />
                    </th>
                    <td>
                        <asp:HiddenField ID="hdnApellidos" runat="server" />
                        <asp:TextBox ID="txtApellidos" ReadOnly="true" runat="server" Width="280" />
                        <asp:ImageButton ID="btnEditApellidos" ImageUrl="~/images/icoXsEditField.png" runat="server"
                            OnClick="btnEditApellidos_Click" />
                        <asp:ImageButton ID="btnSaveApellidos" ImageUrl="~/images/btnSmGuardar.png" runat="server"
                            OnClick="btnSaveApellidos_Click" Visible="false" />
                        <asp:ImageButton ID="btnCancelApellidos" ImageUrl="~/images/btnSmCancelar.png" runat="server"
                            OnClick="btnCancelApellidos_Click" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Label ID="lblEmail" runat="server" Text="Correo Electrónico" />
                    </th>
                    <td>
                        <asp:HiddenField ID="hdnEmail" runat="server" />
                        <asp:TextBox ID="txtEmail" ReadOnly="true" runat="server" Width="280" />
                        <asp:ImageButton ID="btnEditEmail" ImageUrl="~/images/icoXsEditField.png" runat="server"
                            OnClick="btnEditEmail_Click" />
                        <asp:ImageButton ID="btnSaveEmail" ImageUrl="~/images/btnSmGuardar.png" runat="server"
                            Visible="false" OnClick="btnSaveEmail_Click" />
                        <asp:ImageButton ID="btnCancelEmail" ImageUrl="~/images/btnSmCancelar.png" runat="server"
                            Visible="false" OnClick="btnCancelEmail_Click" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Label ID="lblPassword" runat="server" Text="Contraseña" />
                        <asp:HiddenField ID="hdnPassword" runat="server" />
                    </th>
                    <td>
                        <asp:LinkButton ID="btnchangepwd" runat="server" OnClick="btnchangepwd_Click">Cambiar contraseña</asp:LinkButton>
                        <asp:Panel ID="pnlChangePassword" runat="server" BackColor="AntiqueWhite" Style="display: none; padding: 10px;
                            filter: alpha(opacity=95); opacity: 0.95; text-align: center;"
                            ScrollBars="None" DefaultButton="btnSaveNewPwd">
                            <table style="width: 640px; ">
                                <tr>
                                    <td style="width: 30%; text-align: left;">
                                        Contraseña actual:
                                    </td>
                                    <td style="width: 70px">
                                        <asp:TextBox ID="txtPwdCurr" TextMode="Password" runat="server" />
                                        <asp:TextBox ID="txtCurrPwd" runat="server" TextMode="Password" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: left;">
                                        Nueva contraseña:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPwdNew" TextMode="Password" runat="server" />
                                        <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password" Visible="false" />
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: left;"></td>
                                    <td>
                                        <asp:passwordstrength runat="server" id="PasswordStrength1" targetcontrolid="txtPwdNew"
                                            displayposition="AboveLeft" minimumsymbolcharacters="1" minimumuppercasecharacters="1"
                                            preferredpasswordlength="12" calculationweightings="25;25;15;35" requiresupperandlowercasecharacters="true"
                                            textstrengthdescriptions="Inseguro; Débil; Aceptable; Bueno; Excelente"
                                            strengthindicatortype="BarIndicator" barbordercssclass="barIndicatorBorder"
                                            strengthstyles="barIndicator_poor; barIndicator_weak; barIndicator_good; barIndicator_strong; barIndicator_excellent"
                                            ClientIDMode="Static">
                                        </asp:passwordstrength>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: left;">
                                        Confirmación de nueva contraseña:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPwdNewConf" TextMode="Password" runat="server" />
                                        <asp:TextBox ID="txtNewPwdConf" runat="server" TextMode="Password" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: right;">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <asp:ImageButton ID="btnSaveNewPwd" ImageUrl="~/images/btnSmGuardar.png" runat="server"
                                OnClick="btnSaveNewPwd_Click" ClientIDMode="Static" />
                            <asp:ImageButton ID="btnCancelNewPwd" ImageUrl="~/images/btnSmCancelar.png" runat="server"
                                OnClick="btnCancelNewPwd_Click" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Label ID="lblEstado" runat="server" Text="Estado" />
                    </th>
                    <td>
                        <asp:TextBox ID="txtEstado" ReadOnly="true" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Button ID="Button1" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="MPE" runat="server" TargetControlID="Button1" PopupControlID="pnlChangePassword"
            BackgroundCssClass="modalBackground" DropShadow="false" CancelControlID="btnCancelNewPwd" />
    </div>
</asp:Content>
