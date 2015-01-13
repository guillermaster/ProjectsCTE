<%@ Page Language="C#" MasterPageFile="~/LoginMP.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Logon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="float: left; width: 50%;">
        <img src="images/SRICTE.jpg" />
    </div>
    <div style="float: right; width: 37%; ">
        <asp:Login ID="Login1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" 
            BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" 
            DisplayRememberMe="False" Font-Names="Verdana" Font-Size="0.9em" 
            ForeColor="#333333" LoginButtonText="Iniciar sesión" PasswordLabelText="Contraseña:&nbsp;" 
            PasswordRequiredErrorMessage="Debe ingresar su contraseña" 
            TitleText="Ingrese sus datos de acceso" UserNameLabelText="Usuario:&nbsp;" 
            UserNameRequiredErrorMessage="Debe ingresar su nombre de usuario" 
            Width="349px" onauthenticate="Login1_Authenticate" Height="206px">
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
                BorderWidth="1px" Font-Names="Verdana" Font-Size="1.1em" ForeColor="#284775" />
            <TextBoxStyle Font-Size="1.1em" />
            <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="1.2em" 
                ForeColor="White" />
        </asp:Login>
    </div>
</asp:Content>

