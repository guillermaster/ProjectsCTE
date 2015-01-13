<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CitacionPorCodigo.aspx.cs"
    Inherits="Consultas_Citaciones_CitacionPorCodigo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../../css/layout4_text.css" />

    <script type="text/javascript" src="../../js/common.js"></script>

</head>
<body>
    <div>
        <form id="form1" runat="server">
            <h2>
                Infracciones por Número de Citación&nbsp;</h2>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div style="height: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                    DisplayAfter="5">
                    <ProgressTemplate>
                        <div>
                            <img src="../../img/ajax-loader.gif" />
                            Procesando datos...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="contactform">
                        <asp:Label ID="lblCodCitacion" runat="server" Text="Número de Citación:"></asp:Label>
                        <asp:TextBox ID="txtCodCitacion" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqValCodCitacion" runat="server" SetFocusOnError="True"
                            ErrorMessage="*" ControlToValidate="txtCodCitacion"></asp:RequiredFieldValidator>
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar" CssClass="button2" OnClick="btnSearch_Click"></asp:Button>
                        <br />
                        <asp:RegularExpressionValidator ID="regExpValCodCitacion" runat="server" SetFocusOnError="True"
                            ErrorMessage="El código debe ser numérico" ControlToValidate="txtCodCitacion"
                            ValidationExpression="0*[1-9][0-9]*"></asp:RegularExpressionValidator>&nbsp;
                    </div>
                    <br />
                    <div id="divError" runat="server" visible="false">
                        <table class="error2" align="center" width="480">
                            <tr>
                                <td style="width: 54px">
                                    <img src="../../img/error.gif" /></td>
                                <td>
                                    <b>Error:</b><br />
                                    <asp:Label ID="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    <div id="divWarning" runat="server" visible="false">
                        <table class="warning" align="center" width="480">
                            <tr>
                                <td style="width: 54px">
                                    <img src="../../img/warning.gif" /></td>
                                <td>
                                    <asp:Label ID="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    <asp:DetailsView ID="detViewDetCitacion" runat="server" CellPadding="4" ForeColor="#333333"
                        GridLines="None" Height="50px" Width="550px">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                        <RowStyle BackColor="#EFF3FB" />
                        <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" Width="200px" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:DetailsView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
        <div class="column1-unit" style="margin-top:20px;">
		    <a href="../../DefaultConsultas.aspx"><img src="../../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
        </div>  
    </div>
</body>
</html>
