<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Registro, App_Web_registro.aspx.cdcab7d2" theme="efot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" type="text/css" href="Styles/niftyCorners.css">
    <link rel="stylesheet" type="text/css" href="Styles/niftyPrint.css" media="print">
    <script type="text/javascript" src="js/nifty.js"></script>
    <script type="text/javascript" src="js/final.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="1000" />
    <div style="height: 15px; text-align: center">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                DisplayAfter="5">
                                <ProgressTemplate>
                                    <div>
                                        <asp:Image ID="Image2" ImageUrl="~/images/ajax-loader.gif" runat="server" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        
        <asp:Panel runat="server" ID="pnlForm1">
            <h2>Ingreso de Datos Personales Básicos</h2>
            <div align="center">
            <div style="background: url(images/bgmessage.png) no-repeat; width: 413px;
                height: 38px; padding: 2px 3px 2px 3px; text-align: center;">
                Este proceso de selección es para aspirantes a tropa, este año solo se reclutarán hombres, en el siguiente proceso de admisión se aceptarán mujeres.
            </div>
        </div>
            <asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label>
            <asp:TextBox ID="txtNombres" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqValNombres" runat="server" ControlToValidate="txtNombres" ErrorMessage="*" />
            <asp:RegularExpressionValidator ID="regExpValNombres" runat="server" 
                ControlToValidate="txtNombres" 
                ErrorMessage="Los nombres ingresados son incorrectos" 
                ValidationExpression="[a-zA-Z áéíóúüñäëïöüÁÉÍÓÚÑÄËÏÖÜ]{3,}" />
            <br />
            <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label>
            <asp:TextBox ID="txtApellidos" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqValApellidos" runat="server" ControlToValidate="txtApellidos" ErrorMessage="*" />
            <asp:RegularExpressionValidator ID="regExpValApellidos" runat="server" 
                ControlToValidate="txtApellidos" 
                ErrorMessage="Los apellidos ingresados son incorrectos" 
                ValidationExpression="[a-zA-Z áéíóúüñäëïöüÁÉÍÓÚÑÄËÏÖÜ]{3,}" />
            <br />
            <asp:Label ID="lblCedula" runat="server" Text="Cédula de Identidad:"></asp:Label>
            <asp:TextBox ID="txtCedula" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqValCedula" runat="server" ControlToValidate="txtCedula" ErrorMessage="*" />
            <asp:RegularExpressionValidator ID="regExpValCedula" runat="server" 
                ControlToValidate="txtCedula" 
                ErrorMessage="El número de cédula ingresado es incorrecto" 
                ValidationExpression="\d+"></asp:RegularExpressionValidator>
            <br />
            <asp:Label ID="lblEmail" runat="server" Text="Correo electrónico:"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqValEmail" runat="server" ErrorMessage="*" 
                ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regExpValEmail" runat="server" 
                ControlToValidate="txtEmail" 
                ErrorMessage="El correo electrónico ingresado es incorrecto" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            <br />
            
            <asp:Label ID="lblFecNac" runat="server" Text="Fecha de Nacimiento:"></asp:Label>
            <asp:TextBox ID="txtFecNac" runat="server" SkinID="skinMedium"></asp:TextBox>
            <asp:CalendarExtender ID="txtFecNac_CalendarExtender" runat="server" TargetControlID="txtFecNac">
            </asp:CalendarExtender>
            <asp:RequiredFieldValidator ID="reqValFecNac" runat="server" 
                ControlToValidate="txtFecNac" ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regExpValFecNac" runat="server" 
                ControlToValidate="txtFecNac" ErrorMessage="Solo se aceptan personas nacidas entre 01/01/1986 y 12/31/1993" 
                ValidationExpression="^([1-9]|1[012])[- /.]([1-9]|[12][0-9]|3[01])[- /.](1986|1987|1988|1989|1990|1991|1992|1993)$"  
                ></asp:RegularExpressionValidator>

            <br />
            <asp:Label ID="lblPaisNac" runat="server" Text="País de Nacimiento:"></asp:Label>
                <asp:DropDownList ID="ddlPaisNac" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlPaisNac_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqValPaisNac" runat="server" 
                    ControlToValidate="ddlPaisNac" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
            <asp:Label ID="lblProvNac" runat="server" Text="Provincia de Nacimiento:"></asp:Label>
                <asp:DropDownList ID="ddlProvNac" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlProvNac_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqValProvNac" runat="server" 
                    ControlToValidate="ddlProvNac" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblCiudadNac" runat="server" Text="Ciudad de Nacimiento:"></asp:Label>
                <asp:DropDownList ID="ddlCiudadNac" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqValCiudadNac" runat="server" 
                    ControlToValidate="ddlCiudadNac" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
            <asp:Label ID="lblSexo" runat="server" Text="Sexo:"></asp:Label>
            <asp:DropDownList ID="ddlSexo" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="reqValSexo" runat="server" 
                ControlToValidate="ddlSexo" ErrorMessage="*"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblEstadoCivil" runat="server" Text="Estado Civil:"></asp:Label>
            <asp:DropDownList ID="ddlEstadoCivil" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="reqValEstadoCivil" runat="server" 
                ControlToValidate="ddlEstadoCivil" ErrorMessage="*"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblCargasFam" runat="server" Text="Cargas familiares:"></asp:Label>
            <asp:TextBox ID="txtCargasFam" runat="server" SkinID="skinMedium"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqValCargasFam" runat="server" ControlToValidate="txtCargasFam" ErrorMessage="*" />
            <asp:RegularExpressionValidator ID="regExpValCargasFam" runat="server" 
                ControlToValidate="txtCargasFam" 
                ErrorMessage="La cantidad ingresada es incorrecta" 
                ValidationExpression="\d+"></asp:RegularExpressionValidator>
            <br />
            <asp:Button ID="btnNextStep1" runat="server" Text="Siguiente >" 
                onclick="btnNextStep1_Click" />
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlForm2" Visible="false">
                <h2>Ingreso de Rasgos Físicos y Medidas</h2>
                <asp:Label ID="lblEstatura" runat="server" Text="Estatura:"></asp:Label>
                <asp:DropDownList ID="ddlEstatura" runat="server">
                </asp:DropDownList>
                cm<asp:RequiredFieldValidator ID="reqValEstatura" runat="server" 
                    ControlToValidate="ddlEstatura" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblTipoSangre" runat="server" Text="Tipo de Sangre:"></asp:Label>
                <asp:DropDownList ID="ddlTipoSangre" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqValTipoSangre" runat="server" 
                    ControlToValidate="ddlTipoSangre" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblPeso" runat="server" Text="Peso:"></asp:Label>
                <asp:TextBox ID="txtPeso" runat="server" SkinID="skinMedium"></asp:TextBox>
                kg<asp:RequiredFieldValidator ID="reqValPeso" runat="server" 
                    ControlToValidate="txtPeso" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblTallaCalzado" runat="server" Text="Talla de Calzado:"></asp:Label>
                <asp:DropDownList ID="ddlTallaCalzado" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqValTallaCalzado" runat="server" 
                    ControlToValidate="ddlTallaCalzado" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblTallaPantalon" runat="server" Text="Talla de Pantalón:"></asp:Label>
                <asp:DropDownList ID="ddlTallaPantalon" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="ddlTallaPantalon" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblTallaCamisa" runat="server" Text="Talla de Camisa:"></asp:Label>
                <asp:DropDownList ID="ddlTallaCamisa" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqValTallaCamisa" runat="server" 
                    ControlToValidate="ddlTallaCamisa" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblTallaGorra" runat="server" Text="Talla de Gorra:"></asp:Label>
                <asp:DropDownList ID="ddlTallaGorra" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqValTallaGorra" runat="server" 
                    ControlToValidate="ddlTallaGorra" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblIdeDactilar" runat="server" Text="Ide. Dactilar:"></asp:Label>
                <asp:TextBox ID="txtIdeDactilar" runat="server" SkinID="skinMedium"></asp:TextBox>
                <br />
                <asp:Button ID="btnNextStep2" runat="server" Text="Siguiente >" 
                    onclick="btnNextStep2_Click" />
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlForm3" Visible="false">
                <h2>Ingreso de Dirección de Residencia</h2>
                <asp:Label ID="lblProvinciaRes" runat="server" Text="Provincia:"></asp:Label>
                <asp:DropDownList ID="ddlProvinciaRes" runat="server" AutoPostBack="true"
                    onselectedindexchanged="ddlProvincia_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqValProvincia" runat="server" 
                    ControlToValidate="ddlProvinciaRes" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblLocalidadRes" runat="server" Text="Ciudad:"></asp:Label>
                <asp:DropDownList ID="ddlLocalidadRes" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqValLocalidad" runat="server" 
                    ControlToValidate="ddlLocalidadRes" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblDireccion" runat="server" Text="Dirección domiciliaria:"></asp:Label>
                <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqValDireccion" runat="server" 
                    ControlToValidate="txtDireccion" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblTelConv" runat="server" Text="Teléfono convencional:"></asp:Label>
                <asp:TextBox ID="txtTelConv" runat="server" SkinID="skinMedium"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqValTel" runat="server" 
                    ControlToValidate="txtTelConv" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblTelCel" runat="server" Text="Teléfono celular:"></asp:Label>
                <asp:TextBox ID="txtTelCel" runat="server" SkinID="skinMedium"></asp:TextBox><br />
                <asp:Label ID="lblReferenciaUbicac" runat="server" Text="Referencia:"></asp:Label>
                <asp:TextBox ID="txtReferenciaUbicac" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqValReferenciaUbicac" runat="server" 
                    ControlToValidate="txtReferenciaUbicac" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar datos" OnClick="btnRegistrar_Click" />
            </asp:Panel>      
             
            <div id="divError" runat="server" clientidmode="Static" style="visibility: hidden; background-color: #fbbcc8">
            <asp:Label ID="lblError" runat="server" Text="" SkinID="Large" />
            <asp:LinkButton ID="btnTryAgain" runat="server" onclick="btnTryAgain_Click">Intentar nuevamente</asp:LinkButton>  
        </div>
        <div id="divSuccess" runat="server" clientidmode="Static" style="visibility: hidden; background-color: #cfe7f2">
            <asp:Label ID="lblSuccess" runat="server" Text="" SkinID="Large" />
            <a href="Default.aspx">Continuar</a>
        </div>      
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
