<%@ page language="C#" masterpagefile="~/LoginMP.master" autoeventwireup="true" inherits="Logon, App_Web_login.aspx.cdcab7d2" theme="SkinFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .pics
        {
            height: 192px;
            width: 232px;
            padding: 0;
            margin: 0;
            background: #aca5a5;
        }
        
        .pics img
        {
            padding: 15px;
        }
        span.img-rollover
        {
            width: 232px;
            height: 57px;
            overflow: hidden;
            display: block;
            position: relative;
        }
        span.img-rollover a:hover
        {
            top: -57px;
            position: relative;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery.cycle.all.js"></script>
    <script type="text/javascript">
        function showLogin() {
            $('#btnViewLogin').hide();
            $('#<%= Login1.ClientID %>').fadeIn();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float: left; position: relative">
        <div id="featured">
            <h2>
                Sistema de Información de Tránsito</h2>
            <div class="pics">
                <img src="images/imgslider/img1.png" alt="" />
                <img src="images/imgslider/img2.png" alt="" />
                <img src="images/imgslider/img3.png" alt="" />
            </div>
        </div>
        <div class="gridAnuncios" style="position: absolute; left: 50px; top: 280px">
            <asp:GridView ID="gvNoticias" SkinID="gvPlain" runat="server" AutoGenerateColumns="False"
                ShowHeader="false">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="url" DataTextField="title" Target="_blank" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.pics').cycle({
                fx: 'fade',
                speed: 2500
            });
        });
    </script>
    <div style="float: left; width: 37%; padding-top: 20px">
        <div id="btnViewLogin" style="margin: 30px 20px">
            <span class="img-rollover"><a onclick="javascript:showLogin();">
                <img src="images/login-button.png" alt="Iniciar sesión" /></a>
            </span>
        </div>
        <asp:Login ID="Login1" runat="server" BackColor="#f5f5f5" BorderColor="#e5e5e5" BorderPadding="4"
            BorderStyle="Solid" BorderWidth="1px" DisplayRememberMe="False" Font-Size="12px"
            ForeColor="#222222" LoginButtonText="Iniciar sesión" PasswordLabelText="Contraseña:&nbsp;"
            PasswordRequiredErrorMessage="Debe ingresar su contraseña" TitleText="Ingrese con su usuario y contraseña"
            UserNameLabelText="Usuario:&nbsp;" UserNameRequiredErrorMessage="Debe ingresar su nombre de usuario"
            Width="335px" OnAuthenticate="Login1_Authenticate" Height="206px" style="display: none">
            <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                Font-Names="Verdana" Font-Size="1.1em" ForeColor="#284775" />
            <TextBoxStyle Font-Size="12px" ForeColor="#222222" CssClass="inputlarge" />
            <TitleTextStyle Font-Bold="False" Font-Size="14px" ForeColor="#222222" />
        </asp:Login>
        <img src="images/sit.gif" style="margin: 10px 0 0 70px" />
    </div>
</asp:Content>
