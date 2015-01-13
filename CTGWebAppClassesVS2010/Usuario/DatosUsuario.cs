namespace Usuario
{
    public class DatosUsuario
    {
        private string user;
        private string password;
        private string database;
        private string _error;

        public DatosUsuario(string sUsuario, string sClave, string sBaseDatos)
        {
            user = sUsuario;
            password = sClave;
            database = sBaseDatos;
        }

        

        #region "Métodos que se utilizarán en el módulo de Actualización de Datos (direcciones)"
        public string[] ObtenerDatosPersonales(string identificacion)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Usuario.DatosUsuario.cs ObtenerDatosPersonales()");
            string[] datosPersonales = new string[31];

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaDatosDetLicencia);
            oDatos.Parametro("pv_identificacion", identificacion);
            oDatos.Parametro("pv_nombres", "V", 120, "O");
            oDatos.Parametro("pv_apellido1", "V", 120, "O");
            oDatos.Parametro("pv_apellido2", "V", 120, "O");
            oDatos.Parametro("pv_Fecha_Nacimiento", "D", 120, "O");
            oDatos.Parametro("pv_Localidad", "V", 120, "O");
            oDatos.Parametro("pv_Provincia", "V", 120, "O");
            oDatos.Parametro("pv_Pais", "V", 120, "O");
            oDatos.Parametro("pv_Sexo", "V", 120, "O");
            oDatos.Parametro("pv_Sangre", "V", 120, "O");
            oDatos.Parametro("pv_Estatura", "V", 120, "O");
            oDatos.Parametro("pv_Estado_Civil", "V", 120, "O");
            oDatos.Parametro("pv_Profesion", "V", 120, "O");
            oDatos.Parametro("pv_calle1", "V", 120, "O");
            oDatos.Parametro("pv_calle2", "V", 120, "O");
            oDatos.Parametro("pv_numero", "V", 10, "O");
            oDatos.Parametro("pv_manzana", "V", 10, "O");
            oDatos.Parametro("pv_piso", "V", 10, "O");
            oDatos.Parametro("pv_departamento", "V", 10, "O");
            oDatos.Parametro("pv_id_ciudadela", "V", 3, "O");
            oDatos.Parametro("pv_ciudadela", "V", 100, "O");
            oDatos.Parametro("pv_Direccion", "V", 120, "O");
            oDatos.Parametro("pv_Telefono", "V", 15, "O");
            oDatos.Parametro("pv_celular", "V", 15, "O");
            oDatos.Parametro("pv_id_canton", "V", 3, "O");
            oDatos.Parametro("pv_Canton", "V", 120, "O");
            oDatos.Parametro("pv_id_prov_ubicacion", "V", 120, "O");
            oDatos.Parametro("pv_prov_ubicacion", "V", 120, "O");
            oDatos.Parametro("pv_pais_ubicacion", "V", 120, "O");
            oDatos.Parametro("pv_existe", "V", 120, "O");
            oDatos.Parametro("pv_mensaje", "V", 120, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_mensaje").ToString();
                    //ERROR 1 ->  El usuario no puede ser nulo
                    //ERROR 2 -> El usuario no existe o no está registrado
                    //ERROR 3 -> Debe modificar su clave, es la primera vez que entra al sistema
                    //ERROR 4 -> el password no puede ser nulo
                    //ERROR S -> NO HAY ERROR
                    if (error == "")
                    {
                        datosPersonales[0] = oDatos.RetornarParametro("pv_identificacion").ToString();
                        datosPersonales[1] = oDatos.RetornarParametro("pv_nombres").ToString();
                        datosPersonales[2] = oDatos.RetornarParametro("pv_apellido1").ToString();
                        datosPersonales[3] = oDatos.RetornarParametro("pv_apellido2").ToString();
                        datosPersonales[4] = oDatos.RetornarParametro("pv_Fecha_Nacimiento").ToString();
                        datosPersonales[5] = oDatos.RetornarParametro("pv_Localidad").ToString();
                        datosPersonales[6] = oDatos.RetornarParametro("pv_Provincia").ToString();
                        datosPersonales[7] = oDatos.RetornarParametro("pv_Pais").ToString();
                        datosPersonales[8] = oDatos.RetornarParametro("pv_Sexo").ToString();
                        datosPersonales[9] = oDatos.RetornarParametro("pv_Sangre").ToString();
                        datosPersonales[10] = oDatos.RetornarParametro("pv_Estatura").ToString();
                        datosPersonales[11] = oDatos.RetornarParametro("pv_Estado_Civil").ToString();
                        datosPersonales[12] = oDatos.RetornarParametro("pv_Profesion").ToString();
                        datosPersonales[13] = oDatos.RetornarParametro("pv_calle1").ToString();
                        datosPersonales[14] = oDatos.RetornarParametro("pv_calle2").ToString();
                        datosPersonales[15] = oDatos.RetornarParametro("pv_numero").ToString();
                        datosPersonales[16] = oDatos.RetornarParametro("pv_manzana").ToString();
                        datosPersonales[17] = oDatos.RetornarParametro("pv_piso").ToString();
                        datosPersonales[18] = oDatos.RetornarParametro("pv_departamento").ToString();
                        datosPersonales[19] = oDatos.RetornarParametro("pv_ciudadela").ToString();
                        datosPersonales[20] = oDatos.RetornarParametro("pv_Direccion").ToString();
                        datosPersonales[21] = oDatos.RetornarParametro("pv_Telefono").ToString();
                        datosPersonales[22] = oDatos.RetornarParametro("pv_Canton").ToString();
                        datosPersonales[23] = oDatos.RetornarParametro("pv_prov_ubicacion").ToString();
                        datosPersonales[24] = oDatos.RetornarParametro("pv_pais_ubicacion").ToString();
                        datosPersonales[25] = oDatos.RetornarParametro("pv_existe").ToString();
                        datosPersonales[26] = oDatos.RetornarParametro("pv_mensaje").ToString();
                        datosPersonales[27] = oDatos.RetornarParametro("pv_celular").ToString();
                        datosPersonales[28] = oDatos.RetornarParametro("pv_id_ciudadela").ToString();
                        datosPersonales[29] = oDatos.RetornarParametro("pv_id_canton").ToString();
                        datosPersonales[30] = oDatos.RetornarParametro("pv_id_prov_ubicacion").ToString();
                    }
                    else
                    {
                        datosPersonales[0] = "error";
                        datosPersonales[1] = error;
                    }
                }
                else
                {
                    datosPersonales[0] = "error";
                    datosPersonales[1] = "Error: No se pudo conectar a la Base de Datos";
                }
            }
            catch
            {
                datosPersonales[0] = "error";
                datosPersonales[1] = "Error: No se pudo conectar a la Base de Datos";
            }
            finally
            {
                oDatos.Dispose();
            }

            return datosPersonales;
        }

        public bool ActualizaDato(string identificacion, string campo, string newValue, string oldValue)
        {
            bool result = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Usuario.DatosUsuario.cs ActualizaDato()");
            oDatos.Paquete("web_api_transacciones.actualiza_personas");
            oDatos.Parametro("pv_iden", identificacion);
            oDatos.Parametro("pv_campo", campo);
            oDatos.Parametro("pv_valor_anterior", oldValue);
            oDatos.Parametro("pv_valor_nuevo", newValue);
            oDatos.Parametro("pv_usuario", identificacion);
            oDatos.Parametro("pv_error", "V", 1000, "O");

            try
            {
                if (oDatos.Ejecutar("N"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                        result = true;
                    else
                    {
                        _error = oDatos.RetornarParametro("pv_error").ToString();
                        Constantes.WebApp.SendErrorAlert("PortalTRX actualizadato", _error);
                    }
                }
                else
                {
                    _error = oDatos.Mensaje;
                    Constantes.WebApp.SendErrorAlert("PortalTRX actualizadato", _error);
                }
            }
            catch
            {
                _error = "Error al actualizar dato";
            }
            finally
            {
                oDatos.Dispose();
            }

            return result;
        }

        public bool ActualizaPais(string identificacion, string newCodPais, string newCodProvincia, string newCodCanton, string newCodCiudadela, string oldCodPais, string oldCodProvincia, string oldCodCanton, string oldCodCiudadela)
        {
            bool result = false;

            if (ActualizaProvincia(identificacion, newCodProvincia, newCodCanton, newCodCiudadela, oldCodProvincia, oldCodCanton, oldCodCiudadela))
            {
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Usuario.DatosUsuario.cs ActualizaPais()");
                oDatos.Paquete("web_api_transacciones.actualiza_personas");
                oDatos.Parametro("pv_iden", identificacion);
                oDatos.Parametro("pv_campo", "ID_PAIS");
                oDatos.Parametro("pv_valor_anterior", oldCodPais);
                oDatos.Parametro("pv_valor_nuevo", newCodPais);
                oDatos.Parametro("pv_usuario", identificacion);
                oDatos.Parametro("pv_error", "V", 1000, "O");
                try
                {
                    if (oDatos.Ejecutar("N"))
                    {
                        if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                            result = true;
                        else
                            _error = oDatos.RetornarParametro("pv_error").ToString();
                    }
                    else
                        _error = oDatos.Mensaje;
                }
                catch
                {
                    _error = "Error al actualizar país";
                }
                finally
                {
                    oDatos.Dispose();
                }
            }                  

            return result;
        }


        public bool ActualizaProvincia(string identificacion, string newCodProvincia, string newCodCanton, string newCodCiudadela, string oldCodProvincia, string oldCodCanton, string oldCodCiudadela)
        {
            bool result = false;

            if (ActualizaCanton(identificacion, newCodCanton, newCodCiudadela, oldCodCanton, oldCodCiudadela))
            {
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Usuario.DatosUsuario.cs ActualizaProvincia()");
                oDatos.Paquete("web_api_transacciones.actualiza_personas");
                oDatos.Parametro("pv_iden", identificacion);
                oDatos.Parametro("pv_campo", "ID_PROVINCIA");
                oDatos.Parametro("pv_valor_anterior", oldCodProvincia);
                oDatos.Parametro("pv_valor_nuevo", newCodProvincia);
                oDatos.Parametro("pv_usuario", identificacion);
                oDatos.Parametro("pv_error", "V", 1000, "O");
                try
                {
                    if (oDatos.Ejecutar("N"))
                    {
                        if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                            result = true;
                        else
                            _error = oDatos.RetornarParametro("pv_error").ToString();
                    }
                    else
                        _error = oDatos.Mensaje;
                }
                catch
                {
                    _error = "Error al actualizar provincia";
                }
                finally
                {
                    oDatos.Dispose();
                }
            }

            return result;
        }


        public bool ActualizaCanton(string identificacion, string newCodCanton, string newCodCiudadela, string oldCodCanton, string oldCodCiudadela)
        {
            bool result = false;

            if (ActualizaCiudadela(identificacion, newCodCiudadela, oldCodCiudadela))
            {
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Usuario.DatosUsuario.cs ActualizaCanton()");
                oDatos.Paquete("web_api_transacciones.actualiza_personas");
                oDatos.Parametro("pv_iden", identificacion);
                oDatos.Parametro("pv_campo", "ID_LOCALIDAD");
                oDatos.Parametro("pv_valor_anterior", oldCodCanton);
                oDatos.Parametro("pv_valor_nuevo", newCodCanton);
                oDatos.Parametro("pv_usuario", identificacion);
                oDatos.Parametro("pv_error", "V", 1000, "O");
                try
                {
                    if (oDatos.Ejecutar("N"))
                    {
                        if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                            result = true;
                        else
                            _error = oDatos.RetornarParametro("pv_error").ToString();
                    }
                    else
                        _error = oDatos.Mensaje;
                }
                catch
                {
                    _error = "Error al actualizar cantón";
                }
                finally
                {
                    oDatos.Dispose();
                }
            }

            return result;
        }

        public bool ActualizaCiudadela(string identificacion, string newCodCiudadela, string oldCodCiudadela)
        {
            bool result = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Usuario.DatosUsuario.cs ActualizaCiudadela()");
            oDatos.Paquete("web_api_transacciones.actualiza_personas");
            oDatos.Parametro("pv_iden", identificacion);
            oDatos.Parametro("pv_campo", "ID_CIUDADELA");
            oDatos.Parametro("pv_valor_anterior", oldCodCiudadela);
            oDatos.Parametro("pv_valor_nuevo", newCodCiudadela);
            oDatos.Parametro("pv_usuario", identificacion);
            oDatos.Parametro("pv_error", "V", 1000, "O");
            try
            {
                if (oDatos.Ejecutar("N"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                        result = true;
                    else
                        _error = oDatos.RetornarParametro("pv_error").ToString();
                }
                else
                    _error = oDatos.Mensaje;
            }
            catch
            {
                _error = "Error al actualizar ciudadela";
            }
            finally
            {
                oDatos.Dispose();
            }

            return result;
        }
        #endregion

        public string Error 
        {
            get { return _error;  }
        }
    }
}
