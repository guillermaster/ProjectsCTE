<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div style="width: 30%; padding: 20px 10px; float: left;" align="center">
            <span class="img-rollover"><a href="ConsVeh.aspx">
                <img src="images/btn-consulta-vehiculos.png" alt="Consulta de vehículos" /></a>
            </span>
        </div>
        <div style="width: 30%; padding: 20px 10px; float: left;" align="center">
            <span class="img-rollover"><a href="http://secure.cte.gob.ec:8080/zkdesign/" target="_blank">
                <img src="images/btn-consulta-transpub.png" alt="Transporte Público" /></a>
            </span>
        </div>
</asp:Content>

