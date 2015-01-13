<%@ Page Title="" Language="C#" MasterPageFile="~/OneColumnV2.master" AutoEventWireup="true" CodeFile="ExamPracticoNotas.aspx.cs" Inherits="Consultas_Varios_ExamPracticoCalif" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="divMain" runat="server">
        <div class="title">
           Notas en Exámenes Prácticos
        </div>
        <div class="full" id="divContent" runat="server">
            <asp:GridView ID="gvNotasExamenes" runat="server">
            </asp:GridView>
        </div>
    </div>
</asp:Content>

