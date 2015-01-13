using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MatriculacionCNTTTSV
{
    public class MatriculaCNTTTSV
    {
        private string _dbUser;
        private string _dbUserPwd;
        private string _dbTNS;
        private string _error;
        private DatosMatricula _datosMatricula;
        

        public MatriculaCNTTTSV(string _dbUser, string _dbUserPwd, string _dbTNS)
        {
            this._dbUser = _dbUser;
            this._dbUserPwd = _dbUserPwd;
            this._dbTNS = _dbTNS;
        }


        public bool RegistrarMatriculaNueva(string placa, string chasis, string identificacion, string direccion, string telefono, string codCantonProp, string numEspecieAnt,
            string numEspecieNva, string codBanco, string fechaPago, string valorPagado, string numDocumento, string codAgencia, string usuario, string observacion, 
            string fechaCaducidad, string tipoCobro, 
            out string especieAnt, out string especieNueva,
            out string tramite, out string outChasis, out string outPlaca, out string hayError
            )
        {
            bool retValue = false;
            outPlaca = string.Empty;
            outChasis = string.Empty;
            especieAnt = string.Empty;
            especieNueva = string.Empty;
            tramite = string.Empty;
            hayError = string.Empty;

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbUserPwd, _dbTNS, "MatriculacionCNTTTSV.VehiculoCNTTTSV.cs RegistrarMatriculaNueva()");
            oDatos.Paquete("web_corpaire_ctg.inserta_matricula_corpaire2");
            oDatos.Parametro("pv_placa", "V", 8, "A", placa.Trim().ToUpper());
            oDatos.Parametro("pv_chasis", "V", 30, "A", chasis.Trim().ToUpper());
            oDatos.Parametro("pv_identificacion", identificacion.Trim());
            oDatos.Parametro("pv_direccion", direccion.Trim().ToUpper());
            oDatos.Parametro("pv_telefono", telefono.Trim().ToUpper());
            oDatos.Parametro("pv_canton_prop", codCantonProp.Trim().ToUpper());
            oDatos.Parametro("pv_num_especie_ant", numEspecieAnt.Trim().ToUpper());
            oDatos.Parametro("pv_num_especie_nueva", numEspecieNva.Trim().ToUpper());
            oDatos.Parametro("pv_cod_banco", codBanco.Trim().ToUpper());
            oDatos.Parametro("pv_fecha_pago", fechaPago.Trim());
            oDatos.Parametro("pn_valor_pagado", valorPagado);
            oDatos.Parametro("pv_num_documento", numDocumento.Trim().ToUpper());
            oDatos.Parametro("pv_cod_agencia", codAgencia.Trim().ToUpper());
            oDatos.Parametro("pv_usuario", usuario.Trim());
            oDatos.Parametro("pv_observacion", observacion.Trim());
            oDatos.Parametro("pv_fecha_caducidad", fechaCaducidad.Trim());
            oDatos.Parametro("pv_tipo_cobro", tipoCobro);
            oDatos.Parametro("pv_especie_ant", "V", 100, "O");
            oDatos.Parametro("pv_especie_nueva", "V", 100, "O");
            oDatos.Parametro("pn_tramite", "N", 5, "O");
            oDatos.Parametro("pv_error", "V", 500, "O");
            oDatos.Parametro("pn_error", "N", 5, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    hayError = oDatos.RetornarParametro("pn_error").ToString();
                    if (oDatos.RetornarParametro("pn_error").ToString()=="0")
                    {
                        outPlaca = oDatos.RetornarParametro("pv_placa").ToString();
                        outChasis = oDatos.RetornarParametro("pv_chasis").ToString();
                        especieAnt = oDatos.RetornarParametro("pv_especie_ant").ToString();
                        especieNueva = oDatos.RetornarParametro("pv_especie_nueva").ToString();
                        tramite = oDatos.RetornarParametro("pn_tramite").ToString();
                        retValue = true;
                    }
                    else
                    {
                        _error = oDatos.RetornarParametro("pv_error").ToString();
                        especieAnt = oDatos.RetornarParametro("pv_especie_ant").ToString();
                        especieNueva = oDatos.RetornarParametro("pv_especie_nueva").ToString();
                        tramite = oDatos.RetornarParametro("pn_tramite").ToString();
                        retValue = true;
                    }
                }
                else
                {
                    _error = oDatos.Mensaje;
                }
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


        /*public bool UltimaMatricula(string placa, string chasis, string camv, out string anio,
            out string valorPagado)
        {
            bool retValue = false;
            anio = string.Empty;
            valorPagado = string.Empty;
            _error = string.Empty;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbUserPwd, _dbTNS, "MatriculacionCNTTTSV.VehiculoCNTTTSV.cs LoadDatosVehiculo()");
            oDatos.Paquete("web_corpaire_ctg.consulta_datos_matricula");
            oDatos.Parametro("pv_placa", placa.ToUpper());
            oDatos.Parametro("pv_chasis", chasis.ToUpper());
            oDatos.Parametro("pv_camv", camv.ToUpper());
            oDatos.Parametro("pn_anio_mat", "N", 4, "O");
            oDatos.Parametro("pn_valor_mat", "F", 8, "O");
            oDatos.Parametro("pn_error", "N", 4, "O");
            oDatos.Parametro("pv_mensaje_error", "V", 2000, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pn_error").ToString() == "0")
                    {
                        anio = oDatos.RetornarParametro("pn_anio_mat").ToString();
                        valorPagado = oDatos.RetornarParametro("pn_valor_mat").ToString();
                        retValue = true;
                    }
                    else
                        _error = oDatos.RetornarParametro("pv_mensaje_error").ToString();
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
        }*/


        public bool LoadDatosMatricula(string placa, string chasis, string camv)
        {
            bool retValue = false;
            _error = string.Empty;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbUserPwd, _dbTNS, "MatriculacionCNTTTSV.VehiculoCNTTTSV.cs LoadDatosVehiculo()");
            oDatos.Paquete("web_corpaire_ctg.consulta_datos_mat_1");
            oDatos.Parametro("pv_placa", placa.ToUpper());
            oDatos.Parametro("pv_chasis", chasis.ToUpper());
            oDatos.Parametro("pv_camv", camv.ToUpper());
            oDatos.Parametro("c_vehiculos", "R", 0, "O");
            oDatos.Parametro("d_tasas", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 500, "O");
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (string.IsNullOrEmpty(oDatos.RetornarParametro("pv_error").ToString()))
                    {
                        System.Data.DataSet ds = oDatos.RetornarDatosCursores();
                        _datosMatricula = new DatosMatricula(ds.Tables[0].Rows[0]["pv_placa"].ToString(), ds.Tables[0].Rows[0]["pv_chasis"].ToString(), ds.Tables[0].Rows[0]["pv_camv"].ToString(), ds.Tables[0].Rows[0]["pn_anio_mat"].ToString(),
                            ds.Tables[0].Rows[0]["pn_valor_mat"].ToString(), ds.Tables[0].Rows[0]["pn_anio_prod"].ToString(),
                            ds.Tables[0].Rows[0]["pn_asientos"].ToString(), ds.Tables[0].Rows[0]["pn_avaluo"].ToString(), ds.Tables[0].Rows[0]["pv_canton"].ToString(),
                            ds.Tables[0].Rows[0]["pv_carroceria"].ToString(), ds.Tables[0].Rows[0]["pn_cilindraje"].ToString(), ds.Tables[0].Rows[0]["pv_clase"].ToString(),
                            ds.Tables[0].Rows[0]["pv_tipo"].ToString(), ds.Tables[0].Rows[0]["pv_color"].ToString(),
                            ds.Tables[0].Rows[0]["pv_combustible"].ToString(), ds.Tables[0].Rows[0]["pv_cooperativa"].ToString(),
                            ds.Tables[0].Rows[0]["pn_disco"].ToString(), ds.Tables[0].Rows[0]["pd_fecha_compra"].ToString(),
                            ds.Tables[0].Rows[0]["pv_marca"].ToString(), ds.Tables[0].Rows[0]["pv_modalidad"].ToString(),
                            ds.Tables[0].Rows[0]["pv_modelo"].ToString(), ds.Tables[0].Rows[0]["pv_motor"].ToString(),
                            ds.Tables[0].Rows[0]["pv_pais_origen"].ToString(), ds.Tables[0].Rows[0]["pv_placa_ant"].ToString(),
                            ds.Tables[0].Rows[0]["pd_registro"].ToString(), ds.Tables[0].Rows[0]["pv_servicio"].ToString(),
                            ds.Tables[0].Rows[0]["pn_tonelaje"].ToString(), ds.Tables[0].Rows[0]["pd_mat_anterior"].ToString(),
                            ds.Tables[0].Rows[0]["pd_caducidad"].ToString(), ds.Tables[0].Rows[0]["pv_digitador"].ToString(),
                            ds.Tables[0].Rows[0]["pd_emision"].ToString(), ds.Tables[0].Rows[0]["pv_emision_hora"].ToString(),
                            ds.Tables[0].Rows[0]["pv_gravamen"].ToString(), ds.Tables[0].Rows[0]["pn_matvalor"].ToString(),
                            ds.Tables[0].Rows[0]["pv_cedula"].ToString(), ds.Tables[0].Rows[0]["pv_propietario"].ToString(),
                            ds.Tables[0].Rows[0]["pv_domicilio"].ToString(), ds.Tables[0].Rows[0]["pv_telefono"].ToString(), ds.Tables[1]);
                        retValue = true;
                        /*if (oDatos.oDataReader.Read())
                        {
                            _datosMatricula = new DatosMatricula(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_placa")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_chasis")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_camv")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pn_anio_mat")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pn_valor_mat")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pn_anio_prod")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pn_asientos")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pn_avaluo")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_canton")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_carroceria")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pn_cilindraje")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_clase")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_tipo")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_color")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_combustible")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_cooperativa")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pn_disco")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pd_fecha_compra")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_marca")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_modalidad")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_modelo")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_motor")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_pais_origen")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_placa_ant")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pd_registro")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_servicio")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pn_tonelaje")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pd_mat_anterior")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pd_caducidad")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_digitador")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pd_emision")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_emision_hora")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_gravamen")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pn_matvalor")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_cedula")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_propietario")).ToString(),
                                oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_domicilio")).ToString(), oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("pv_telefono")).ToString());
                            retValue = true;
                        }
                        else
                            _error = "No existen datos de pago de matrícula";*/
                        
                    }
                    else
                        _error = oDatos.RetornarParametro("pv_error").ToString();
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }
            /*oDatos.Paquete("web_corpaire_ctg.consulta_datos_matricula");
            oDatos.Parametro("pv_placa", "V", 30, "A", placa.ToUpper());
            oDatos.Parametro("pv_chasis", "V", 30, "A", string.Empty);
            oDatos.Parametro("pv_camv", "V", 30, "A", string.Empty);
            oDatos.Parametro("pn_anio_mat", "N", 50, "O");
            oDatos.Parametro("pn_valor_mat", "F", 10, "O");
            oDatos.Parametro("pn_anio_prod", "N", 50, "O");
            oDatos.Parametro("pn_asientos", "N", 50, "O");
            oDatos.Parametro("pn_avaluo", "N", 50, "O");
            oDatos.Parametro("pv_canton", "V", "O");
            oDatos.Parametro("pv_carroceria", "V", "5", "O");
            oDatos.Parametro("pn_cilindraje", "N", 50, "O");
            oDatos.Parametro("pv_clase", "V", 10, "O");
            oDatos.Parametro("pv_tipo", "V", 10, "O");
            oDatos.Parametro("pv_color", "V", 10, "O");
            oDatos.Parametro("pv_combustible", "V", 10, "O");
            oDatos.Parametro("pv_cooperativa", "V", 100, "O");
            oDatos.Parametro("pn_disco", "N", 50, "O");
            oDatos.Parametro("pd_fecha_compra", "D", 10, "O");
            oDatos.Parametro("pv_marca", "V", 30, "O");
            oDatos.Parametro("pv_modalidad", "V", 30, "O");
            oDatos.Parametro("pv_modelo", "V", 30, "O");
            oDatos.Parametro("pv_motor", "V", 30, "O");
            oDatos.Parametro("pv_pais_origen", "V", 30, "O");
            oDatos.Parametro("pv_placa_ant", "V", 30, "O");
            oDatos.Parametro("pd_registro", "D", 10, "O");
            oDatos.Parametro("pv_servicio", "V", 30, "O");
            oDatos.Parametro("pn_tonelaje", "N", 50, "O");
            oDatos.Parametro("pd_mat_anterior", "D", 10, "O");
            oDatos.Parametro("pd_caducidad", "D", 10, "O");
            oDatos.Parametro("pv_digitador", "V", 30, "O");
            oDatos.Parametro("pd_emision", "D", 10, "O");
            oDatos.Parametro("pv_emision_hora", "V", 20, "O");
            oDatos.Parametro("pv_gravamen", "V", 5, "O");
            oDatos.Parametro("pn_matvalor", "N", 50, "O");
            oDatos.Parametro("pv_cedula", "V", 30, "O");
            oDatos.Parametro("pv_propietario", "V", 80, "O");
            oDatos.Parametro("pv_domicilio", "V", 100, "O");
            oDatos.Parametro("pv_telefono", "V", 30, "O");
            oDatos.Parametro("pn_error", "N", 50, "O");
            oDatos.Parametro("pv_mensaje_error", "V", 2000, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pn_error").ToString() == "0")
                    {
                        _datosMatricula = new DatosMatricula(placa, chasis, camv, oDatos.RetornarParametro("pn_anio_mat").ToString(), oDatos.RetornarParametro("pn_valor_mat").ToString(),
                            oDatos.RetornarParametro("pn_anio_prod").ToString(), oDatos.RetornarParametro("pn_asientos").ToString(), oDatos.RetornarParametro("pn_avaluo").ToString(), oDatos.RetornarParametro("pv_canton").ToString(),
                            oDatos.RetornarParametro("pv_carroceria").ToString(), oDatos.RetornarParametro("pv_cilindraje").ToString(), oDatos.RetornarParametro("pv_clase").ToString(), oDatos.RetornarParametro("pv_tipo").ToString(),
                            oDatos.RetornarParametro("pv_color").ToString(), oDatos.RetornarParametro("pv_combustible").ToString(), oDatos.RetornarParametro("pv_cooperativa").ToString(), oDatos.RetornarParametro("pn_disco").ToString(),
                            oDatos.RetornarParametro("pd_fecha_compra").ToString(), oDatos.RetornarParametro("pv_marca").ToString(), oDatos.RetornarParametro("pv_modalidad").ToString(), oDatos.RetornarParametro("pv_modelo").ToString(),
                            oDatos.RetornarParametro("pv_motor").ToString(), oDatos.RetornarParametro("pv_pais_origen").ToString(), oDatos.RetornarParametro("pv_placa_ant").ToString(), oDatos.RetornarParametro("pd_registro").ToString(),
                            oDatos.RetornarParametro("pv_servicio").ToString(), oDatos.RetornarParametro("pn_tonelaje").ToString(), oDatos.RetornarParametro("pd_mat_anterior").ToString(), oDatos.RetornarParametro("pd_caducidad").ToString(),
                            oDatos.RetornarParametro("pv_digitador").ToString(), oDatos.RetornarParametro("pd_emision").ToString(), oDatos.RetornarParametro("pv_emision_hora").ToString(), oDatos.RetornarParametro("pv_gravamen").ToString(),
                            oDatos.RetornarParametro("pn_matvalor").ToString(), oDatos.RetornarParametro("pv_cedula").ToString(), oDatos.RetornarParametro("pv_propietario").ToString(), oDatos.RetornarParametro("pv_domicilio").ToString(),
                            oDatos.RetornarParametro("pv_telefono").ToString());
                        retValue = true;
                    }
                    else
                        _error = oDatos.RetornarParametro("pv_mensaje_error").ToString();
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
            }*/

            return retValue;
        }


        public bool LoadDatosMatricula2(string placa, string chasis, string camv, string claseTipo, string ejes, string cantonCirc)
        {
            bool retValue = false;
            _error = string.Empty;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbUserPwd, _dbTNS, "MatriculacionCNTTTSV.VehiculoCNTTTSV.cs LoadDatosMatricula2()");
            //oDatos.Paquete("web_corpaire_ctg.consulta_datos_matricula");
            oDatos.Paquete("consulta_datos_matricula");
            #region Parámetros
            oDatos.Parametro("pv_placa", "V", 30, "A", placa.ToUpper());
            oDatos.Parametro("pv_chasis", "V", 30, "A", chasis);
            oDatos.Parametro("pv_camv", "V", 30, "A", camv);
            oDatos.Parametro("pn_clase_tipo", claseTipo);
            oDatos.Parametro("pn_ejes", ejes);
            oDatos.Parametro("pv_canton_circ", cantonCirc);
            //oDatos.Parametro("pn_anio_mat", "N", 50, "O");
            oDatos.Parametro("pn_valor_mat", "F", 10, "O");
            oDatos.Parametro("pn_anio_prod", "N", 50, "O");
            oDatos.Parametro("pn_asientos", "V", 50, "O");
            oDatos.Parametro("pn_avaluo", "V", 50, "O");
            //oDatos.Parametro("pv_canton", "V", "O");
            oDatos.Parametro("pv_carroceria", "V", 5, "O");
            oDatos.Parametro("pn_cilindraje", "V", 50, "O");
            oDatos.Parametro("pv_clase", "V", 10, "O");
            oDatos.Parametro("pv_tipo", "V", 20, "O");
            oDatos.Parametro("pv_color", "V", 10, "O");
            oDatos.Parametro("pv_color2", "V", 10, "O");
            oDatos.Parametro("pv_combustible", "V", 10, "O");
            oDatos.Parametro("pv_cooperativa", "V", 100, "O");
            oDatos.Parametro("pn_disco", "V", 50, "O");
            //oDatos.Parametro("pd_fecha_compra", "D", 10, "O");
            oDatos.Parametro("pv_marca", "V", 30, "O");
            //oDatos.Parametro("pv_modalidad", "V", 30, "O");
            oDatos.Parametro("pv_modelo", "V", 30, "O");
            oDatos.Parametro("pv_motor", "V", 30, "O");
            oDatos.Parametro("pv_pais_origen", "V", 30, "O");
            oDatos.Parametro("pv_placa_ant", "V", 30, "O");
            //oDatos.Parametro("pd_registro", "D", 10, "O");
            oDatos.Parametro("pv_servicio", "V", 30, "O");
            oDatos.Parametro("pn_tonelaje", "V", 50, "O");
            oDatos.Parametro("pd_mat_anterior", "V", 25, "O");
            /*oDatos.Parametro("pd_caducidad", "D", 10, "O");
            oDatos.Parametro("pv_digitador", "V", 30, "O");
            oDatos.Parametro("pd_emision", "D", 10, "O");
            oDatos.Parametro("pv_emision_hora", "V", 20, "O");*/
            oDatos.Parametro("pv_gravamen", "V", 5, "O");
            //oDatos.Parametro("pn_matvalor", "N", 50, "O");
            oDatos.Parametro("pv_cedula", "V", 30, "O");
            oDatos.Parametro("pv_propietario", "V", 80, "O");
            oDatos.Parametro("pv_domicilio", "V", 100, "O");
            oDatos.Parametro("pv_telefono", "V", 30, "O");
            oDatos.Parametro("pn_valor_especie", "V", 10, "O");
            oDatos.Parametro("pn_mat_anio_ant", "V", 10, "O");
            oDatos.Parametro("pn_recargo_anio_ant", "V", 10, "O");
            oDatos.Parametro("pn_placas", "V", 10, "O");
            oDatos.Parametro("pn_otros1", "V", 10, "O");
            oDatos.Parametro("pn_otros2", "V", 10, "O");
            oDatos.Parametro("pn_otros3", "V", 10, "O");
            oDatos.Parametro("pn_total_cntttsv", "V", 10, "O");
            oDatos.Parametro("pn_num_comp_sri", "V", 10, "O");
            oDatos.Parametro("pn_num_traspasos", "V", 5, "O");
            oDatos.Parametro("pv_cedula_prop_ant", "V", 12, "O");
            oDatos.Parametro("pv_nombre_prop_ant", "V", 250, "O");
            oDatos.Parametro("pv_factura_comercial", "V", 120, "O");
            oDatos.Parametro("pv_casa_comercial", "V", 250, "O");
            oDatos.Parametro("pv_canton_prop", "V", 120, "O");
            oDatos.Parametro("pn_saldo", "V", 5, "O");
            oDatos.Parametro("pv_desc_modelo", "V", 120, "O");
            oDatos.Parametro("pv_desc_marca", "V", 120, "O");
            oDatos.Parametro("pv_desc_pais", "V", 120, "O");
            oDatos.Parametro("pn_error", "V", 50, "O");
            oDatos.Parametro("pv_mensaje_error", "V", 2000, "O");
            #endregion
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pn_error").ToString() == "0")
                    {
                        _datosMatricula = new DatosMatricula(oDatos.RetornarParametro("pv_placa").ToString(), oDatos.RetornarParametro("pv_chasis").ToString(), oDatos.RetornarParametro("pv_camv").ToString(), oDatos.RetornarParametro("pn_clase_tipo").ToString(), oDatos.RetornarParametro("pn_ejes").ToString(), oDatos.RetornarParametro("pv_canton_circ").ToString(), oDatos.RetornarParametro("pn_valor_mat").ToString(),
                            oDatos.RetornarParametro("pn_anio_prod").ToString(), oDatos.RetornarParametro("pn_asientos").ToString(), oDatos.RetornarParametro("pn_avaluo").ToString(), oDatos.RetornarParametro("pv_carroceria").ToString(), oDatos.RetornarParametro("pn_cilindraje").ToString(), oDatos.RetornarParametro("pv_clase").ToString(), oDatos.RetornarParametro("pv_tipo").ToString(),
                            oDatos.RetornarParametro("pv_color").ToString(), oDatos.RetornarParametro("pv_color2").ToString(), oDatos.RetornarParametro("pv_combustible").ToString(), oDatos.RetornarParametro("pv_cooperativa").ToString(), oDatos.RetornarParametro("pn_disco").ToString(),
                            oDatos.RetornarParametro("pv_marca").ToString(), oDatos.RetornarParametro("pv_modelo").ToString(),
                            oDatos.RetornarParametro("pv_motor").ToString(), oDatos.RetornarParametro("pv_pais_origen").ToString(), oDatos.RetornarParametro("pv_placa_ant").ToString(), 
                            oDatos.RetornarParametro("pv_servicio").ToString(), oDatos.RetornarParametro("pn_tonelaje").ToString(), oDatos.RetornarParametro("pd_mat_anterior").ToString(),
                            oDatos.RetornarParametro("pv_gravamen").ToString(), oDatos.RetornarParametro("pv_cedula").ToString(), oDatos.RetornarParametro("pv_propietario").ToString(), oDatos.RetornarParametro("pv_domicilio").ToString(),
                            oDatos.RetornarParametro("pv_telefono").ToString(), oDatos.RetornarParametro("pn_valor_especie").ToString(), oDatos.RetornarParametro("pn_mat_anio_ant").ToString(), oDatos.RetornarParametro("pn_recargo_anio_ant").ToString(), oDatos.RetornarParametro("pn_placas").ToString(), oDatos.RetornarParametro("pn_otros1").ToString(),
                            oDatos.RetornarParametro("pn_otros2").ToString(), oDatos.RetornarParametro("pn_otros3").ToString(), oDatos.RetornarParametro("pn_total_cntttsv").ToString(), oDatos.RetornarParametro("pn_num_comp_sri").ToString(), oDatos.RetornarParametro("pn_num_traspasos").ToString(), oDatos.RetornarParametro("pv_cedula_prop_ant").ToString(), oDatos.RetornarParametro("pv_nombre_prop_ant").ToString(),
                            oDatos.RetornarParametro("pv_factura_comercial").ToString(), oDatos.RetornarParametro("pv_casa_comercial").ToString(), oDatos.RetornarParametro("pv_canton_prop").ToString(), oDatos.RetornarParametro("pn_saldo").ToString(),
                            oDatos.RetornarParametro("pv_desc_modelo").ToString(), oDatos.RetornarParametro("pv_desc_marca").ToString(), oDatos.RetornarParametro("pv_desc_pais").ToString(),
                            oDatos.RetornarParametro("pn_error").ToString(), oDatos.RetornarParametro("pv_mensaje_error").ToString());
                        retValue = true;
                    }
                    else
                        _error = oDatos.RetornarParametro("pv_mensaje_error").ToString();
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


        public bool ActualizarEspecie(int numTramite, string chasis, string nvaEsp)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbUserPwd, _dbTNS, "MatriculacionCNTTTSV.VehiculoCNTTTSV.cs ActualizarEspecie()");
            oDatos.Paquete("web_corpaire_ctg.actualiza_especie");
            oDatos.Parametro("pn_tramite", numTramite);
            oDatos.Parametro("pv_chasis", chasis);
            oDatos.Parametro("pv_especie_nue", nvaEsp);
            oDatos.Parametro("pv_error", "V", 2000, "O");
            oDatos.Parametro("pn_error", "N", 5, "O");
            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pn_error").ToString() == "0")
                        retValue = true;
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
            return retValue;
        }


        public DatosMatricula DatosDeMatricula
        {
            get { return _datosMatricula; }
        }

        public string Error
        {
            get{
                return _error;
            }
        }
    }

    public class DatosMatricula
    {
        private string _placa, _chasis, _camv, _anioMat, _valorMat, _anioProd, _asientos, _avaluo, _canton, _carroceria, _cilindraje, _clase, _tipo, _color,
            _combustible, _cooperativa, _disco, _fechaCompra, _marca, _modalidad, _modelo, _motor, _paisOrigen, _placaAnt, _registro, _servicio, _tonelaje,
            _matricAnterior, _caducidad, _digitador, _emision, _emisionHora, _gravamen, _matValor, _cedula, _propietario, _domicilio, _telefono, _claseTipo,
            _ejes, _cantonCirc, _color2, _valorEspecie, matAnioAnterior, _recargoAnioAnt, _placas, _otros1, _otros2, _otros3, _totalCntttsv, _numCompSRI,
            _numTraspasos, _cedulaPropAnt, _nombrePropAnt, _facturaCom, _casaCom, _cantonProp, _saldo, _matAnioAnterior, 
            _descModelo, _descMarca, _descPais,
            _hayError, _mensaje;

        public string DescPais
        {
            get { return _descPais; }
        }

        public string DescMarca
        {
            get { return _descMarca; }
        }

        public string DescModelo
        {
            get { return _descModelo; }
        }

        public string Mensaje
        {
            get { return _mensaje; }
        }

        public string HayError
        {
            get { return _hayError; }
        }

        public string CantonProp
        {
            get { return _cantonProp; }
        }

        public string MatAnioAnterior1
        {
            get { return _matAnioAnterior; }
        }

        public string Saldo
        {
            get { return _saldo; }
        }

        public string CasaCom
        {
            get { return _casaCom; }
        }

        public string FacturaCom
        {
            get { return _facturaCom; }
        }

        public string NombrePropAnt
        {
            get { return _nombrePropAnt; }
        }

        public string CedulaPropAnt
        {
            get { return _cedulaPropAnt; }
        }

        public string NumTraspasos
        {
            get { return _numTraspasos; }
        }

        public string NumCompSRI
        {
            get { return _numCompSRI; }
        }

        public string TotalCntttsv
        {
            get { return _totalCntttsv; }
        }

        public string Otros3
        {
            get { return _otros3; }
        }

        public string Otros2
        {
            get { return _otros2; }
        }

        public string Otros1
        {
            get { return _otros1; }
        }

        public string Placas
        {
            get { return _placas; }
        }

        public string RecargoAnioAnt
        {
            get { return _recargoAnioAnt; }
        }

        public string MatAnioAnterior
        {
            get { return matAnioAnterior; }
        }

        public string ValorEspecie
        {
            get { return _valorEspecie; }
        }

        public string Color2
        {
            get { return _color2; }
        }

        public string CantonCirc
        {
            get { return _cantonCirc; }
        }

        public string Ejes
        {
            get { return _ejes; }
        }

        public string ClaseTipo
        {
            get { return _claseTipo; }
        }
        private DataTable _tasas;

        public DatosMatricula(string placa, string chasis, string camv, string anioMat, string valorMat, string anioProd, string asientos, string avaluo, string canton, string carroceria,
            string cilindraje, string clase, string tipo, string color, string combustible, string cooperativa, string disco, string fechaCompra, string marca, string modalidad, string modelo, string motor, 
            string paisOrigen, string placaAnt, string registro, string servicio, string tonelaje, string matricAnterior, string caducidad, string digitador, string emision, string emisionHora,
            string gravamen, string matValor, string cedula, string propietario, string domicilio, string telefono, DataTable tasas)
        {
            _placa = placa;
            _chasis = chasis;
            _camv = camv;
            _anioMat = anioMat;
            _valorMat = valorMat;
            _anioProd = anioProd;
            _asientos = asientos;
            _avaluo = avaluo;
            _canton = canton;
            _carroceria = carroceria;
            _cilindraje = cilindraje;
            _clase = clase;
            _tipo = tipo;
            _color = color;
            _combustible = combustible;
            _cooperativa = cooperativa;
            _disco = disco;
            //_fechaCompra = fechaCompra.Length < 10 ? fechaCompra : fechaCompra.Substring(0,10);
            _fechaCompra = UtilitiesCNTTTSV.Utilities.FormatearFechaCorpaire(fechaCompra);
            _marca = marca;
            _modalidad = modalidad;
            _modelo = modelo;
            _motor = motor;
            _paisOrigen = paisOrigen;
            _placaAnt = placaAnt;
            //_registro = registro.Length < 10 ? registro : registro.Substring(0,10);
            _registro = UtilitiesCNTTTSV.Utilities.FormatearFechaCorpaire(registro);
            _servicio = servicio;
            _tonelaje = tonelaje;
            _matricAnterior = UtilitiesCNTTTSV.Utilities.FormatearFechaCorpaire(matricAnterior);
            _caducidad = UtilitiesCNTTTSV.Utilities.FormatearFechaCorpaire(caducidad);
            _digitador = digitador;
            //_emision = emision.Length < 10 ? emision : emision.Substring(0, 10);
            _emision = UtilitiesCNTTTSV.Utilities.FormatearFechaCorpaire(emision);
            _emisionHora = UtilitiesCNTTTSV.Utilities.FormatearFechaHoraCorpaire(emisionHora);
            _gravamen = gravamen;
            _valorMat = valorMat;
            _cedula = cedula;
            _propietario = propietario;
            _domicilio = domicilio;
            _telefono = telefono;
            _tasas = tasas;
        }

        public DatosMatricula(string placa, string chasis, string camv, string claseTipo, string ejes, string cantonCirc, string valorMat, string anioProd, string asientos, string avaluo, string carroceria,
            string cilindraje, string clase, string tipo, string color, string color2, string combustible, string cooperativa, string disco, string marca, string modelo, string motor,
            string paisOrigen, string placaAnt, string servicio, string tonelaje, string matricAnterior, string gravamen, string cedula, string propietario, string domicilio, string telefono,
            string valorEspecie, string matAnioMat, string recargoAnioAnt, string placas, string otros1, string otros2, string otros3, string total_cntttsv, string num_comp_sri, string num_traspasos,
            string cedulaPropAnt, string nombrePropAnt, string facturaCom, string casaCom, string cantonProp, string saldo, string descModelo, string descMarca, string descPais, string hayError, string msjError)
        {
            _placa = placa;
            _chasis = chasis;
            _camv = camv;
            _claseTipo = claseTipo;
            _ejes = ejes;
            _cantonCirc = cantonCirc;
            _valorMat = valorMat;
            _anioProd = anioProd;
            _asientos = asientos;
            _avaluo = avaluo;
            _carroceria = carroceria;
            _cilindraje = cilindraje;
            _clase = clase;
            _tipo = tipo;
            _color = color;
            _color2 = color2;
            _combustible = combustible;
            _cooperativa = cooperativa;
            _disco = disco;
            _marca = marca;
            _modelo = modelo;
            _motor = motor;
            _paisOrigen = paisOrigen;
            _placaAnt = placaAnt;
            _servicio = servicio;
            _tonelaje = tonelaje;
            _matricAnterior = UtilitiesCNTTTSV.Utilities.FormatearFechaCorpaire(matricAnterior);            
            _gravamen = gravamen;
            _cedula = cedula;
            _propietario = propietario;
            _domicilio = domicilio;
            _telefono = telefono;
            _valorEspecie = valorEspecie;
            _matAnioAnterior = matAnioMat;
            _recargoAnioAnt = recargoAnioAnt;
            _placas = placas;
            _otros1 = otros1;
            _otros2 = otros2;
            _otros3 = otros3;
            _totalCntttsv = total_cntttsv;
            _numCompSRI = num_comp_sri;
            _numTraspasos = num_traspasos;
            _cedulaPropAnt = cedulaPropAnt;
            _nombrePropAnt = nombrePropAnt;
            _facturaCom = facturaCom;
            _casaCom = casaCom;
            _cantonProp = cantonProp;
            _saldo = saldo;
            _descModelo = descModelo;
            _descMarca = descMarca;
            _descPais = descPais;
            _hayError = hayError;
            _mensaje = msjError;
        }

        #region Propiedades
        public DataTable Tasas
        {
            get { return _tasas; }
        }

        public string Telefono
        {
            get { return _telefono; }
        }

        public string Domicilio
        {
            get { return _domicilio; }
        }

        public string Propietario
        {
            get { return _propietario; }
        }

        public string Cedula
        {
            get { return _cedula; }
        }

        public string MatValor
        {
            get { return _matValor; }
        }

        public string Gravamen
        {
            get { return _gravamen; }
        }

        public string EmisionHora
        {
            get { return _emisionHora; }
        }

        public string Emision
        {
            get { return _emision; }
        }

        public string Digitador
        {
            get { return _digitador; }
        }

        public string Caducidad
        {
            get { return _caducidad; }
        }

        public string MatricAnterior
        {
            get { return _matricAnterior; }
        }

        public string Tonelaje
        {
            get { return _tonelaje; }
        }

        public string Servicio
        {
            get { return _servicio; }
        }

        public string Registro
        {
            get { return _registro; }
        }

        public string PlacaAnt
        {
            get { return _placaAnt; }
        }

        public string PaisOrigen
        {
            get { return _paisOrigen; }
        }

        public string Motor
        {
            get { return _motor; }
        }

        public string Modelo
        {
            get { return _modelo; }
        }

        public string Modalidad
        {
            get { return _modalidad; }
        }

        public string Marca
        {
            get { return _marca; }
        }

        public string FechaCompra
        {
            get { return _fechaCompra; }
        }

        public string Disco
        {
            get { return _disco; }
        }

        public string Cooperativa
        {
            get { return _cooperativa; }
        }

        public string Combustible
        {
            get { return _combustible; }
        }

        public string Color
        {
            get { return _color; }
        }

        public string Tipo
        {
            get { return _tipo; }
        }

        public string Clase
        {
            get { return _clase; }
        }

        public string Cilindraje
        {
            get { return _cilindraje; }
        }

        public string Carroceria
        {
            get { return _carroceria; }
        }

        public string Canton
        {
            get { return _canton; }
        }

        public string Avaluo
        {
            get { return _avaluo; }
        }

        public string Asientos
        {
            get { return _asientos; }
        }

        public string AnioProd
        {
            get { return _anioProd; }
        }

        public string ValorMat
        {
            get { return _valorMat; }
        }

        public string AnioMat
        {
            get { return _anioMat; }
        }

        public string Camv
        {
            get { return _camv; }
        }

        public string Chasis
        {
            get { return _chasis; }
        }

        public string Placa
        {
            get { return _placa; }
        }
        
        public string AnioMatricula
        {
            get { return _anioMat; }
        }
        #endregion
    }
        
}
