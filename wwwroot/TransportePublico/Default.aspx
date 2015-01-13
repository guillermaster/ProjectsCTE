<%@ Page Language="C#" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdnTitle" runat="server" Value="Inicio" />
    <div style="float:left; margin: 20px 60px 5px 30px;">
        <div><asp:Image ID="imgCoop" ImageUrl="~/img/transPubCooperativa.png" runat="server" /></div>
        <div style="margin: 10px 5px 10px 120px;"><asp:ImageButton ID="btnCoopCitacPend" PostBackUrl="Citaciones.aspx?type=C&amp;paid=S" ImageUrl="~/img/transPubCitacPend.png" runat="server" /></div>
        <div style="margin: 10px 5px 10px 120px;"><asp:ImageButton ID="btnCoopCitacHist" PostBackUrl="Citaciones.aspx?type=C&amp;paid=N" ImageUrl="~/img/transPubCitacHist.png" runat="server" /></div>
    </div>
    <div style="margin: 20px 10px 5px 80px;">
        <div><asp:Image ID="imgGrem" ImageUrl="~/img/transPubGremio.png" runat="server" /></div>
        <div style="margin: 10px 5px 10px 120px;"><asp:ImageButton ID="btnGremCitacPend" PostBackUrl="Citaciones.aspx?type=G&amp;paid=S" ImageUrl="~/img/transPubCitacPend.png" runat="server" /></div>
        <div style="margin: 10px 5px 10px 120px;"><asp:ImageButton ID="btnGremCitacHist" PostBackUrl="Citaciones.aspx?type=G&amp;paid=N" ImageUrl="~/img/transPubCitacHist.png" runat="server" /></div>
    </div>
</asp:Content>

