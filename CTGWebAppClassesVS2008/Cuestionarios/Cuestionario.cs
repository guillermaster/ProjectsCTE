using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AccesoDatos;

namespace Cuestionarios
{
    public class Cuestionario
    {
        private string user;
        private string password;
        private string database;
        private string error;

        public Cuestionario(string sUsuario, string sClave, string sBaseDatos)
        {
            this.user = sUsuario;
            this.password = sClave;
            this.database = sBaseDatos;
        }

        /* Retorna 20 preguntas que conformaran el cuestionario */
        public DataTable ObtenerPreguntas()
        {
            DataTable oPreguntas = new DataTable("Preguntas");
            oPreguntas.Columns.Add("COD_PREGUNTA");
            oPreguntas.Columns.Add("COD_CATEGORIA");
            oPreguntas.Columns.Add("DESC_PREGUNTA");
            //oPreguntas.Columns.Add("IMAGEN");
            oPreguntas.Columns.Add("NUMERO_RESPUESTAS");
            //oPreguntas.Columns.Add("DESC_PREGUNTA_ING");

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Cuestionarios.Cuestionario.cs ObtenerPreguntas()");
            oDatos.Paquete("web_api_examen.p_genera_aleatorio");
            oDatos.Parametro("c_preguntas_examen", "R", 0, "O");
            oDatos.Parametro("pv_codigoerror", "V", 1000, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    #region Leer datos de cursor
                    while (oDatos.oDataReader.Read())//recorrer cursor
                    {
                        DataRow newrow = oPreguntas.NewRow();
                        try
                        {
                            newrow[0] = oDatos.oDataReader.GetValue(0).ToString();//COD_PREGUNTA
                        }
                        catch (Exception ex) { newrow[0] = ""; }
                        try
                        {
                            newrow[1] = oDatos.oDataReader.GetValue(1).ToString();//COD_CATEGORIA
                        }
                        catch (Exception ex) { newrow[1] = ""; }
                        try
                        {
                            newrow[2] = oDatos.oDataReader.GetValue(2).ToString();//DESC_PREGUNTA
                        }
                        catch (Exception ex) { newrow[2] = ""; }
                        /*try
                        {
                            newrow[3] = oDatos.oDataReader[3];//IMAGEN
                        }
                        catch (Exception ex) { newrow[3] = ""; }*/
                        try
                        {
                            newrow[3] = oDatos.oDataReader.GetValue(6).ToString();//NUMERO_RESPUESTAS
                        }
                        catch (Exception ex) { newrow[3] = ""; }
                        /*try
                        {
                            newrow[5] = oDatos.oDataReader.GetString(4);//DESC_PREGUNTA_ING
                        }
                        catch (Exception ex) { newrow[11] = ""; }*/
                        oPreguntas.Rows.Add(newrow);
                    }
                    #endregion
                }
                else
                    error = oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return oPreguntas;
        }

        /* Retorna respuestas para una determinada pregunta */
        public DataTable ObtenerRespuestas(string cod_pregunta)
        {
            DataTable oRespuestas = new DataTable("Respuestas");
            oRespuestas.Columns.Add("COD_RESPUESTA");
            oRespuestas.Columns.Add("DESC_RESPUESTA");
            oRespuestas.Columns.Add("RESPUESTA_CORRECTA");
            oRespuestas.Columns.Add("DESC_RESPUESTA_ING");

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Cuestionarios.Cuestionario.cs ObtenerRespuestas()");
            oDatos.Paquete("WEB_API_EXAMEN.P_retorna_respuesta");
            oDatos.Parametro("pn_cod_pregunta", cod_pregunta);
            oDatos.Parametro("c_respuestas", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");
            
            if (oDatos.Ejecutar("R"))
            {
                #region Leer datos de cursor
                while (oDatos.oDataReader.Read())//recorrer cursor
                {
                    DataRow newrow = oRespuestas.NewRow();
                    try
                    {
                        newrow[0] = oDatos.oDataReader.GetValue(0);//COD_RESPUESTA
                    }
                    catch (Exception ex) { newrow[0] = ""; }
                    try
                    {
                        newrow[1] = oDatos.oDataReader.GetValue(1);//DESC_RESPUESTA
                    }
                    catch (Exception ex) { newrow[1] = ""; }
                    try
                    {
                        newrow[2] = oDatos.oDataReader.GetValue(2);//RESPUESTA_CORRECTA
                    }
                    catch (Exception ex) { newrow[2] = ""; }
                    try
                    {
                        newrow[3] = oDatos.oDataReader.GetValue(3);//DESC_PREGUNTA_ING
                    }
                    catch (Exception ex) { newrow[3] = ""; }
                    oRespuestas.Rows.Add(newrow);
                }
                #endregion
            }

            oDatos.Dispose();

            return oRespuestas;
        }
    }
}
