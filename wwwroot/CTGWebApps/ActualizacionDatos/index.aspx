<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="ActualizacionDatos_index" %>

<%@ Register Src="../controls/editableTextBox.ascx" TagName="editableTextBox" TagPrefix="uc2" %>

<%@ Register Src="../controls/logoutActDatos.ascx" TagName="logoutActDatos" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_text.css" />
    <script type="text/javascript" src="../js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <!--<h2>
            Actualización de Datos
        </h2>  -->
            <uc1:logoutActDatos ID="LogoutPagos1" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="height: 15px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5">
        <ProgressTemplate>
            <div>
                <img src="../img/ajax-loader.gif" /> 
                Procesando datos...
            </div>
        </ProgressTemplate>
        </asp:UpdateProgress>
        </div>
        <div id="mensajeErrorDiv" runat="server">
          <asp:Label ID="lblMensaje" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <div id="contenido" runat="server">
        <table width="100%">
         <tr>
          <td style="width: 159px; height: 22px;"><asp:Label ID="lblLicencia" runat="server" Text="Cedula/Pasaporte:"></asp:Label></td>
          <td style="width: 602px; height: 22px;"><asp:TextBox ID="txtLicencia" runat="server" ReadOnly="True"></asp:TextBox>
              </td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label></td>
          <td style="width: 602px"><asp:TextBox ID="txtNombres" runat="server" ReadOnly="True"></asp:TextBox></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblApellido1" runat="server" Text="Primer Apellido:"></asp:Label></td>
          <td style="width: 602px"><asp:TextBox ID="txtApellido1" runat="server" ReadOnly="True"></asp:TextBox></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblApellido2" runat="server" Text="Segundo Apellido:"></asp:Label></td>
          <td style="width: 602px"><asp:TextBox ID="txtApellido2" runat="server" ReadOnly="True"></asp:TextBox></td>
         </tr>
         <tr>
          <td colspan="2"><h3>Datos de Nacimiento</h3></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblFechaNac" runat="server" Text="Fecha:"></asp:Label></td>
          <td style="width: 602px"><asp:TextBox ID="txtFechaNac" runat="server" ReadOnly="True"></asp:TextBox></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblCantonNac" runat="server" Text="Cantón:"></asp:Label></td>
          <td style="width: 602px"><asp:TextBox ID="txtCantonNac" runat="server" ReadOnly="True"></asp:TextBox></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblProvinciaNac" runat="server" Text="Provincia:"></asp:Label></td>
          <td style="width: 602px"><asp:TextBox ID="txtProvinciaNac" runat="server" ReadOnly="True"></asp:TextBox></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblPaisNac" runat="server" Text="País:"></asp:Label></td>
          <td style="width: 602px"><asp:TextBox ID="txtPaisNac" runat="server" ReadOnly="True"></asp:TextBox></td>
         </tr>
         <tr>
          <td colspan="2" style="height: 21px"><h3>Rasgos Físicos y Actividades</h3></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblSexo" runat="server" Text="Sexo:"></asp:Label></td>
          <td style="width: 602px"><asp:TextBox ID="txtSexo" runat="server" ReadOnly="True"></asp:TextBox></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblEstatura" runat="server" Text="Estatura:"></asp:Label></td>
          <td style="width: 602px"><asp:TextBox ID="txtEstatura" runat="server" ReadOnly="True"></asp:TextBox></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblTipoSangre" runat="server" Text="Tipo de Sangre:"></asp:Label></td>
          <td style="width: 602px"><asp:TextBox ID="txtTipoSangre" runat="server" ReadOnly="True"></asp:TextBox></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblEstadoCivil" runat="server" Text="Estado Civil:"></asp:Label></td>
          <td style="width: 602px"><asp:TextBox ID="txtEstadoCivil" runat="server" ReadOnly="True"></asp:TextBox></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblProfesion" runat="server" Text="Profesión:"></asp:Label></td>
          <td style="width: 602px"><asp:TextBox ID="txtProfesion" runat="server" ReadOnly="True"></asp:TextBox></td>
         </tr>
         <tr>
          <td colspan="2"><h3>Datos de Registro en www.ctg.gov.ec</h3></td>
         </tr>
         <tr>
          <td style="width: 159px; height: 38px;"><asp:Label ID="lblEmail" runat="server" Text="Correo Electrónico:"></asp:Label></td>
          <td style="width: 602px; height: 38px;">
              <asp:TextBox ID="txtEmailVis" runat="server" ReadOnly="True"></asp:TextBox>
              <asp:TextBox ID="txtEmailReal" runat="server" BackColor="LightBlue" Visible="False"></asp:TextBox>
              <asp:LinkButton ID="btnEditarEmail" runat="server" OnClick="btnEditarEmail_Click">Editar</asp:LinkButton>&nbsp;
              <asp:LinkButton ID="btnGuardarEmail" runat="server" OnClick="btnGuardarEmail_Click1"
                  Visible="False">Guardar</asp:LinkButton>
              <asp:LinkButton ID="btnCancelarEmail" runat="server" OnClick="btnCancelarEmail_Click1"
                  Visible="False">Cancelar</asp:LinkButton><br />
              <asp:Label ID="lblMsgEmailChanged" runat="server" ForeColor="Blue"></asp:Label>
              <asp:Label ID="lblErrorEmailChanged" runat="server" ForeColor="Red"></asp:Label></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblPassword" runat="server" Text="Contraseña:"></asp:Label>
              <asp:HiddenField ID="hdnPassword" runat="server" />
          </td>
          <td style="width: 602px">
           <div id="fieldsChangePassword" runat="server">
             <table>
               <tr>
                 <td>Contraseña actual:</td>
                 <td><asp:TextBox ID="txtCurrPwd" runat="server" MaxLength="12" TextMode="Password" Width="100px"></asp:TextBox>
                     <asp:Label ID="lblMsgCurrPwd" runat="server" ForeColor="Red" Text=" *" Visible="False"></asp:Label></td>
               </tr>
               <tr>
                 <td>Nueva Contraseña:</td>
                 <td><asp:TextBox ID="txtNewPwd" runat="server" MaxLength="12" TextMode="Password" Width="100px"></asp:TextBox></td>
               </tr>
               <tr>
                 <td>Confirmar nueva contraseña:</td>
                 <td><asp:TextBox ID="txtConfNewPwd" runat="server" MaxLength="12" TextMode="Password" Width="100px"></asp:TextBox>
                     <asp:Label ID="lblMsgConfNewPwd" runat="server" ForeColor="Red" Text=" *" Visible="False"></asp:Label></td>
               </tr>
               <tr><td colspan="2">
                   <asp:LinkButton ID="btnSavePwd" runat="server" OnClick="btnSavePwd_Click">Guardar</asp:LinkButton>&nbsp;
                   <asp:LinkButton ID="btnCancelPwd" runat="server" OnClick="btnCancelPwd_Click">Cancelar</asp:LinkButton>
                </td></tr>
             </table>
           </div>
              <asp:LinkButton ID="btnChangePwd" runat="server" OnClick="btnChangePwd_Click">Cambiar contraseña</asp:LinkButton><br />
              <asp:Label ID="lblMsgPwdChanged" runat="server" ForeColor="Blue"></asp:Label>
              <asp:Label ID="lblErrorPwdChanged" runat="server" ForeColor="Red"></asp:Label>
           </td>
         </tr>
         <tr>
          <td colspan="2"><h3>Datos de Contacto y Residencia</h3></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblTelefono" runat="server" Text="Teléfono:"></asp:Label></td>
          <td style="width: 602px"><uc2:editableTextBox ID="editTelefono" runat="server" /></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblTelefonoMovil" runat="server" Text="Teléfono Móvil:"></asp:Label></td>
          <td style="width: 602px"><uc2:editableTextBox ID="editTelefonoMovil" runat="server" /></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblDireccion" runat="server" Text="Dirección:"></asp:Label></td>
          <td style="width: 602px"><asp:TextBox ID="txtDireccion" runat="server" ReadOnly="True" Width="400px"></asp:TextBox></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblCalle1" runat="server" Text="Calle 1:"></asp:Label></td>
          <td style="width: 602px"><uc2:editableTextBox ID="editCalle1" runat="server" /></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblCalle2" runat="server" Text="Calle 2 o Sector (Etapa):"></asp:Label></td>
          <td style="width: 602px"><uc2:editableTextBox ID="editCalle2" runat="server" /></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblNumeroVilla" runat="server" Text="Número o Villa:"></asp:Label></td>
          <td style="width: 602px"><uc2:editableTextBox ID="editNumeroVilla" runat="server" /></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblManzana" runat="server" Text="Manzana:"></asp:Label></td>
          <td style="width: 602px"><uc2:editableTextBox ID="editManzana" runat="server" /></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblPiso" runat="server" Text="Piso:"></asp:Label></td>
          <td style="width: 602px"><uc2:editableTextBox ID="editPiso" runat="server" /></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblDepartamento" runat="server" Text="Departamento:"></asp:Label></td>
          <td style="width: 602px"><uc2:editableTextBox ID="editDepartamento" runat="server" /></td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblCiudadela" runat="server" Text="Ciudadela:"></asp:Label>
            <asp:HiddenField ID="hdnPreviousCiudadelaVal" runat="server" />
          </td>
          <td style="width: 602px">
            <asp:DropDownList ID="ddlCiudadela" runat="server" CssClass="combo3" Enabled="False" OnSelectedIndexChanged="ddlCiudadela_SelectedIndexChanged">
              </asp:DropDownList>
              <asp:LinkButton ID="btnEditarCiudadela" runat="server" OnClick="btnEditarCiudadela_Click">Editar</asp:LinkButton>
              <asp:LinkButton ID="btnGuardarCiudadela" runat="server" Visible="False" OnClick="btnGuardarCiudadela_Click">Guardar</asp:LinkButton>
              <asp:LinkButton ID="btnCancelarCiudadela" runat="server" Visible="False" OnClick="btnCancelarCiudadela_Click">Cancelar</asp:LinkButton>
              <br />
              <asp:Label ID="lblMsgCiudadela" runat="server" ForeColor="Red" />
              <asp:Label ID="lblSuccessCiudadela" runat="server" ForeColor="Blue" />
          </td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblCanton" runat="server" Text="Cantón:"></asp:Label>
            <asp:HiddenField ID="hdnPreviousCantonVal" runat="server" />
          </td>
          <td style="width: 602px">
            <asp:DropDownList ID="ddlCanton" runat="server" CssClass="combo3" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="ddlCanton_SelectedIndexChanged">
              </asp:DropDownList>
              <asp:LinkButton ID="btnEditarCanton" runat="server" OnClick="btnEditarCanton_Click">Editar</asp:LinkButton>
              <asp:LinkButton ID="btnGuardarCanton" runat="server" Visible="False" OnClick="btnGuardarCanton_Click">Guardar</asp:LinkButton>
              <asp:LinkButton ID="btnCancelarCanton" runat="server" Visible="False" OnClick="btnCancelarCanton_Click">Cancelar</asp:LinkButton>
              <br />
              <asp:Label ID="lblMsgCanton" runat="server" ForeColor="Red" />
              <asp:Label ID="lblSuccessCanton" runat="server" ForeColor="Blue" />
          </td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblProvincia" runat="server" Text="Provincia:"></asp:Label>
            <asp:HiddenField ID="hdnPreviousProvinciaVal" runat="server" />
          </td>
          <td style="width: 602px">
              <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="combo3" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged">
              </asp:DropDownList>
              <asp:LinkButton ID="btnEditarProvincia" runat="server" OnClick="btnEditarProvincia_Click">Editar</asp:LinkButton>
              <asp:LinkButton ID="btnGuardarProvincia" runat="server" Visible="False" OnClick="btnGuardarProvincia_Click">Guardar</asp:LinkButton>
              <asp:LinkButton ID="btnCancelarProvincia" runat="server" Visible="False" OnClick="btnCancelarProvincia_Click">Cancelar</asp:LinkButton>
              <br />
              <asp:Label ID="lblMsgProvincia" runat="server" ForeColor="Red" />
              <asp:Label ID="lblSuccessProvincia" runat="server" ForeColor="Blue" />
          </td>
         </tr>
         <tr>
          <td style="width: 159px"><asp:Label ID="lblPais" runat="server" Text="País:"></asp:Label>
              <asp:HiddenField ID="hdnPreviousPaisVal" runat="server" />
          </td>
          <td style="width: 602px">
              <asp:DropDownList ID="ddlPais" runat="server" CssClass="combo3" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged">
              </asp:DropDownList>
              <asp:LinkButton ID="btnEditarPais" runat="server" OnClick="btnEditarPais_Click">Editar</asp:LinkButton>
              <asp:LinkButton ID="btnGuardarPais" runat="server" Visible="False" OnClick="btnGuardarPais_Click">Guardar</asp:LinkButton>
              <asp:LinkButton ID="btnCancelarPais" runat="server" Visible="False" OnClick="btnCancelarPais_Click">Cancelar</asp:LinkButton>
              <br />
              <asp:Label ID="lblMsgPais" runat="server" ForeColor="Red" />
              <asp:Label ID="lblSuccessPais" runat="server" ForeColor="Blue" />
          </td>
         </tr>
        </table>
    
    </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    <div style="height: 15px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5">
        <ProgressTemplate>
            <div>
                <img src="../img/ajax-loader.gif" /> 
                Procesando datos...
            </div>
        </ProgressTemplate>
        </asp:UpdateProgress>
        </div>
    </form>
</body>
</html>
