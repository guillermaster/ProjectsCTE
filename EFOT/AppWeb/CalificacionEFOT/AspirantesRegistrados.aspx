<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AspirantesRegistrados.aspx.cs" Inherits="AspirantesRegistrados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel ID="pnlForm" runat="server">
        <asp:Label ID="lblCriterio" runat="server" Text="Criterio de Búsqueda:" />
        <asp:DropDownList ID="ddlCriterio" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCriterio_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlTipoActividad" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlTipoActividad_SelectedIndexChanged" Visible="False">
            <asp:ListItem Selected="True" Value="">-- Seleccione el tipo de actividad --</asp:ListItem>
            <asp:ListItem Value="REV">Revisión</asp:ListItem>
            <asp:ListItem Value="REG">Registro</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Label ID="lblRequisitoCriterio" runat="server" Text="Dato:" Visible="false"></asp:Label>
        <asp:TextBox ID="txtRequisitoCriterio" runat="server" Visible="false"></asp:TextBox>
        <asp:DropDownList ID="ddlRequisitoCriterio" runat="server" Visible="false">
        </asp:DropDownList>
        <br />
        <asp:Label ID="lblEstadoAct" runat="server" Text="Estado:" Visible="false"></asp:Label>
        <asp:DropDownList ID="ddlEstadoAct" runat="server" Visible="false">
            <asp:ListItem Selected="True" Value="S">Idóneo</asp:ListItem>
            <asp:ListItem Value="N">No Idóneo</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" 
            OnClick="btnConsultar_Click" CausesValidation="False" />
        <br />
        <div style="margin: 10px 0 10px 0;">
            <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
            <asp:Button ID="btnNextExam" runat="server" onclick="btnNextExam_Click" 
                Text="Registrar Actividad" Visible="False" />

            <asp:Panel ID="pnlRegActividadBulk" runat="server" Style="padding: 10px; text-align: center;
            border: solid thin #000000" BackColor="AntiqueWhite" ScrollBars="None">
            <div>
                <h3>
                    Registro de Actividad</h3>
                <asp:Label ID="lblTipoActBulk" runat="server" Text="Actividad:" />
                <asp:DropDownList ID="ddlTipoActBulk" runat="server">
                </asp:DropDownList><br />
                <asp:Label ID="lblFechaActBulk" runat="server" Text="Fecha:" />
                <asp:TextBox ID="txtFecActBulk" runat="server" SkinID="skinMedium"></asp:TextBox>
                <asp:CalendarExtender ID="txtFecNac_CalendarExtenderBulk" runat="server" TargetControlID="txtFecActBulk">
                </asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1Bulk" runat="server" 
                    ControlToValidate="txtFecActBulk" ErrorMessage="*" ValidationGroup="NuevaActividadBulk"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblHoraActBulk" runat="server" Text="Hora:" />
                <asp:TextBox ID="txtHoraActBulk" runat="server" SkinID="skinMedium"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2Bulk" runat="server" 
                    ControlToValidate="txtHoraActBulk" ErrorMessage="*" 
                    ValidationGroup="NuevaActividadBulk"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1Bulk" runat="server" 
                    ControlToValidate="txtHoraActBulk" ErrorMessage="Hora inválida" 
                    ValidationExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]" 
                    ValidationGroup="NuevaActividadBulk"></asp:RegularExpressionValidator>
                <br />
                <asp:Label ID="lblObservacActBulk" runat="server" Text="Observación:" />
                <asp:TextBox ID="txtObservacActBulk" runat="server"></asp:TextBox><br />
                <asp:Button ID="btnGuardarRegActBulk" runat="server" Text="Guardar" 
                    onclick="btnGuardarRegActBulk_Click" ValidationGroup="NuevaActividadBulk" />
                <asp:Button ID="btnCancelaRegActBulk" runat="server" Text="Cancelar" 
                    CausesValidation="False" />
            </div>
        </asp:Panel>
	<asp:ModalPopupExtender ID="mpeRegActBulk" runat="server" TargetControlID="ButtonRegActBulk"
            PopupControlID="pnlRegActividadBulk" BackgroundCssClass="modalBackground" DropShadow="false"
            CancelControlID="btnCancelaRegActBulk" />
	<asp:Button ID="ButtonRegActBulk" runat="server" Style="display: none" />

    <asp:Panel ID="pnlCalificaciones" runat="server" Style="padding: 10px; text-align: center;
            border: solid thin #000000" BackColor="AntiqueWhite" ScrollBars="None">
        No. Cédula: <asp:Label ID="lblValNumCedulaCalif" runat="server" />
        Nombre: <asp:Label ID="lblValNombreCalif" runat="server" />
        <asp:GridView ID="gvCalificaciones" runat="server">
        </asp:GridView>
        <asp:ImageButton ID="imgBtnRevertirCalificacion" 
            ImageUrl="~/images/deleteCalif.png" runat="server" Visible="false" 
            onclick="imgBtnRevertirCalificacion_Click" />
        <div align="center"><asp:Button ID="btnCerrarCalif" CausesValidation="false" runat="server" Text="Cerrar" /></div>
    </asp:Panel>
    <asp:ModalPopupExtender ID="mpeCalificaciones" runat="server" TargetControlID="btnCalif"
            PopupControlID="pnlCalificaciones" BackgroundCssClass="modalBackground" DropShadow="false"
            CancelControlID="btnCerrarCalif" />
	<asp:Button ID="btnCalif" runat="server" Style="display: none" />

            <asp:GridView ID="gvAspirantesReg" runat="server" OnSelectedIndexChanged="gvAspirantesReg_SelectedIndexChanged"
                AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="200"
                OnPageIndexChanging="AspRegGridViewPageIndexChanging">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField CausesValidation="False" SelectText="Ver más datos" ShowSelectButton="True" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkAprobac" runat="server"   />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnVerCalif" ImageUrl="~/images/note_pin.png" OnClick="btnVerCalificaciones_Click" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </asp:Panel>
    <div align="center">
        <asp:Panel ID="pnlMoreData" runat="server" Visible="false">
            <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/arrowleft.png" runat="server"
                OnClick="ImageButton1_Click" CausesValidation="False" />
            <h2>
                Datos Personales</h2>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblNombres" runat="server" Text="Nombres:" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombres" runat="server" ReadOnly="true" BorderStyle="None" BorderWidth="1px" />
                        <asp:ImageButton ID="btnEditNombres" ImageUrl="~/images/edit.png" runat="server"
                            OnClick="btnEditNombres_Click" />
                    </td>
                    <td>
                        <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtApellidos" runat="server" ReadOnly="true" BorderStyle="None"
                            BorderWidth="1px" />
                        <asp:ImageButton ID="btnEditApellidos" ImageUrl="~/images/edit.png" runat="server"
                            OnClick="btnEditApellidos_Click" Style="width: 16px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCedula" runat="server" Text="Cédula de Identidad:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValCedula" runat="server" Text="" />
                    </td>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Correo electrónico:" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" ReadOnly="true" BorderStyle="None" BorderWidth="1px" />
                        <asp:ImageButton ID="btnEditEmail" ImageUrl="~/images/edit.png" runat="server" OnClick="btnEditEmail_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFechaNac" runat="server" Text="Fecha de Nacimiento:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValFechaNac" runat="server" Text="" />
                    </td>
                    <td>
                        <asp:Label ID="lblPaisNac" runat="server" Text="País de nacimiento:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValPaisNac" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProvNac" runat="server" Text="Provincia de nacimiento:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValProvNac" runat="server" Text="" />
                    </td>
                    <td>
                        <asp:Label ID="lblCiudadNac" runat="server" Text="Ciudad de nacimiento:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValCiudadNac" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSexo" runat="server" Text="Sexo:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValSexo" runat="server" Text="" />
                    </td>
                    <td>
                        <asp:Label ID="lblEstadoCivil" runat="server" Text="Estado Civil:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValEstadoCivil" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCargFam" runat="server" Text="Cargas familiares:" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtCargFam" runat="server" ReadOnly="true" BorderStyle="None" BorderWidth="1px" />
                        <asp:ImageButton ID="btnEditCargFam" ImageUrl="~/images/edit.png" runat="server"
                            OnClick="btnEditCargFam_Click" />
                    </td>
                    <td>
                        <asp:Label ID="lblEstatura" runat="server" Text="Estatura:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValEstatura" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoSangre" runat="server" Text="Tipo de sangre:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValTipoSangre" runat="server" Text="" />
                    </td>
                    <td>
                        <asp:Label ID="lblPeso" runat="server" Text="Peso:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValPeso" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTallaCalzado" runat="server" Text="Talla de calzado:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValTallaCalzado" runat="server" Text="" />
                    </td>
                    <td>
                        <asp:Label ID="lblTallaPantalon" runat="server" Text="Talla de pantalón:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValTallaPantalon" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTallaCamisa" runat="server" Text="Talla de camisa:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValTallaCamisa" runat="server" Text="" />
                    </td>
                    <td>
                        <asp:Label ID="lblTallaGorra" runat="server" Text="Talla de gorra:" />
                    </td>
                    <td>
                        <asp:Label ID="lblValTallaGorra" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIdeDactilar" runat="server" Text="Ide. Dactilar:" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdeDactilar" runat="server" ReadOnly="true" BorderStyle="None"
                            BorderWidth="1px" />
                        <asp:ImageButton ID="btnEditIdeDactilar" ImageUrl="~/images/edit.png" runat="server"
                            OnClick="btnEditIdeDactilar_Click" Style="width: 16px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:ImageButton ID="btnSaveChanges" ImageUrl="~/images/btnSaveChanges.png" runat="server"
                            OnClick="btnSaveChanges_Click" Visible="False" CausesValidation="False" />
                    </td>
                </tr>
            </table>
            <h2>
                Direcciones de Contacto</h2>
            <asp:GridView ID="gvDirecciones" runat="server" CellPadding="4" ForeColor="#333333"
                GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <h2>
                Estudios Realizados</h2>
            <asp:GridView ID="gvEstudios" runat="server" CellPadding="4" ForeColor="#333333"
                GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <h2>
                Referencias Personales</h2>
            <asp:GridView ID="gvReferencias" runat="server" CellPadding="4" ForeColor="#333333"
                GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:Button ID="btnRevisarAct" runat="server" Text="Revisar Actividad" 
                CausesValidation="False" onclick="btnRevisarAct_Click" />
            <asp:Button ID="btnRegistrarAct" runat="server" Text="Registrar Actividad" 
                OnClick="btnRegistrarAct_Click" CausesValidation="False" />
        </asp:Panel>
        <asp:Panel ID="pnlRegActividad" runat="server" Style="padding: 10px; text-align: center;
            border: solid thin #000000" BackColor="AntiqueWhite" ScrollBars="None">
            <div>
                <h3>
                    Registro de Actividad</h3>
                <asp:Label ID="lblTipoAct" runat="server" Text="Actividad:" />
                <asp:DropDownList ID="ddlTipoAct" runat="server">
                </asp:DropDownList><br />
                <asp:Label ID="lblFechaAct" runat="server" Text="Fecha:" />
                <asp:TextBox ID="txtFecAct" runat="server" SkinID="skinMedium"></asp:TextBox>
                <asp:CalendarExtender ID="txtFecNac_CalendarExtender" runat="server" TargetControlID="txtFecAct">
                </asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtFecAct" ErrorMessage="*" ValidationGroup="NuevaActividad"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblHoraAct" runat="server" Text="Hora:" />
                <asp:TextBox ID="txtHoraAct" runat="server" SkinID="skinMedium"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtHoraAct" ErrorMessage="*" 
                    ValidationGroup="NuevaActividad"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtHoraAct" ErrorMessage="Hora inválida" 
                    ValidationExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]" 
                    ValidationGroup="NuevaActividad"></asp:RegularExpressionValidator>
                <br />
                <asp:Label ID="lblObservacAct" runat="server" Text="Observación:" />
                <asp:TextBox ID="txtObservacAct" runat="server"></asp:TextBox><br />
                <asp:Button ID="btnGuardarRegAct" runat="server" Text="Guardar" 
                    onclick="btnGuardarRegAct_Click" ValidationGroup="NuevaActividad" />
                <asp:Button ID="btnCancelaRegAct" runat="server" Text="Cancelar" 
                    CausesValidation="False" />
            </div>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeRegAct" runat="server" TargetControlID="ButtonRegAct"
            PopupControlID="pnlRegActividad" BackgroundCssClass="modalBackground" DropShadow="false"
            CancelControlID="btnCancelaRegAct" />
        <asp:Button ID="ButtonRegAct" runat="server" Style="display: none" />


        <asp:Panel ID="pnlRevision" runat="server" Style="padding: 10px; text-align: center;
            border: solid thin #000000" BackColor="AntiqueWhite" ScrollBars="None">
            <div>
                <h3>
                    Revisión de Actividad</h3>
                <asp:Label ID="lblTipoRevision" runat="server" Text="Actividad:" />
                <asp:DropDownList ID="ddlTipoRevision" runat="server">
                </asp:DropDownList><br />
                <asp:Label ID="lblEstadoRevision" runat="server" Text="Estado:" />
                <asp:DropDownList ID="ddlEstadoRevision" runat="server">
                    <asp:ListItem Selected="True" Value="S">Idóneo</asp:ListItem>
                    <asp:ListItem Value="N">No Idóneo</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="lblObservacRevision" runat="server" Text="Observación:" />
                <asp:TextBox ID="txtObservacRevision" runat="server"></asp:TextBox><br />
                <asp:Button ID="btnGuardarRevision" runat="server" Text="Guardar" 
                    ValidationGroup="Revision" onclick="btnGuardarRevision_Click" />
                <asp:Button ID="btnCancelaRevision" runat="server" Text="Cancelar" 
                    CausesValidation="False" />
            </div>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeRevision" runat="server" TargetControlID="ButtonRevAct"
            PopupControlID="pnlRevision" BackgroundCssClass="modalBackground" DropShadow="false"
            CancelControlID="btnCancelaRevision" />
        <asp:Button ID="ButtonRevAct" runat="server" Style="display: none" />

    </div>
</asp:Content>
