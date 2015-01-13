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

public partial class Cuestionarios_Cuestionario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Cuestionario cuest = new Cuestionario(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            DataTable tblPreguntas = cuest.ObtenerPreguntas();
            if (tblPreguntas.Rows.Count > 0)
            {
                Session["preguntasCuestionario"] = tblPreguntas;
                Session["noPregunta"] = 1;
                DataTable dtPreguntas = new DataTable("Preguntas");
                dtPreguntas.Columns.Add("cod_pregunta");
                dtPreguntas.Columns.Add("desc_pregunta");
                dtPreguntas.Columns.Add("num_respuestas");
                Session["dtPreguntas"] = dtPreguntas;
                PrintQuestion();
                DataTable dtRespuestas = new DataTable("Respuestas");
                dtRespuestas.Columns.Add("cod_pregunta");
                dtRespuestas.Columns.Add("cod_respuesta");
                dtRespuestas.Columns.Add("des_respuesta");
                dtRespuestas.Columns.Add("correcta");
                Session["dtRespuestas"] = dtRespuestas;
                PrintPossibleAnswers();
                validaBotonesNavegacionExamen();
                DataTable dtRespCont;
                dtRespCont = new DataTable("RespuestasContestadas");
                dtRespCont.Columns.Add("cod_pregunta");
                dtRespCont.Columns.Add("cod_respuesta");
                Session["dtRespCont"] = dtRespCont;
            }
            else
            {
                this.divOK.Visible = false;
                this.divError.Visible = true;
                this.lblMsgError.Text = "No se pudo cargar el examen teórico.";
                this.hypRetryLoadExam.Visible = true;
            }
        }
        
    }

    void PrintQuestion()
    {
        int noPregunta = Convert.ToInt32(Session["noPregunta"]);
        DataTable tblPreguntas = (DataTable) Session["preguntasCuestionario"];
        this.lblNoPregunta.Text = "Pregunta No. " + noPregunta.ToString();

        DataRow rowCurrentQuestion = tblPreguntas.Rows[noPregunta - 1];
        //Session["imagenPregunta"] = rowCurrentQuestion[3];
        Session["codPregunta"] = rowCurrentQuestion[0].ToString();
        this.imgFoto.ImageUrl = "imagenPregunta.aspx?cod=" + rowCurrentQuestion[0].ToString();
        //Response.BinaryWrite((byte[])rowCurrentQuestion[3]);
        //this.hdnCodPregunta.Value = rowCurrentQuestion[0].ToString();
        this.hdnNoRespuestas.Value = rowCurrentQuestion[3].ToString();
        this.lblPregunta.Text = rowCurrentQuestion[2].ToString();

        DataTable dtPreguntas = (DataTable)Session["dtPreguntas"];
        DataRow row_pregunta = dtPreguntas.NewRow();
        row_pregunta[0] = rowCurrentQuestion[0].ToString();
        row_pregunta[1] = rowCurrentQuestion[2].ToString();
        row_pregunta[2] = rowCurrentQuestion[3].ToString();
        dtPreguntas.Rows.Add(row_pregunta);
        Session["dtPreguntas"] = dtPreguntas;
        
    }

    void PrintPossibleAnswers()
    {
        Cuestionario cuest = new Cuestionario(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable tblPosiblesRespuestas = cuest.ObtenerRespuestas(Session["codPregunta"].ToString());

        DataTable dtRespuestas = (DataTable)Session["dtRespuestas"];
        for (int i = 0; i < tblPosiblesRespuestas.Rows.Count; i++)
        {
            DataRow row = dtRespuestas.NewRow();
            row[0] = Session["codPregunta"];
            row[1] = tblPosiblesRespuestas.Rows[i]["COD_RESPUESTA"];
            row[2] = tblPosiblesRespuestas.Rows[i]["DESC_RESPUESTA"];
            row[3] = tblPosiblesRespuestas.Rows[i]["RESPUESTA_CORRECTA"];
            dtRespuestas.Rows.Add(row);
        }
        Session["dtRespuestas"] = dtRespuestas;

        int n_resp = Convert.ToInt32(this.hdnNoRespuestas.Value);

        if (n_resp > 1)//checkbox
        {
            this.gvCheckboxAnswers.DataSource = tblPosiblesRespuestas;
            this.gvCheckboxAnswers.DataBind();
            this.gvCheckboxAnswers.Visible = true;
            this.gvCheckboxAnswers.Columns[0].Visible = false;
        }
        else
        {
            this.gvRadioAnswers.DataSource = tblPosiblesRespuestas;
            this.gvRadioAnswers.DataBind();
            this.gvRadioAnswers.Visible = true;
        }
    }

    protected void btnSiguiente_Click(object sender, EventArgs e)
    {
        ArrayList selectedValues = new ArrayList();
        string selectedValue = null;
        bool continuar = false;

        if (this.hdnNoRespuestas.Value == "1")
        {
            selectedValue = Request.Form["radbtnResp"];
            if (selectedValue != null)
                continuar = true;
        }
        else
        {
            // Select the checkboxes from the GridView control
            for (int i = 0; i < gvCheckboxAnswers.Rows.Count; i++)
            {
                GridViewRow row = gvCheckboxAnswers.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkResp")).Checked;

                if (isChecked)
                {
                    // Column 2 is the name column
                    selectedValues.Add(gvCheckboxAnswers.Rows[i].Cells[0].Text);
                    continuar = true;
                }
            }
        }
        
        if (continuar)  //se selecciono una respuesta
        {
            if (this.hdnNoRespuestas.Value == "1")//si solo se debe seleccinar una respuesta
            {
                DataTable dtRespCont = (DataTable)Session["dtRespCont"];
                DataRow answer_row = dtRespCont.NewRow();
                answer_row[0] = Session["codPregunta"].ToString();
                answer_row[1] = selectedValue;
                dtRespCont.Rows.Add(answer_row);
                Session["dtRespCont"] = dtRespCont;
            }
            else//si se puede seleccionar mas de una respuesta
            {
                DataTable dtRespCont = (DataTable)Session["dtRespCont"];
                for (int i = 0; i < selectedValues.Count; i++)
                {
                    DataRow answer_row = dtRespCont.NewRow();
                    answer_row[0] = Session["codPregunta"].ToString();
                    answer_row[1] = selectedValues[i].ToString();
                    dtRespCont.Rows.Add(answer_row);
                }
                Session["dtRespCont"] = dtRespCont;
            }
            #region Pasar a la sgte pregunta
            if (Session["noPregunta"].ToString() == "20")
            {
                Response.Redirect("CalificacionCuestionario.aspx");
            }
            else
            {
                this.gvCheckboxAnswers.Visible = false;
                this.gvRadioAnswers.Visible = false;
                Session["noPregunta"] = Convert.ToInt32(Session["noPregunta"]) + 1;
                PrintQuestion();
                PrintPossibleAnswers();
                validaBotonesNavegacionExamen();
            }
            #endregion
        }
    }
    protected void btnAnterior_Click(object sender, EventArgs e)
    {
        this.gvCheckboxAnswers.Visible = false;
        this.gvRadioAnswers.Visible = false;
        Session["noPregunta"] = Convert.ToInt32(Session["noPregunta"]) - 1;
        PrintQuestion();
        PrintPossibleAnswers();
        validaBotonesNavegacionExamen();
    }


    protected void validaBotonesNavegacionExamen()
    {
        int noPregunta = Convert.ToInt32(Session["noPregunta"]);
        if (noPregunta >= 20)
        {
            if (noPregunta == 20)
                this.btnSiguiente.Text = "Finalizar";
            else
                this.btnSiguiente.Visible = false;
        }
        else
        {
            this.btnSiguiente.Visible = true;
            //if (noPregunta <= 1)
            //    this.btnAnterior.Visible = false;
            //else
            //    this.btnAnterior.Visible = true;
        }
    }
}
