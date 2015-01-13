using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/*using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;*/

public partial class Consultas_Tramites_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        /*ReporteTramite.ATW_CD reporte = new ReporteTramite.ATW_CD();

        DataSet dsSource = new DataSet();

        DataTable dt0 = new DataTable("DataTableReporte");
        dt0.Columns.Add("nombreTramite");
        dt0.Columns.Add("nombreTo");
        DataRow dr0 = dt0.NewRow();
        dr0[0] = "Corrección de Datos";
        dr0[1] = "SECRETARIO GENERAL DE LA CTE";
        dt0.Rows.Add(dr0);

        dsSource.Tables.Add(dt0);

        DataTable dt1 = new DataTable("dtCommonFields");
        dt1.Columns.Add("nombreTramite");
        dt1.Columns.Add("nombreTo");
        dt1.Columns.Add("nombreFrom");
        dt1.Columns.Add("identFrom");
        dt1.Columns.Add("frase1");
        dt1.Columns.Add("frase2");
        dt1.Columns.Add("datoOpcional");
        dt1.Columns.Add("codigoBarras", typeof(byte[]));
        dt1.Columns.Add("codigoTramite");
        DataRow dr1 = dt1.NewRow();
        dr1[0] = "CORRECCIÓN DE DATOS";
        dr1[1] = "SECRETARIO GENERAL DE LA CTE";
        dr1[2] = "LLANOS HALL MARIUXI ELIZABETH";
        dr1[3] = "09020131018";
        dr1[4] = "por error en ingreso:";
        dr1[5] = "";
        dr1[6] = "";//placa u otro dato
        dr1[7] = CodeBar("999456");
        dr1[8] = "999456";
        dt1.Rows.Add(dr1);

        dsSource.Tables.Add(dt1);

        DataTable dt2 = new DataTable("header");
        dt2.Columns.Add("fecha");
        dt2.Columns.Add("ciudad");
        dt2.Columns.Add("numTramite");
        DataRow dr2 = dt2.NewRow();
        dr2[0] = "06 de AGOSTO del 2010";
        dr2[1] = "GUAYAQUIL";
        dr2[2] = "000251864";
        dt2.Rows.Add(dr2);

        dsSource.Tables.Add(dt2);

        DataTable dt3 = new DataTable("leftColumn");
        dt3.Columns.Add("firmaTesorero");
        dt3.Columns.Add("valor");
        dt3.Columns.Add("valorTxt");
        DataRow dr3 = dt3.NewRow();
        dr3[0] = "ssds";
        dr3[1] = "4.50";
        dr3[2] = "CUATRO DÓLARES CINCUENTA CENTAVOS";
        dt3.Rows.Add(dr3);

        dsSource.Tables.Add(dt3);

        DataTable dt4 = new DataTable("footer");
        dt4.Columns.Add("usuario");
        dt4.Columns.Add("fechaEmision");
        dt4.Columns.Add("ubicacion");
        dt4.Columns.Add("mensajeOpcional");
        DataRow dr4 = dt4.NewRow();
        dr4[0] = "AXISCTG";
        dr4[1] = "06/AGOSTO/2010";
        dr4[2] = "CENTRO";
        dr4[3] = "CERTIFICADO VALIDO POR 30 DIAS";
        dt4.Rows.Add(dr4);

        dsSource.Tables.Add(dt4);

        DataTable dt5 = new DataTable("datosAdicionales");
        dt5.Columns.Add("label");
        dt5.Columns.Add("data");
        DataRow dr51 = dt5.NewRow();
        dr51[0] = "DESCRIPCIÓN:";
        dr51[1] = "CAMBIO DE TONELAJE";
        dt5.Rows.Add(dr51);
        DataRow dr52 = dt5.NewRow();
        dr52[0] = "PLACA:";
        dr52[1] = "GMM0216";
        dt5.Rows.Add(dr52);
        DataRow dr53 = dt5.NewRow();
        dr53[0] = "DICE:";
        dr53[1] = "0.75";
        dt5.Rows.Add(dr53);
        DataRow dr54 = dt5.NewRow();
        dr54[0] = "DEBE DECIR:";
        dr54[1] = "0.75";
        dt5.Rows.Add(dr54);

        dsSource.Tables.Add(dt5);

        reporte.SetDataSource(dsSource);
        CrystalReportViewer1.ReportSource = reporte;

        System.IO.MemoryStream rptStream = new System.IO.MemoryStream();
        rptStream = reporte.ExportToStream(ExportFormatType.PortableDocFormat) as System.IO.MemoryStream;
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(rptStream.ToArray());
        Response.End();*/
    }

    public byte[] CodeBar(string code)
    {
        //Response.ContentType = "image/gif";
        //Response.Clear();

        System.Drawing.Bitmap bmp = Utilities.Utils.CreateBarcode(code);
        //bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        //Response.End();
        //bmp.Dispose();
        System.IO.MemoryStream stream = new System.IO.MemoryStream();
        bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);


        return stream.ToArray();
    }
}