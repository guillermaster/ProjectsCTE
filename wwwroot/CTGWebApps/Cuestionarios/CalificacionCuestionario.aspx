<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalificacionCuestionario.aspx.cs" Inherits="Cuestionarios_CalificacionCuestionario" %>

<%@ Register Src="../controls/logoutCuestionarios.ascx" TagName="logoutCuestionarios"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_text.css" />
    <script type="text/javascript" src="../js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc1:logoutCuestionarios ID="LogoutCuestionarios1" runat="server" />
        <img src="../img/btn_textzoomout_off.gif" align="right" onclick="zoomText('disminuir','UpdatePanel1');" style="margin-right: 20px; cursor:pointer;" />
        <img src="../img/btn_textzoomin_off.gif" align="right" onclick="zoomText('aumentar','UpdatePanel1'); " style="cursor:pointer;" />
    </div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img src="../img/ajax-loader.gif" /> 
                Procesando datos...
            </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblMensajeFinCuestionario" runat="server" CssClass="h2"></asp:Label>&nbsp;<br />
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            <br />
            <br />
            <asp:LinkButton ID="btnVerGrid" runat="server" OnClick="btnVerGrid_Click">Haga clíc aquí para ver su examen calificado.</asp:LinkButton>
            <br />
            <br />
            <asp:Label ID="lblMensajeOtroExamen" runat="server" Text="Si desea seguir practicando y realizar otro examen de prueba " />
            <asp:LinkButton ID="btnNuevoExamen" runat="server" OnClick="btnNuevoExamen_Click">haga clíc aquí.</asp:LinkButton>
            <br />
            <br />
            <asp:GridView ID="gvCuestionario" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="None" Width="604px" Visible="False" OnSelectedIndexChanged="gvCuestionario_SelectedIndexChanged">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="n" ReadOnly="True" SortExpression="n" >
                        <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cod_pregunta" ReadOnly="True" SortExpression="cod_pregunta" />
                    <asp:BoundField DataField="desc_pregunta" HeaderText="Pregunta" ReadOnly="True" SortExpression="desc_pregunta" >
                        <ItemStyle Width="400px" />
                    </asp:BoundField>
                    <asp:ImageField DataImageUrlField="imagen">
                        <ItemStyle Height="40px" Width="40px" />
                    </asp:ImageField>
                    <asp:ImageField DataImageUrlField="calificacion">
                    </asp:ImageField>
                    <asp:CommandField SelectText="Ver Soluci&#243;n" ShowSelectButton="True" />
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <br />
            <br />
            <table width="100%">
            <tr><td>
            <asp:GridView ID="gvSolucion" runat="server" AutoGenerateColumns="False" Width="476px" GridLines="Horizontal" HorizontalAlign="Left">
                <Columns>
                    <asp:ImageField DataImageUrlField="seleccion" HeaderText="Respuesta Seleccionada(s)">
                        <ItemStyle HorizontalAlign="Center" Width="20%" VerticalAlign="Middle" />
                    </asp:ImageField>
                    <asp:ImageField DataImageUrlField="solucion" HeaderText="Respuesta Correcta(s)">
                        <ItemStyle HorizontalAlign="Center" Width="20%" VerticalAlign="Middle" />
                    </asp:ImageField>
                    <asp:BoundField DataField="respuesta" HeaderText="Respuesta" ReadOnly="True"
                        SortExpression="respuesta" />
                </Columns>
            </asp:GridView>
            </td></tr>
            <tr><td>
                &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Cuestionarios/img/bullet_back.gif"
                    OnClick="ImageButton1_Click" Visible="False" /></td></tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
