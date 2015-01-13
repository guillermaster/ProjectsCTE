<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewShippingAddress.ascx.cs" Inherits="UserControls_NewShippingAddress" %>


<asp:Panel ID="pnlNewShipAdd" ClientIDMode="Static" runat="server">
    <table>
        <tr>
            <th><asp:Label ID="lblNombreResponsable1" runat="server" Text="Nombre completo"></asp:Label>
                <asp:RequiredFieldValidator ID="reqValNomb1" runat="server" 
                    ControlToValidate="txtNombreResponsable1" ErrorMessage="Debe ingresar el nombre del autorizado a recibir el documento"
                    ValidationGroup="newShipAddValGroup" >*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regExpValNomb1" runat="server" ControlToValidate="txtNombreResponsable1" ValidationExpression="[a-zA-Z áéíóúüñäëïöüÁÉÍÓÚÑÄËÏÖÜ]{3,}"
                  ValidationGroup="newShipAddValGroup"  ErrorMessage="El nombre de la persona autorizada contiene caracteres no permitidos.">*</asp:RegularExpressionValidator>
            </th>
            <td>
                <asp:TextBox ID="txtNombreResponsable1" runat="server" Width="450"  CausesValidation="true"></asp:TextBox>
                <br /><span class="small">Persona autorizada a recibir el documento</span>
            </td>
        </tr>
        <tr>
            <th><asp:Label ID="lblNombreResponsable2" runat="server" Text="Nombre completo (2)"></asp:Label>
                <asp:RegularExpressionValidator ID="regExpValNomb2" runat="server" ControlToValidate="txtNombreResponsable2" ValidationExpression="[a-zA-Z áéíóúüñäëïöüÁÉÍÓÚÑÄËÏÖÜ]{3,}"
                  ValidationGroup="newShipAddValGroup"  ErrorMessage="El nombre de la segunda persona autorizada contiene caracteres no permitidos.">*</asp:RegularExpressionValidator>
            </th>
            <td>
                <asp:TextBox ID="txtNombreResponsable2" runat="server" Width="450"></asp:TextBox>
                <br /><span class="small">Otra persona autorizada a recibir el documento (opcional)</span>
            </td>
        </tr>
        <tr>
            <th><asp:Label ID="lblDireccion" runat="server" Text="Dirección:"></asp:Label>
                <asp:RequiredFieldValidator ID="reqValDir" runat="server" 
                    ControlToValidate="txtDireccion" ErrorMessage="Debe ingresar una dirección" 
                    ValidationGroup="newShipAddValGroup" >*</asp:RequiredFieldValidator>
            </th>
            <td><asp:TextBox ID="txtDireccion" runat="server"  Width="450" Rows="2" 
                    TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <th><asp:Label ID="lblDirRef" runat="server" Text="Referencia a la dirección:"></asp:Label>
                <asp:RequiredFieldValidator ID="reqValDirRef" runat="server" 
                    ControlToValidate="txtDirRef" ErrorMessage="Debe ingresar una referencia a la dirección" 
                    ValidationGroup="newShipAddValGroup" >*</asp:RequiredFieldValidator>
            </th>
            <td><asp:TextBox ID="txtDirRef" runat="server" Width="450"></asp:TextBox></td>
        </tr>
        <tr>
            <th><asp:Label ID="lblPais" runat="server" Text="País:"></asp:Label>
                <asp:RequiredFieldValidator ID="reqValPais" runat="server" 
                    ControlToValidate="ddlPais" ErrorMessage="Debe seleccionar el país de destino" 
                    ValidationGroup="newShipAddValGroup" >*</asp:RequiredFieldValidator>
            </th>
            <td><asp:DropDownList ID="ddlPais" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="ddlPais_SelectedIndexChanged" style="width:180px" />
                <asp:HiddenField ID="hdnCodEcuador" Value="ECU" runat="server" />
            </td>
        </tr>
        <tr>
            <th><asp:Label ID="lblProvincia" runat="server" Text="Provincia:"></asp:Label>
                <asp:RequiredFieldValidator ID="reqValProv" runat="server" 
                    ControlToValidate="ddlProvincia" ErrorMessage="Debe seleccionar la provincia de destino" 
                    ValidationGroup="newShipAddValGroup" >*</asp:RequiredFieldValidator>
            </th>
            <td>
                <asp:TextBox ID="txtProvincia" ClientIDMode="Static" style="position: absolute; width:160px" Visible="true"
                    AutoPostBack="true" OnTextChanged="txtProvincia_TextChanged" runat="server" />
                <asp:DropDownList ID="ddlProvincia" runat="server" AutoPostBack="true" ClientIDMode="Static"
                    onselectedindexchanged="ddlProvincia_SelectedIndexChanged" OnDataBound="ddlProvincia_DataBound" style="width:180px" />                
            </td>
        </tr>
        <tr>
            <th><asp:Label ID="lblCiudad" runat="server" Text="Ciudad:"></asp:Label>
                <asp:RequiredFieldValidator ID="reqValCiudad" runat="server" 
                    ControlToValidate="ddlCiudad" ErrorMessage="Debe seleccionar la ciudad de destino" 
                    ValidationGroup="newShipAddValGroup" >*</asp:RequiredFieldValidator>
            </th>
            <td>
                <asp:TextBox ID="txtCiudad" ClientIDMode="Static" style="position: absolute; width:160px" Visible="true"
                    AutoPostBack="true" OnTextChanged="txtCiudad_TextChanged" runat="server" />
                <asp:DropDownList ID="ddlCiudad" runat="server" AutoPostBack="True" style="width:180px" ClientIDMode="Static" OnDataBound="ddlCiudad_DataBound" />
            </td>
        </tr>
        <tr>
            <th><asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" />
                <asp:RequiredFieldValidator ID="reqValTel" runat="server" 
                    ControlToValidate="txtTelefono" ErrorMessage="Debe ingresar el número telefónico del lugar donde se recibirá el documento" 
                    ValidationGroup="newShipAddValGroup" >*</asp:RequiredFieldValidator>
            </th>
            <td><asp:TextBox ID="txtTelefono" runat="server" MaxLength="12" /></td>
        </tr>
        <tr>
            <th><asp:Label ID="lblTelefonoMovil" runat="server" Text="Teléfono móvil:" />
                <asp:RequiredFieldValidator ID="reqValTelMovil" runat="server" 
                    ControlToValidate="txtTelefonoMovil" ErrorMessage="Debe ingresar su número de teléfono móvil" 
                    ValidationGroup="newShipAddValGroup" >*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regExpValTelMovil" runat="server" ControlToValidate="txtTelefonoMovil" ValidationExpression="\d+"
                   ValidationGroup="newShipAddValGroup" ErrorMessage="Debe ingresar solo caracteres numéricos como teléfono móvil.">*</asp:RegularExpressionValidator>
            </th>
            <td><asp:TextBox ID="txtTelefonoMovil" runat="server" MaxLength="12" /></td>
        </tr>
        <tr>
            <th><asp:Label ID="lblCosto" runat="server" Text="Valor a pagar ($):" /></th>
            <td>
                <asp:TextBox ID="txtCosto" runat="server" ReadOnly="true" Text="--" Width="40"></asp:TextBox>
                <br /><span class="small">El valor a pagar por el envío se sumará al costo del trámite (<asp:Label ID="lblCostoTramite" runat="server" Text="Label" />)</span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ValidationGroup="newShipAddValGroup" DisplayMode="List" />
            </td>
        </tr>
    </table>
</asp:Panel>