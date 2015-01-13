<%@ Page Language="C#" MasterPageFile="~/MainV2.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"  %>

<%@ Register Src="../UserControls/BcoBolivariano.ascx" TagName="BcoBolivariano" TagPrefix="ucBancos" %>
<%@ Register Src="../UserControls/BcoPacifico.ascx" TagName="BcoPacifico" TagPrefix="ucBancos" %>
<%@ Register Src="../UserControls/BcoGuayaquil.ascx" TagName="BcoGuayaquil" TagPrefix="ucBancos" %>
<%@ Register Src="../UserControls/BcoProdubanco.ascx" TagName="BcoProdubanco" TagPrefix="ucBancos" %>
<%@ Register Src="../UserControls/BcoServipagos.ascx" TagName="BcoServipagos" TagPrefix="ucBancos" %>
<%@ Register src="../UserControls/BcoWesternUnion.ascx" tagname="BcoWesternUnion" tagprefix="ucBancos" %>

<%@ Register src="../UserControls/LinkTramitesTracking.ascx" tagname="LinkTramitesTracking" tagprefix="uc1" %>
<%@ Register src="../UserControls/LinkTramitesFinalizados.ascx" tagname="LinkTramitesFinalizados" tagprefix="uc1" %>
<%@ Register src="../UserControls/LinkCitacionesPagadasBanca.ascx" tagname="LinkCitacionesPagadasBanca" tagprefix="uc1" %>
<%@ Register src="../UserControls/LinkSolTramitesNoPag.ascx" tagname="LinkSolTramitesNoPag" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../js/common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


    <div class="full">
        <h2>Citaciones y Trámites</h2>
        <div class="div3ColsRow">
            <div class="div3Cols"><a href="Citaciones/CitacionesPendientes.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsCitacPend.png" /><br />Pendientes<br />de pago</a></div>
            <div class="div3Cols"><a href="Citaciones/CitacionPorCodigo.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsCitacPorCod.png" /><br />Por código</a></div>
            <div class="div3Cols"><a href="Tramites/EstadosTramite.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsTramite.png" /><br />Estado de trámite</a></div>
        </div>
    </div>

    <div class="full" style="float: left; width:100%;">
        <h2>Datos Personales</h2>
        <div class="div3ColsRow">
            <div class="div3Cols"><a href="Licencias/DatosLicencia.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsLicenc.png" /><br />Mi licencia</a></div>
            <div class="div3Cols"><a href="Licencias/PuntosLicencia.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsPtosLicenc.png" /><br />Puntos en<br />mi licencia</a></div>
            <div class="div3Cols"><a href="Vehiculos/Vehiculos.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsVehic.png" /><br />Mis vehículos</a></div>
        </div>
    </div>

    <div class="full" style="float: left; width:100%;">
        <h2>Otras Consultas</h2>
        <div class="div4ColsRow">
            <div class="div4Cols"><a href="Varios/ExamPracticoNotas.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsExamPracCalif.png" /><br />Exámenes prácticos</a></div>
            <div class="div4Cols"><a href="javascript:popup('https://declaraciones.sri.gov.ec/mat-vehicular-internet/reportes/general/valoresAPagar.jsp','1000','580','1','sri')" onmouseover="window.status='';return true"><img src="../images/icoSRI.png" /><br />Valor de matrícula</a></div>
            <div class="div4Cols"><a href="Vehiculos/VehiculosRobados.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsVehicRobados.png" /><br />Vehículos robados</a></div>
            <div class="div4Cols"><a href="Licencias/LicGroupBySangre.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsLicencPorTipoSangre.png" /><br />Licencias por<br />tipo de sangre</a></div>
        </div>
    </div>

    <!--<div style="float: left; width:48%;">
        <h2>Trámites</h2>
        <div>
            <a href="Tramites/EstadosTramite.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsTramite.png" /><br />Seguimiento de trámites</a>
        </div>
    </div>

    <div style="float: left; width: 100%">
        <h2>Datos de Licencia y Vehículos</h2>

    </div>

    <div class="title">
        Citaciones
    </div>
    
    <div class="full" style="height:70px">
        <div class="left" style="text-align:center">
            <a href="Citaciones/CitacionesPendientes.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsCitacPend.png" /></a>
        </div>
        <div class="right" style="text-align:center">
            <a href="Citaciones/CitacionPorCodigo.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsCitacPorCod.png" /><br />Citaciones por código</a>
        </div>
    </div>

    <div class="title">
        Trámites
    </div>
    <div class="full" style="height:70px">
        <div class="center" style="text-align:center">
            <a href="Tramites/EstadosTramite.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsTramite.png" /><br />Seguimiento de trámites</a>
        </div>
    </div>

    <div class="title">
        Datos de Licencia y Vehículos
        <div class="left" style="text-align:center">
            <a href="Licencias/DatosLicencia.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsLicenc.png" /><br />Mi licencia de conducir</a>
        </div>
        <div class="left" style="text-align:center">
            <a href="Licencias/PuntosLicencia.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsPtosLicenc.png" /><br />Puntos en mi licencia</a>
        </div>
        <div class="left" style="text-align:center">
            <a href="Vehiculos/Vehiculos.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsVehic.png" /><br />Mis vehículos</a>
        </div>
        <div class="left" style="text-align:center">
            <a href="Vehiculos/VehiculosCanchon.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsVehicRet.png" /><br />Vehículos retenidos</a>
        </div>
    </div>

    <div class="full" style="height:155px">
        <div class="left" style="text-align:center">
            <a href="Licencias/DatosLicencia.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsLicenc.png" /><br />Mi licencia de conducir</a>
        </div>
        <div class="right" style="text-align:center">
            <a href="Licencias/PuntosLicencia.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsPtosLicenc.png" /><br />Puntos en mi licencia</a>
        </div>
        <div class="left" style="text-align:center">
            <a href="Vehiculos/Vehiculos.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsVehic.png" /><br />Mis vehículos</a>
        </div>
        <div class="right" style="text-align:center">
            <a href="Vehiculos/VehiculosCanchon.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsVehicRet.png" /><br />Vehículos retenidos</a>
        </div>
    </div>

    

    <div class="title">
        Consultas Varias
    </div>
    
    <div class="full" style="height:155px">
        <div class="left" style="text-align:center">
            <a href="Varios/ExamPracticoNotas.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsExamPracCalif.png" /><br />Notas en exámenes prácticos</a>
        </div>
        <div class="right" style="text-align:center">
            <a href="javascript:popup('https://declaraciones.sri.gov.ec/mat-vehicular-internet/reportes/general/valoresAPagar.jsp','1000','580','1','sri')" onmouseover="window.status='';return true"><img src="../images/icoSRI.png" /><br />Valor a pagar por matrícula</a>
        </div>
        <div class="left" style="text-align:center">
            <a href="Vehiculos/VehiculosRobados.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsVehicRobados.png" /><br />Vehículos robados</a>
        </div>
        <div class="right" style="text-align:center">
            <a href="Licencias/LicGroupBySangre.aspx" onmouseover="window.status='';return true"><img src="../images/icoConsLicencPorTipoSangre.png" /><br />Licencias por tipo de sangre</a>
        </div>
    </div>-->

    
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="RightColumnContent">

    <div class="full">
        <h2>Transacciones en línea/banca</h2>
        <div class="left" style="text-align:center">
            <uc1:LinkTramitesTracking ID="LinkTramitesTracking" runat="server" />
        </div>
        <div class="right" style="text-align:center">
            <uc1:LinkTramitesFinalizados ID="LinkTramitesFinalizados" runat="server" />
        </div>         
        <div class="left" style="text-align:center">
            <uc1:LinkSolTramitesNoPag ID="LinkSolTramitesNoPag" runat="server" />
        </div>
        <div class="right" style="text-align:center">
            <uc1:LinkCitacionesPagadasBanca ID="LinkCitacionesPagadasBanca" runat="server" />
        </div>
    </div>
           
    <div class="full">
        <h2>Pague citaciones y trámites por Internet en</h2>
        <div class="left" style="text-align:center"><ucBancos:BcoBolivariano ID="BcoBolivariano1" runat="server" /></div>
        <div class="right" style="text-align:center"><ucBancos:BcoGuayaquil ID="BcoGuayaquil1" runat="server" /></div>
        <div class="left" style="text-align:center"><ucBancos:BcoPacifico ID="BcoPacifico1" runat="server" /></div>
        <div class="right" style="text-align:center"><ucBancos:BcoProdubanco ID="BcoProdubanco1" runat="server" /></div>
        <h6>También puede realizar los pagos en las agencias de los bancos mencionados.</h6>
    </div>

    <div class="full">
        <h2>Pague citaciones y trámites en las agencias de</h2>
        <div class="left" style="text-align:center"><ucBancos:BcoServipagos ID="BcoServipagos1" runat="server" /></div>
        <div class="right" style="text-align:center"><ucBancos:BcoWesternUnion ID="BcoWesternUnion1" runat="server" /></div>
    </div>
    
</asp:Content>


