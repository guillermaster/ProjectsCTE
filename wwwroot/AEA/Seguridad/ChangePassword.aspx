<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Seguridad_ChangePassword" %>

<%@ Register Src="../UserControls/SaveButton.ascx" TagName="SaveButton" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-content1" align="center">
        
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <contenttemplate>
                <uc1:SaveButton ID="btnGuardar" runat="server" OnLoad="btnGuardar_Load" />
                <asp:UpdateProgress id="UpdateProgress1" runat="server" DynamicLayout="False" DisplayAfter="5" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <img src="../img/ajax-loader.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <DIV id="divForm" style="width: 455px; height:120px;" align="center"  class="registerform" runat="server">
                    <asp:Label ID="lblCurrentPwd" runat="server" Text="Contraseña actual:" />
                    <asp:RequiredFieldValidator id="reqValCurrPwd" ControlToValidate="txtCurrentPwd" runat="server" ErrorMessage="*" /><br />
                    <asp:TextBox ID="txtCurrentPwd" TextMode="Password" Width="200px" runat="server" /><br /><br />
                    <div style="float:left; width:50%;">
                        <asp:Label ID="lblNewPwd" runat="server" Text="Nueva contraseña:" />
                        <asp:RequiredFieldValidator id="reqValNewPwd" ControlToValidate="txtNewPwd" runat="server" ErrorMessage="*" /><br />
                        <asp:TextBox ID="txtNewPwd" TextMode="Password" Width="200px" runat="server" />
                    </div>
                    <div style="float:right; width:50%;">
                        <asp:Label ID="lblConfNewPwd" runat="server" Text="Confirme su nueva contraseña:" />
                        <asp:RequiredFieldValidator id="reqValConfNewPwd" ControlToValidate="txtConfNewPwd" runat="server" ErrorMessage="*" /><br />
                        <asp:TextBox ID="txtConfNewPwd" TextMode="Password" Width="200px" runat="server" />
                    </div>
                    <asp:CompareValidator id="cmpValNewPwd" ControlToCompare="txtNewPwd" ControlToValidate="txtConfNewPwd" runat="server" ErrorMessage="La nueva contraseña y su confirmación no coinciden"></asp:CompareValidator>
                </DIV>
                <DIV id="divError" runat="server" visible="false"><TABLE class="error2" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="../img/error.gif" /></TD><TD><B>Error:</B><BR /><asp:Label id="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV><DIV id="divWarning" runat="server" visible="false"><TABLE class="warning" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="../img/warning.gif" /></TD><TD><asp:Label id="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV>
            </contenttemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

