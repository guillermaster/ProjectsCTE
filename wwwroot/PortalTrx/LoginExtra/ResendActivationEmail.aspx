<%@ Page Language="C#" MasterPageFile="OneColumnV2.master" AutoEventWireup="true"
    CodeFile="ResendActivationEmail.aspx.cs" Inherits="LoginExtra_ResendActivationEmail" %>

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
    </style>
    <script type="text/javascript" src="../js/jquery.min.js"></script>
    <script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../js/alertbox.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../js/style.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="height: 15px; text-align: center">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DisplayAfter="5">
            <ProgressTemplate>
                <div>
                    <img src="../images/ajax-loader.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="title">
                Reenvío de Correo Electrónico de Activación
            </div>
            <div class="full" style="height:100px">
                <div style="text-align: center; margin: 15px;">
                    <asp:Label ID="lblMensaje" runat="server" Text="Ingrese su identificación para que reciba su mensaje de activación." /></div>
                <div runat="server" id="divForm">
                    <div style="height:30px">
                        <div class="regLeft">
                            <asp:Label ID="lblCedula" runat="server" Text="Número de Cédula / Pasaporte/ RUC:" />
                        </div>
                        <div class="regRight">
                            <asp:TextBox ID="txtCedula" runat="server" CausesValidation="True" MaxLength="18" />
                            <asp:RequiredFieldValidator ID="reqValIdent" runat="server" 
                                ControlToValidate="txtCedula" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div>
                        <div class="regLeft">
                            <asp:Button ID="btnSolicitar" runat="server" Text="Enviar" OnClick="btnSolicitar_Click" />
                        </div>
                        <div class="regRight">
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
