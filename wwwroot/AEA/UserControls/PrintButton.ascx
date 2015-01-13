<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PrintButton.ascx.cs" Inherits="UserControls_PrintButton" %>
<script language=javascript>
function CallPrint(strid, width, height)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width='+width+',height='+height+',toolbar=0,scrollbars=0,status=0');
 WinPrint.document.write("<head><title>Comisión de Tránsito de la Provincia del Guayas</title><link rel='stylesheet' type='text/css' href='../css/layout4_setup.css' /><link rel='stylesheet' type='text/css' href='../css/layout4_text.css' />");
 WinPrint.document.write("<style type='text/css'> table { font-size:100%; } </style></head>");
 WinPrint.document.write("<body><div class=main-content1>");
 WinPrint.document.write("<img src='../img/CTG-AEAprintable.gif' /><br />"+prtContent.innerHTML);
 WinPrint.document.write("</div></body>");
 WinPrint.document.close();
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
}
</script>

<asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/img/btnImprimir.gif" CausesValidation="False" />