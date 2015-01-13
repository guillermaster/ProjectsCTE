using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;

namespace Matriculacion
{
    public class MatriculacionVehicular
    {
        private string user;
        private string password;
        private string database;
        private string error;

        public string Error
        {
            get { return error; }
            //set { error = value; }
        }


        public MatriculacionVehicular(string sUsuario, string sClave, string sBaseDatos)
        {
            this.user = sUsuario;
            this.password = sClave;
            this.database = sBaseDatos;
        }


        public DataTable VehiculosPorPersona(string licencia)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Matriculacion.MatriculacionVehicular.cs VehiculosPorPersona()");
            oDatos.Paquete("WEB_INTER_INFRACCIONES_1.retorna_cursor_datos_veh");
            oDatos.Parametro("pv_licencia", licencia);
            oDatos.Parametro("C_VEH", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");

            DataTable dtVehiculos = new DataTable("vehiculosxpersona");
            dtVehiculos.Columns.Add("placa");
            dtVehiculos.Columns.Add("clase");
            dtVehiculos.Columns.Add("marca");
            dtVehiculos.Columns.Add("modelo");
            dtVehiculos.Columns.Add("color");

            if (oDatos.Ejecutar("R"))
            {
                error = string.Empty;
                if (oDatos.RetornarParametro("pv_error").ToString() == "")
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtVehiculos.NewRow();
                        dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                        dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                        dr[2] = oDatos.oDataReader.GetValue(2).ToString();
                        dr[3] = oDatos.oDataReader.GetValue(3).ToString();
                        dr[4] = oDatos.oDataReader.GetValue(4).ToString();
                        dtVehiculos.Rows.Add(dr);
                    }
                }
                else
                {
                    error = oDatos.RetornarParametro("pv_error").ToString();
                }
            }
            else
            {
                error = "Error: No se pudo conectar a la Base de Datos";
            }

            oDatos.Dispose();
            
            return dtVehiculos;
        }


        public DataTable VehiculosPorPersonaDatosBloqueos(string licencia)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Matriculacion.MatriculacionVehicular.cs VehiculosPorPersona()");
            oDatos.Paquete("WEB_INTER_INFRACCIONES_1.retorna_cursor_datos_veh");
            oDatos.Parametro("pv_licencia", licencia);
            oDatos.Parametro("C_VEH", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");

            DataTable dtVehiculos = new DataTable("vehiculosxpersona");
            dtVehiculos.Columns.Add("placa");
            dtVehiculos.Columns.Add("clase");
            dtVehiculos.Columns.Add("marca");
            dtVehiculos.Columns.Add("modelo");
            dtVehiculos.Columns.Add("color");
            dtVehiculos.Columns.Add("bloqueos");

            if (oDatos.Ejecutar("R"))
            {
                error = string.Empty;
                if (oDatos.RetornarParametro("pv_error").ToString() == "")
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtVehiculos.NewRow();
                        dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                        dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                        dr[2] = oDatos.oDataReader.GetValue(2).ToString();
                        dr[3] = oDatos.oDataReader.GetValue(3).ToString();
                        dr[4] = oDatos.oDataReader.GetValue(4).ToString();
                        DataTable dtBloqueos = BloqueosPorVehiculo(dr[0].ToString());
                        int nBloq = dtBloqueos.Rows.Count;
                        if(nBloq==0)
                            dr[5] = "No";
                        else
                            dr[5] = "Si: " + nBloq;
                        dtVehiculos.Rows.Add(dr);
                    }
                }
                else
                {
                    error = oDatos.RetornarParametro("pv_error").ToString();
                }
            }
            else
            {
                error = "Error: No se pudo conectar a la Base de Datos";
            }

            oDatos.Dispose();

            return dtVehiculos;
        }


        public DataTable BloqueosPorVehiculo(string placa)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Matriculacion.MatriculacionVehicular.cs BloqueosPorVehiculo()");
            oDatos.Paquete("WEB_INTER_INFRACCIONES_1.retorna_bloqueo");
            oDatos.Parametro("pv_placa", placa);
            oDatos.Parametro("C_BLOQUEOS", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");

            DataTable dtBloqueos = new DataTable("bloqueosxvehiculo");
            dtBloqueos.Columns.Add("fecha_ingreso");
            dtBloqueos.Columns.Add("descripcion");

            if (oDatos.Ejecutar("R"))
            {
                if (oDatos.RetornarParametro("pv_error").ToString() == "")
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtBloqueos.NewRow();
                        dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                        dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                        dtBloqueos.Rows.Add(dr);
                    }
                }
                else
                {
                    error = oDatos.RetornarParametro("pv_error").ToString();
                }
            }
            else
            {
                error = "Error: No se pudo conectar a la Base de Datos";
            }

            oDatos.Dispose();

            return dtBloqueos;
        }
        

        public string[] DatosAvanzadosVehiculo(string placa)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Matriculacion.MatriculacionVehicular.cs DatosAvanzadosVehiculo()");
            string[] datosvehiculo = new string[21];

            oDatos.Paquete("WEB_INTER_INFRACCIONES_1.retorna_mas_datos_veh");
            oDatos.Parametro("pv_placa", placa);
            oDatos.Parametro("pv_chasis", "V", 120, "O");
            oDatos.Parametro("pv_anio_prod", "V", 120, "O");
            oDatos.Parametro("pv_tonelaje", "V", 120, "O");
            oDatos.Parametro("pv_camv", "V", 120, "O");//null
            oDatos.Parametro("pv_motor", "V", 120, "O");
            oDatos.Parametro("pv_pais_origen", "V", 120, "O");//null
            oDatos.Parametro("pv_tipo", "V", 120, "O");
            oDatos.Parametro("pv_cilindraje", "V", 120, "O");//null
            oDatos.Parametro("pv_servicio", "V", 120, "O");
            oDatos.Parametro("pv_pasajeros", "V", 120, "O");
            oDatos.Parametro("pv_modalidad", "V", 120, "O");
            oDatos.Parametro("pv_cooperativa", "V", 120, "O");
            oDatos.Parametro("pv_clase_serv", "V", 120, "O");
            oDatos.Parametro("pv_clase", "V", 120, "O");
            oDatos.Parametro("pv_marca", "V", 120, "O");
            oDatos.Parametro("pv_modelo", "V", 120, "O");
            oDatos.Parametro("pv_color", "V", 120, "O");
            //oDatos.Parametro("pv_num_soat", "V", 120, "O");
            //oDatos.Parametro("pv_emp_soat", "V", 120, "O");
            //oDatos.Parametro("pv_fecha_ini_soat", "V", 120, "O");
            //oDatos.Parametro("pv_fecha_fin_soat", "V", 120, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");

            if (oDatos.Ejecutar("R"))
            {
                string error = oDatos.RetornarParametro("pv_error").ToString();
                //ERROR 1 ->  El usuario no puede ser nulo
                //ERROR 2 -> El usuario no existe o no está registrado
                //ERROR 3 -> Debe modificar su clave, es la primera vez que entra al sistema
                //ERROR 4 -> el password no puede ser nulo
                //ERROR S -> NO HAY ERROR
                if (error == "")
                {
                    datosvehiculo[0] = oDatos.RetornarParametro("pv_chasis").ToString();
                    datosvehiculo[1] = oDatos.RetornarParametro("pv_anio_prod").ToString();
                    datosvehiculo[2] = oDatos.RetornarParametro("pv_tonelaje").ToString();
                    datosvehiculo[3] = oDatos.RetornarParametro("pv_camv").ToString();
                    datosvehiculo[4] = oDatos.RetornarParametro("pv_motor").ToString();
                    datosvehiculo[5] = oDatos.RetornarParametro("pv_pais_origen").ToString();
                    datosvehiculo[6] = oDatos.RetornarParametro("pv_tipo").ToString();
                    datosvehiculo[7] = oDatos.RetornarParametro("pv_cilindraje").ToString();
                    datosvehiculo[8] = oDatos.RetornarParametro("pv_servicio").ToString();
                    datosvehiculo[9] = oDatos.RetornarParametro("pv_pasajeros").ToString();
                    datosvehiculo[10] = oDatos.RetornarParametro("pv_modalidad").ToString();
                    datosvehiculo[11] = oDatos.RetornarParametro("pv_cooperativa").ToString();
                    datosvehiculo[12] = oDatos.RetornarParametro("pv_clase_serv").ToString();
                    datosvehiculo[13] = oDatos.RetornarParametro("pv_clase").ToString();
                    datosvehiculo[14] = oDatos.RetornarParametro("pv_marca").ToString();
                    datosvehiculo[15] = oDatos.RetornarParametro("pv_modelo").ToString();
                    datosvehiculo[16] = oDatos.RetornarParametro("pv_color").ToString();
                    datosvehiculo[17] = oDatos.RetornarParametro("pv_num_soat").ToString();
                    datosvehiculo[18] = oDatos.RetornarParametro("pv_emp_soat").ToString();
                    datosvehiculo[19] = oDatos.RetornarParametro("pv_fecha_ini_soat").ToString();
                    datosvehiculo[20] = oDatos.RetornarParametro("pv_fecha_fin_soat").ToString();
                }
                else
                {
                    datosvehiculo[0] = "error";
                    datosvehiculo[1] = error;
                }
            }
            else
            {
                datosvehiculo[0] = "error";
                datosvehiculo[1] = "Error: No se pudo conectar a la Base de Datos";
            }

            oDatos.Dispose();

            return datosvehiculo;
        }


        public DataTable HistorialMatriculacion(string placa)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Matriculacion.MatriculacionVehicular.cs HistorialMatriculacion()");
            oDatos.Paquete("WEB_INTER_INFRACCIONES_1.retorna_datos_mat");
            oDatos.Parametro("pv_placa", placa);
            oDatos.Parametro("C_MATRICULACION", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");

            DataTable dtMatriculacion = new DataTable("histmatriculacion");
            dtMatriculacion.Columns.Add("anio_matricula");
            dtMatriculacion.Columns.Add("emision");
            dtMatriculacion.Columns.Add("caducidad");
            dtMatriculacion.Columns.Add("tipo_mat");
            dtMatriculacion.Columns.Add("tipo_cobro");

            if (oDatos.Ejecutar("R"))
            {
                if (oDatos.RetornarParametro("pv_error").ToString() == "")
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtMatriculacion.NewRow();
                        dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                        dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                        dr[2] = oDatos.oDataReader.GetValue(2).ToString();
                        dr[3] = oDatos.oDataReader.GetValue(3).ToString();
                        dr[4] = oDatos.oDataReader.GetValue(4).ToString();
                        dtMatriculacion.Rows.Add(dr);
                    }
                }
                else
                {
                    error = oDatos.RetornarParametro("pv_error").ToString();
                }
            }
            else
            {
                error = "Error: No se pudo conectar a la Base de Datos";
            }

            oDatos.Dispose();

            return dtMatriculacion;
        }

        public string GetCedulaPropietario(string placa)
        {
            string cedula = string.Empty;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Matriculacion.MatriculacionVehicular.cs VehiculosPorPersona()");
            oDatos.Paquete("WEB_INTER_INFRACCIONES_1.consulta_identificacion");
            oDatos.Parametro("pv_placa", placa);
            oDatos.Parametro("pv_identificacion", "V", 30, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");


            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "")
                    {
                        cedula = oDatos.RetornarParametro("pv_identificacion").ToString();
                    }
                    else
                    {
                        error = oDatos.RetornarParametro("pv_error").ToString();
                    }
                }
                else
                {
                    error = "Error: No se pudo conectar a la Base de Datos";
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

            return cedula;
        }
    }
}
