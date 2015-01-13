using System;
using System.Collections.Generic;
using System.Text;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using AccesoDatos;

namespace Usuario
{
    public class DatosUsuario
    {
        private string user;
        private string password;
        private string database;
        private string error;

        public DatosUsuario(string sUsuario, string sClave, string sBaseDatos)
        {
            this.user = sUsuario;
            this.password = sClave;
            this.database = sBaseDatos;
        }

        

        #region "Métodos que se utilizarán en el módulo de Actualización de Datos (direcciones)"
        public string[] ObtenerDatosPersonales(string identificacion)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Usuario.DatosUsuario.cs ObtenerDatosPersonales()");
            string[] datos_personales = new string[31];

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
                    datos_personales[0] = oDatos.RetornarParametro("pv_identificacion").ToString();
                    datos_personales[1] = oDatos.RetornarParametro("pv_nombres").ToString();
                    datos_personales[2] = oDatos.RetornarParametro("pv_apellido1").ToString();
                    datos_personales[3] = oDatos.RetornarParametro("pv_apellido2").ToString();
                    datos_personales[4] = oDatos.RetornarParametro("pv_Fecha_Nacimiento").ToString();
                    datos_personales[5] = oDatos.RetornarParametro("pv_Localidad").ToString();
                    datos_personales[6] = oDatos.RetornarParametro("pv_Provincia").ToString();
                    datos_personales[7] = oDatos.RetornarParametro("pv_Pais").ToString();
                    datos_personales[8] = oDatos.RetornarParametro("pv_Sexo").ToString();
                    datos_personales[9] = oDatos.RetornarParametro("pv_Sangre").ToString();
                    datos_personales[10] = oDatos.RetornarParametro("pv_Estatura").ToString();
                    datos_personales[11] = oDatos.RetornarParametro("pv_Estado_Civil").ToString();
                    datos_personales[12] = oDatos.RetornarParametro("pv_Profesion").ToString();
                    datos_personales[13] = oDatos.RetornarParametro("pv_calle1").ToString();
                    datos_personales[14] = oDatos.RetornarParametro("pv_calle2").ToString();
                    datos_personales[15] = oDatos.RetornarParametro("pv_numero").ToString();
                    datos_personales[16] = oDatos.RetornarParametro("pv_manzana").ToString();
                    datos_personales[17] = oDatos.RetornarParametro("pv_piso").ToString();
                    datos_personales[18] = oDatos.RetornarParametro("pv_departamento").ToString();
                    datos_personales[19] = oDatos.RetornarParametro("pv_ciudadela").ToString();
                    datos_personales[20] = oDatos.RetornarParametro("pv_Direccion").ToString();
                    datos_personales[21] = oDatos.RetornarParametro("pv_Telefono").ToString();
                    datos_personales[22] = oDatos.RetornarParametro("pv_Canton").ToString();
                    datos_personales[23] = oDatos.RetornarParametro("pv_prov_ubicacion").ToString();
                    datos_personales[24] = oDatos.RetornarParametro("pv_pais_ubicacion").ToString();
                    datos_personales[25] = oDatos.RetornarParametro("pv_existe").ToString();
                    datos_personales[26] = oDatos.RetornarParametro("pv_mensaje").ToString();
                    datos_personales[27] = oDatos.RetornarParametro("pv_celular").ToString();
                    datos_personales[28] = oDatos.RetornarParametro("pv_id_ciudadela").ToString();
                    datos_personales[29] = oDatos.RetornarParametro("pv_id_canton").ToString();
                    datos_personales[30] = oDatos.RetornarParametro("pv_id_prov_ubicacion").ToString();
                }
                else
                {
                    datos_personales[0] = "error";
                    datos_personales[1] = error;
                }
            }
            else
            {
                datos_personales[0] = "error";
                datos_personales[1] = "Error: No se pudo conectar a la Base de Datos";
            }

            oDatos.Dispose();

            return datos_personales;
        }

        public bool ActualizaDato(string identificacion, string campo, string new_value, string old_value)
        {
            bool result = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Usuario.DatosUsuario.cs ActualizaDato()");
            oDatos.Paquete("web_api_transacciones.actualiza_personas");
            oDatos.Parametro("pv_iden", identificacion);
            oDatos.Parametro("pv_campo", campo);
            oDatos.Parametro("pv_valor_anterior", old_value);
            oDatos.Parametro("pv_valor_nuevo", new_value);
            oDatos.Parametro("pv_usuario", identificacion);
            oDatos.Parametro("pv_error", "V", 1000, "O");

            if (oDatos.Ejecutar("N"))
            {
                if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                {
                    result = true;
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

            return result;
        }

        public bool ActualizaPais(string identificacion, string new_cod_pais, string new_cod_provincia, string new_cod_canton, string new_cod_ciudadela, string old_cod_pais, string old_cod_provincia, string old_cod_canton, string old_cod_ciudadela)
        {
            bool result = false;

            if (ActualizaProvincia(identificacion, new_cod_provincia, new_cod_canton, new_cod_ciudadela, old_cod_provincia, old_cod_canton, old_cod_ciudadela))
            {
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Usuario.DatosUsuario.cs ActualizaPais()");
                oDatos.Paquete("web_api_transacciones.actualiza_personas");
                oDatos.Parametro("pv_iden", identificacion);
                oDatos.Parametro("pv_campo", "ID_PAIS");
                oDatos.Parametro("pv_valor_anterior", old_cod_pais);
                oDatos.Parametro("pv_valor_nuevo", new_cod_pais);
                oDatos.Parametro("pv_usuario", identificacion);
                oDatos.Parametro("pv_error", "V", 1000, "O");

                if (oDatos.Ejecutar("N"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                    {
                        result = true;
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
            }                  

            return result;
        }


        public bool ActualizaProvincia(string identificacion, string new_cod_provincia, string new_cod_canton, string new_cod_ciudadela, string old_cod_provincia, string old_cod_canton, string old_cod_ciudadela)
        {
            bool result = false;

            if (ActualizaCanton(identificacion, new_cod_canton, new_cod_ciudadela, old_cod_canton, old_cod_ciudadela))
            {
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Usuario.DatosUsuario.cs ActualizaProvincia()");
                oDatos.Paquete("web_api_transacciones.actualiza_personas");
                oDatos.Parametro("pv_iden", identificacion);
                oDatos.Parametro("pv_campo", "ID_PROVINCIA");
                oDatos.Parametro("pv_valor_anterior", old_cod_provincia);
                oDatos.Parametro("pv_valor_nuevo", new_cod_provincia);
                oDatos.Parametro("pv_usuario", identificacion);
                oDatos.Parametro("pv_error", "V", 1000, "O");

                if (oDatos.Ejecutar("N"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                    {
                        result = true;
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
            }

            return result;
        }


        public bool ActualizaCanton(string identificacion, string new_cod_canton, string new_cod_ciudadela, string old_cod_canton, string old_cod_ciudadela)
        {
            bool result = false;

            if (ActualizaCiudadela(identificacion, new_cod_ciudadela, old_cod_ciudadela))
            {
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Usuario.DatosUsuario.cs ActualizaCanton()");
                oDatos.Paquete("web_api_transacciones.actualiza_personas");
                oDatos.Parametro("pv_iden", identificacion);
                oDatos.Parametro("pv_campo", "ID_LOCALIDAD");
                oDatos.Parametro("pv_valor_anterior", old_cod_canton);
                oDatos.Parametro("pv_valor_nuevo", new_cod_canton);
                oDatos.Parametro("pv_usuario", identificacion);
                oDatos.Parametro("pv_error", "V", 1000, "O");

                if (oDatos.Ejecutar("N"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                    {
                        result = true;
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
            }
            

            return result;
        }

        public bool ActualizaCiudadela(string identificacion, string new_cod_ciudadela, string old_cod_ciudadela)
        {
            bool result = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Usuario.DatosUsuario.cs ActualizaCiudadela()");
            oDatos.Paquete("web_api_transacciones.actualiza_personas");
            oDatos.Parametro("pv_iden", identificacion);
            oDatos.Parametro("pv_campo", "ID_CIUDADELA");
            oDatos.Parametro("pv_valor_anterior", old_cod_ciudadela);
            oDatos.Parametro("pv_valor_nuevo", new_cod_ciudadela);
            oDatos.Parametro("pv_usuario", identificacion);
            oDatos.Parametro("pv_error", "V", 1000, "O");

            if (oDatos.Ejecutar("N"))
            {
                if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                {
                    result = true;
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

            return result;
        }
        #endregion

        public string Error 
        {
            get { return error;  }
        }
    }
}
