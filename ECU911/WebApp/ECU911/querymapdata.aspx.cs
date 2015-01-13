using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Consultas_querymapdata : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        wsUVC.Service objWsUVC = new wsUVC.Service();
        DataTable dtDelegac = objWsUVC.GetUVCsLocation();
        ArrayList list_delegac = new ArrayList();
        int i = 0;

        foreach (DataRow dr in dtDelegac.Rows)
        {
            i++;
            string longitud = dr[3].ToString();
            string pre_long;
            pre_long = (longitud[0] == 'S') ? "-" : "";
            longitud = pre_long + longitud.Substring(1, longitud.Length - 1);

            string latitud = dr[4].ToString();
            string pre_lat;
            pre_lat = (latitud[0] == 'W') ? "-" : "";
            latitud = pre_lat + latitud.Substring(1, latitud.Length - 1);

            object[] delegacion = { i, longitud, latitud, dr[0] + " - " + dr[1], "(" + dr[2] + ")" };
            list_delegac.Add(delegacion);
        }

        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string json_string = serializer.Serialize(list_delegac.ToArray());
        Response.Write(json_string);

    }

    /*protected DataTable GetData()
    {
        DataTable dtDeleg = new DataTable("DELEGACIONES");
        dtDeleg.Columns.Add("nombre");
        dtDeleg.Columns.Add("longitud");
        dtDeleg.Columns.Add("latitud");

        
        DataRow dr1 = dtDeleg.NewRow();
        dr1[0] = "delegacion 1";
        dr1[1] = "-2.202239";
        dr1[2] = "-79.929979";
        dtDeleg.Rows.Add(dr1);

        DataRow dr2 = dtDeleg.NewRow();
        dr2[0] = "delegacion 2";
        dr2[1] = "-2.201637";
        dr2[2] = "-79.930687";
        dtDeleg.Rows.Add(dr2);

        DataRow dr3 = dtDeleg.NewRow();
        dr3[0] = "delegacion 3";
        dr3[1] = "-2.201637";
        dr3[2] = "-79.931352";
        dtDeleg.Rows.Add(dr3);
        
        return dtDeleg;
    }*/
}