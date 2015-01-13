using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SecretariaGeneral
{
    public class Contratos
    {
        private string userDB, passwordDB, serverDB;
        private string error;
                
        private string nombres, apellido1, apellido2, fechaNacimiento, localidad, provincia, pais, sexo, sangre, estatura, estadoCivil, profesion,
            direccion, telefono, celular, cantonRes, provinciaRes, paisRes, existe;


        #region Properties

        public string Error
        {
            get { return error; }
        }

        public string PaisRes
        {
            get { return paisRes; }
            //set { paisRes = value; }
        }

        public string ProvinciaRes
        {
            get { return provinciaRes; }
            //set { provinciaRes = value; }
        }

        public string CantonRes
        {
            get { return cantonRes; }
            //set { cantonRes = value; }
        }

        public string Celular
        {
            get { return celular; }
            //set { celular = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            //set { telefono = value; }
        }

        public string Direccion
        {
            get { return direccion; }
            //set { direccion = value; }
        }

        public string Profesion
        {
            get { return profesion; }
            //set { profesion = value; }
        }

        public string EstadoCivil
        {
            get { return estadoCivil; }
            //set { estadoCivil = value; }
        }

        public string Estatura
        {
            get { return estatura; }
            //set { estatura = value; }
        }

        public string Sangre
        {
            get { return sangre; }
            //set { sangre = value; }
        }

        public string Pais
        {
            get { return pais; }
            //set { pais = value; }
        }

        public string Sexo
        {
            get { return sexo; }
            //set { sexo = value; }
        }

        public string Provincia
        {
            get { return provincia; }
            //set { provincia = value; }
        }

        public string Localidad
        {
            get { return localidad; }
            //set { localidad = value; }
        }

        public string FechaNacimiento
        {
            get { return fechaNacimiento; }
            //set { fechaNacimiento = value; }
        }

        public string Apellido2
        {
            get { return apellido2; }
            //set { apellido2 = value; }
        }

        public string Apellido1
        {
            get { return apellido1; }
            //set { apellido1 = value; }
        }

        public string Nombres
        {
            get { return nombres; }
            //set { nombres = value; }
        }

        #endregion

        public Contratos(string user, string password, string database)
        {
            userDB = user;
            passwordDB = password;
            serverDB = database;
        }


        public bool LoadDatosBasicosUsuarioById(string id)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(userDB, passwordDB, serverDB, "SecretariaGeneral.Contratos.GetDatosBasicosUsuarioById()");
            oDatos.Paquete("WEB_INTER_LICENCIAS.Datos_Licencia2");
            #region "Add parameters"
            oDatos.Parametro("pv_identificacion", id);
            oDatos.Parametro("pv_nombres", "V", 60, "O");
            oDatos.Parametro("pv_apellido1", "V", 60, "O");
            oDatos.Parametro("pv_apellido2", "V", 60, "O");
            oDatos.Parametro("pv_fecha_nacimiento", "D", 20, "O");
            oDatos.Parametro("pv_Localidad", "V", 60, "O");
            oDatos.Parametro("pv_Provincia", "V", 60, "O");
            oDatos.Parametro("pv_Pais", "V", 60, "O");
            oDatos.Parametro("pv_Sexo", "V", 60, "O");
            oDatos.Parametro("pv_Sangre", "V", 60, "O");            
            oDatos.Parametro("pv_Estatura", "V", 60, "O");
            oDatos.Parametro("pv_Estado_Civil", "V", 60, "O");
            oDatos.Parametro("pv_Profesion", "V", 60, "O");
            oDatos.Parametro("pv_calle1", "V", 60, "O");
            oDatos.Parametro("pv_calle2", "V", 60, "O");
            oDatos.Parametro("pv_numero", "V", 60, "O");
            oDatos.Parametro("pv_manzana", "V", 60, "O");
            oDatos.Parametro("pv_piso", "V", 60, "O");
            oDatos.Parametro("pv_departamento", "V", 60, "O");
            oDatos.Parametro("pv_id_ciudadela", "V", 60, "O");
            oDatos.Parametro("pv_ciudadela", "V", 60, "O");
            oDatos.Parametro("pv_direccion", "V", 180, "O");
            oDatos.Parametro("pv_Telefono", "V", 60, "O");
            oDatos.Parametro("pv_celular", "V", 60, "O");
            oDatos.Parametro("pv_id_canton", "V", 60, "O");
            oDatos.Parametro("pv_Canton", "V", 60, "O");
            oDatos.Parametro("pv_id_prov_ubicacion", "V", 60, "O");
            oDatos.Parametro("pv_prov_ubicacion", "V", 60, "O");
            oDatos.Parametro("pv_pais_ubicacion", "V", 60, "O");
            oDatos.Parametro("pv_existe", "V", 30, "O");
            oDatos.Parametro("pv_mensaje", "V", 200, "O");
            #endregion
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_existe").ToString() == "S")
                    {
                        nombres = oDatos.RetornarParametro("pv_nombres").ToString();
                        apellido1 = oDatos.RetornarParametro("pv_apellido1").ToString();
                        apellido2 = oDatos.RetornarParametro("pv_apellido2").ToString();
                        fechaNacimiento = oDatos.RetornarParametro("pv_fecha_nacimiento").ToString();
                        localidad = oDatos.RetornarParametro("pv_localidad").ToString();
                        provincia = oDatos.RetornarParametro("pv_provincia").ToString();
                        pais = oDatos.RetornarParametro("pv_pais").ToString();
                        sexo = oDatos.RetornarParametro("pv_sexo").ToString();
                        sangre = oDatos.RetornarParametro("pv_sangre").ToString();
                        estatura = oDatos.RetornarParametro("pv_estatura").ToString();
                        estadoCivil = oDatos.RetornarParametro("pv_estado_civil").ToString();
                        profesion = oDatos.RetornarParametro("pv_profesion").ToString();
                        direccion = oDatos.RetornarParametro("pv_direccion").ToString();
                        telefono = oDatos.RetornarParametro("pv_telefono").ToString();
                        celular = oDatos.RetornarParametro("pv_celular").ToString();
                        cantonRes = oDatos.RetornarParametro("pv_canton").ToString();
                        provinciaRes = oDatos.RetornarParametro("pv_prov_ubicacion").ToString();
                        paisRes = oDatos.RetornarParametro("pv_pais_ubicacion").ToString();
                        retValue = true;
                    }
                    else
                        error = oDatos.RetornarParametro("pv_mensaje").ToString();
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


        public DataTable ConsultarPersonas(string nombre, string urlDetails)
        {
            DataTable dtPersonas = new DataTable("Personas");
            #region Add Columns
            dtPersonas.Columns.Add("IDENTIFICACION");
            dtPersonas.Columns.Add("NOMBRE_COMPLETO");
            dtPersonas.Columns.Add("DIRECCION");
            dtPersonas.Columns.Add("TELEFONO1");
            dtPersonas.Columns.Add("CELULAR");
            dtPersonas.Columns.Add("PROVINCIA");
            dtPersonas.Columns.Add("LOCALIDAD");
            dtPersonas.Columns.Add("URL");
            #endregion
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(userDB, passwordDB, serverDB, "SecretariaGeneral.Contratos.ConsultarPersonas()");
            oDatos.Paquete("web_inter_licencias.datos_persona_x_nombres");
            oDatos.Parametro("pv_nombres", nombre);
            oDatos.Parametro("c_datos_persona", "R", 0, "O");
            
            try
            {
                error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtPersonas.NewRow();
                        int i;
                        for (i = 0; i < dtPersonas.Columns.Count-1; i++)
                            dr[i] = oDatos.oDataReader[i].ToString();
                        if (string.IsNullOrEmpty(urlDetails))
                            dr[i] = dr[0].ToString();
                        else
                            dr[i] = urlDetails + "?Lic=" + dr[0].ToString();
                        dtPersonas.Rows.Add(dr);
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

            return dtPersonas;
        }
    }
}
