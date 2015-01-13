<%@ Language = "VBScript" %>
<% Response.Buffer = True %>

<html>

<%

' Prepare variables.

Dim oFS, oFSPath
Dim sServername, sServerinst, sPhyspath, sServerVersion 
Dim sServerIP, sRemoteIP
Dim sPath, oDefSite, sDefDoc, sDocName, aDefDoc

Dim bSuccess           ' This value is used later to warn the user if a default document does not exist.
Dim iVer               ' This value is used to pass the server version number to a function.

bSuccess = False
iVer = 0

' Get some server variables to help with the next task.

sServername = LCase(Request.ServerVariables("SERVER_NAME"))
sServerinst = Request.ServerVariables("INSTANCE_ID")
sPhyspath = LCase(Request.ServerVariables("APPL_PHYSICAL_PATH"))
sServerVersion = LCase(Request.ServerVariables("SERVER_SOFTWARE"))
sServerIP = LCase(Request.ServerVariables("LOCAL_ADDR"))      ' Server's IP address
sRemoteIP =  LCase(Request.ServerVariables("REMOTE_ADDR"))    ' Client's IP address

' If the querystring variable uc <> 1, and the user is browsing from the server machine, 
' go ahead and show them localstart.asp.  We don't want localstart.asp shown to outside users.

If Not (sServername = "localhost" Or sServerIP = sRemoteIP) Then
  Response.Redirect "iisstart.asp"
Else 

' Using ADSI, get the list of default documents for this Web site.

sPath = "IIS://" & sServername & "/W3SVC/" & sServerinst
Set oDefSite = GetObject(sPath)
sDefDoc = LCase(oDefSite.DefaultDoc)
aDefDocs = split(sDefDoc, ",")

' Make sure at least one of them is valid.

Set oFS = CreateObject("Scripting.FileSystemObject")

For Each sDocName in aDefDocs
  If oFS.FileExists(sPhyspath & sDocName) Then
    If InStr(sDocName,"iisstart") = 0 Then
      ' IISstart doesn't count because it is an IIS file.
      bSuccess = True  ' This value will be used later to warn the user if a default document does not exist.
      Exit For
    End If
  End If
Next

' Find out what version of IIS is running.

Select Case sServerVersion 
   Case "microsoft-iis/5.0"
     iVer = 50         ' This value is used to pass the server version number to a function.
   Case "microsoft-iis/5.1"
     iVer = 51
   Case "microsoft-iis/6.0"
     iVer = 60
End Select

%>

<head>
<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=iso-8859-1">

<script language="javascript">

  // This code is executed before the rest of the page, even before the ASP code above.
  
  var gWinheight;
  var gDialogsize;
  var ghelpwin;
  
  // Move the current window to the top left corner.
  
  window.moveTo(5,5);
  
  // Change the size of the window.

  gWinheight= 480;
  gDialogsize= "width=640,height=480,left=300,top=50,"
  
  if (window.screen.height > 600)
  {
<% if not success and Err = 0 then %>
    gWinheight= 700;
<% else %>
    gWinheight= 700;
<% end if %>
    gDialogsize= "width=640,height=480,left=500,top=50"
  }
  
  window.resizeTo(620,gWinheight);
  
  // Launch IIS Help in another browser window.
  
  loadHelpFront();

function loadHelpFront()
// This function opens IIS Help in another browser window.
{
  ghelpwin = window.open("http://localhost/iishelp/","Help","status=yes,toolbar=yes,scrollbars=yes,menubar=yes,location=yes,resizable=yes,"+gDialogsize,true);  
      window.resizeTo(620,gWinheight);
}

function activate(ServerVersion)
// This function brings up a little help window showing how to open the IIS snap-in.
{
  if (50 == ServerVersion)
    window.open("http://localhost/iishelp/iis/htm/core/iisnapin.htm", "SnapIn", 'toolbar=no, left=200, top=200, scrollbars=yes, resizeable=yes,  width=350, height=350');
  if (51 == ServerVersion)
    window.open("http://localhost/iishelp/iis/htm/core/iiabuti.htm", "SnapIn", 'toolbar=no, left=200, top=200, scrollbars=yes, resizeable=yes,  width=350, height=350');
  if (60 == ServerVersion)
    window.open("http://localhost/iishelp/iis/htm/core/gs_iissnapin.htm", "SnapIn", 'toolbar=no, left=200, top=200, scrollbars=yes, resizeable=yes,  width=350, height=350');
  if (0 == ServerVersion)
    window.open("http://localhost/iishelp/", "Help", 'toolbar=no, left=200, top=200, scrollbars=yes, resizeable=yes,  width=350, height=350');  
}

</script>

<title>Servicios Internet de Windows XP Server</title>
<style>
  ul{margin-left: 15px;}
  .clsHeading {font-family: verdana; color: black; font-size: 11; font-weight: 800; width:210;}  
  .clsEntryText {font-family: verdana; color: black; font-size: 11; font-weight: 400; background-color:#FFFFFF;}    
  .clsWarningText {font-family: verdana; color: #B80A2D; font-size: 11; font-weight: 600; width:550;  background-color:#EFE7EA;}  
  .clsCopy {font-family: verdana; color: black; font-size: 11; font-weight: 400;  background-color:#FFFFFF;}  
</style>
</head>

<body topmargin="3" leftmargin="3" marginheight="0" marginwidth="0" bgcolor="#FFFFFF"
link="#000066" vlink="#000000" alink="#0000FF" text="#000000">

<!-- BEGIN MAIN DOCUMENT BODY --->

<p align="center"><img src="winXP.gif" vspace="0" hspace="0"></p>
<table width="500" cellpadding="5" cellspacing="3" border="0" align="center">

  <tr>
  <td class="clsWarningText" colspan="2">
  
  <table><tr><td>
  <img src="warning.gif" width="40" height="40" border="0" align="left">
  </td><td class="clsWarningText">
  <b>Su servicio Web está activo.
  
<% If Not bSuccess And Err = 0 Then %>
  
  <p>Actualmente no tiene establecida una página Web predeterminada para sus
 usuarios. Cualquier usuario que intente conectarse a su sitio Web desde otra máquina recibirá una 
  <a href="iisstart.asp?uc=1">En construcción</a> página.
 Su servidor Web enumera los siguientes archivos como posibles páginas Web predeterminadas: <%=sDefDoc%>. Actualmente, sólo existe iisstart.asp.<br><br>
  
<% End If %>

  Para agregar documentos a su sitio Web predeterminado, guarde los archivos en <%=sPhyspath%>. 
  </b>
  </td></tr></table>
 
  </td>
  </tr>
  
  <tr>
  <td>
  <table cellpadding="3" cellspacing="3" border=0 >
  <tr>
    <td valign="top" rowspan=3>
      <img src="web.gif">
    </td>  
    <td valign="top" rowspan=3>
  <span class="clsHeading">
  IIS 5.1</span><br>
      <span class="clsEntryText">    
    Los Servicios de Internet Information Server (IIS) 5.1 para Microsoft Windows XP Professional
    ofrecen la eficacia de las páginas 
    Web a Windows. Con ayuda de IIS, podrá compartir fácilmente archivos e impresoras, o bien podrá crear aplicaciones para 
    publicar de forma segura información en el Web a fin de mejorar la forma en que su organización comparte información. IIS es una plataforma segura 
    para crear y distribuir soluciones de comercio electrónico y aplicaciones críticas en el Web.
  <p>
    La utilización de Windows XP Professional con IIS instalado proporciona un sistema operativo personal y de desarrollo que le permite:</span>
  <p>
    <ul class="clsEntryText">
      <li>Configurar un servidor Web personal
      <li>Compartir información con su equipo
      <li>Tener acceso a las bases de datos
      <li>Desarrollar una intranet empresarial
      <li>Desarrollar aplicaciones para el Web.
    </ul>
  <p>
  <span class="clsEntryText">
    IIS integra estándares de Internet consolidados con Windows, para que utilizar el Web no 
    signifique tener que empezar desde el principio y aprender nuevas formas de publicar, administrar o desarrollar contenido. 
  <p>
  </span>
  </td>

    <td valign="top">
      <img src="mmc.gif">
    </td>
    <td valign="top">
      <span class="clsHeading">Administración integrada</span>
      <br>
      <span class="clsEntryText">
        Puede administrar IIS a través de la Administración de equipos de Windows XP <a href="javascript:activate(<%=iVer%>);">consola</a> 
        o utilizando secuencias de comandos. Mediante la consola, podrá también compartir el contenido de sus sitios y servidores que se administran con 
        los Servicios de Internet Information Server con otros usuarios a través del Web. Al tener acceso al complemento IIS desde la consola, podrá
        configurar los valores y propiedades más comunes de IIS. Después del desarrollo de sitios y aplicaciones, estos valores y propiedades pueden utilizarse en un 
        entorno de producción que ejecute versiones más avanzadas de servidores de Windows.  
      <p>
       
      </span>
    </td>
  </tr>
  <tr>
    <td valign="top">
      <img src="help.gif">
    </td>
    <td valign="top">
      <span class="clsHeading"><a href="javascript:loadHelpFront();">Documentación en pantalla</a></span>
      <br>
      <span class="clsEntryText">La documentación en pantalla de IIS incluye un índice, la búsqueda de texto completo, 
        y la capacidad de imprimir por nodo o por tema concreto. Para la administración de código y el desarrollo 
        de secuencias de comandos, utilice los ejemplos que se instalaron con IIS. Los archivos de Ayuda emplean el formato 
        HTML, que le permite anotar en ellos y compartirlos según sea necesario. Mediante la documentación en pantalla 
        de IIS, podrá:<p>
      </span>
      <ul class="clsEntryText">
         <li>Obtener ayuda con las tareas
         <li>Conocer el funcionamiento y la administración de servidores
         <li>Consultar el material de referencia
         <li>Ver ejemplos de código.
      </ul>
      <p>
        <span class="clsEntryText">
        Otras fuentes de información valiosa e importante acerca de IIS son los sitios Web 
        de Microsoft.com: MSDN, TechNet y el sitio de Windows.
        </span>
    </td>
  </tr>
  
  <tr>
    <td valign="top">
      <img src="print.gif">
    </td>
    <td valign="top">
      <span class="clsHeading">Impresión Web</span>
      <br>
      <span class="clsEntryText">Windows XP Professional enumera de manera dinámica todas las impresoras 
        de su servidor en un sitio Web de fácil acceso. Puede examinar este sitio para 
        supervisar impresoras y sus trabajos. Asimismo, podrá conectarse a las impresoras a través de este 
        sitio desde cualquier equipo con Windows. Consulte Imprimir en Internet en su documentación de Ayuda de Windows.
      </span>
    </td>
  </tr>
  
  </table>
</td>
</tr>
</table>

<p align=center><em><a href="/iishelp/common/colegal.htm">© 1997-2001 Microsoft Corporation. Reservados todos los derechos.</a></em></p>

</body>
</html>

<% End If %>

