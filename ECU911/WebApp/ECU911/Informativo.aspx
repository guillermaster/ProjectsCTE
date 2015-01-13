<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Informativo.aspx.cs" Inherits="Informativo" %>

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
    <h1>Informativo</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center; width: 100%; margin:0">
        <div style="padding: 0px 0px 10px 120px; float: left; width: 55%">
            <div class="gridAnuncios" style="width: 100%">
                <asp:GridView ID="gvNoticias" SkinID="gvPlain" runat="server" AutoGenerateColumns="False"
                    ShowHeader="false">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="url" DataTextField="title" Target="_blank" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div style="width: 40%; padding: 20px 0px 30px 20px; float: left;" align="center">
            <span class="img-rollover"><a href="OrdenCuerpo.aspx" target="_blank">
                <img src="images/btn-consulta-ordencuerpo.png" alt="Orden del cuerpo" /></a>
            </span>
        </div>
        <div style="width: 40%; padding: 20px 0px 30px 20px; float: left;" align="center">
            <span class="img-rollover"><a href="https://mail.cte.gob.ec/exchange" target="_blank">
                <img src="images/btn-consulta-emails.png" alt="E-mail" /></a> </span>
        </div>
        
    </div>
</asp:Content>

