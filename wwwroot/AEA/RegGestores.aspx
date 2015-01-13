<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RegGestores.aspx.cs" Inherits="RegGestores" %>

<%@ Register Src="UserControls/SaveButton.ascx" TagName="SaveButton" TagPrefix="uc1" %>

<%@ Register Src="UserControls/BackButton.ascx" TagName="BackButton" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-content1" align="center">
        <uc1:BackButton ID="btnBack" runat="server" />
        <uc1:SaveButton ID="btnSave" Visible="false" runat="server" OnLoad="btnSave_Load" />
        <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5" DynamicLayout="False">
                            <ProgressTemplate>
                                <div align="center">
                                    <img src="img/ajax-loader.gif" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
        
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
<DIV id="divForms" runat="server"><TABLE class="registerform"><TBODY><TR><TD><asp:Label id="lblCedula" runat="server" Text="Cédula:" CssClass="label"></asp:Label> <asp:RequiredFieldValidator id="reqValCedula" runat="server" __designer:wfdid="w35" ErrorMessage="*" ControlToValidate="txtCedula"></asp:RequiredFieldValidator> <asp:RegularExpressionValidator id="regExpValCedula" runat="server" __designer:wfdid="w42" ErrorMessage="Ingrese una cédula válida" ControlToValidate="txtCedula" ValidationExpression="0*[1-9][0-9]*"></asp:RegularExpressionValidator><BR /><asp:TextBox id="txtCedula" runat="server" CssClass="field" Width="168px"></asp:TextBox> </TD><TD><asp:Label id="lblNombre" runat="server" Text="Nombre:" CssClass="label"></asp:Label> <asp:RequiredFieldValidator id="reqValNombre" runat="server" __designer:wfdid="w36" ErrorMessage="*" ControlToValidate="txtNombre"></asp:RequiredFieldValidator><BR /><asp:TextBox id="txtNombre" runat="server" CssClass="field" Width="380px"></asp:TextBox> </TD></TR><TR><TD><asp:Label id="lblProvincia" runat="server" Text="Provincia:" CssClass="label"></asp:Label> <asp:RequiredFieldValidator id="reqValProvincia" runat="server" __designer:wfdid="w37" ErrorMessage="*" ControlToValidate="ddlProvincia"></asp:RequiredFieldValidator><BR /><asp:DropDownList id="ddlProvincia" runat="server" CssClass="field" Width="179px"></asp:DropDownList> </TD><TD><asp:Label id="lblCanton" runat="server" Text="Cantón:" Visible="False" CssClass="label"></asp:Label> <asp:RequiredFieldValidator id="reqValCanton" runat="server" __designer:wfdid="w38" ErrorMessage="*" ControlToValidate="ddlCanton"></asp:RequiredFieldValidator><BR /><asp:DropDownList id="ddlCanton" runat="server" Visible="False" CssClass="field" Width="179px"></asp:DropDownList> </TD></TR><TR><TD><asp:Label id="lblTelefonoConv" runat="server" Text="Teléfono Convencional:" CssClass="label"></asp:Label> <asp:RequiredFieldValidator id="reqValTelefonoConv" runat="server" __designer:wfdid="w39" ErrorMessage="*" ControlToValidate="txtTelefonoConv"></asp:RequiredFieldValidator><BR /><asp:TextBox id="txtTelefonoConv" runat="server" CssClass="field" Width="160px"></asp:TextBox> </TD><TD><asp:Label id="lblTelefonoMovil" runat="server" Text="Teléfono Móvil:" CssClass="label"></asp:Label> <asp:RegularExpressionValidator id="regExpValTelefonoMov" runat="server" __designer:wfdid="w43" ErrorMessage="Ingrese un teléfono válido" ControlToValidate="txtTelefonoMovil" ValidationExpression="0*[1-9][0-9]*"></asp:RegularExpressionValidator> <BR /><asp:TextBox id="txtTelefonoMovil" runat="server" CssClass="field" Width="160px"></asp:TextBox> </TD></TR><TR><TD><asp:Label id="lblEmail" runat="server" Text="E-Mail:" CssClass="label"></asp:Label> <asp:RequiredFieldValidator id="reqValEmail" runat="server" __designer:wfdid="w41" ErrorMessage="*" ControlToValidate="txtEmail"></asp:RequiredFieldValidator> <asp:RegularExpressionValidator id="regExpVal" runat="server" __designer:wfdid="w44" ErrorMessage="Ingrese un e-mail válido" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><BR /><asp:TextBox id="txtEmail" runat="server" CssClass="field" Width="278px"></asp:TextBox><BR /></TD><TD></TD></TR><TR><TD colSpan=2><asp:Label id="lblDireccionDomicilio" runat="server" Text="Dirección de Domicilio:" CssClass="label"></asp:Label> <BR /><asp:TextBox id="txtDireccionDomicilio" runat="server" CssClass="field" Width="600px"></asp:TextBox> </TD></TR><TR><TD colSpan=2><asp:Label id="lblDireccionLaboral" runat="server" Text="Dirección Laboral:" CssClass="label"></asp:Label> <BR /><asp:TextBox id="txtDireccionLaboral" runat="server" CssClass="field" Width="600px"></asp:TextBox><BR /></TD></TR><TR><TD colSpan=2><asp:Label id="lblFoto" runat="server" Text="Foto (o licencia digitalizada):" CssClass="label"></asp:Label> <asp:RequiredFieldValidator id="reqFieldFoto" runat="server" ErrorMessage="&nbsp;*" ControlToValidate="fileupFoto"></asp:RequiredFieldValidator> <asp:RegularExpressionValidator id="FileUpLoadValidator" runat="server" ErrorMessage="El formato de la foto debe ser JPG" ControlToValidate="fileupFoto" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpeg|.JPEG|.jpg|.JPG)$"></asp:RegularExpressionValidator><BR /><asp:FileUpload id="fileupFoto" runat="server" CssClass="field" Width="248px"></asp:FileUpload> <asp:ImageButton id="btnVerFoto" runat="server" ImageUrl="~/img/btnVerFoto.gif" Visible="False"></asp:ImageButton> </TD></TR></TBODY></TABLE><BR /><cc1:ModalPopupExtender id="mdlPopup" runat="server" TargetControlID="btnVerFoto" PopupControlID="pnlFotoModeloAutomotor" CancelControlID="imgFotoModeloAutomotor" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender> <DIV align=center>&nbsp;&nbsp;<BR /></DIV></DIV><DIV id="divError" runat="server" visible="false"><TABLE class="error2" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/error.gif" /></TD><TD><B>Error:</B><BR /><asp:Label id="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV><DIV id="divWarning" runat="server" visible="false"><TABLE class="warning" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/warning.gif" /></TD><TD><asp:Label id="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
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
            
    </div>
 </asp:Content>