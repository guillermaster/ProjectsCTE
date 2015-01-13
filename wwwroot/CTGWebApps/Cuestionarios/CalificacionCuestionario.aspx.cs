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
using Cuestionarios;

public partial class Cuestionarios_CalificacionCuestionario : System.Web.UI.Page
{
    protected DataTable preguntas_final;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            preguntas_final = new DataTable("ResumenCuestionario");
            preguntas_final.Columns.Add("n");
            preguntas_final.Columns.Add("cod_pregunta");
            preguntas_final.Columns.Add("desc_pregunta");
            preguntas_final.Columns.Add("calificacion");
            preguntas_final.Columns.Add("imagen");

            int calificacion = CalificarCuestionario();
            if (calificacion >= 15)
            {
                this.lblMensajeFinCuestionario.Text = "¡Felicitaciones, has obtenido una nota de examen aprobado!";
                this.lblMensaje.Text = "Usted ha contestado " + calificacion.ToString() + " preguntas correctas.";
            }
            else
            {
                this.lblMensajeFinCuestionario.Text = "Sigue estudiando, has obtenido una nota de examen reprobado";
                int n_preguntas_incorrectas = 20 - calificacion;
                this.lblMensaje.Text = "Usted se ha equivocado en " + n_preguntas_incorrectas.ToString() + " preguntas.";
            }

            this.gvCuestionario.DataSource = this.preguntas_final;
            this.gvCuestionario.DataBind();
            
        }
    }

    protected int CalificarCuestionario()
    {
        DataTable dtPreguntas = (DataTable)Session["dtPreguntas"];
        int n_respuestas_correctas = 0;

        for (int i = 0; i < dtPreguntas.Rows.Count; i++)
        {
            DataRow row_pregunta_final = this.preguntas_final.NewRow();
            row_pregunta_final[0] = i + 1;
            row_pregunta_final[1] = dtPreguntas.Rows[i]["cod_pregunta"];
            row_pregunta_final[2] = dtPreguntas.Rows[i]["desc_pregunta"];
            row_pregunta_final[4] = "imagenPregunta.aspx?cod=" + dtPreguntas.Rows[i]["cod_pregunta"].ToString();
            
            if (dtPreguntas.Rows[i]["num_respuestas"].ToString() == "1")
            {
                if (ValidaRespuesta(dtPreguntas.Rows[i]["cod_pregunta"].ToString()))
                {
                    n_respuestas_correctas++;
                    row_pregunta_final[3] = "img/good.gif";
                }
                else
                    row_pregunta_final[3] = "img/wrong.gif";
            }
            else
            {
                if (ValidarRespuestas(dtPreguntas.Rows[i]["cod_pregunta"].ToString(), Convert.ToInt32(dtPreguntas.Rows[i]["num_respuestas"].ToString())))
                {
                    n_respuestas_correctas++;
                    row_pregunta_final[3] = "img/good.gif";
                }
                else
                    row_pregunta_final[3] = "img/wrong.gif";
            }
            this.preguntas_final.Rows.Add(row_pregunta_final);
        }


        return n_respuestas_correctas;
    }



    protected bool ValidaRespuesta(string cod_pregunta)
    {
        bool correcta = false;
        string cod_respuestacorrecta = "";

        DataTable dtRespuestas = (DataTable)Session["dtRespuestas"];
        for (int i = 0; i < dtRespuestas.Rows.Count; i++)
        {
            if (dtRespuestas.Rows[i]["cod_pregunta"].ToString() == cod_pregunta)
            {
                if (dtRespuestas.Rows[i]["correcta"].ToString() == "S")
                {
                    cod_respuestacorrecta = dtRespuestas.Rows[i]["cod_respuesta"].ToString();
                    break;
                }
            }
        }

        DataTable dtRespCont = (DataTable)Session["dtRespCont"];
        for (int i = 0; i < dtRespCont.Rows.Count; i++)
        {
            if (dtRespCont.Rows[i]["cod_pregunta"].ToString() == cod_pregunta)
            {
                if (dtRespCont.Rows[i]["cod_respuesta"].ToString() == cod_respuestacorrecta)
                    correcta = true;
                break;
            }
        }

        return correcta;
    }


    protected bool ValidarRespuestas(string cod_pregunta, int num_respuestas_correctas)
    {
        bool correcta = false;
        ArrayList cod_respuestascorrectas = new ArrayList();

        DataTable dtRespuestas = (DataTable)Session["dtRespuestas"];
        for (int i = 0; i < dtRespuestas.Rows.Count; i++)
        {
            if (dtRespuestas.Rows[i]["cod_pregunta"].ToString() == cod_pregunta)
            {
                if (dtRespuestas.Rows[i]["correcta"].ToString() == "S")
                {
                    cod_respuestascorrectas.Add(dtRespuestas.Rows[i]["cod_respuesta"].ToString());
                }
                if (cod_respuestascorrectas.Count == num_respuestas_correctas)
                {
                    correcta = true;
                    break;
                }
            }
        }

        int n_resp_seleccionadas = 0;
        int n_resp_correctas = 0;
        bool done = false;
        DataTable dtRespCont = (DataTable)Session["dtRespCont"];
        for (int i = 0; i < dtRespCont.Rows.Count; i++)
        {
            if (dtRespCont.Rows[i]["cod_pregunta"].ToString() == cod_pregunta)
            {
                for (int j = 0; j < cod_respuestascorrectas.Count; j++)
                {
                    if (dtRespCont.Rows[i]["cod_respuesta"].ToString() == cod_respuestascorrectas[j].ToString())
                    {
                        n_resp_correctas++;
                        break;
                    }
                }
                n_resp_seleccionadas++;
                done = true;
            }
            else
            {
                if (done)
                    break;
            }
        }

        if ((n_resp_seleccionadas == num_respuestas_correctas) && (n_resp_correctas == num_respuestas_correctas))
            correcta = true;

        return correcta;
    }


    protected void btnVerGrid_Click(object sender, EventArgs e)
    {
        this.gvCuestionario.Visible = true;
        this.gvCuestionario.Columns[1].Visible = false;
    }


    protected void gvCuestionario_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.GrdInfrac.SelectedItem.Cells[5].Text.ToString();
        string cod_selectedquestion = this.gvCuestionario.SelectedRow.Cells[1].Text;
        Cuestionario cuest = new Cuestionario(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtPosiblesRespuestas = cuest.ObtenerRespuestas(cod_selectedquestion);

        DataTable dtSolucion = new DataTable();
        dtSolucion.Columns.Add("seleccion");
        dtSolucion.Columns.Add("solucion");
        dtSolucion.Columns.Add("respuesta");

        DataTable dtRespCont = (DataTable)Session["dtRespCont"];

        for (int i = 0; i < dtPosiblesRespuestas.Rows.Count; i++)
        {
            DataRow row_solucion = dtSolucion.NewRow();
            row_solucion[2] = dtPosiblesRespuestas.Rows[i]["DESC_RESPUESTA"];
            if (dtPosiblesRespuestas.Rows[i]["RESPUESTA_CORRECTA"].ToString() == "S")
            {
                row_solucion[1] = "img/good.gif";
            }
            else
            {
                row_solucion[1] = "img/blank.gif";
            }

            for (int j = 0; j < dtRespCont.Rows.Count; j++)
            {
                if ((dtRespCont.Rows[j]["cod_pregunta"].ToString()==cod_selectedquestion) && (dtRespCont.Rows[j]["cod_respuesta"].ToString() == dtPosiblesRespuestas.Rows[i]["COD_RESPUESTA"].ToString()))
                {
                    if (dtPosiblesRespuestas.Rows[i]["RESPUESTA_CORRECTA"].ToString() == "S")
                    {
                        row_solucion[0] = "img/good.gif";
                    }
                    else
                    {
                        row_solucion[0] = "img/wrong.gif";
                    }
                }
            }
            dtSolucion.Rows.Add(row_solucion);
        }

        int selectedindex = this.gvCuestionario.SelectedIndex;

        for (int i = 0; i < this.gvCuestionario.Rows.Count; i++)
        {
            if (i != selectedindex)
                this.gvCuestionario.Rows[i].Visible = false;
        }

        this.gvSolucion.DataSource = dtSolucion;
        this.gvSolucion.DataBind();
        this.gvSolucion.Visible = true;
        this.ImageButton1.Visible = true;
    }


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < this.gvCuestionario.Rows.Count; i++)
        {
                this.gvCuestionario.Rows[i].Visible = true;
        }
        this.gvSolucion.Dispose();
        this.gvSolucion.Visible = false;
        this.ImageButton1.Visible = false;
    }
    protected void btnNuevoExamen_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("index.aspx");
    }
}
