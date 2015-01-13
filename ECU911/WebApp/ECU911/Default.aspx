<%@ Page Language="C#" MasterPageFile="~/LoginMP.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        span.img-rollover
        {
            width: 170px;
            height: 98px;
            overflow: hidden;
            display: block;
            position: relative;
        }
        span.img-rollover a:hover
        {
            top: -98px;
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <!--<h1>
        Consulta de Datos de Vehículos</h1>-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center; width: 97%; height: 200px; background: #616161; margin: -20px 0 0 20px; padding: 10px 0;">
        <h1 style="color:#efefef">Consulta de Datos</h1>
        <div style="width: 30%; padding: 20px 0px 30px 20px; float: left;" align="center">
            <span class="img-rollover"><a href="ConsLicencias.aspx">
                <img src="images/btn-consulta-licencias.png" alt="Consulta de licencias" /></a>
            </span>
        </div>
        <div style="width: 30%; padding: 20px 10px; float: left;" align="center">
            <span class="img-rollover"><a href="ConsVehiculo.aspx">
                <img src="images/btn-consulta-vehiculos.png" alt="Consulta de vehículos" /></a>
            </span>
        </div>
        <div style="width: 30%; padding: 20px 0px 30px 20px; float: left;" align="center">
            <span class="img-rollover"><a href="ConsCitaciones.aspx">
                <img src="images/btn-consulta-citaciones.png" alt="Consulta de citaciones" /></a>
            </span>
        </div>
    </div>


    <div style="text-align: center; width: 97%; height: 200px; background: #eff1f2; margin: -20px 0 -20px 20px; padding: 10px 0;">
        <h1 style="color:#616161">Informativo</h1>
        <div style="padding: 20px 0px 30px 20px; float: left; margin-right: 20px">
            <div class="gridAnuncios" style="width: 100%">
                <asp:GridView ID="gvNoticias" SkinID="gvPlain" runat="server" AutoGenerateColumns="False"
                    ShowHeader="false">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="url" DataTextField="title" Target="_blank" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div style="width: 25%; padding: 20px 0px 30px 20px; float: left;" align="center">
            <span class="img-rollover"><a href="OrdenCuerpo.aspx" target="_blank">
                <img src="images/btn-consulta-ordencuerpo.png" alt="Orden del cuerpo" /></a>
            </span>
        </div>
        <div style="width: 25%; padding: 20px 0px 30px 20px; float: left;" align="center">
            <span class="img-rollover"><a href="https://mail.cte.gob.ec/exchange" target="_blank">
                <img src="images/btn-consulta-emails.png" alt="E-mail" /></a> </span>
        </div>
        
    </div>
</asp:Content>
