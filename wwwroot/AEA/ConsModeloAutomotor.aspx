<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="ConsModeloAutomotor.aspx.cs"
    Inherits="Automotor" %>

<%@ Register Src="UserControls/PrintButton.ascx" TagName="PrintButton" TagPrefix="uc1" %>

<%@ Register Src="UserControls/BackButton.ascx" TagName="BackButton" TagPrefix="uc1" %>

<%@ Register Src="UserControls/EditButton.ascx" TagName="EditButton" TagPrefix="uc1" %>

<%@ Register Src="UserControls/NewButton.ascx" TagName="NewButton" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-content1" align="center">
        <form id="regForm">
            <div>
                <uc1:BackButton ID="btnBack" Visible="false" runat="server" />
                <uc1:NewButton ID="btnNew" runat="server" />
                <uc1:NewButton ID="btnNewFrom" ImageURL="~/img/btnNuevoFrom.gif" Visible="false" runat="server" />
                <uc1:EditButton ID="btnModify" runat="server" Visible="false" />
                &nbsp;
                <asp:ImageButton ID="btnDesactivar" runat="server" ImageUrl="~/img/btnDesactivar.gif" Visible="False" OnClick="btnDesactivar_Click" />
                <uc1:PrintButton ID="btnPrint" runat="server" />
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
<asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5" DynamicLayout="False">
            <ProgressTemplate>
             <div align="center">
                <img src="img/ajax-loader.gif" />
              </div>
        </ProgressTemplate>
        </asp:UpdateProgress> <DIV id="divContent"><DIV class="registerform"><asp:Label id="lblCodAutomotor" runat="server" Text="Código de Automotor:"></asp:Label> <asp:TextBox id="txtCodAutomotor" runat="server"></asp:TextBox> <asp:RequiredFieldValidator id="reqValCodAutomotor" runat="server" SetFocusOnError="True" __designer:wfdid="w17" ErrorMessage="*" ControlToValidate="txtCodAutomotor"></asp:RequiredFieldValidator> <asp:RegularExpressionValidator id="regExpValCodAutomotor" runat="server" SetFocusOnError="True" __designer:wfdid="w18" ErrorMessage="*" ControlToValidate="txtCodAutomotor" ValidationExpression="0*[1-9][0-9]*"></asp:RegularExpressionValidator> <asp:Button id="btnSearch" onclick="btnSearch_Click" runat="server" Text="Buscar" CssClass="button_horiz"></asp:Button><BR /></DIV><BR /><asp:HiddenField id="hdnAnio" runat="server"></asp:HiddenField> <asp:HiddenField id="hdnCodMarca" runat="server"></asp:HiddenField> <asp:HiddenField id="hdnCodModelo" runat="server"></asp:HiddenField> <asp:HiddenField id="hdnCodColor" runat="server"></asp:HiddenField> <asp:DetailsView id="detViewAutomotor" runat="server" ForeColor="#333333" AutoGenerateRows="False" CellPadding="4" GridLines="None" Height="50px" EmptyDataText="No existe modelo de automotor con el código ingresado">
<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

<CommandRowStyle BackColor="#E2DED6" Font-Bold="True"></CommandRowStyle>

<RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>

<FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True"></FieldHeaderStyle>

<PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>
<Fields>
<asp:BoundField DataField="anio_produccion" HeaderText="A&#241;o de producci&#243;n"></asp:BoundField>
<asp:BoundField DataField="marca" HeaderText="Marca"></asp:BoundField>
<asp:BoundField DataField="modelo" HeaderText="Modelo"></asp:BoundField>
<asp:BoundField DataField="color" HeaderText="Color"></asp:BoundField>
<asp:BoundField DataField="estado" HeaderText="Estado"></asp:BoundField>
<asp:BoundField DataField="fecha_registro" HeaderText="Fecha de Registro"></asp:BoundField>
<asp:BoundField DataField="fecha_actualizacion" HeaderText="Fecha de Actualizaci&#243;n"></asp:BoundField>
<asp:BoundField DataField="fecha_crea_axis" HeaderText="Fecha de Aprobaci&#243;n"></asp:BoundField>
<asp:BoundField DataField="fecha_eliminacion" HeaderText="Fecha de Desactivaci&#243;n"></asp:BoundField>
<asp:BoundField DataField="usuario_registra" HeaderText="Usuario Registra"></asp:BoundField>
<asp:BoundField DataField="usuario_actualiza" HeaderText="Usuario Actualiza"></asp:BoundField>
<asp:BoundField DataField="usuario_crea_axis" HeaderText="Usuario Aprueba"></asp:BoundField>
<asp:BoundField DataField="usuario_elimina" HeaderText="Usuario Desactiva"></asp:BoundField>
<asp:BoundField DataField="observacion" HeaderText="Observaci&#243;n"></asp:BoundField>
</Fields>

<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#999999"></EditRowStyle>

<AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
</asp:DetailsView> </DIV><BR /><asp:ImageButton id="btnVerFoto" onclick="btnVerFoto_Click" runat="server" ImageUrl="~/img/btnVerFoto.gif" Visible="False"></asp:ImageButton> <cc1:ModalPopupExtender id="mdlPopup" runat="server" TargetControlID="btnVerFoto" PopupControlID="pnlFotoModeloAutomotor" CancelControlID="imgFotoModeloAutomotor" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender> <DIV id="divDesactiva" runat="server" visible="false"><asp:Label id="lblDesactivaObs" runat="server" Text="Observación:"></asp:Label> <asp:TextBox id="txtDesactivaObs" runat="server" Rows="3" TextMode="MultiLine" Width="430px"></asp:TextBox> <asp:RequiredFieldValidator id="reqFieldObserv" runat="server" ErrorMessage="Debe ingresar una observación" ControlToValidate="txtDesactivaObs"></asp:RequiredFieldValidator> <BR /><asp:Button id="btnDesacitvar" onclick="btnDesacitvar_Click" runat="server" Text="Desactivar" CssClass="button" Width="93px"></asp:Button> <asp:Button id="btnDesacCancelar" onclick="btnDesacCancelar_Click" runat="server" Text="Cancelar" CausesValidation="False" CssClass="button" Width="80px"></asp:Button> </DIV><DIV id="divError" runat="server" visible="false"><TABLE class="error2" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/error.gif" /></TD><TD><B>Error:</B><BR /><asp:Label id="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV><DIV id="divWarning" runat="server" visible="false"><TABLE class="warning" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/warning.gif" /></TD><TD><asp:Label id="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                </Triggers>
            </asp:UpdatePanel>
            
                
            <asp:Panel ID="pnlFotoModeloAutomotor" runat="server" Style="display: none;">
                <asp:UpdatePanel ID="upFotoModeloAutomotor" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div align="center">
                         <asp:ImageButton ID="imgFotoModeloAutomotor" ImageUrl="FotoModeloAutomotor.aspx" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </form>
    </div>
    
</asp:Content>