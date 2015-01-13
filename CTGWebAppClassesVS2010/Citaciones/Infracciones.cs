using System;
using System.Collections;
using System.Data;

namespace Citaciones
{
    public class Infracciones
    {
        private string user;
        private string password;
        private string database;
        private string _error;

        public Infracciones(string sUsuario, string sClave, string sBaseDatos)
        {
            user = sUsuario;
            password = sClave;
            database = sBaseDatos;
        }

        public int PuntosInicioLicencia()
        {
            int puntos = -1;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Citaciones.Infracciones.cs PuntosIniciosLicencia()");
            try
            {
                oDatos.EjecutarQuery("select web_inter_infracciones_1.retorna_puntos_lic() puntos from dual");
                if (oDatos.oDataReader.Read())
                {
                    puntos = int.Parse(oDatos.oDataReader["puntos"].ToString());
                }
                oDatos.Dispose();
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                oDatos.Dispose();
            }
            return puntos;
        }

        //public string PuntosPerdidosPorLicencia(string id)
        //{
        //    string puntos = "";
        //    AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Citaciones.Infracciones.cs ConsultarLicencia()");
        //    try
        //    {
        //        oDatos.EjecutarQuery("select web_inter_infracciones_1.retorna_puntos('" + id + "') puntos from dual");
        //        if (oDatos.oDataReader.Read())
        //        {
        //            puntos = oDatos.oDataReader["puntos"].ToString();
        //        }
        //        oDatos.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.error = ex.Message;
        //        oDatos.Dispose();
        //    }
        //    return puntos;
        //}

        public string PuntosPorLicencia(string id)
        {
            string puntos = string.Empty;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Citaciones.Infracciones.cs PuntosPorLicencia()");

            oDatos.Paquete("consulta_puntos_licencia");
            oDatos.Parametro("pv_identificacion", id);
            oDatos.Parametro("pn_puntos", "V", 3, "O");
            oDatos.Parametro("pv_mensaje", "V", 320, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_mensaje").ToString() == "")
                        puntos = oDatos.RetornarParametro("pn_puntos").ToString();
                    else
                        _error = oDatos.RetornarParametro("pv_mensaje").ToString();
                }
                else
                    _error = oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return puntos;
        }

        public object[] InfraccionPendiente(string codCitacion)
        {
            ArrayList oInfracciones = new ArrayList();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Citaciones.Infracciones.cs InfraccionPendiente()");

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaCitacionPendiente);
            oDatos.Parametro("pv_citacion", codCitacion);
            oDatos.Parametro("C_INFRAC", "R", 0, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    ArrayList oLista = new ArrayList();
                    while (oDatos.oDataReader.Read())
                    {
                        try
                        {
                            oLista.Add(oDatos.oDataReader.GetString(0));
                        }
                        catch 
                        {
                            oLista.Add("");
                        }
                        try
                        {
                            oLista.Add(oDatos.oDataReader.GetString(1));
                        }
                        catch
                        {
                            oLista.Add("");
                        }
                        try
                        {
                            oLista.Add(oDatos.oDataReader.GetString(2));
                        }
                        catch
                        {
                            oLista.Add("");
                        }
                        try
                        {
                            oLista.Add(oDatos.oDataReader.GetString(3));
                        }
                        catch
                        {
                            oLista.Add("");
                        }
                        try
                        {
                            oLista.Add(oDatos.oDataReader.GetString(4));
                        }
                        catch
                        {
                            oLista.Add("");
                        }
                        try
                        {
                            oLista.Add(oDatos.oDataReader.GetString(5));
                        } // articulo literal 
                        catch
                        {
                            oLista.Add("");
                        }
                        try
                        {
                            oLista.Add(oDatos.oDataReader.GetString(6));
                        } //descripcion contravencion
                        catch 
                        {
                            oLista.Add("");
                        }
                        try
                        {
                            oLista.Add(oDatos.oDataReader.GetDecimal(7));
                        }
                        catch
                        {
                            oLista.Add("");
                        }
                        try
                        {
                            oLista.Add(oDatos.oDataReader.GetDecimal(8));
                        }
                        catch
                        {
                            oLista.Add("");
                        }
                        try
                        {
                            oLista.Add(oDatos.oDataReader.GetDecimal(9));
                        }
                        catch
                        {
                            oLista.Add("");
                        }
                        oInfracciones.Add(oLista.ToArray());
                        oLista.Clear();
                    }
                }
                else
                {
                    ArrayList oLista = new ArrayList();
                    oLista.Add("Error");
                    oLista.Add(oDatos.Mensaje);
                    oInfracciones.Add(oLista.ToArray());
                }
            }
            catch (Exception ex)
            {
                ArrayList oLista = new ArrayList();
                oLista.Add("Error");
                oLista.Add(ex.Message);
                oInfracciones.Add(oLista.ToArray());
            }
            finally
            {
                oDatos.Dispose();
            }

            return oInfracciones.ToArray();
        }

        public class TagFieldsCitaciones
        {
            public static readonly string NumeroCitacion = "num_citacion";
            public static readonly string TipoCitacion = "tipo_citacion";
            public static readonly string LicenciaInfractor = "licencia_infractor";
            public static readonly string PlacaVehInfractor = "placa_vehiculo";
            public static readonly string FechaCitacion = "fecha_citacion";
            public static readonly string Contravencion = "contravencion";
            public static readonly string Articulo = "articulo";
            public static readonly string Puntos = "puntos_perdidos";
            public static readonly string ValorCitacion = "valor_citacion";
            public static readonly string MultaCitacion = "multa_citacion";
            public static readonly string TotalPagar = "total_pagar";
        }


        public DataTable InfraccionesPendientes(string id, string tipoId)
        {
            DataTable dtCitacPend = new DataTable("Citaciones pendientes");
            #region Add Columns
            dtCitacPend.Columns.Add("idFactura");
            dtCitacPend.Columns.Add(TagFieldsCitaciones.NumeroCitacion);
            dtCitacPend.Columns.Add(TagFieldsCitaciones.TipoCitacion);
            dtCitacPend.Columns.Add(TagFieldsCitaciones.LicenciaInfractor);
            dtCitacPend.Columns.Add(TagFieldsCitaciones.PlacaVehInfractor);
            dtCitacPend.Columns.Add(TagFieldsCitaciones.FechaCitacion);
            if (tipoId == "I")
            {
                dtCitacPend.Columns.Add(TagFieldsCitaciones.Articulo);
                dtCitacPend.Columns.Add(TagFieldsCitaciones.Puntos);
            }
            dtCitacPend.Columns.Add(TagFieldsCitaciones.Contravencion);
            dtCitacPend.Columns.Add(TagFieldsCitaciones.ValorCitacion);
            dtCitacPend.Columns.Add(TagFieldsCitaciones.MultaCitacion);
            dtCitacPend.Columns.Add(TagFieldsCitaciones.TotalPagar);
            #endregion
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Citaciones.Infracciones.cs InfraccionesPendientes()");

            switch (tipoId)
            {
                case "P":
                    oDatos.Paquete(Constantes.StoredProcedures.ConsultaCitacionesPendPorPlaca);
                    oDatos.Parametro("pv_placa", id.ToUpper());
                    break;
                case "I":
                    oDatos.Paquete(Constantes.StoredProcedures.ConsultaCitacionesPendPorIdent);
                    oDatos.Parametro("pv_identificacion", id);
                    break;
            }

            oDatos.Parametro("C_INFRAC", "R", 0, "O");
            _error = string.Empty;

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtCitacPend.NewRow();
                        foreach (DataColumn col in dtCitacPend.Columns)
                        {
                            try
                            {
                                dr[col.Ordinal] = oDatos.oDataReader[col.Ordinal].ToString();
                            }
                            catch
                            {
                                dr[col.Ordinal] = "Error";
                            }
                        }
                        dtCitacPend.Rows.Add(dr);
                    }
                }
                else
                    _error = oDatos.Mensaje;
            }
            catch(Exception)
            {
                _error = "Ha ocurrido un error al consultar las citaciones pendientes de pago";
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtCitacPend;
        }


        public DataTable InfraccionesPagadas(string id)
        {
            DataTable dtCitacPag = new DataTable("Citaciones pagadas");
            #region Add Columns
            dtCitacPag.Columns.Add("factura");
            dtCitacPag.Columns.Add("num_infraccion");
            dtCitacPag.Columns.Add("fec_infraccion", typeof(DateTime));
            dtCitacPag.Columns.Add("contravencion");
            dtCitacPag.Columns.Add("articulo");
            dtCitacPag.Columns.Add("nombres");
            dtCitacPag.Columns.Add("identificacion");
            dtCitacPag.Columns.Add("placa");
            dtCitacPag.Columns.Add("uniformado");
            dtCitacPag.Columns.Add("id_persona_vig");
            dtCitacPag.Columns.Add("val_contrav");
            dtCitacPag.Columns.Add("pagada");
            dtCitacPag.Columns.Add("puntos");
            dtCitacPag.Columns.Add("reincidencias");
            #endregion

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Citaciones.Infracciones.cs InfraccionesPagadas()");
            try
            {
                oDatos.Paquete("web_inter_infracciones_1.Infracciones_x_Ident_jueces");
                oDatos.Parametro("pv_identificacion", id);
                oDatos.Parametro("pv_pagada", "S");
                oDatos.Parametro("C_INFRAC", "R", 0, "O");
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtCitacPag.NewRow();
                        foreach (DataColumn dc in dtCitacPag.Columns)
                        {
                            if (dc.ColumnName != "fec_infraccion" && dc.ColumnName != "reincidencias")
                                dr[dc.ColumnName] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dc.ColumnName)).ToString();
                            else if(dc.ColumnName != "fec_infraccion")
                                dr[dc.ColumnName] = DateTime.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dc.ColumnName)).ToString());
                            else
                                dr[dc.ColumnName] = int.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dc.ColumnName)).ToString()) - 1;
                        }
                        dtCitacPag.Rows.Add(dr);
                    }
                }
                else
                    _error = oDatos.Mensaje;
            }
            catch 
            {
                _error = "Ha ocurrido un error al consultar las citaciones pagadas";
            }
            finally
            {
                oDatos.Dispose();
            }
            return dtCitacPag;
        }

        public string[] ObtenerUniformadoSancionador(string numCitacion)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Citaciones.Infracciones.cs ObtenerUniformadoSancionador()");
            string[] uniformado = null;

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaUniformadoSancCitacion);
            oDatos.Parametro("pv_sec_libretin", numCitacion);
            oDatos.Parametro("pn_persona_uni", "N", 120, "O");
            oDatos.Parametro("pv_dec_persona_uni", "V", 120, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");

            try
            {
                _error = string.Empty;
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
                        uniformado = new string[2];
                        uniformado[0] = oDatos.RetornarParametro("pv_dec_persona_uni").ToString();
                        uniformado[1] = oDatos.RetornarParametro("pn_persona_uni").ToString();
                    }
                    else
                        _error = "Información no disponible";
                }
                else
                    _error = "Error al consultar información de uniformado que emitió la citación.";
            }
            catch
            {
                _error = "Error al consultar información de uniformado que emitió la citación";
            }
            finally
            {
                oDatos.Dispose();
            }

            return uniformado;
        }

        public string[] ObtenerDatosBasicosVehiculo(string placa)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Citaciones.Infracciones.cs ObtenerDatosBasicosVehiculo()");
            string[] datosVehiculo = null;

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaDatosBasicosVehiculo);
            oDatos.Parametro("pv_placa", placa);
            oDatos.Parametro("pv_marca", "V", 120, "O");
            oDatos.Parametro("pv_modelo", "V", 120, "O");
            oDatos.Parametro("pv_clase", "V", 120, "O");
            oDatos.Parametro("pv_color", "V", 120, "O");
            oDatos.Parametro("pv_propietario", "V", 120, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");
            _error = "";
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(error))
                    {
                        datosVehiculo = new string[5];
                        datosVehiculo[0] = oDatos.RetornarParametro("pv_propietario").ToString();
                        datosVehiculo[1] = oDatos.RetornarParametro("pv_clase").ToString();
                        datosVehiculo[2] = oDatos.RetornarParametro("pv_marca").ToString();
                        datosVehiculo[3] = oDatos.RetornarParametro("pv_modelo").ToString();
                        datosVehiculo[4] = oDatos.RetornarParametro("pv_color").ToString();
                    }
                    else
                        _error = "Información no disponible";
                }
                else
                    _error = "Error al consultar datos de vehículo.";
            }
            catch
            {
                _error = "Error al consultar datos de vehículo";
            }
            finally
            {
                oDatos.Dispose();
            }
            return datosVehiculo;
        }

        public DataTable CitacionesPorCodigoCitac(string codCitac)
        {
            string[] dtColumnsValues = { "fec_infraccion", "contravencion", "articulo", "nombres", "identificacion", "placa", "uniformado", "id_persona_vig", "val_contrav", "pagada", "puntos", "reincidencias" };
            string[] dtColumnsHeader = { "Fecha de Infracción", "Contravención", "Artículo", "Nombre del Infractor", "Identificación del Infractor", "Placa del vehículo infractor", "Uniformado", "Código del Uniformado", "Valor a Pagar", "Pagada (S/N)", "Puntos a Reducir", "Cantidad de Reincidencias" };
            DataTable dtResCitaciones = new DataTable("dtCitaciones");
            for (int i = 0; i < dtColumnsHeader.Length; i++)
            {
                if (i == 0)
                    dtResCitaciones.Columns.Add(dtColumnsHeader[0], typeof(DateTime));
                else
                    dtResCitaciones.Columns.Add(dtColumnsHeader[i]);
            }
            _error = "";

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Citaciones.Infracciones.cs CitacionesPorCodigoCitac");
            try
            {
                oDatos.Paquete("web_inter_infracciones_1.Infracciones_x_cit_jueces");
                oDatos.Parametro("pv_citac", codCitac);
                oDatos.Parametro("pv_pagada", "");
                oDatos.Parametro("C_INFRAC", "R", 0, "O");
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtResCitaciones.NewRow();
                        for (int i = 0; i < dtColumnsHeader.Length; i++)
                        {
                            if(i==0)
                                dr[dtColumnsHeader[0]] = DateTime.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtColumnsValues[0])).ToString(), new System.Globalization.CultureInfo("es-ES", false));
                            else if(i==(dtColumnsHeader.Length - 1))
                                dr[dtColumnsHeader[i]] = int.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtColumnsValues[i])).ToString()) - 1;
                            else 
                                dr[dtColumnsHeader[i]] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtColumnsValues[i])).ToString();

                            if (i == 8)//valor a pagar
                                dr[dtColumnsHeader[i]] = "$ " + dr[dtColumnsHeader[i]];
                        }
                        dtResCitaciones.Rows.Add(dr);
                    }
                }
                else
                {
                    _error = "Error al consultar citacion, intente nuevamente por favor ";// + oDatos.Mensaje;
                }
            }
            catch
            {
                _error = "Error al consultar citacion, intente nuevamente por favor ";
            }
            finally
            {
                oDatos.Dispose();
            }           

            return dtResCitaciones;
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
