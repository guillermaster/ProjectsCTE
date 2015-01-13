<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cuestionario.aspx.cs" Inherits="Cuestionarios_Cuestionario" %>

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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <uc1:logoutCuestionarios ID="LogoutCuestionarios1" runat="server" />
        <img src="../img/btn_textzoomout_off.gif" align="right" onclick="zoomText('disminuir','UpdatePanel1');" style="margin-right: 20px; cursor:pointer;" />
        <img src="../img/btn_textzoomin_off.gif" align="right" onclick="zoomText('aumentar','UpdatePanel1');" style="cursor:pointer;" />
        
    <div style="height:10px">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5">
        <ProgressTemplate>
            <div>
                <img src="../img/ajax-loader.gif" /> 
                Procesando datos...
            </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
        <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divError" runat="server" visible="false">
            <br /><br />
             <table class="error2" align="center" width="480">
                <tr>
                    <td style="width: 54px"><img src="../img/error.gif" /></td>
                    <td><asp:Label ID="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label><br />
                        <asp:HyperLink ID="hypRetryLoadExam" runat="server" Visible="False" NavigateUrl="~/Cuestionarios/Cuestionario.aspx">Haga clíc aquí para intentar cargar el examen nuevamente</asp:HyperLink></td>
                </tr>         
              </table>
            </div>
            <div id="divOK" runat="server">
          <div id="divCuestionario" style="height:150px">
            <asp:Label ID="lblNoPregunta" runat="server" CssClass="h2"></asp:Label>
            <br /><br />
            <asp:HiddenField ID="hdnNoRespuestas" runat="server" />
            <asp:Label ID="lblPregunta" runat="server"></asp:Label>
            <br /><br />
            <table width="100%">
             <tr>
                <td width="70%">
                <asp:GridView ID="gvCheckboxAnswers" runat="server" Visible="False" AutoGenerateColumns="False" GridLines="None" ShowHeader="False">
                <Columns>
                    <asp:BoundField DataField="COD_RESPUESTA" HeaderText="C&#243;digo" ReadOnly="True"
                        SortExpression="COD_RESPUESTA" />
                    <asp:TemplateField>
                     <ItemTemplate>
                        <asp:CheckBox ID="chkResp" runat="server" />
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DESC_RESPUESTA" HeaderText="Respuesta" ReadOnly="True"
                        SortExpression="DESC_RESPUESTA" />
                </Columns>
            </asp:GridView>
            <asp:GridView ID="gvRadioAnswers" runat="server" Visible="False" AutoGenerateColumns="False" GridLines="None" ShowHeader="False">
                <Columns>
                    <asp:BoundField DataField="COD_RESPUESTA" HeaderText="C&#243;digo" SortExpression="COD_RESPUESTA"
                        Visible="False" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input name="radbtnResp" type="radio" 
                                value='<%# Eval("COD_RESPUESTA") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DESC_RESPUESTA" HeaderText="Respuesta" ReadOnly="True"
                        SortExpression="DESC_RESPUESTA" />
                </Columns>
            </asp:GridView>
                </td>
                <td width="30%"><asp:Image ID="imgFoto" runat="server" ImageAlign="Left" ImageUrl="~/Cuestionarios/imagenPregunta.aspx" /></td>
             </tr>
            </table>
            </div>
            <table width="100%">
            <tr>
            <td width="50%" align="left">
                &nbsp;</td>
            <td width="50%" align="right">
                <asp:LinkButton ID="btnSiguiente" runat="server" CssClass="button" Width="100px" OnClick="btnSiguiente_Click">Siguiente >></asp:LinkButton>&nbsp;
            </td>
            </tr>
            </table>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
     </div>
    </form>
</body>
</html>
