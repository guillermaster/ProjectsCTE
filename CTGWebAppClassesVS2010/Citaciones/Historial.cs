using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Citaciones
{
    public class Historial
    {
        private string _app;
        private string _dbUser;
        private string _dbPassword;
        private string _dbServer;
        private string _error;


        public Historial(string dbUser, string dbPassword, string dbServer)
        {
            _dbUser = dbUser;
            _dbPassword = dbPassword;
            _dbServer = dbServer;
        }

        public Historial(string dbUser, string dbPassword, string dbServer, string app)
        {
            _dbUser = dbUser;
            _dbPassword = dbPassword;
            _dbServer = dbServer;
            _app = app;
        }


        public DataTable CitacionesPorIdentificacion(string numLicencia)
        {
            DataTable dtResCitaciones = new DataTable("Historia de Citaciones");
            #region "Add columns"
            dtResCitaciones.Columns.Add("Número de citación");
            dtResCitaciones.Columns.Add("Fecha de citación");
            dtResCitaciones.Columns.Add("Contravención");
            dtResCitaciones.Columns.Add("Artículo");
            dtResCitaciones.Columns.Add("Nombre");
            dtResCitaciones.Columns.Add("Identificación");
            dtResCitaciones.Columns.Add("Placa Vehic.");
            dtResCitaciones.Columns.Add("Nombre de Uniformado");
            dtResCitaciones.Columns.Add("Código de Uniformado");
            dtResCitaciones.Columns.Add("Valor ($)");
            dtResCitaciones.Columns.Add("Está pagada");
            dtResCitaciones.Columns.Add("Puntos reducidos");
            dtResCitaciones.Columns.Add("Reincidencias");
            #endregion
            

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, _app + "Citaciones.Historial.CitacionesPorIdentificacion(string)");

            oDatos.Paquete("web_inter_infracciones_1.Infracciones_x_Ident_jueces");
            oDatos.Parametro("pv_identificacion", numLicencia);
            oDatos.Parametro("pv_pagada", "S");
            oDatos.Parametro("C_INFRAC", "R", 0, "O");
            
            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtResCitaciones.NewRow();
                        #region "Get Values"
                        //dr["seleccion"] = false;
                        //dr["factura"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("factura")).ToString();
                        dr[0] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("num_infraccion")).ToString();
                        dr[1] = DateTime.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("fec_infraccion")).ToString(), new System.Globalization.CultureInfo("es-ES", false));
                        dr[2] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("contravencion")).ToString();
                        dr[3] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("articulo")).ToString();
                        dr[4] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("nombres")).ToString();
                        dr[5] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("identificacion")).ToString();
                        dr[6] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("placa")).ToString();
                        dr[7] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("uniformado")).ToString();
                        dr[8] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("id_persona_vig")).ToString();
                        dr[9] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("val_contrav")).ToString();
                        dr[10] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pagada")).ToString();
                        dr[11] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("puntos")).ToString();
                        dr[12] = int.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("reincidencias")).ToString()) - 1;
                        #endregion
                        dtResCitaciones.Rows.Add(dr);
                    }
                }
                else
                    _error = "Error al consultar citaciones - " + oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtResCitaciones;
        }


    }
}

