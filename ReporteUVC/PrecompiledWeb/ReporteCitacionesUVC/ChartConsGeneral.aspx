<%@ page language="C#" autoeventwireup="true" inherits="charts_ChartConsPlacas, App_Web_chartconsgeneral.aspx.cdcab7d2" theme="SkinFile" %>

<html>
<head id="Head1" runat="server">
    <title>CTE - Reportes UVC</title>
    <link rel="stylesheet" href="css/StyleSheet.css" />
    <style type="text/css">

body {
  background: #fff;
  color: #333;
  font-family: "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif;
  font-size: 0.9em;
  padding: 40px;
}

img { border: 0; }

.wideBox {
  clear: both;
  text-align: center;
  margin-bottom: 50px;
  padding: 10px;
  background: #ebedf2;
  border: 1px solid #333;
  line-height: 80%;
}

#container {
  width: 1014px;
  margin: 0 auto;
}

#chart, .chartData {
  border: 1px solid #333;
  background: #ebedf2 url("images/gradient.png") repeat-x 0 0;
}

#chart {
  display: block;
  margin: 0 0 50px 0;
  float: left;
  cursor: pointer;
}

.chartData {
  width: 200px;
  margin: 0 40px 0 0;
  float: right;
  border-collapse: collapse;
  box-shadow: 0 0 1em rgba(0, 0, 0, 0.5);
  -moz-box-shadow: 0 0 1em rgba(0, 0, 0, 0.5);
  -webkit-box-shadow: 0 0 1em rgba(0, 0, 0, 0.5);
  background-position: 0 -100px;
}

.chartData th, .chartData td {
  padding: 0.5em;
  border: 1px dotted #666;
  text-align: left;
}

.chartData th {
  border-bottom: 2px solid #333;
  text-transform: uppercase;
}

.chartData td {
  cursor: pointer;
  font-size: 9px;
}

.chartData td.highlight {
  background: #3a3b3d;
}

.chartData tr:hover td {
  background: #f0f0f0;
}

</style>
    <!--[if IE]><script type="text/javascript" src="js/excanvas.js"></script><![endif]-->

    <script type="text/javascript" src="js/jquery.min.js"></script>

    <script type="text/javascript" src="js/piechart.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; float: left;">
            <div style="text-align:center"><h2>Consultas Generales</h2></div>
            <div id="aux">
            </div>
            <canvas id="chart" width="600" height="500"></canvas>
            <asp:GridView ID="gvChartData" CssClass="chartData" OnRowDataBound="GridView_RowDataBound"
                runat="server" ShowFooter="true" ShowHeader="true" AutoGenerateColumns="false">
               <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Delegación
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("Delegación").ToString() %>
                    </ItemTemplate>
                    <FooterTemplate>
                        TOTAL:
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        No.Cons.
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("Cons").ToString()%>
                    </ItemTemplate>
                    <FooterTemplate>
                        <%# TotalCitaciones%>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        %
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("%").ToString() %>
                    </ItemTemplate>
                    <FooterTemplate>
                        100
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        No.UVC
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("Veh").ToString()%>
                    </ItemTemplate>
                    <FooterTemplate>
                        <%# TotalUVC%>
                    </FooterTemplate>
                </asp:TemplateField>
               </Columns>
            </asp:GridView>
            <asp:TextBox ID="hdnTotRegistros" style="display: none;" CssClass="numTotReg" runat="server" />
        </div>
    </form>
</body>
</html>