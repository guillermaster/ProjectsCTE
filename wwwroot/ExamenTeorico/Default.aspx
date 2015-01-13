<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Cuestionarios_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="css/layout4_text.css" />
    <script type="text/javascript" src="js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <img src="img/bannerCNTTTSV.png" alt="CNTTTSV" style="margin-bottom:20px;" />
    <h1>Examen Teórico</h1>
    <br /><br />    
    <p>A continuación se generará un cuestionario similar al examen teórico que usted dará en el momento de solicitar una nueva licencia o la renovación de la misma.</p>
    <br />
    <p>Tendrá que contestar 20 preguntas, las cuales son tomadas del banco de preguntas del examen.</p>
    <br />
    </div>
    <br /><br /><br />
    <div align="center">
        <asp:HyperLink ID="hypComenzar" runat="server" ImageUrl="img/cuestinarios_btn_comenzar.jpg" NavigateUrl="Cuestionario.aspx"></asp:HyperLink>
    </div>
    </form>
</body>
</html>
