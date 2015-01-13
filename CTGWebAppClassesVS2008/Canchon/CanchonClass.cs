using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AccesoDatos;


namespace Canchon
{
    public class CanchonClass
    {
        private string _sUsuario = "";
        private string _sClave = "";
        private string _sBaseDatos = "";
        private string error = "";
        
        // Constructor de la Clase
        public CanchonClass(string sUsuario, string sClave, string sBaseDatos)
        {
            this._sUsuario = sUsuario;
            this._sClave = sClave;
            this._sBaseDatos = sBaseDatos;
        }


        // Consulta si un vehículo se encuentra en canchón
        public DataTable ConsultarVehiculo(string tipoIdentificacion, string identificacion)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this._sUsuario, this._sClave, this._sBaseDatos, "Canchon.CanchonClass.cs ConsultarVehiculo()");

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaVehiculoCanchon);
            switch (tipoIdentificacion)
            {
                case "placa":
                    oDatos.Parametro("pv_placa", identificacion.ToUpper());
                    oDatos.Parametro("pv_ramv", "");
                    oDatos.Parametro("pv_iden", "");
                    break;
                case "ramv":
                    oDatos.Parametro("pv_placa", "");
                    oDatos.Parametro("pv_ramv", identificacion.ToUpper());
                    oDatos.Parametro("pv_iden", "");
                    break;
                default:
                    oDatos.Parametro("pv_placa", "");
                    oDatos.Parametro("pv_ramv", "");
                    oDatos.Parametro("pv_iden", identificacion);
                    break;
            }
            oDatos.Parametro("C_CANCHON", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");

            DataTable dtVehiculosRet = new DataTable("vehiculosxpersona");
            dtVehiculosRet.Columns.Add("fecha_hora");
            dtVehiculosRet.Columns.Add("canchon");
            dtVehiculosRet.Columns.Add("dias");
            dtVehiculosRet.Columns.Add("placa");
            dtVehiculosRet.Columns.Add("color");
            dtVehiculosRet.Columns.Add("marca");
            dtVehiculosRet.Columns.Add("modelo");

            if (oDatos.Ejecutar("R"))
            {
                if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtVehiculosRet.NewRow();
                        dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                        dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                        dr[2] = oDatos.oDataReader.GetValue(2).ToString();
                        dr[3] = oDatos.oDataReader.GetValue(3).ToString();
                        dr[4] = oDatos.oDataReader.GetValue(4).ToString();
                        dr[5] = oDatos.oDataReader.GetValue(5).ToString();
                        dr[6] = oDatos.oDataReader.GetValue(6).ToString();
                        dtVehiculosRet.Rows.Add(dr);
                    }
                }
                else
                {
                    error = oDatos.RetornarParametro("pv_error").ToString();
                }
            }
            else
            {
                error = oDatos.Mensaje;
            }

            oDatos.Dispose();

            return dtVehiculosRet;
        }
    }
}
