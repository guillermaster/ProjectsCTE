<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ModeloAutomotor.aspx.cs" Inherits="ModeloAutomotor" %>

<%@ Register Src="UserControls/SaveButton.ascx" TagName="SaveButton" TagPrefix="uc1" %>

<%@ Register Src="UserControls/BackButton.ascx" TagName="BackButton" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-content1" align="center">
    <uc1:BackButton ID="btnBack" runat="server" />
        <uc1:SaveButton ID="btnSave" runat="server" Visible="false" OnLoad="SaveButton1_Load" />
  <form id="regForm">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager><asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
      DisplayAfter="5" DynamicLayout="False">
      <progresstemplate>
             <div align="center">
                <img src="img/ajax-loader.gif" />
              </div>
        </progresstemplate>
  </asp:UpdateProgress>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
<DIV id="divForms" class="registerform" runat="server"><asp:HiddenField id="hdnCodAutomotor" runat="server"></asp:HiddenField> <asp:Label id="lblAnioProd" runat="server" Text="Año de Producción:" CssClass="label"></asp:Label> <asp:RequiredFieldValidator id="reqAnioProd" runat="server" ErrorMessage="&nbsp;*" ControlToValidate="txtAnioProd"></asp:RequiredFieldValidator> <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="&nbsp;Ingrese un año correcto (formato YYYY)" ControlToValidate="txtAnioProd" ValidationExpression="^[0-9]{4,4}$"></asp:RegularExpressionValidator> <BR /><asp:TextBox id="txtAnioProd" runat="server" CssClass="field" Width="160px" AutoPostBack="true" OnTextChanged="txtAnioProd_TextChanged"></asp:TextBox>&nbsp;<BR /><asp:Label id="lblMarca" runat="server" Text="Marca:" CssClass="label"></asp:Label>&nbsp; <asp:RequiredFieldValidator id="reqMarca" runat="server" ErrorMessage="&nbsp;*" ControlToValidate="ddlMarca"></asp:RequiredFieldValidator><BR /><asp:DropDownList id="ddlMarca" runat="server" CssClass="field" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged" Enabled="False"></asp:DropDownList><BR /><asp:Label id="lblModelo" runat="server" Text="Modelo:" CssClass="label"></asp:Label>&nbsp; <asp:RequiredFieldValidator id="reqModelo" runat="server" ErrorMessage="&nbsp;*" ControlToValidate="ddlModelo"></asp:RequiredFieldValidator><BR /><asp:DropDownList id="ddlModelo" runat="server" CssClass="field" Width="250px" Enabled="False"></asp:DropDownList> <BR /><asp:Label id="lblColor" runat="server" Text="Color:" CssClass="label"></asp:Label>&nbsp; <asp:RequiredFieldValidator id="reqColor" runat="server" ErrorMessage="&nbsp;*" ControlToValidate="ddlColor"></asp:RequiredFieldValidator><BR /><asp:DropDownList id="ddlColor" runat="server" CssClass="field" Width="250px"></asp:DropDownList><BR /><asp:Label id="lblFoto" runat="server" Text="Foto:" CssClass="label"></asp:Label> <asp:RequiredFieldValidator id="reqFieldFoto" runat="server" ErrorMessage="&nbsp;*" ControlToValidate="fileupFoto"></asp:RequiredFieldValidator> <asp:RegularExpressionValidator id="FileUpLoadValidator" runat="server" ErrorMessage="El formato de la foto debe ser JPG" ControlToValidate="fileupFoto" ValidationExpression="(.*\.jpg)|(.*\.jpeg)|(.*\.JPG)|(.*\.JPEG)"></asp:RegularExpressionValidator><BR />&nbsp;<asp:FileUpload id="fileupFoto" runat="server" CssClass="field" Width="248px"></asp:FileUpload>&nbsp; <BR /><asp:Image id="imgFotoAutomotor" runat="server" Visible="False" Width="160px" Height="120px"></asp:Image> <BR /><asp:LinkButton id="btnDeletePhoto" onclick="btnDeletePhoto_Click" runat="server" CausesValidation="False" Visible="False">Eliminar esta foto</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<BR /></DIV><DIV id="divError" runat="server" visible="false"><TABLE class="error2" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/error.gif" /></TD><TD><B>Error:</B><BR /><asp:Label id="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV><DIV id="divWarning" runat="server" visible="false"><TABLE class="warning" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/warning.gif" /></TD><TD><asp:Label id="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
   <Triggers>
    <asp:PostBackTrigger ControlID="btnDeletePhoto" />
   </Triggers>
   </asp:UpdatePanel>
   </form>
  </div>
  
  </asp:Content>