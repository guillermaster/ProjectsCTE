using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace EFOTclass
{
    public class EFOT
    {
        private string _dbUser;
        private string _dbPassword;
        private string _dbServer;
        private string _appUser;
        private string _error;
        private Aspirante _oAspirante;

        public EFOT(string dbUser, string dbPassword, string dbServer)
        {
            _dbUser = dbUser;
            _dbPassword = dbPassword;
            _dbServer = dbServer;
            _error = string.Empty;
        }


        public bool RegistrarAspirante(string nombres, string apellidos, string identificacion, string direccion,
            string email, DateTime fechaNacim, string estatura, string sexo, string estadoCivil, string numCargasFamiliares,
            string ide_dactilar, string codPaisNac, string codProvNac, string codCiudadNac, string tipoSangre, int peso,
            int tallaCalzado, int tallaPantalon, int tallaCamisa, int tallaGorra, string contrasena)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs RegistrarAspirante()");

            oDatos.Paquete("clk_aspirantes_efot.clp_ingresa_aspirante");
            oDatos.Parametro("pv_nombres", nombres);
            oDatos.Parametro("pv_apellidos", apellidos);
            oDatos.Parametro("pv_identificacion", identificacion);
            oDatos.Parametro("pv_direccion", direccion);
            oDatos.Parametro("pv_email", email);
            oDatos.Parametro("pv_fecha_nac", fechaNacim.ToString("dd/MM/yyyy"));
            oDatos.Parametro("pv_sexo", sexo);
            oDatos.Parametro("pv_idoneo", string.Empty);
            oDatos.Parametro("pv_estado_civil", estadoCivil);
            oDatos.Parametro("pv_cargas_familiares", numCargasFamiliares);
            oDatos.Parametro("pv_pais_nac", codPaisNac);
            oDatos.Parametro("pv_localidad_nac", codCiudadNac);
            oDatos.Parametro("pv_provincia_nac", codProvNac);
            oDatos.Parametro("pv_clave", contrasena);
            oDatos.Parametro("pv_estatura", estatura);
            oDatos.Parametro("pv_tipo_sangre", tipoSangre);
            oDatos.Parametro("pn_peso", peso);
            oDatos.Parametro("pn_talla_calzado", tallaCalzado);
            oDatos.Parametro("pn_talla_pantalon", tallaPantalon);
            oDatos.Parametro("pn_talla_camisa", tallaCamisa);
            oDatos.Parametro("pn_talla_gorra", tallaGorra);
            oDatos.Parametro("pv_ide_dactilar", ide_dactilar);
            oDatos.Parametro("pn_cod_aspirante", "N", 12, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");

            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(error))
                    {
                        retValue = true;
                        int codAspirante = int.Parse(oDatos.RetornarParametro("pn_cod_aspirante").ToString());
                        _oAspirante = new Aspirante(codAspirante);
                    }
                    else
                        _error = error;
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
            return retValue;
        }


        public bool LoadCodigoAspirante(string numCedula)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs RegistrarAspirante()");

            oDatos.Paquete("clk_aspirantes_efot.clp_consulta_codigo");
            oDatos.Parametro("pv_identificacion", numCedula);
            oDatos.Parametro("pn_codigo", "N", 12, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(error))
                    {
                        int codAspirante = int.Parse(oDatos.RetornarParametro("pn_codigo").ToString());
                        _oAspirante = new Aspirante(codAspirante);
                        retValue = true;
                    }
                    else
                        _error = error;
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
            return retValue;
        }


        public bool UpdateDataAspirante(string identificacion, string nombres, string apellidos, string email,
            string cargasFam, string ideDactilar)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs RegistrarAspirante()");

            oDatos.Paquete("clk_aspirantes_efot.clp_actualiza_aspirantes");
            oDatos.Parametro("pv_identificacion", identificacion);
            oDatos.Parametro("pv_nombres", nombres);
            oDatos.Parametro("pv_apellidos", apellidos);
            oDatos.Parametro("pv_email", email);
            oDatos.Parametro("pv_error", "V", 300, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(error))
                        retValue = true;
                    else
                        _error = error;
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
            return retValue;
        }


        public bool LoadDatosAspirante(string idAspirante)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs RegistrarAspirante()");

            oDatos.Paquete("clk_aspirantes_efot.clp_consulta_aspirante");
            oDatos.Parametro("pn_aspirante", idAspirante);
            oDatos.Parametro("c_aspirantes", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        _oAspirante = new Aspirante(oDatos.oDataReader[0].ToString(), oDatos.oDataReader[1].ToString(), oDatos.oDataReader[2].ToString(),
                            oDatos.oDataReader[3].ToString(), oDatos.oDataReader[4].ToString(), oDatos.oDataReader[5].ToString(), oDatos.oDataReader[6].ToString(),
                            oDatos.oDataReader[7].ToString(), oDatos.oDataReader[8].ToString(), oDatos.oDataReader[9].ToString(), oDatos.oDataReader[10].ToString(),
                            oDatos.oDataReader[11].ToString(), oDatos.oDataReader[12].ToString(), oDatos.oDataReader[13].ToString(), oDatos.oDataReader[14].ToString(),
                            oDatos.oDataReader[15].ToString(), oDatos.oDataReader[16].ToString(), oDatos.oDataReader[17].ToString(), oDatos.oDataReader[18].ToString(), oDatos.oDataReader[19].ToString(), oDatos.oDataReader[20].ToString());
                        retValue = true;
                    }
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
            return retValue;
        }

        public bool LoadDireccionesAspirante(string idAspirante, out DataTable dtDirecciones)
        {
            dtDirecciones = new DataTable();
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs LoadDireccionesAspirante()");

            oDatos.Paquete("clk_aspirantes_efot.clp_consulta_ubicaciones");
            oDatos.Parametro("pn_aspirante", idAspirante);
            oDatos.Parametro("c_ubicaciones", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    _oAspirante = new Aspirante();
                    while (oDatos.oDataReader.Read())
                    {
                        _oAspirante.AddDireccion(oDatos.oDataReader[0].ToString(), oDatos.oDataReader[1].ToString(), oDatos.oDataReader[2].ToString(),
                            oDatos.oDataReader[3].ToString(), oDatos.oDataReader[4].ToString(), oDatos.oDataReader[5].ToString(), oDatos.oDataReader[6].ToString());
                    }
                    dtDirecciones = _oAspirante.Direcciones;
                    retValue = true;
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
            return retValue;
        }


        public bool LoadEstudiosAspirante(string idAspirante, out DataTable dtEstudios)
        {
            dtEstudios = new DataTable();
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs LoadEstudiosAspirante()");

            oDatos.Paquete("clk_aspirantes_efot.clp_consulta_educacion");
            oDatos.Parametro("pn_aspirante", idAspirante);
            oDatos.Parametro("c_educacion", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    _oAspirante = new Aspirante();
                    while (oDatos.oDataReader.Read())
                    {
                        _oAspirante.AddEducacion(oDatos.oDataReader[0].ToString(), oDatos.oDataReader[1].ToString(), oDatos.oDataReader[2].ToString(), oDatos.oDataReader[3].ToString(),
                            oDatos.oDataReader[4].ToString(), oDatos.oDataReader[5].ToString(), oDatos.oDataReader[6].ToString(), oDatos.oDataReader[7].ToString(), oDatos.oDataReader[8].ToString());
                    }
                    dtEstudios = _oAspirante.Estudios;
                    retValue = true;
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
            return retValue;
        }

        public bool LoadReferenciasAspirante(string idAspirante, out DataTable dtReferencias)
        {
            dtReferencias = new DataTable();
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs LoadEstudiosAspirante()");

            oDatos.Paquete("clk_aspirantes_efot.clp_consulta_referencias");
            oDatos.Parametro("pn_aspirante", idAspirante);
            oDatos.Parametro("c_referencias", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    _oAspirante = new Aspirante();
                    while (oDatos.oDataReader.Read())
                    {
                        _oAspirante.AddReferencia(oDatos.oDataReader[0].ToString(), oDatos.oDataReader[1].ToString(), oDatos.oDataReader[2].ToString(), oDatos.oDataReader[3].ToString(),
                            oDatos.oDataReader[4].ToString(), oDatos.oDataReader[5].ToString(), oDatos.oDataReader[6].ToString(), oDatos.oDataReader[7].ToString(), oDatos.oDataReader[8].ToString());
                    }
                    dtReferencias = _oAspirante.Referencias;
                    retValue = true;
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
            return retValue;
        }


        public bool LoadPruebasAdmision(string idAspirante, out DataTable dtPruebas)
        {
            dtPruebas = new DataTable();
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs LoadPruebasAdmision()");

            oDatos.Paquete("clk_aspirantes_efot.clp_retorna_actividad_asp");
            oDatos.Parametro("pn_aspirante", idAspirante);
            oDatos.Parametro("c_actividad_aspirante", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    _oAspirante = new Aspirante();
                    while (oDatos.oDataReader.Read())
                    {
                        _oAspirante.AddPruebaAdmision(oDatos.oDataReader[1].ToString(), oDatos.oDataReader[2].ToString(), oDatos.oDataReader[3].ToString(),
                            oDatos.oDataReader[4].ToString(), oDatos.oDataReader[5].ToString(), oDatos.oDataReader[6].ToString());
                    }
                    dtPruebas = _oAspirante.PruebasAdmision;
                    retValue = true;
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
            return retValue;
        }

        private DataTable InitDataTableAspirantesRegistrados()
        {
            DataTable dtAspirantes = new DataTable();
            dtAspirantes.Columns.Add("Código");
            dtAspirantes.Columns.Add("Cédula");
            dtAspirantes.Columns.Add("Nombres");
            dtAspirantes.Columns.Add("Apellidos");
            return dtAspirantes;
        }

        public DataTable AspirantesRegistrados(string codCriterioBusqueda, string dato, out int total)
        {
            DataTable dtAspReg = InitDataTableAspirantesRegistrados();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs AspirantesRegistrados()");

            oDatos.Paquete("clk_aspirantes_efot.clp_busqueda_aspirantes");
            oDatos.Parametro("pv_criterio", codCriterioBusqueda);
            oDatos.Parametro("pv_dato", dato);
            oDatos.Parametro("c_aspirantes", "R", 0, "O");
            oDatos.Parametro("pn_total", "N", 10, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");
            total = 0;
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (string.IsNullOrWhiteSpace(oDatos.RetornarParametro("pv_error").ToString()))
                    {
                        total = int.Parse(oDatos.RetornarParametro("pn_total").ToString());
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtAspReg.NewRow();
                            for (int i = 0; i < dtAspReg.Columns.Count; i++)
                            {
                                dr[i] = oDatos.oDataReader[i].ToString();
                            }
                            dtAspReg.Rows.Add(dr);
                        }
                    }
                    else
                        _error = oDatos.RetornarParametro("pv_error").ToString();
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
            return dtAspReg;
        }


        public DataTable AspirantesPorActividad(string codActividad, string idoneo)
        {
            DataTable dtAspReg = InitDataTableAspirantesRegistrados();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs AspirantesPorActividad()");

            oDatos.Paquete("clk_aspirantes_efot.clp_retorna_aspirantes_act");
            oDatos.Parametro("pv_actividad", codActividad);
            oDatos.Parametro("pv_idoneo", idoneo);
            oDatos.Parametro("c_aspirantes", "R", 0, "O");
            try
            {
                if (oDatos.Ejecutar("R"))
                {

                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtAspReg.NewRow();
                        for (int i = 0; i < dtAspReg.Columns.Count; i++)
                        {
                            dr[i] = oDatos.oDataReader[i].ToString();
                        }
                        dtAspReg.Rows.Add(dr);
                    }
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
            return dtAspReg;
        }


        public DataTable AspirantesPorRegistroActividad(string codRegAct)
        {
            DataTable dtAspReg = InitDataTableAspirantesRegistrados();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs AspirantesPorRegistroActividad()");

            oDatos.Paquete("clk_aspirantes_efot.clp_retorna_aspirantes_act2");
            oDatos.Parametro("pv_actividad", codRegAct);
            oDatos.Parametro("c_aspirantes", "R", 0, "O");
            try
            {
                if (oDatos.Ejecutar("R"))
                {

                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtAspReg.NewRow();
                        for (int i = 0; i < dtAspReg.Columns.Count; i++)
                        {
                            dr[i] = oDatos.oDataReader[i].ToString();
                        }
                        dtAspReg.Rows.Add(dr);
                    }
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
            return dtAspReg;
        }


        private DataTable InitDataTableConsActInstructores()
        {
            DataTable dtAct = new DataTable();
            dtAct.Columns.Add("Código");
            dtAct.Columns.Add("Cédula");
            dtAct.Columns.Add("Nombres");
            dtAct.Columns.Add("Actividad");
            dtAct.Columns.Add("Fecha de registro");
            dtAct.Columns.Add("Observación");
            return dtAct;
        }

        public DataTable ConsultaActividadesPorInstructor(string usuario, DateTime fecIni, DateTime fecFin, string codTipoAct, string codAct, string estado, out int total)
        {
            total = 0;
            DataTable dtReg = InitDataTableConsActInstructores();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs AspirantesRegistrados()");

            oDatos.Paquete("clk_aspirantes_efot.clp_reporte_instructores");
            oDatos.Parametro("pv_usuario", usuario);
            oDatos.Parametro("pd_fecha_ini", fecIni);
            oDatos.Parametro("pd_fecha_fin", fecFin);
            oDatos.Parametro("pv_tipo", codTipoAct);
            oDatos.Parametro("pv_tipo_actividad", codAct);
            oDatos.Parametro("pv_idoneo", estado);
            oDatos.Parametro("pn_total", "N", 10, "O");
            oDatos.Parametro("c_instructores", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (string.IsNullOrWhiteSpace(oDatos.RetornarParametro("pv_error").ToString()))
                    {
                        total = int.Parse(oDatos.RetornarParametro("pn_total").ToString());
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtReg.NewRow();
                            for (int i = 0; i < dtReg.Columns.Count; i++)
                            {
                                dr[i] = oDatos.oDataReader[i].ToString();
                            }
                            dtReg.Rows.Add(dr);
                        }
                    }
                    else
                        _error = oDatos.RetornarParametro("pv_error").ToString();
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
            return dtReg;
        }


        public string Error
        {
            get { return _error; }
        }

        public Aspirante DatosAspirante
        {
            get { return _oAspirante; }
        }


        public class Aspirante
        {
            private string _nombres, _apellidos, _identificacion, _email, _fechaNac, _sexo, _idoneo, _estadocivil, _cargasfamiliares,
                _PaisNac, _ProvinciaNac, _CiudadNac, _fechaRegistro, _estatura, _tiposangre, _peso, _tallacalzado,
                _tallapantalon, _tallacamisa, _tallagorra, _ideDactilar;
            private DataTable _direcciones, _estudios, _referencias, _pruebasAdmision;
            private string _dbUser;
            private string _dbPassword;
            private string _dbServer;
            private string _error;
            private string _user;
            private int _codigoAspirante;

            public Aspirante(string nombres, string apellidos, string identificacion, string email, string fechaNac, string sexo, string ideoneo, string estadocivil, string cargasfamiliares,
                string PaisNac, string ProvinciaNac, string CiudadNac, string fechaRegistro, string estatura, string tiposangre, string peso, string tallacalzado,
                string tallapantalon, string tallacamisa, string tallagorra, string ideDactilar)
            {
                _nombres = nombres;
                _apellidos = apellidos;
                _identificacion = identificacion;
                _email = email;
                _fechaNac = fechaNac;
                _sexo = sexo;
                _idoneo = ideoneo;
                _estadocivil = estadocivil;
                _cargasfamiliares = cargasfamiliares;
                _PaisNac = PaisNac;
                _ProvinciaNac = ProvinciaNac;
                _CiudadNac = CiudadNac;
                _fechaRegistro = fechaRegistro;
                _estatura = estatura;
                _tiposangre = tiposangre;
                _peso = peso;
                _tallacalzado = tallacalzado;
                _tallapantalon = tallapantalon;
                _tallacamisa = tallacamisa;
                _tallagorra = tallagorra;
                _ideDactilar = ideDactilar;
            }

            public Aspirante()
            {
            }

            public Aspirante(string dbUser, string dbPassword, string dbServer)
            {
                _dbUser = dbUser;
                _dbPassword = dbPassword;
                _dbServer = dbServer;
                _error = string.Empty;
            }

            public Aspirante(int codAspirante)
            {
                _codigoAspirante = codAspirante;
            }

            public Aspirante(int codAspirante, string currentUser, string dbUser, string dbPassword, string dbServer)
            {
                _codigoAspirante = codAspirante;
                _user = currentUser;
                _dbUser = dbUser;
                _dbPassword = dbPassword;
                _dbServer = dbServer;
                _error = string.Empty;
            }

            private DataTable InitDirecciones()
            {
                DataTable dtDir = new DataTable();
                dtDir.Columns.Add("Tipo de Ubicación");
                dtDir.Columns.Add("Dirección");
                dtDir.Columns.Add("Teléfono Conv.");
                dtDir.Columns.Add("Teléfomo Mov.");
                dtDir.Columns.Add("Referencia");
                dtDir.Columns.Add("Provincia");
                dtDir.Columns.Add("Ciudad");
                return dtDir;
            }

            public void AddDireccion(string tipoUbicac, string direccion, string telConv, string telMov, string referencia, string provincia, string ciudad)
            {
                if (_direcciones == null)
                    _direcciones = InitDirecciones();
                DataRow dr = _direcciones.NewRow();
                dr[0] = tipoUbicac;
                dr[1] = direccion;
                dr[2] = telConv;
                dr[3] = telMov;
                dr[4] = referencia;
                dr[5] = provincia;
                dr[6] = ciudad;
                _direcciones.Rows.Add(dr);
            }


            private DataTable InitEducacion()
            {
                DataTable dtEducac = new DataTable();
                dtEducac.Columns.Add("Tipo");
                dtEducac.Columns.Add("País");
                dtEducac.Columns.Add("Provincia");
                dtEducac.Columns.Add("Ciudad");
                dtEducac.Columns.Add("Institución");
                dtEducac.Columns.Add("Título");
                dtEducac.Columns.Add("Nota");
                dtEducac.Columns.Add("Año");
                dtEducac.Columns.Add("Observación");
                return dtEducac;
            }

            public void AddEducacion(string institucion, string titulo, string nota, string anio, string observacion, string tipo, string pais, string provincia, string ciudad)
            {
                if (_estudios == null)
                    _estudios = InitEducacion();
                DataRow dr = _estudios.NewRow();
                dr[0] = tipo;
                dr[1] = pais;
                dr[2] = provincia;
                dr[3] = ciudad;
                dr[4] = institucion;
                dr[5] = titulo;
                dr[6] = nota;
                dr[7] = anio;
                dr[8] = observacion;
                _estudios.Rows.Add(dr);
            }

            private DataTable InitReferencias()
            {
                DataTable dtRef = new DataTable();
                dtRef.Columns.Add("Tipo");
                dtRef.Columns.Add("Identificación");
                dtRef.Columns.Add("Nombres");
                dtRef.Columns.Add("Apellidos");
                dtRef.Columns.Add("Provincia");
                dtRef.Columns.Add("Ciudad");
                dtRef.Columns.Add("Dirección");
                dtRef.Columns.Add("Teléfono");
                dtRef.Columns.Add("Observación");
                return dtRef;
            }

            public void AddReferencia(string nombres, string apellidos, string identificacion, string direccion, string telefono, string observacion, string tipo, string provincia, string ciudad)
            {
                if (_referencias == null)
                    _referencias = InitReferencias();
                DataRow dr = _referencias.NewRow();
                dr[0] = tipo;
                dr[1] = identificacion;
                dr[2] = nombres;
                dr[3] = apellidos;
                dr[4] = provincia;
                dr[5] = ciudad;
                dr[6] = direccion;
                dr[7] = telefono;
                dr[8] = observacion;
                _referencias.Rows.Add(dr);
            }

            private DataTable InitPruebasAdmision()
            {
                DataTable dtRef = new DataTable();
                dtRef.Columns.Add("Nombres");
                dtRef.Columns.Add("Actividad");
                dtRef.Columns.Add("Fecha de registro");
                dtRef.Columns.Add("Fecha de convocatoria");
                dtRef.Columns.Add("Idóneo");
                dtRef.Columns.Add("Observación");
                return dtRef;
            }

            public void AddPruebaAdmision(string nombres, string actividad, string fecha_registro, string fecha_convocatoria, string idoneo, string observacion)
            {
                if (_pruebasAdmision == null)
                    _pruebasAdmision = InitPruebasAdmision();
                DataRow dr = _pruebasAdmision.NewRow();
                dr[0] = nombres;
                dr[1] = actividad;
                dr[2] = fecha_registro;
                dr[3] = fecha_convocatoria;
                dr[4] = idoneo;
                dr[5] = observacion;
                _pruebasAdmision.Rows.Add(dr);
            }


            public bool InsertEstudioOnDB(int codAspirante, string tipoEducacion, string codPais, string codProvincia, string codCiudad,
                string institucion, string titulo, float nota, int anio, string observacion)
            {
                bool retValue = false;
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs RegistrarAspirante()");

                oDatos.Paquete("clk_aspirantes_efot.clp_datos_educacion");
                oDatos.Parametro("pn_aspirante", codAspirante);
                oDatos.Parametro("pv_tipo_educacion", tipoEducacion);
                oDatos.Parametro("pv_pais", codPais);
                oDatos.Parametro("pv_provincia", codProvincia);
                oDatos.Parametro("pv_localidad", codCiudad);
                oDatos.Parametro("pv_institucion", institucion);
                oDatos.Parametro("pv_titulo_obtenido", titulo);
                oDatos.Parametro("pn_nota_grado", nota);
                oDatos.Parametro("pn_anio_grado", anio);
                oDatos.Parametro("pv_observacion", observacion);
                oDatos.Parametro("pv_error", "V", 300, "O");

                try
                {
                    _error = string.Empty;
                    if (oDatos.Ejecutar("R"))
                    {
                        string error = oDatos.RetornarParametro("pv_error").ToString();
                        if (string.IsNullOrWhiteSpace(error))
                            retValue = true;
                        else
                            _error = error;
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
                return retValue;
            }


            public bool InsertReferenciaOnDB(int codAspirante, string tipoRef, string identificacion, string nombres,
                string apellidos, string codProvincia, string codCiudad, string direccion, string telefono, string observacion)
            {
                bool retValue = false;
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs InsertReferenciaOnDB()");

                oDatos.Paquete("clk_aspirantes_efot.clp_datos_referencias");
                oDatos.Parametro("pn_aspirante", codAspirante);
                oDatos.Parametro("pv_tipo_referencia", tipoRef);
                oDatos.Parametro("pv_identificacion", identificacion);
                oDatos.Parametro("pv_apellidos", apellidos);
                oDatos.Parametro("pv_nombres", nombres);
                oDatos.Parametro("pv_provincia", codProvincia);
                oDatos.Parametro("pv_localidad", codCiudad);
                oDatos.Parametro("pv_direccion", direccion);
                oDatos.Parametro("pv_telefono", telefono);
                oDatos.Parametro("pv_observacion", observacion);
                oDatos.Parametro("pv_error", "V", 300, "O");

                try
                {
                    _error = string.Empty;
                    if (oDatos.Ejecutar("R"))
                    {
                        string error = oDatos.RetornarParametro("pv_error").ToString();
                        if (string.IsNullOrWhiteSpace(error))
                            retValue = true;
                        else
                            _error = error;
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
                return retValue;
            }


            public bool InsertDireccionOnDB(int codAspirante, string tipoUbicac, string codProvincia, string codCiudad, string direccion, string telefono, string telefonoMovil, string referencia)
            {
                bool retValue = false;
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs InsertDireccionOnDB()");

                oDatos.Paquete("clk_aspirantes_efot.clp_datos_ubicacion");
                oDatos.Parametro("pn_aspirante", codAspirante);
                oDatos.Parametro("pv_tipo_ubicacion", tipoUbicac);
                oDatos.Parametro("pv_provincia", codProvincia);
                oDatos.Parametro("pv_localidad", codCiudad);
                oDatos.Parametro("pv_direccion", direccion);
                oDatos.Parametro("pv_telefono", telefono);
                oDatos.Parametro("pv_mobil", telefono);
                oDatos.Parametro("pv_referencia", referencia);
                oDatos.Parametro("pv_error", "V", 300, "O");

                try
                {
                    _error = string.Empty;
                    if (oDatos.Ejecutar("R"))
                    {
                        string error = oDatos.RetornarParametro("pv_error").ToString();
                        if (string.IsNullOrWhiteSpace(error))
                            retValue = true;
                        else
                            _error = error;
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
                return retValue;
            }


            public bool InsertActividadOnDB(string tipoActividad, DateTime fecha, string hora, string observacion,
                string descActividad, string lugar, string nombreAsp, string emailAsp, out bool emailSent)
            {
                emailSent = false;
                bool retValue = false;
                _error = string.Empty;
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs InsertActividadOnDB()");

                try
                {
                    oDatos.Paquete("clk_aspirantes_efot.clp_registra_actividad");
                    oDatos.Parametro("pn_aspirante", _codigoAspirante);
                    oDatos.Parametro("pv_tipo", tipoActividad);
                    oDatos.Parametro("pd_fecha", fecha);
                    oDatos.Parametro("pv_hora", hora);
                    oDatos.Parametro("pv_usuario", _user);
                    oDatos.Parametro("pv_observacion", observacion);
                    oDatos.Parametro("pv_error", "V", 300, "O");


                    if (oDatos.Ejecutar("R"))
                    {
                        string error = oDatos.RetornarParametro("pv_error").ToString();
                        if (string.IsNullOrWhiteSpace(error))
                            retValue = true;
                        else
                            _error = error;
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
                if (retValue)
                {
                    emailSent = SendEmailNotification(emailAsp, "Notificación de Actividad Previo ingreso a EFOT",
                        RevisionEmailBodyRegistro(nombreAsp, descActividad, fecha.ToString("dd/MM/yyyy"), hora, lugar, observacion, tipoActividad));
                }
                return retValue;
            }


            public bool InsertRevisionActividadOnDB(string tipoActividad, string estado, string observacion, string descActividad, string nombreAsp, string emailAsp, out bool emailSent)
            {
                bool retValue = false;
                emailSent = false;
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs InsertRevisionActividadOnDB()");

                oDatos.Paquete("clk_aspirantes_efot.clp_revisa_actividad");
                oDatos.Parametro("pn_aspirante", _codigoAspirante);
                oDatos.Parametro("pv_tipo", tipoActividad);
                oDatos.Parametro("pv_usuario", _user);
                oDatos.Parametro("pv_estado", estado);
                oDatos.Parametro("pv_observacion", observacion);
                oDatos.Parametro("pv_error", "V", 300, "O");

                try
                {
                    _error = string.Empty;
                    if (oDatos.Ejecutar("R"))
                    {
                        string error = oDatos.RetornarParametro("pv_error").ToString();
                        if (string.IsNullOrWhiteSpace(error))
                            retValue = true;
                        else
                            _error = error;
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

                if (retValue)
                {
                    emailSent = SendEmailNotification(emailAsp, "Notificación de Revisión",
                        RevisionEmailBodyRevision(nombreAsp, estado, descActividad, observacion));
                }

                return retValue;
            }


            private bool SendEmailNotification(string email, string subject, string body)
            {
                try
                {
                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                    correo.From = new System.Net.Mail.MailAddress(Constantes.ParametrosEnvioEmail.mailAddress);
                    correo.To.Add(email);
                    correo.Bcc.Add("vruales@cte.gob.ec, jintriagom@cte.gob.ec");
                    correo.Subject = subject;
                    correo.Body = body;
                    correo.IsBodyHtml = true;
                    correo.Priority = System.Net.Mail.MailPriority.High;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.Host = Constantes.ParametrosEnvioEmail.smtpHost;
                    smtp.Credentials = new System.Net.NetworkCredential(Constantes.ParametrosEnvioEmail.smtpUser, Constantes.ParametrosEnvioEmail.smptPassword);
                    smtp.Send(correo);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            private string RevisionEmailBodyRevision(string nombres, string estado, string nombreActividad, string observacion)
            {
                string descEstado = (estado == "S") ? "<b>idóneo</b>" : "<b>no idóneo</b>";
                string body = "Sr. " + nombres + " aspirante a ingresar a la EFOT.<br /><br /> Su " + nombreActividad + " ha sido calificado como  " + descEstado + ".";
                if (!string.IsNullOrWhiteSpace(observacion))
                    body += "<br /><br />Observación: " + observacion;
                body += "<br /><br /><b><i>Escuela de Formación de Aspirantes y Tropas</i></b>";
                return body;
            }

            private string RevisionEmailBodyRegistro(string nombres, string nombreActividad, string fecha, string hora, string lugar, string observacion, string codActividad)
            {
                string body = "Sr. " + nombres + " aspirante a ingresar a la EFOT.";
                if (codActividad == "RGMED")
                {
                    body += "DEBE PRESENTARSE EN LOS SIGUIENTES LUGARES Y FECHAS PARA LA REVALORACIÓN MÉDICA:<br /><br />";
                    body += "<b>OFTALMOLOGÍA: (OJOS)</b><br /><b>FECHA:</b> Jueves 5 de enero de 2012 <br /><b>HORA:</b> 11H00<br /><b>LUGAR:</b> Medikal Agencia Norte Av. Guillermo Pareja Rolando (que es la Av. Principal Garzota cc Garzocentro 2000).<br /><br />";
                    body += "Deberá llevar: Cédula de identidad";
                    body += "<b>EVALUACIÓN TRAUMATOLÓGICA (COLUMNA Y DEMÁS)</b><br /><b>FECHA:</b> Viernes 6 de enero de 2012 <br /><b>HORA:</b> 08H00<br /><b>LUGAR:</b> Hospital de la FAE<br /><br />Deberá llevar: Cédula de Identidad";
                    body += "<b>SE RUEGA PUNTUALIDAD YA QUE DE LO CONTRARIO NO SE REALIZARÁN LAS PRUEBAS.<br /><br />ESTAS PRUEBAS SON RESULTADO DE SUS APELACIONES.</b>";
                }
                else
                {
                    if (codActividad == "RGCON")
                    {
                        body += "Señor aspirante su esfuerzo ha sido recompensado, en todas las actividades del proceso de selección usted ha sido declarado idóneo, por lo tanto tendrá que asistir a una reunión previo el acuartelamiento al curso de aspirantes a vigilantes de la cuadragésima primera promoción de la Comisión de Tránsito del Ecuador.";
                        body += "<br /><br />Por favor ser puntual al asistir a la hora y fecha indicada en la observación.<br /><br />Lugar: Escuela de Formación y Oficiales Tropa de la CTE. ";
                        body += "<br />Fecha: " + fecha + " " + hora;
                        body += "<br /><br />Sr. Aspirante los implementos, uniformes y seguro puede adquirirlos donde crea más conveniente,  nadie está autorizado.";
                    }
                    else
                    {
                        if (codActividad == "RGACU")
                        {
                            body += "Señor Aspirante usted deberá presentarse junto con sus padres (representantes) para prácticas de ceremonia de ingreso el día  jueves 1 de marzo a las 09h00 y el día viernes 2 de marzo a la misma hora deberán venir solo los aspirantes con sus respectivas cajas, las mismas que deberán contener sus uniformes e implementos que van a utilizar para el curso de formación;";
                            body += "<br /><br />Fecha de acuartelamiento: Lunes 5 de Marzo del 2012.<br /><br />Lugar: EFOT ( personal designado a Milagro se lo enviará en buses de la CTE) ";
                        }
                        else
                        {
                            body += "<br /><br /> Debe presentarse a " + nombreActividad + ", ";
                            body += "el " + fecha + " a las " + hora + " en " + lugar + ". ";
                            if (!string.IsNullOrWhiteSpace(observacion))
                                body += "<br /><br />Observación: " + observacion;
                            if (codActividad == "RGSIC")
                                body += "<br /><br /><b>No olvide que al presentarse a esta prueba debe ir con una actitud tranquila, sereno y descansado.</b>";
                            else if (codActividad == "RGFIS")
                            {
                                body += "<br /><br />SEÑOR ASPIRANTE USTED HA SIDO CONVOCADO PARA LA PRUEBA FISICA ANTES DE CONCURRIR POR FAVOR DESCARGAR DE LA PAGINA DE LA EFOT (www.cte.gob.ec/EFOT) EL INSTRUCTIVO DE EDUCACION FISICA, DEPORTE Y RECREACION DE LA CTE";
                            }
                        }
                    }
                }
                body += "<br /><br /><b><i>Escuela de Formación de Aspirantes y Tropas</i></b>";
                return body;
            }



            public bool ReversarCalificacion(string numCedula)
            {
                bool retValue = false;
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.EFOT.cs Aspirante.ReversarCalificacion()");

                oDatos.Paquete("clk_aspirantes_efot.clp_actualiza_estado_aspirante");
                oDatos.Parametro("pv_identificacion", numCedula);
                oDatos.Parametro("pv_error", "V", 300, "O");

                try
                {
                    _error = string.Empty;
                    if (oDatos.Ejecutar("R"))
                    {
                        string error = oDatos.RetornarParametro("pv_error").ToString();
                        if (string.IsNullOrWhiteSpace(error))
                            retValue = true;
                        else
                            _error = error;
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
                return retValue;
            }


            public string Error
            {
                get { return _error; }
            }


            public string TallaGorra
            {
                get { return _tallagorra; }
            }

            public string TallaCamisa
            {
                get { return _tallacamisa; }
            }

            public string TallaPantalon
            {
                get { return _tallapantalon; }
            }

            public string TallaCalzado
            {
                get { return _tallacalzado; }
            }

            public string Peso
            {
                get { return _peso; }
            }

            public string TipoSangre
            {
                get { return _tiposangre; }
            }

            public string Estatura
            {
                get { return _estatura; }
            }

            public string FechaRegistro
            {
                get { return _fechaRegistro; }
            }

            public string CiudadNac
            {
                get { return _CiudadNac; }
            }

            public string ProvinciaNac
            {
                get { return _ProvinciaNac; }
            }

            public string PaisNac
            {
                get { return _PaisNac; }
            }

            public string CargasFamiliares
            {
                get { return _cargasfamiliares; }
            }

            public string EstadoCivil
            {
                get { return _estadocivil; }
            }

            public string Idoneo
            {
                get { return _idoneo; }
            }

            public string Sexo
            {
                get { return _sexo; }
            }

            public string FechaNac
            {
                get { return _fechaNac; }
            }

            public string Identificacion
            {
                get { return _identificacion; }
            }

            public string Apellidos
            {
                get { return _apellidos; }
            }

            public string Nombres
            {
                get { return _nombres; }
            }

            public string Email
            {
                get { return _email; }
            }

            public int CodigoAspirante
            {
                get { return _codigoAspirante; }
            }

            public string IdeDactilar
            {
                get { return _ideDactilar; }
            }

            public DataTable Direcciones
            {
                get
                {
                    if (_direcciones == null)
                        return InitDirecciones();
                    else
                        return _direcciones;
                }
            }

            public DataTable Estudios
            {
                get
                {
                    if (_estudios == null)
                        return InitEducacion();
                    else
                        return _estudios;
                }
            }

            public DataTable Referencias
            {
                get
                {
                    if (_referencias == null)
                        return InitReferencias();
                    else
                        return _referencias;
                }
            }

            public DataTable PruebasAdmision
            {
                get
                {
                    if (_pruebasAdmision == null)
                        return InitPruebasAdmision();
                    else
                        return _pruebasAdmision;
                }
            }
        }
    }
}
