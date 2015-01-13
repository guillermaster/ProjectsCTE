<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="DatosAspirante, App_Web_datosaspirante.aspx.dae9cef9" theme="efot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="width:100%; height:40px">
        <div style="float:left; width:40%; height: 20px; vertical-align:bottom">
        <h2>Admisión</h2>
        </div>
        <div style="float:right; width:40%; text-align: right; height: 20px; vertical-align:bottom">
            <asp:ImageButton ID="btnLogout" runat="server" AlternateText="Cerrar sesión" ToolTip="Cerrar sesión"
                ImageUrl="~/images/btnlogout.png" onclick="btnLogout_Click" />
        </div>
    </div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
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
            <asp:Accordion ID="Accordion1" runat="server">
                <Panes>
                    <asp:AccordionPane ID="AccordionPane5" runat="server">
                        <Header>
                            Pruebas de Admisión</Header>
                        <Content>
                            <asp:GridView ID="gvPruebas" runat="server">
                            </asp:GridView>
                        </Content>
                    </asp:AccordionPane>
                    <asp:AccordionPane ID="AccordionPane1" runat="server">
                        <Header>
                            Datos Personales</Header>
                        <Content>
                            <table>
                                <tr>
                                    <td><asp:Label ID="lblNombres" runat="server" Text="Nombres:" /></td>
                                    <td><asp:Label ID="lblValNombres" runat="server" Text="" /></td>
                                    <td><asp:Label ID="lblApellidos" runat="server" Text="Apellidos:" /></td>
                                    <td><asp:Label ID="lblValApellidos" runat="server" Text="" /></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblCedula" runat="server" Text="Cédula de Identidad:" /></td>
                                    <td><asp:Label ID="lblValCedula" runat="server" Text="" /></td>
                                    <td><asp:Label ID="lblEmail" runat="server" Text="Correo electrónico:" /></td>
                                    <td><asp:Label ID="lblValEmail" runat="server" Text="" /></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblFechaNac" runat="server" Text="Fecha de Nacimiento:" /></td>
                                    <td><asp:Label ID="lblValFechaNac" runat="server" Text="" /></td>
                                    <td><asp:Label ID="lblPaisNac" runat="server" Text="País de nacimiento:" /></td>
                                    <td><asp:Label ID="lblValPaisNac" runat="server" Text="" /></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblProvNac" runat="server" Text="Provincia de nacimiento:" /></td>
                                    <td><asp:Label ID="lblValProvNac" runat="server" Text="" /></td>
                                    <td><asp:Label ID="lblCiudadNac" runat="server" Text="Ciudad de nacimiento:" /></td>
                                    <td><asp:Label ID="lblValCiudadNac" runat="server" Text="" /></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblSexo" runat="server" Text="Sexo:" /></td>
                                    <td><asp:Label ID="lblValSexo" runat="server" Text="" /></td>
                                    <td><asp:Label ID="lblEstadoCivil" runat="server" Text="Estado Civil:" /></td>
                                    <td><asp:Label ID="lblValEstadoCivil" runat="server" Text="" /></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblCargFam" runat="server" Text="Cargas familiares:" /></td>
                                    <td><asp:Label ID="lblValCargFam" runat="server" Text="" /></td>
                                    <td><asp:Label ID="lblEstatura" runat="server" Text="Estatura:" /></td>
                                    <td><asp:Label ID="lblValEstatura" runat="server" Text="" /></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblTipoSangre" runat="server" Text="Tipo de sangre:" /></td>
                                    <td><asp:Label ID="lblValTipoSangre" runat="server" Text="" /></td>
                                    <td><asp:Label ID="lblPeso" runat="server" Text="Peso:" /></td>
                                    <td><asp:Label ID="lblValPeso" runat="server" Text="" /></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblTallaCalzado" runat="server" Text="Talla de calzado:" /></td>
                                    <td><asp:Label ID="lblValTallaCalzado" runat="server" Text="" /></td>
                                    <td><asp:Label ID="lblTallaPantalon" runat="server" Text="Talla de pantalón:" /></td>
                                    <td><asp:Label ID="lblValTallaPantalon" runat="server" Text="" /></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblTallaCamisa" runat="server" Text="Talla de camisa:" /></td>
                                    <td><asp:Label ID="lblValTallaCamisa" runat="server" Text="" /></td>
                                    <td><asp:Label ID="lblTallaGorra" runat="server" Text="Talla de gorra:" /></td>
                                    <td><asp:Label ID="lblValTallaGorra" runat="server" Text="" /></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblIdeDactilar" runat="server" Text="Ide. Dactilar:" /></td>
                                    <td><asp:Label ID="lblValIdeDactilar" runat="server" Text="" /></td>
                                </tr>
                            </table>                            
                        </Content>
                    </asp:AccordionPane>
                    <asp:AccordionPane ID="AccordionPane2" runat="server">
                        <Header>
                            Direcciones de Contacto</Header>
                        <Content>
                            <asp:GridView ID="gvDirecciones" runat="server">
                            </asp:GridView>
                            <asp:LinkButton ID="btnAddDireccion" runat="server"  OnClick="btnAddDireccion_Click">Agregar dirección</asp:LinkButton>
                            <asp:Panel runat="server" ID="pnlNvaDirecc" Visible="false">
                                <asp:Label ID="lblTipoDireccion" runat="server" Text="Tipo de Ubicación:"></asp:Label>
                                <asp:DropDownList ID="ddlTipoDireccion" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:Label ID="lblPaisDireccion" runat="server" Text="País:"></asp:Label>
                                <asp:DropDownList ID="ddlPaisDireccion" runat="server" AutoPostBack="true" onselectedindexchanged="ddlPaisDireccion_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:Label ID="lblProvinciaDireccion" runat="server" Text="Provincia:"></asp:Label>
                                <asp:DropDownList ID="ddlProvinciaDireccion" runat="server" AutoPostBack="true" onselectedindexchanged="ddlProvDireccion_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqValProvincDireccion" runat="server" ErrorMessage="*" ControlToValidate="ddlProvinciaDireccion"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblCiudadDireccion" runat="server" Text="Ciudad:"></asp:Label>
                                <asp:DropDownList ID="ddlCiudadDireccion" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqValCiudadDireccion" runat="server" ErrorMessage="*" ControlToValidate="ddlCiudadDireccion"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" />
                                <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValDireccion" runat="server" ErrorMessage="*" ControlToValidate="txtDireccion"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblTelefConvDireccion" runat="server" Text="Teléfono convencional:" />
                                <asp:TextBox ID="txtTelefConvDireccion" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValTelefConvDireccion" runat="server" ErrorMessage="*" ControlToValidate="txtTelefConvDireccion"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblTelefMovDireccion" runat="server" Text="Teléfono móvil:" />
                                <asp:TextBox ID="txtTelefMovDireccion" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblReferenciaDireccion" runat="server" Text="Referencia:" />
                                <asp:TextBox ID="txtReferenciaDireccion" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValReferenciaDireccion" runat="server" ErrorMessage="*" ControlToValidate="txtReferenciaDireccion"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Button ID="btnGuardarDireccion" runat="server" Text="Agregar" OnClick="btnGuardarDireccion_Click" />
                                <asp:Button ID="btnCancelarDireccion" runat="server" Text="Cancelar" CausesValidation="false" OnClick="btnCancelarDireccion_Click" />
                            </asp:Panel>
                        </Content>
                    </asp:AccordionPane>
                    <asp:AccordionPane ID="AccordionPane3" runat="server">
                        <Header>
                            Estudios Realizados</Header>
                        <Content>
                            <asp:GridView ID="gvEstudios" runat="server">
                            </asp:GridView>
                            <asp:LinkButton ID="btnAddEstudio" runat="server"  OnClick="btnAddEstudio_Click">Agregar estudio realizado</asp:LinkButton>
                            <asp:Panel runat="server" ID="pnlEstudioRealizado" Visible="false">
                                <asp:Label ID="lblTipoEducacion" runat="server" Text="Tipo de Educación:"></asp:Label>
                                <asp:DropDownList ID="ddlTipoEducacion" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:Label ID="lblPaisEducac" runat="server" Text="País:"></asp:Label>
                                <asp:DropDownList ID="ddlPaisEducac" runat="server" AutoPostBack="true" onselectedindexchanged="ddlPaisEducac_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:Label ID="lblProvEducac" runat="server" Text="Provincia:"></asp:Label>
                                <asp:DropDownList ID="ddlProvEducac" runat="server" AutoPostBack="true" onselectedindexchanged="ddlProvEducac_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqValProvEducac" runat="server" ErrorMessage="*" ControlToValidate="ddlProvEducac"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblCiudEducac" runat="server" Text="Ciudad:"></asp:Label>
                                <asp:DropDownList ID="ddlCiudEducac" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqValCiudEducac" runat="server" ErrorMessage="*" ControlToValidate="ddlCiudEducac"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblInstitucion" runat="server" Text="Institución:" />
                                <asp:TextBox ID="txtInstitucion" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValInstitucion" runat="server" ErrorMessage="*" ControlToValidate="txtInstitucion"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblTitulo" runat="server" Text="Título:" />
                                <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValTitulo" runat="server" ErrorMessage="*" ControlToValidate="txtTitulo"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblNotaGrado" runat="server" Text="Nota:" />
                                <asp:TextBox ID="txtNotaGrado" runat="server" SkinID="skinMedium"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValNotaGrado" runat="server" ErrorMessage="*" ControlToValidate="txtNotaGrado"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblAnioGrado" runat="server" Text="Año:" />
                                <asp:TextBox ID="txtAnioGrado" runat="server" SkinID="skinMedium"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValAnioGrado" runat="server" ErrorMessage="*" ControlToValidate="txtAnioGrado"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblObservacEducac" runat="server" Text="Observación:" />
                                <asp:TextBox ID="txtObservacEducac" runat="server"></asp:TextBox>
                                <br />
                                <asp:Button ID="btnGuardarEstudio" runat="server" Text="Agregar" OnClick="btnGuardarEstudio_Click" />
                                <asp:Button ID="btnCancelarEstudio" runat="server" Text="Cancelar" CausesValidation="false" OnClick="btnCancelarEstudio_Click" />
                            </asp:Panel>
                        </Content>
                    </asp:AccordionPane>
                    <asp:AccordionPane ID="AccordionPane4" runat="server">
                        <Header>
                            Referencias Personales</Header>
                        <Content>
                            <asp:GridView ID="gvReferencias" runat="server">
                            </asp:GridView>
                            <asp:LinkButton ID="btnAddReferencia" runat="server"  OnClick="btnAddReferencia_Click">Agregar referencia personal</asp:LinkButton>
                            <asp:Panel runat="server" ID="pnlReferencia" Visible="false">
                                <asp:Label ID="lblTipoReferencia" runat="server" Text="Tipo de Referencia:"></asp:Label>
                                <asp:DropDownList ID="ddlTipoReferencia" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:Label ID="lblIdentReferencia" runat="server" Text="Identificación:"></asp:Label>
                                <asp:TextBox ID="txtIdentReferencia" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValIdentReferencia" runat="server" ErrorMessage="*" ControlToValidate="txtIdentReferencia"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblNombresReferencia" runat="server" Text="Nombres:"></asp:Label>
                                <asp:TextBox ID="txtNombresReferencia" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValNombresReferencia" runat="server" ErrorMessage="*" ControlToValidate="txtNombresReferencia"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblApellidReferencia" runat="server" Text="Apellidos:"></asp:Label>
                                <asp:TextBox ID="txtApellidReferencia" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValApellidReferencia" runat="server" ErrorMessage="*" ControlToValidate="txtApellidReferencia"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblPaisReferencia" runat="server" Text="País:"></asp:Label>
                                <asp:DropDownList ID="ddlPaisReferencia" runat="server" AutoPostBack="true" onselectedindexchanged="ddlPaisReferencia_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:Label ID="lblProvincReferencia" runat="server" Text="Provincia:"></asp:Label>
                                <asp:DropDownList ID="ddlProvincReferencia" runat="server" AutoPostBack="true" onselectedindexchanged="ddlProvReferencia_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqValProvincReferencia" runat="server" ErrorMessage="*" ControlToValidate="ddlProvincReferencia"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblCiudadReferencia" runat="server" Text="Ciudad:"></asp:Label>
                                <asp:DropDownList ID="ddlCiudadReferencia" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqValCiudadReferencia" runat="server" ErrorMessage="*" ControlToValidate="ddlCiudadReferencia"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblDireccReferencia" runat="server" Text="Dirección:" />
                                <asp:TextBox ID="txtDireccReferencia" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValDireccReferencia" runat="server" ErrorMessage="*" ControlToValidate="txtDireccReferencia"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblTelefReferencia" runat="server" Text="Teléfono:" />
                                <asp:TextBox ID="txtTelefReferencia" runat="server" SkinID="skinMedium"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValTelefReferencia" runat="server" ErrorMessage="*" ControlToValidate="txtTelefReferencia"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblObservacReferencia" runat="server" Text="Observación:" />
                                <asp:TextBox ID="txtObservacReferencia" runat="server"></asp:TextBox>
                                <br />
                                <asp:Button ID="btnGuardarReferencia" runat="server" Text="Agregar" OnClick="btnGuardarReferencia_Click" />
                                <asp:Button ID="btnCancelarReferencia" runat="server" Text="Cancelar" CausesValidation="false" OnClick="btnCancelarReferencia_Click" />
                            </asp:Panel>
                        </Content>
                    </asp:AccordionPane>
                </Panes>
            </asp:Accordion>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
