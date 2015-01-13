using System;
using System.Data;

namespace Matriculacion
{
    public class VehiculosRobados
    {
        private string user;
        private string password;
        private string database;
        private string _error;
        private DataTable _dtVehicRobadosRecientes;

        public VehiculosRobados(string sUsuario, string sClave, string sBaseDatos)
        {
            user = sUsuario;
            password = sClave;
            database = sBaseDatos;
        }


        public bool LoadVehiculosRobadosRecientes(string desde, string hasta)
        {
            bool exito = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Matriculacion VehiculosRobados.LoadVehiculosRobadosRecientes()");
            oDatos.Paquete("GCK_CONSULTA_VEH_ROBADOS.GCP_Datos_Vehiculo");
            oDatos.Parametro("pv_fecha_desde", desde);
            oDatos.Parametro("pv_fecha_hasta", hasta);
            oDatos.Parametro("c_datos_vehiculo", "R", 0, "O");
            oDatos.Parametro("pv_mensaje", "V", 400, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_mensaje").ToString() == "")
                    {
                        _dtVehicRobadosRecientes = new DataTable("VehiculosRobados");
                        _dtVehicRobadosRecientes.Columns.Add("Placa");
                        _dtVehicRobadosRecientes.Columns.Add("Propietario del vehículo");
                        _dtVehicRobadosRecientes.Columns.Add("Fecha de denuncia");
                        
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = _dtVehicRobadosRecientes.NewRow();
                            dr[0] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("vehiculo")).ToString();
                            dr[1] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("nombres")).ToString();
                            dr[2] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("fecha_ingreso")).ToString();
                            _dtVehicRobadosRecientes.Rows.Add(dr);
                        }
                        exito = true;
                    }
                    else
                    {
                        _error = oDatos.RetornarParametro("pv_mensaje").ToString();
                    }
                }
                else
                {
                    _error = "Error al consultar vehículos robados";
                }
            }
            catch (Exception)
            {
                _error = "Error al consultar vehículos robados";
            }
            finally
            {
                oDatos.Dispose();
            }

            return exito;
        }


        public bool VehiculoEstaRobado(string placa, out string fechaDenuncia)
        {
            bool stolen = false;
            fechaDenuncia = string.Empty;
            _error = string.Empty;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Matriculacion VehiculosRobados.LoadVehiculosRobadosRecientes()");
            oDatos.Paquete("GCK_CONSULTA_VEH_ROBADOS.GCP_VEHICULO_ES_ROBADO");
            oDatos.Parametro("pv_placa", placa);
            oDatos.Parametro("pv_esrobado", "V", 1, "O");
            oDatos.Parametro("pv_fecha_registro", "V", 10, "O");
            oDatos.Parametro("pv_error", "V", 400, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "")
                    {
                        if (oDatos.RetornarParametro("pv_esrobado").ToString() == "S")
                        {
                            stolen = true;
                            fechaDenuncia = oDatos.RetornarParametro("pv_fecha_registro").ToString();
                            _error = string.Empty;
                        }
                    }
                    else
                    {
                        _error = oDatos.RetornarParametro("pv_error").ToString();
                    }
                }
                else
                {
                    _error = "Error al consultar estado de vehículo";
                }
            }
            catch (Exception)
            {
                _error = "Error al consultar estado de vehículo";
            }
            finally
            {
                oDatos.Dispose();
            }
            return stolen;
        }


        public bool VehiculoEstaRobadoChasis(string chasis, out string fechaDenuncia)
        {
            bool stolen = false;
            fechaDenuncia = string.Empty;
            _error = string.Empty;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Matriculacion VehiculosRobados.VehiculoEstaRobadoChasis()");
            oDatos.Paquete("GCK_CONSULTA_VEH_ROBADOS.GCP_VEHICULO_ES_ROBADO_CHASIS");
            oDatos.Parametro("pv_chasis", chasis);
            oDatos.Parametro("pv_esrobado", "V", 1, "O");
            oDatos.Parametro("pv_fecha_registro", "V", 10, "O");
            oDatos.Parametro("pv_error", "V", 400, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "")
                    {
                        if (oDatos.RetornarParametro("pv_esrobado").ToString() == "S")
                        {
                            stolen = true;
                            fechaDenuncia = oDatos.RetornarParametro("pv_fecha_registro").ToString();
                            _error = string.Empty;
                        }
                    }
                    else
                    {
                        _error = oDatos.RetornarParametro("pv_error").ToString();
                    }
                }
                else
                {
                    _error = "Error inesperado al consultar estado de vehículo";
                }
            }
            catch (Exception)
            {
                _error = "Error inesperado al consultar estado de vehículo.";
            }
            finally
            {
                oDatos.Dispose();
            }
            return stolen;
        }


        public DataTable VehiculosRobadosRecientemente
        {
            get
            {
                return _dtVehicRobadosRecientes;
            }
        }

        public string Error
        {
            get
            {
                return _error;
            }
        }

    }
}
