using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;

namespace MatriculacionCNTTTSV
{
    public class VehiculoCNTTTSV
    {
        private string userDB;
        private string passwordDB;
        private string database;
        private string error;
        private string placa, chasis, camv;
        private DataTable dtDatosVehiculo;
        private DataTable dtBloqueosVehiculo;
        private DataTable dtContCompraVenta;
        private DataTable dtPropietario;


        public VehiculoCNTTTSV(string sUsuario, string sClave, string sBaseDatos,
            string placa, string chasis, string camv)
        {
            this.userDB = sUsuario;
            this.passwordDB = sClave;
            this.database = sBaseDatos;
            this.placa = placa;
            this.chasis = chasis;
            this.camv = camv;
        }

        public void InitDataTableDatosVehiculo()
        {
            dtDatosVehiculo = new DataTable("DatosVehiculo");
            #region "Add Columns"
            dtDatosVehiculo.Columns.Add("PLACA");
            dtDatosVehiculo.Columns.Add("CHASIS");
            dtDatosVehiculo.Columns.Add("ID_MARCA1");
            dtDatosVehiculo.Columns.Add("MARCA");
            dtDatosVehiculo.Columns.Add("ID_MODELO1");
            dtDatosVehiculo.Columns.Add("MODELO");
            dtDatosVehiculo.Columns.Add("ID_CLASE1");
            dtDatosVehiculo.Columns.Add("CLASE");
            dtDatosVehiculo.Columns.Add("ID_TIPO_SERVICIO1");
            dtDatosVehiculo.Columns.Add("SERVICIO");
            dtDatosVehiculo.Columns.Add("ID_COLOR_1");
            dtDatosVehiculo.Columns.Add("COLOR1");
            dtDatosVehiculo.Columns.Add("ID_COLOR_2");
            dtDatosVehiculo.Columns.Add("COLOR2");
            dtDatosVehiculo.Columns.Add("ULT_REVISION");
            dtDatosVehiculo.Columns.Add("ULT_MATRICULA");
            dtDatosVehiculo.Columns.Add("TIPO_IDENT");
            dtDatosVehiculo.Columns.Add("IDENTIFICACION");
            dtDatosVehiculo.Columns.Add("NOMBRES");
            dtDatosVehiculo.Columns.Add("DIRECCION");
            dtDatosVehiculo.Columns.Add("TELEFONO");
            dtDatosVehiculo.Columns.Add("MOTOR");
            dtDatosVehiculo.Columns.Add("PLACA_ANTERIOR");
            dtDatosVehiculo.Columns.Add("ANIO_PROD");
            dtDatosVehiculo.Columns.Add("ID_COMBUSTIBLE");
            dtDatosVehiculo.Columns.Add("ID_CARROCERIA");
            dtDatosVehiculo.Columns.Add("TONELAJE");
            dtDatosVehiculo.Columns.Add("CILINDRAJE");
            dtDatosVehiculo.Columns.Add("CAPACIDAD");
            dtDatosVehiculo.Columns.Add("COOPERATIVA_RUC");
            dtDatosVehiculo.Columns.Add("COOPERATIVA_NOM");
            dtDatosVehiculo.Columns.Add("DISCO");
            dtDatosVehiculo.Columns.Add("ID_CANTON");
            dtDatosVehiculo.Columns.Add("CANTON");
            dtDatosVehiculo.Columns.Add("ID_PROVINCIA");
            dtDatosVehiculo.Columns.Add("PROVINCIA");
            dtDatosVehiculo.Columns.Add("ID_TIPO");
            dtDatosVehiculo.Columns.Add("TIPO");            
            #endregion
        }

        public void InitDataTableBloqueosVehiculo()
        {
            dtBloqueosVehiculo = new DataTable("BloqueosVehiculo");
            #region "Add Columns"
            dtBloqueosVehiculo.Columns.Add("CDG");
            dtBloqueosVehiculo.Columns.Add("CHASIS");
            dtBloqueosVehiculo.Columns.Add("ID_TIPO_BLOQUEO");
            dtBloqueosVehiculo.Columns.Add("FECHA_REGISTRO");
            dtBloqueosVehiculo.Columns.Add("NUM_DOCUMENTO");
            dtBloqueosVehiculo.Columns.Add("FECHA_DOCUMENTO");
            dtBloqueosVehiculo.Columns.Add("ACTIVA");
            dtBloqueosVehiculo.Columns.Add("FECHA_DESACTIVA");
            dtBloqueosVehiculo.Columns.Add("NUM_DOC_DESACTIVA");
            dtBloqueosVehiculo.Columns.Add("TIPO_PERSONA");
            dtBloqueosVehiculo.Columns.Add("LUGARDESACTIVACION");
            dtBloqueosVehiculo.Columns.Add("AUTORIDAD");
            dtBloqueosVehiculo.Columns.Add("SISTEMA");
            dtBloqueosVehiculo.Columns.Add("BANDERA");
            #endregion
        }

        private void InitDataTableContratoCompraVenta()
        {
            dtContCompraVenta = new DataTable("CompraVentaVeh");
            dtContCompraVenta.Columns.Add("ident_comprador");
            dtContCompraVenta.Columns.Add("nombre_comprador");
            dtContCompraVenta.Columns.Add("ident_vendedor");
            dtContCompraVenta.Columns.Add("nombre_vendedor");
            dtContCompraVenta.Columns.Add("placa_camv_cpn");
            dtContCompraVenta.Columns.Add("fecha_contrato");
            dtContCompraVenta.Columns.Add("numero_comprobante");
            dtContCompraVenta.Columns.Add("descripcion_ifi");
        }

        private void InitDataTablePropietario()
        {
            dtPropietario = new DataTable("PropietarioVeh");
            dtPropietario.Columns.Add("identificacion");
            dtPropietario.Columns.Add("nombres");
            dtPropietario.Columns.Add("nombre_empresa");
            dtPropietario.Columns.Add("direccion");
            dtPropietario.Columns.Add("telefono");
            dtPropietario.Columns.Add("id_canton");
            dtPropietario.Columns.Add("canton");
            dtPropietario.Columns.Add("id_provincia");
            dtPropietario.Columns.Add("provincia");
        }


        public bool LoadDatosVehiculo()
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(userDB, passwordDB, database, "MatriculacionCNTTTSV.VehiculoCNTTTSV.cs LoadDatosVehiculo()");
            oDatos.Paquete("WEB_CORPAIRE_CTG.consulta_vehiculo");
            oDatos.Parametro("pv_placa", placa);
            oDatos.Parametro("pv_chasis", chasis);
            oDatos.Parametro("pv_camv", camv);
            oDatos.Parametro("c_vehiculos", "R", 0, "O");
            oDatos.Parametro("pn_error", "V", 2, "O");
            oDatos.Parametro("pv_mensaje_error", "V", 2000, "O");

            InitDataTableDatosVehiculo();
            error = string.Empty;
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (int.Parse(oDatos.RetornarParametro("pn_error").ToString()) == 0)
                    {
                        bool existsVeh = false;
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtDatosVehiculo.NewRow();
                            for (int i = 0; i < dtDatosVehiculo.Columns.Count; i++)
                            {
                                if (dtDatosVehiculo.Columns[i].ColumnName == "ULT_REVISION" || dtDatosVehiculo.Columns[i].ColumnName == "ULT_MATRICULA")
                                    dr[i] = UtilitiesCNTTTSV.Utilities.FormatearFechaCorpaire(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtDatosVehiculo.Columns[i].ColumnName)).ToString());
                                else
                                    dr[i] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtDatosVehiculo.Columns[i].ColumnName)).ToString();
                            }
                            dtDatosVehiculo.Rows.Add(dr);
                            existsVeh = true;
                        }
                        if (existsVeh)
                            retValue = true;
                        else
                            error = "Vehículo no existe o hubo error al cargar informaciónd de vehículo.";
                    }
                    else
                    {
                        error = oDatos.RetornarParametro("pv_mensaje_error").ToString();
                    }
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

            return retValue;
        }


        public bool LoadBloqueosVehiculo()
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.userDB, this.passwordDB, this.database, "MatriculacionCNTTTSV.VehiculoCNTTTSV.cs LoadDatosVehiculo()");
            oDatos.Paquete("WEB_CORPAIRE_CTG.consulta_bloqueos");
            oDatos.Parametro("pv_placa", placa);
            oDatos.Parametro("pv_chasis", chasis);
            oDatos.Parametro("pv_camv", camv);
            oDatos.Parametro("c_bloqueos", "R", 0, "O");
            oDatos.Parametro("pn_error", "V", 2, "O");
            oDatos.Parametro("pv_mensaje_error", "V", 2000, "O");

            InitDataTableBloqueosVehiculo();

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (int.Parse(oDatos.RetornarParametro("pn_error").ToString()) == 0)
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtBloqueosVehiculo.NewRow();
                            #region "Map data"
                            for (int i = 0; i < dtBloqueosVehiculo.Columns.Count; i++)
                            {
                                if (dtBloqueosVehiculo.Columns[i].ColumnName == "FECHA_REGISTRO" || dtBloqueosVehiculo.Columns[i].ColumnName == "FECHA_DOCUMENTO" || dtBloqueosVehiculo.Columns[i].ColumnName == "FECHA_DESACTIVA")
                                    dr[dtBloqueosVehiculo.Columns[i].ColumnName] = UtilitiesCNTTTSV.Utilities.FormatearFechaCorpaire(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtBloqueosVehiculo.Columns[i].ColumnName)).ToString());
                                else
                                    dr[dtBloqueosVehiculo.Columns[i].ColumnName] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtBloqueosVehiculo.Columns[i].ColumnName)).ToString();
                            }
                            #endregion
                            dtBloqueosVehiculo.Rows.Add(dr);
                            retValue = true;
                        }
                    }
                    else
                    {
                        if (int.Parse(oDatos.RetornarParametro("pn_error").ToString()) == 1)
                            retValue = true;
                        //error = "Vehículo no tiene bloqueos."; 
                        else
                            error = oDatos.RetornarParametro("pv_mensaje_error").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return retValue;
        }


        public bool LoadDataContratoCompraVenta()
        {
            bool retValue = false;
            InitDataTableContratoCompraVenta();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(userDB, passwordDB, database, "MatriculacionCNTTTSV.VehiculoCNTTTSV.cs LoadDatosVehiculo()");
            oDatos.Paquete("web_corpaire_ctg.consulta_compra_venta");
            oDatos.Parametro("pv_chasis", chasis);
            oDatos.Parametro("pv_placa_camv_cpn", placa);
            oDatos.Parametro("c_compra_venta", "R", 0, "O");
            oDatos.Parametro("pn_error", "V", 2, "O");
            oDatos.Parametro("pv_mensaje_error", "V", 2000, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (int.Parse(oDatos.RetornarParametro("pn_error").ToString()) == 0)
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtContCompraVenta.NewRow();
                            foreach (DataColumn dc in dtContCompraVenta.Columns)
                            {
                                if(dc.ColumnName=="fecha_contrato")
                                    dr[dc.ColumnName] = UtilitiesCNTTTSV.Utilities.FormatearFechaCorpaire(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dc.ColumnName)).ToString());
                                else
                                    dr[dc.ColumnName] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dc.ColumnName)).ToString();
                            }
                            dtContCompraVenta.Rows.Add(dr);
                        }
                        retValue = true;
                    }
                    else
                        error = oDatos.RetornarParametro("pv_mensaje_error").ToString();
                }
                else
                {
                    error = oDatos.Mensaje;
                }
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }
            return retValue;
        }


        public bool LoadDataPropietario()
        {
            bool retValue = false;
            InitDataTablePropietario();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(userDB, passwordDB, database, "MatriculacionCNTTTSV.VehiculoCNTTTSV.cs LoadDatosVehiculo()");
            oDatos.Paquete("web_corpaire_ctg.consulta_datos_propietario");
            oDatos.Parametro("pv_chasis", chasis);
            oDatos.Parametro("pv_placa_camv_cpn", placa);
            oDatos.Parametro("c_datos_propietario", "R", 0, "O");
            oDatos.Parametro("pn_error", "V", 2, "O");
            oDatos.Parametro("pv_mensaje_error", "V", 2000, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (int.Parse(oDatos.RetornarParametro("pn_error").ToString()) == 0)
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtPropietario.NewRow();
                            foreach (DataColumn dc in dtPropietario.Columns)
                            {
                                dr[dc.ColumnName] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dc.ColumnName)).ToString();
                            }
                            dtPropietario.Rows.Add(dr);
                        }
                        retValue = true;
                    }
                    else
                        error = oDatos.RetornarParametro("pv_mensaje_error").ToString();
                }
                else
                {
                    error = oDatos.Mensaje;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }
            return retValue;
        }


        public DataTable DatosVehiculo
        {
            get
            {
                return dtDatosVehiculo;
            }
        }

        public DataTable BloqueosVehiculo
        {
            get
            {
                return dtBloqueosVehiculo;
            }
        }

        public DataTable ContratoCompraVenta
        {
            get { return dtContCompraVenta; }
        }

        public DataTable Propietario
        {
            get { return dtPropietario; }
        }

        public string Error
        {
            get
            {
                return error;
            }
        }
    }
}
