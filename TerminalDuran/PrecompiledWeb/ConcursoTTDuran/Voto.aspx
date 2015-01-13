<%@ page title="Concurso para el nombre del Terminal Terrestre de Durán" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="_Default, App_Web_voto.aspx.cdcab7d2" theme="SkinFile" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script language="javascript" type="text/javascript">
        function goStep2() {
            $("#step1").fadeOut("slow");
            setTimeout(function () { $("#step2").fadeIn("slow"); }, 600);
        }
        function goStep3() {
            $("#step1").css("display", "none");
            $("#step2").fadeOut("slow");
            setTimeout(function () { $("#step3").fadeIn("slow"); }, 600);
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="text-align: center">
            <img src="images/ttduranMain.jpg" alt="Terminal Terrestre" style="margin: 0 0 0 -20px" />
        </div>
    <div id="step1">
        <asp:Label ID="lblNombreTT" runat="server" Text="¿Cómo se debe llamar el Terminal Terrestre de Durán?" /><br />
        <asp:TextBox ID="txtNombreTT" runat="server" />
        <asp:AutoCompleteExtender ID="txtNombreTT_AutoCompleteExtender" runat="server" 
            ServiceMethod="GetCompletionList" TargetControlID="txtNombreTT" EnableCaching="true" CompletionInterval="100"
            UseContextKey="True" CompletionListCssClass="autocomplete" >
        </asp:AutoCompleteExtender>
        <asp:RequiredFieldValidator ID="reqValNombreTT" runat="server" ErrorMessage="Ingrese el nombre de su preferencia" ControlToValidate="txtNombreTT" ValidationGroup="step1ValGroup" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="¿Por qué se debe llevar ese nombre?" /><br />
        <asp:TextBox ID="txtMotivoNombreTT" runat="server" />
        <asp:RequiredFieldValidator ID="reqValMotivoNombreTT" runat="server" ErrorMessage="Ingrese el motivo de su elección" ControlToValidate="txtMotivoNombreTT" ValidationGroup="step1ValGroup" />
        <br />
        <asp:LinkButton ID="step1NextBtn" runat="server" CausesValidation="true" ClientIDMode="Static" 
            onclick="step1NextBtn_Click" ValidationGroup="step1ValGroup" SkinID="BtnNext"></asp:LinkButton>
    </div>
    <div id="step2" style="display: none">
        <asp:Label ID="lblNombre" runat="server" Text="Su nombre:" /><br />
        <asp:TextBox ID="txtNombre" runat="server" />
        <asp:RequiredFieldValidator ID="reqValNombre" runat="server" ErrorMessage="Ingrese su nombre" ControlToValidate="txtNombre" ValidationGroup="step2ValGroup" />
        <asp:RegularExpressionValidator ID="regExpValNombres" runat="server" ControlToValidate="txtNombre" ErrorMessage="*" ValidationExpression="[a-zA-Z áéíóúüñ]{3,}" ValidationGroup="step2ValGroup" />
        <br />
        <br />
        <asp:Label ID="lblCedula" runat="server" Text="Su número de cédula:" /><br />
        <asp:TextBox ID="txtCedula" runat="server" MaxLength="10" />
        <asp:RequiredFieldValidator ID="reqValCedula" runat="server" ErrorMessage="Ingrese su número de cédula" ControlToValidate="txtCedula" ValidationGroup="step2ValGroup" />
        <asp:RegularExpressionValidator ID="regExpValCedula" runat="server" ErrorMessage="La identificación ingresada es incorrecta."
                            ControlToValidate="txtCedula" ValidationExpression="[0-9A-Z]{10,10}" ValidationGroup="step2ValGroup" /><br /><br />
        <asp:Label ID="lblTelefono" runat="server" Text="Su número de teléfono:" /><br />
        <asp:TextBox ID="txtTelefono" runat="server" MaxLength="10" />
        <br /><br />
        <asp:Label ID="lblEmail" runat="server" Text="Su correo electrónico:" /><br />
        <asp:TextBox ID="txtEmail" runat="server" />
        <asp:RequiredFieldValidator ID="reqValEmail" runat="server" ErrorMessage="Ingrese su correo electrónico" ControlToValidate="txtEmail" ValidationGroup="step2ValGroup" />
        <asp:RegularExpressionValidator ID="regExpValEmail" runat="server" 
            ErrorMessage="El correo electrónico ingresado es incorrecto"  ControlToValidate="txtEmail"
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        <br />
        <asp:LinkButton ID="step2NextBtn" Width="122" runat="server" CssClass="nextButton" ClientIDMode="Static" onclick="step2NextBtn_Click" ValidationGroup="step2ValGroup"></asp:LinkButton>
    </div>
    <div id="step3" style="display: none">
        <asp:Panel ID="pnlMessages" runat="server">
        </asp:Panel>
    </div>
</asp:Content>
