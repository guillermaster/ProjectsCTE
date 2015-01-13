<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsDeudaCompradoresAuto.aspx.cs" Inherits="ConsDeudaCompradoresAuto" %>

<%@ Register Src="UserControls/PrintButton.ascx" TagName="PrintButton" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="main-content1">
  <uc1:PrintButton ID="btnPrint" runat="server" />
    <div id="divContent" class="main-content1" align="center">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackErrorMessage="Ocurrió un error en la consulta al servidor CTG" AsyncPostBackTimeout="360">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
          <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5" DynamicLayout="False">
            <ProgressTemplate>
             <div align="center">
                <img src="img/ajax-loader.gif" />
              </div>
        </ProgressTemplate>
        </asp:UpdateProgress>
<DIV id="divConsPorCedula"><TABLE class="registerform" width=350 align=center><TBODY><TR><TD>Cédula:</TD><TD style="HEIGHT: 34px"><asp:TextBox id="txtCedula" runat="server" MaxLength="18"></asp:TextBox> <asp:RequiredFieldValidator id="reqValCedula" runat="server" ForeColor="Maroon" ErrorMessage="&nbsp;*" ControlToValidate="txtCedula"></asp:RequiredFieldValidator> <asp:Button id="btnSearch" onclick="btnSearch_Click" runat="server" Text="Consultar" CssClass="button_horiz"></asp:Button> </TD></TR></TBODY></TABLE></DIV><DIV align=center><BR /><asp:GridView id="gvDeudas" runat="server" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"><Columns>
<asp:BoundField DataField="num_infraccion" HeaderText="Citaci&#243;n"></asp:BoundField>
<asp:BoundField DataField="fec_infraccion" HeaderText="Fecha"></asp:BoundField>
<asp:BoundField DataField="identificacion" HeaderText="Licencia"></asp:BoundField>
<asp:BoundField DataField="tipo" HeaderText="Tipo"></asp:BoundField>
<asp:BoundField DataField="placa" HeaderText="Placa"></asp:BoundField>
<asp:BoundField DataField="cod_contravencion" HeaderText="Art&#237;culo-Literal"></asp:BoundField>
<asp:BoundField DataField="val_contrav" HeaderText="Valor($)"></asp:BoundField>
<asp:BoundField DataField="mul_contrav" HeaderText="Multa($)"></asp:BoundField>
<asp:BoundField DataField="total" HeaderText="Total($)"></asp:BoundField>
</Columns>
</asp:GridView> </DIV><DIV><asp:Label id="Label1" runat="server" Text="Infracciones Pendientes:" Visible="false"></asp:Label> <asp:Label id="lblTotPendientes" runat="server" Visible="false"></asp:Label></DIV><DIV><asp:Label id="Label2" runat="server" Text="Total a Pagar:" Visible="False" Font-Bold="True"></asp:Label> <asp:Label id="lblTotPagar" runat="server" Visible="false"></asp:Label></DIV><DIV id="divError" runat="server" visible="false"><TABLE class="error2" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/error.gif" /></TD><TD><B>Error:</B><BR /><asp:Label id="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV><DIV id="divWarning" runat="server" visible="false"><TABLE class="warning" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/warning.gif" /></TD><TD><asp:Label id="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
  </div>
  </asp:Content>
