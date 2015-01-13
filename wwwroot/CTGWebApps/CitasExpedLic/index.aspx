<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="CitasExpLic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisi�n de Tr�nsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_text.css" />
    <script type="text/javascript" src="../../js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:440px;" id="divStep0" runat="server">
    <div class="contactform">
    <div class="column_small"><img src="../img/expcitas_paso1.gif" /></div>
    <div class="column_medium">Reserve su turno: <br />Eliga la fecha, lugar y hora en donde desea continuar su tr�mite personalmente.<br /><br /><br /><br /></div>
    </div>
    <div class="contactform">
    <div class="column_small"><img src="../img/expcitas_paso2.gif" /></div>
    <div class="column_medium">Solicitud del Tr�mite: <br />Complete y env�e el formulario con los datos requeridos.<br /><br /><br /><br /></div>
    </div>
    <div class="contactform">
    <div class="column_small"><img src="../img/expcitas_paso3.gif" /></div>
    <div class="column_medium">Impresi�n del Formulario: <br />Confirme los datos e imprima el formulario que debe presentar al acercarse a completar su tr�mite personalmente.<br /><br /><br /></div>
    </div>
    <div style="float: right;"><asp:LinkButton ID="btnIniciar" runat="server" CssClass="button" OnClick="btnIniciar_Click" Width="101px">Iniciar Tr�mite</asp:LinkButton></div>
    </div>
    
    <div id="divStep1" runat="server">
    <h2>Paso 1</h2>
    <div class="contactform">
    <div><b>Seleccione el d�a en el que desea realizar el tr�mite</b>
    <br /><br />
    </div>
    <div><asp:Calendar ID="calBrevetacion" runat="server" BackColor="White" BorderColor="#999999" CellPadding="0" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" Height="140px" Width="200px" BorderStyle="Solid">
        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" BorderWidth="1px" />
        <SelectorStyle BackColor="#CCCCCC" />
        <WeekendDayStyle BackColor="#ffffe6" />
        <OtherMonthDayStyle ForeColor="Gray" />
        <NextPrevStyle VerticalAlign="Bottom" />
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="6.5pt" />
        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True"  />
    </asp:Calendar><br /></div>
    </div>
    </div>
    </form>
</body>
</html>
