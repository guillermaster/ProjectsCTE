<%@ Control Language="C#" AutoEventWireup="true" CodeFile="editableTextBox.ascx.cs" Inherits="controls_editableTextBox" %>
<asp:HiddenField ID="hdnCampo" runat="server" />
<asp:HiddenField ID="hdnPreviousValue" runat="server" />
<asp:HiddenField ID="hdnIdentificacion" runat="server" />
<asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" ></asp:TextBox>
<asp:LinkButton ID="btnEditar" runat="server" OnClick="btnEditar_Click">Editar</asp:LinkButton>
<asp:LinkButton ID="btnGuardar" runat="server" Visible="False" OnClick="btnGuardar_Click">Guardar</asp:LinkButton>
<asp:LinkButton ID="btnCancelar" runat="server" Visible="False" OnClick="btnCancelar_Click">Cancelar</asp:LinkButton>
<br />
<asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
<asp:Label ID="lblSuccess" runat="server" ForeColor="Blue" />