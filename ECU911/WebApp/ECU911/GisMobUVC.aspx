<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GisMobUVC.aspx.cs" Inherits="Consultas_GisMobUVC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAMEzhSKcWoYFZdcgJ2yb9VhQfbBedyeTcqIokiCHhEBJKH_N_NRTEMzJanHbfvVY9Llw4oYuILWGf5Q" type="text/javascript"></script>    
    <script type="text/javascript" src="js/prototype.js"></script>
    <script type="text/javascript" src="js/livegmap.js"></script>
    <style type="text/css">
        .map {
	        width: 800px;	height: 400px;	border:#666666 thin dashed;
	        padding: 0;	background: #FFFFFF;	margin-right: auto;	margin-bottom: 10px;
	        margin-left: auto;
        }
        #lastStatus {
	        font-size:12px;	font-family: verdana, arial, helvetica, sans-serif;
	        color:#333;	padding:5px;
        }
        #infoWindow {
	        width:80px; text-align: left;
        }
        #infoWindow tip {
	        float: right;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="map" class="map"></div>
</asp:Content>

