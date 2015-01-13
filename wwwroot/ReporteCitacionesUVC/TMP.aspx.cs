using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class TMP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ArrayList fechas = new ArrayList();
        fechas.Add("01/01/2012");
        fechas.Add("02/01/2012");

        DataTable dt = new DataTable("Delegación 1");
        dt.Columns.Add("fecha");
        dt.Columns.Add("patrulla");
        dt.Columns.Add("total");

        #region "add values"
        DataRow dr = dt.NewRow();
        dr[0] = "01/01/2012";
        dr[1] = "310";
        dr[2] = 3;
        dt.Rows.Add(dr);

        DataRow dr1 = dt.NewRow();
        dr1[0] = "01/01/2012";
        dr1[1] = "311";
        dr1[2] = 0;
        dt.Rows.Add(dr1);

        DataRow dr2 = dt.NewRow();
        dr2[0] = "01/01/2012";
        dr2[1] = "312";
        dr2[2] = 5;
        dt.Rows.Add(dr2);

        DataRow dr3 = dt.NewRow();
        dr3[0] = "02/01/2012";
        dr3[1] = "310";
        dr3[2] = 8;
        dt.Rows.Add(dr3);

        DataRow dr4 = dt.NewRow();
        dr4[0] = "02/01/2012";
        dr4[1] = "311";
        dr4[2] = 2;
        dt.Rows.Add(dr4);

        DataRow dr5 = dt.NewRow();
        dr5[0] = "02/01/2012";
        dr5[1] = "312";
        dr5[2] = 6;
        dt.Rows.Add(dr5);
        #endregion

        GridView1.DataSource = dt;
        GridView1.DataBind();

        ArrayList listPatrullas = GetPatrullas(dt);
        DataTable dt2 = new DataTable("Din");
        dt2.Columns.Add("Fecha");
        foreach (string patrulla in listPatrullas)
        {
            dt2.Columns.Add(patrulla);
        }

        foreach (string fecha in fechas)
        {
            DataRow dr_final = dt2.NewRow();
            dr_final["Fecha"] = fecha;
            foreach (string patrulla in listPatrullas)
            {
                DataRow[] filterRows = dt.Select("fecha = '" + fecha  + "' AND patrulla = " + patrulla);
                foreach (DataRow filtRow in filterRows)
                {
                    /*DataRow dr_dt2 = dt2.NewRow();
                    dr_dt2["Fecha"] = filtRow["fecha"];
                    dr_dt2[patrulla] = filtRow["total"];
                    dt2.Rows.Add(dr_dt2);*/
                    dr_final[patrulla] = filtRow["total"];
                }
            }
            dt2.Rows.Add(dr_final);
        }
        GridView2.DataSource = dt2;
        GridView2.DataBind();
    }

    private ArrayList GetPatrullas(DataTable dt)
    {
        ArrayList list = new ArrayList();
        foreach (DataRow dr in dt.Rows)
        {
            if(list.Count==0)
                list.Add(dr["patrulla"].ToString());
            else
            {
                bool existe = false;
                foreach(string li in list)
                {
                    if (li == dr["patrulla"].ToString())
                        existe = true;
                }
                if(!existe)
                    list.Add(dr["patrulla"].ToString());
            }
        }
        return list;
    }
}
