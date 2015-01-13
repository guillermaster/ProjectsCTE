using System;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Citaciones;


[WebService(Namespace = "https://10.30.1.7/wsTransCTG/")]
//[WebService(Namespace = "http://tempuri.org/")]
//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{

    public Service()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }



    private void GuardarLogError(string nombre_webmethod, string error)
    {
        try
        {
            //Utilities.Utils.CreateTextFileAppend(HttpContext.Current.Request.PhysicalApplicationPath + Constantes.Logs.WebServiceWebTrxLogFile, "\n\n\0" + DateTime.Now.ToString() + " - " + nombre_webmethod + " - " + error);
        }
        catch (Exception ex)
        {

        }
    }



    public bool LogIn(string cod_banco, string contrasena)
    {
        try
        {
            UserManager userManager = new UserManager();
            return userManager.Authenticate(cod_banco, contrasena);
        }
        catch
        {
            return false;
        }
    }



    [WebMethod]
    public string ConsultaCEP(string cep, string cod_banco, string contrasena)
    {
        string result = Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlRootConsultaCep);

        try
        {
            if (LogIn(cod_banco, contrasena))
            {
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "wsTransCTG Service.cs ConsultaCEP");

                #region "Stored Procedure - Parameters"
                oDatos.Paquete(Constantes.StoredProcedures.ConsultaCEP);
                oDatos.Parametro("PV_CEP", cep);
                oDatos.Parametro("PV_COD_BANCO", cod_banco);
                oDatos.Parametro("PD_FECHA", DateTime.Now);
                oDatos.Parametro("PN_VALOR", "F", 12, "O");
                oDatos.Parametro("PV_ESTADO", "V", 1, "O");
                oDatos.Parametro("PV_USUARIO", "V", 80, "O");
                oDatos.Parametro("PV_PROCESO", "V", 60, "O");
                oDatos.Parametro("PV_DESC_PROCESO", "V", 60, "O");
                oDatos.Parametro("PV_NOMBRE_USUARIO", "V", 80, "O");
                oDatos.Parametro("PV_IDENTIFICACION", "V", 18, "O");
                oDatos.Parametro("pv_codigo_error", "V", 5, "O");
                oDatos.Parametro("pv_mensaje_error", "V", 180, "O");
                oDatos.Parametro("pn_error", "N", 1, "O");
                oDatos.Parametro("pv_mensaje", "V", 180, "O");
                #endregion

                #region "Ejecutar"
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    if (error == "1")
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Utilities.Utils.FormatStringNumber(oDatos.RetornarParametro("pn_valor").ToString(), Constantes.Tramites.SeparadorDecimal, Constantes.Tramites.DecimalLong) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + oDatos.RetornarParametro("pv_desc_proceso").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + oDatos.RetornarParametro("pv_nombre_usuario").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + oDatos.RetornarParametro("pv_identificacion").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    }
                    else
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    }
                }
                else
                {
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                    //result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.Mensaje + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    GuardarLogError("ConsultaCEP", oDatos.Mensaje);
                }
                #endregion

                oDatos.Dispose();
            }
            else
            {
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
            }
        }
        catch (Exception ex)
        {
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
            //result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + ex.Message + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
        }

        result += Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlRootConsultaCep);

        return result;
    }



    [WebMethod]
    public string ConsultaCitacionesPendientes(string identificacion, string cod_banco, string fecha_hora, string contrasena)
    {
        string result = Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlRootConsultaCitaciones);
        AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "wsTransCTG Service.cs ConsultaCitacionesPendientes");

        try
        {
            if (LogIn(cod_banco, contrasena))
            {


                #region "Stored Procedure - Parameters"
                oDatos.Paquete(Constantes.StoredProcedures.ConsultaCitacionesCEP);
                oDatos.Parametro("pv_identificacion", identificacion);
                oDatos.Parametro("pn_cod_banco", cod_banco);
                oDatos.Parametro("pd_fecha", DateTime.Parse(fecha_hora, new System.Globalization.CultureInfo("es-ES", false)));
                oDatos.Parametro("pv_nombre_persona", "V", 80, "O");
                oDatos.Parametro("pn_num_citaciones", "N", 5, "O");
                oDatos.Parametro("pn_val_citacion", "F", 12, "O");
                oDatos.Parametro("pn_val_multa", "F", 12, "O");
                oDatos.Parametro("pn_val_total", "F", 12, "O");
                oDatos.Parametro("pv_cep", "V", 12, "O");
                oDatos.Parametro("pv_cod_error", "V", 5, "O");
                oDatos.Parametro("pv_mensaje", "V", 125, "O");
                #endregion

                #region "Ejecutar"
                if (oDatos.Ejecutar("R"))
                {
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + oDatos.RetornarParametro("pv_cep").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones) + oDatos.RetornarParametro("pn_num_citaciones").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorCitaciones) + Utilities.Utils.FormatStringNumber(oDatos.RetornarParametro("pn_val_citacion").ToString(), Constantes.Tramites.SeparadorDecimal, Constantes.Tramites.DecimalLong) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorCitaciones);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones) + Utilities.Utils.FormatStringNumber(oDatos.RetornarParametro("pn_val_multa").ToString(), Constantes.Tramites.SeparadorDecimal, Constantes.Tramites.DecimalLong) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones) + Utilities.Utils.FormatStringNumber(oDatos.RetornarParametro("pn_val_total").ToString(), Constantes.Tramites.SeparadorDecimal, Constantes.Tramites.DecimalLong) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + oDatos.RetornarParametro("pv_nombre_persona").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_cod_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                }
                else
                {
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorCitaciones);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + " - " + oDatos.Mensaje + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                    GuardarLogError("ConsultaCitacionesPendientes", oDatos.Mensaje);
                }
                #endregion


            }
            else
            {
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorCitaciones);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
            }
        }
        catch (Exception ex)
        {
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorCitaciones);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + " - " + ex.Message + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
        }
        finally
        {
            oDatos.Dispose();
        }

        result += Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlRootConsultaCitaciones);

        return result;
    }



    [WebMethod]
    public string ProcesaPagoCEP(string cep, string valor_pagado, string fecha_hora_pago, string fecha_contable, string cod_transaccion, string cod_banco, string cod_canal, string contrasena)
    {
        string result = Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlRootPagoCep);

        try
        {
            if (LogIn(cod_banco, contrasena))
            {
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "wsTransCTG Service.cs ProcesaPagoCEP");

                #region "Stored Procedure - Parameters"
                oDatos.Paquete(Constantes.StoredProcedures.ProcesaPagoCEP);
                oDatos.Parametro("pv_cep", cep);
                oDatos.Parametro("pn_cod_banco", int.Parse(cod_banco));
                oDatos.Parametro("pd_fecha_pago", DateTime.Parse(fecha_hora_pago, new System.Globalization.CultureInfo("es-ES", false)));
                oDatos.Parametro("pn_valor_pagado", float.Parse(valor_pagado));
                oDatos.Parametro("pv_no_recibo", cod_transaccion);
                oDatos.Parametro("pd_fecha_contable", DateTime.Parse(fecha_contable, new System.Globalization.CultureInfo("es-ES", false)));
                oDatos.Parametro("pv_canal", cod_canal);
                oDatos.Parametro("pv_ajuste", "N");
                oDatos.Parametro("pv_proceso", "V", 60, "O");//
                oDatos.Parametro("pv_desc_proceso", "V", 60, "O");//
                oDatos.Parametro("pv_nombre_usuario", "V", 80, "O");//
                oDatos.Parametro("pv_identificacion", "V", 18, "O");//
                oDatos.Parametro("pv_autorizacion", "V", 12, "O");//
                oDatos.Parametro("pv_codigo_error", "V", 5, "O");//
                oDatos.Parametro("pv_mensaje_error", "V", 180, "O");//
                oDatos.Parametro("pn_error", "N", 1, "O");//
                oDatos.Parametro("pv_mensaje", "V", 180, "O");//
                #endregion

                #region "Ejecutar"
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    if (error == "1")
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + oDatos.RetornarParametro("pv_autorizacion").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + oDatos.RetornarParametro("pv_desc_proceso").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + oDatos.RetornarParametro("pv_nombre_usuario").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + oDatos.RetornarParametro("pv_identificacion").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    }
                    else
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    }
                }
                else
                {
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                    GuardarLogError("ProcesaPagoCEP", oDatos.Mensaje);
                }
                #endregion

                oDatos.Dispose();
            }
            else
            {
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
            }
        }
        catch (Exception ex)
        {
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
        }

        result += Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlRootPagoCep);

        return result;
    }


    [WebMethod(MessageName = "ProcesaPagoCEPTC", Description = "Para pagos con tarjeta de crédito")]
    public string ProcesaPagoCEP(string cep, string valor_pagado, string fecha_hora_pago, string fecha_contable, string cod_transaccion, string cod_banco, string cod_canal, string contrasena,
        string tc_emisor, int tc_autorizacion, int tc_voucher, int tc_log)//los 4 campos finales son para identificar la transacción con tarjeta de crédito
    {
        string result = Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlRootPagoCep);

        try
        {
            if (LogIn(cod_banco, contrasena))
            {
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "wsTransCTG Service.cs ProcesaPagoCEP");

                #region "Stored Procedure - Parameters"
                oDatos.Paquete(Constantes.StoredProcedures.ProcesaPagoCEP);
                oDatos.Parametro("pv_cep", cep);
                oDatos.Parametro("pn_cod_banco", int.Parse(cod_banco));
                oDatos.Parametro("pd_fecha_pago", DateTime.Parse(fecha_hora_pago, new System.Globalization.CultureInfo("es-ES", false)));
                oDatos.Parametro("pn_valor_pagado", float.Parse(valor_pagado));
                oDatos.Parametro("pv_no_recibo", cod_transaccion);
                oDatos.Parametro("pd_fecha_contable", DateTime.Parse(fecha_contable, new System.Globalization.CultureInfo("es-ES", false)));
                oDatos.Parametro("pv_canal", cod_canal);
                oDatos.Parametro("pv_ajuste", "N");
                oDatos.Parametro("pv_proceso", "V", 60, "O");//
                oDatos.Parametro("pv_desc_proceso", "V", 60, "O");//
                oDatos.Parametro("pv_nombre_usuario", "V", 80, "O");//
                oDatos.Parametro("pv_identificacion", "V", 18, "O");//
                oDatos.Parametro("pv_autorizacion", "V", 12, "O");//
                oDatos.Parametro("pv_codigo_error", "V", 5, "O");//
                oDatos.Parametro("pv_mensaje_error", "V", 180, "O");//
                oDatos.Parametro("pn_error", "N", 1, "O");//
                oDatos.Parametro("pv_mensaje", "V", 180, "O");//
                #endregion

                #region "Ejecutar"
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    if (error == "1")
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + oDatos.RetornarParametro("pv_autorizacion").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + oDatos.RetornarParametro("pv_desc_proceso").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + oDatos.RetornarParametro("pv_nombre_usuario").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + oDatos.RetornarParametro("pv_identificacion").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    }
                    else
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    }
                }
                else
                {
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                    GuardarLogError("ProcesaPagoCEP", oDatos.Mensaje);
                }
                #endregion

                oDatos.Dispose();
            }
            else
            {
                #region Mensaje de error
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                #endregion
            }
        }
        catch (Exception ex)
        {
            #region Mensaje de error
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
            #endregion
        }

        result += Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlRootPagoCep);

        return result;
    }



    [WebMethod]
    public string ReversaPagoCEP(string cep, string valor_pagado, string fecha_hora_pago, string fecha_contable, string fecha_hora_reverso, string cod_transaccion, string cod_banco, string cod_canal, string motivo, string contrasena)
    {
        string result = Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlRootReversoCep);
        string reversado = Constantes.WebService.CepNoReversado;

        try
        {
            if (LogIn(cod_banco, contrasena))
            {
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "wsTransCTG Service.cs ReversaPagoCEP");

                #region "Stored Procedure - Parameters"
                oDatos.Paquete(Constantes.StoredProcedures.ReversaPagoCEP);
                oDatos.Parametro("pv_cep", cep);
                oDatos.Parametro("pn_cod_banco", int.Parse(cod_banco));
                oDatos.Parametro("pd_fecha_pago", DateTime.Parse(fecha_hora_pago, new System.Globalization.CultureInfo("es-ES", false)));
                oDatos.Parametro("pn_valor_pagado", float.Parse(valor_pagado));
                oDatos.Parametro("pv_no_recibo", cod_transaccion);
                oDatos.Parametro("pd_fecha_contable", DateTime.Parse(fecha_contable, new System.Globalization.CultureInfo("es-ES", false)));
                oDatos.Parametro("pv_canal", cod_canal);
                oDatos.Parametro("pv_observacion", motivo);
                oDatos.Parametro("pv_ajuste", "N");
                oDatos.Parametro("pv_autorizacion", "V", 30, "O");//
                oDatos.Parametro("pv_codigo_error", "V", 5, "O");//
                oDatos.Parametro("pv_estado_cep", "V", 2, "O");//
                oDatos.Parametro("pv_mensaje_error", "V", 120, "O");//
                oDatos.Parametro("pn_error", "N", 1, "O");//
                oDatos.Parametro("pv_mensaje", "V", 180, "O");//
                #endregion

                #region "Ejecutar"
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    if (oDatos.RetornarParametro("pv_estado_cep").ToString() == Constantes.Tramites.CEPestadoReversado)
                    {
                        reversado = Constantes.WebService.CepReversado;
                    }
                    if (error == "1")
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeEstadoReverso) + reversado + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeEstadoReverso);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    }
                    else
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeEstadoReverso) + reversado + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeEstadoReverso);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    }
                }
                else
                {
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeEstadoReverso) + reversado + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeEstadoReverso);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                    GuardarLogError("ReversaPagoCEP", oDatos.Mensaje);
                }
                #endregion

                oDatos.Dispose();
            }
            else
            {
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeEstadoReverso) + reversado + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeEstadoReverso);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
            }
        }
        catch (Exception ex)
        {
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeEstadoReverso) + reversado + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeEstadoReverso);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
        }

        result += Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlRootReversoCep);

        return result;
    }


    [WebMethod]
    public string ConsultaCEPCNTTTSV(string cep, string cod_banco, string contrasena)
    {
        string result = Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlRootConsultaCep);

        try
        {
            if (cod_banco != "10")
            {
                if (LogIn(cod_banco, contrasena))
                {
                    AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "wsTransCTG Service.cs ConsultaCEP");

                    #region "Stored Procedure - Parameters"
                    oDatos.Paquete("WFK_TRX_CEP_REP.WFP_CONSULTA_CEP_CNTTTSV");
                    oDatos.Parametro("PV_CEP", cep);
                    oDatos.Parametro("PV_COD_BANCO", cod_banco);
                    oDatos.Parametro("PD_FECHA", DateTime.Now);
                    oDatos.Parametro("PN_VALOR", "F", 12, "O");
                    oDatos.Parametro("PV_ESTADO", "V", 1, "O");
                    oDatos.Parametro("PV_USUARIO", "V", 80, "O");
                    oDatos.Parametro("PV_PROCESO", "V", 60, "O");
                    oDatos.Parametro("PV_DESC_PROCESO", "V", 60, "O");
                    oDatos.Parametro("PV_NOMBRE_USUARIO", "V", 80, "O");
                    oDatos.Parametro("PV_IDENTIFICACION", "V", 18, "O");
                    oDatos.Parametro("pv_codigo_error", "V", 5, "O");
                    oDatos.Parametro("pv_mensaje_error", "V", 180, "O");
                    oDatos.Parametro("pn_error", "N", 1, "O");
                    oDatos.Parametro("pv_mensaje", "V", 180, "O");
                    #endregion

                    #region "Ejecutar"
                    if (oDatos.Ejecutar("R"))
                    {
                        string error = oDatos.RetornarParametro("pn_error").ToString();
                        if (error == "1")
                        {
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Utilities.Utils.FormatStringNumber(oDatos.RetornarParametro("pn_valor").ToString(), Constantes.Tramites.SeparadorDecimal, Constantes.Tramites.DecimalLong) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + oDatos.RetornarParametro("pv_desc_proceso").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + oDatos.RetornarParametro("pv_nombre_usuario").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + oDatos.RetornarParametro("pv_identificacion").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                        }
                        else
                        {
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                        }
                    }
                    else
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                        GuardarLogError("ConsultaCEP", oDatos.Mensaje);
                    }
                    #endregion

                    oDatos.Dispose();
                }
                else
                {
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                }
            }
        }
        catch (Exception ex)
        {
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
        }

        result += Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlRootConsultaCep);

        return result;
    }


    [WebMethod]
    public string ConsultaCitacionesPendientesCNTTTSV(string identificacion, string cod_banco, string fecha_hora, string contrasena)
    {
        string result = Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlRootConsultaCitaciones);

        try
        {
            if (cod_banco != "10")
            {
                if (LogIn(cod_banco, contrasena))
                {
                    AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "wsTransCTG Service.cs ConsultaCitacionesPendientes");

                    #region "Stored Procedure - Parameters"
                    oDatos.Paquete("Web_trx_pago_citaciones.consulta_citacion_cntttsv");
                    oDatos.Parametro("pv_identificacion", identificacion);
                    oDatos.Parametro("pn_cod_banco", cod_banco);
                    oDatos.Parametro("pd_fecha", DateTime.Parse(fecha_hora, new System.Globalization.CultureInfo("es-ES", false)));
                    oDatos.Parametro("pv_nombre_persona", "V", 80, "O");
                    oDatos.Parametro("pn_num_citaciones", "N", 5, "O");
                    oDatos.Parametro("pn_val_citacion", "F", 12, "O");
                    oDatos.Parametro("pn_val_multa", "F", 12, "O");
                    oDatos.Parametro("pn_val_total", "F", 12, "O");
                    oDatos.Parametro("pv_cep", "V", 12, "O");
                    oDatos.Parametro("pv_cod_error", "V", 5, "O");
                    oDatos.Parametro("pv_mensaje", "V", 125, "O");
                    #endregion

                    #region "Ejecutar"
                    if (oDatos.Ejecutar("R"))
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + oDatos.RetornarParametro("pv_cep").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones) + oDatos.RetornarParametro("pn_num_citaciones").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorCitaciones) + Utilities.Utils.FormatStringNumber(oDatos.RetornarParametro("pn_val_citacion").ToString(), Constantes.Tramites.SeparadorDecimal, Constantes.Tramites.DecimalLong) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorCitaciones);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones) + Utilities.Utils.FormatStringNumber(oDatos.RetornarParametro("pn_val_multa").ToString(), Constantes.Tramites.SeparadorDecimal, Constantes.Tramites.DecimalLong) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones) + Utilities.Utils.FormatStringNumber(oDatos.RetornarParametro("pn_val_total").ToString(), Constantes.Tramites.SeparadorDecimal, Constantes.Tramites.DecimalLong) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + oDatos.RetornarParametro("pv_nombre_persona").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_cod_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                    }
                    else
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorCitaciones);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                        GuardarLogError("ConsultaCitacionesPendientesCNTTTSV", oDatos.Mensaje);
                    }
                    #endregion

                    oDatos.Dispose();
                }
                else
                {
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorCitaciones);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                }
            }
        }
        catch (Exception ex)
        {
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroCitaciones);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorCitaciones);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorMultaCitaciones);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorTotalCitaciones);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
        }

        result += Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlRootConsultaCitaciones);

        return result;
    }


    [WebMethod]
    public string ProcesaPagoCEPCNTTTSV(string cep, string valor_pagado, string fecha_hora_pago, string fecha_contable, string cod_transaccion, string cod_banco, string cod_canal, string contrasena)
    {
        string result = Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlRootPagoCep);

        try
        {
            if (cod_banco != "10")
            {
                if (LogIn(cod_banco, contrasena))
                {
                    AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "wsTransCTG Service.cs ProcesaPagoCEP");

                    #region "Stored Procedure - Parameters"
                    oDatos.Paquete("WFK_TRX_CEP_REP.WFP_PAGO_CEP_CNTTTSV");
                    oDatos.Parametro("pv_cep", cep);
                    oDatos.Parametro("PV_COD_BANCO", cod_banco);
                    oDatos.Parametro("pd_fecha_pago", DateTime.Parse(fecha_hora_pago, new System.Globalization.CultureInfo("es-ES", false)));
                    oDatos.Parametro("pn_valor_pagado", float.Parse(valor_pagado));
                    oDatos.Parametro("pv_no_recibo", cod_transaccion);
                    oDatos.Parametro("pd_fecha_contable", DateTime.Parse(fecha_contable, new System.Globalization.CultureInfo("es-ES", false)));
                    oDatos.Parametro("pv_canal", cod_canal);
                    oDatos.Parametro("pv_ajuste", "N");
                    oDatos.Parametro("pv_proceso", "V", 60, "O");//
                    oDatos.Parametro("pv_desc_proceso", "V", 60, "O");//
                    oDatos.Parametro("pv_nombre_usuario", "V", 80, "O");//
                    oDatos.Parametro("pv_identificacion", "V", 18, "O");//
                    oDatos.Parametro("pv_autorizacion", "V", 12, "O");//
                    oDatos.Parametro("pv_codigo_error", "V", 5, "O");//
                    oDatos.Parametro("pv_mensaje_error", "V", 180, "O");//
                    oDatos.Parametro("pn_error", "N", 1, "O");//
                    oDatos.Parametro("pv_mensaje", "V", 180, "O");//
                    #endregion

                    #region "Ejecutar"
                    if (oDatos.Ejecutar("R"))
                    {
                        string error = oDatos.RetornarParametro("pn_error").ToString();
                        if (error == "1")
                        {
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + oDatos.RetornarParametro("pv_autorizacion").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + oDatos.RetornarParametro("pv_desc_proceso").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + oDatos.RetornarParametro("pv_nombre_usuario").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + oDatos.RetornarParametro("pv_identificacion").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                        }
                        else
                        {
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                        }
                    }
                    else
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                        GuardarLogError("ProcesaPagoCEPCNTTTSV", oDatos.Mensaje);
                    }
                    #endregion

                    oDatos.Dispose();
                }
                else
                {
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                }
            }
        }
        catch (Exception ex)
        {
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNumeroAutorizacion);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreTramite) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreTramite);//nombre del tramite
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeNombreUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeNombreUsuario);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeIdentificacionUsuario);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
        }

        result += Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlRootPagoCep);

        return result;
    }


    [WebMethod]
    public string ReversaPagoCEPCNTTTSV(string cep, string valor_pagado, string fecha_hora_pago, string fecha_contable, string fecha_hora_reverso, string cod_transaccion, string cod_banco, string cod_canal, string motivo, string contrasena)
    {
        string result = Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlRootReversoCep);
        string reversado = Constantes.WebService.CepNoReversado;

        try
        {
            if (cod_banco != "10")
            {
                if (LogIn(cod_banco, contrasena))
                {
                    AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "wsTransCTG Service.cs ReversaPagoCEP");

                    #region "Stored Procedure - Parameters"
                    oDatos.Paquete("WFK_TRX_CEP_REP.WFP_REVERSO_PAGO_CEP_CNTTTSV");
                    oDatos.Parametro("pv_cep", cep);
                    oDatos.Parametro("pv_cod_banco", cod_banco);
                    oDatos.Parametro("pd_fecha_pago", DateTime.Parse(fecha_hora_pago, new System.Globalization.CultureInfo("es-ES", false)));
                    oDatos.Parametro("pn_valor_pagado", float.Parse(valor_pagado));
                    oDatos.Parametro("pv_no_recibo", cod_transaccion);
                    oDatos.Parametro("pd_fecha_contable", DateTime.Parse(fecha_contable, new System.Globalization.CultureInfo("es-ES", false)));
                    oDatos.Parametro("pv_canal", cod_canal);
                    oDatos.Parametro("pv_observacion", motivo);
                    oDatos.Parametro("pv_ajuste", "N");
                    oDatos.Parametro("pv_autorizacion", "V", 30, "O");//
                    oDatos.Parametro("pv_codigo_error", "V", 5, "O");//
                    oDatos.Parametro("pv_estado_cep", "V", 2, "O");//
                    oDatos.Parametro("pv_mensaje_error", "V", 120, "O");//
                    oDatos.Parametro("pn_error", "N", 1, "O");//
                    oDatos.Parametro("pv_mensaje", "V", 180, "O");//
                    #endregion

                    #region "Ejecutar"
                    if (oDatos.Ejecutar("R"))
                    {
                        string error = oDatos.RetornarParametro("pn_error").ToString();
                        if (oDatos.RetornarParametro("pv_estado_cep").ToString() == Constantes.Tramites.CEPestadoReversado)
                        {
                            reversado = Constantes.WebService.CepReversado;
                        }
                        if (error == "1")
                        {
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeEstadoReverso) + reversado + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeEstadoReverso);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                        }
                        else
                        {
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeEstadoReverso) + reversado + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeEstadoReverso);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                        }
                    }
                    else
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeEstadoReverso) + reversado + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeEstadoReverso);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                        GuardarLogError("ReversaPagoCEPCNTTTSV", oDatos.Mensaje);
                    }
                    #endregion

                    oDatos.Dispose();
                }
                else
                {
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeEstadoReverso) + reversado + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeEstadoReverso);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                }
            }
        }
        catch (Exception ex)
        {
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + cep + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeEstadoReverso) + reversado + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeEstadoReverso);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
        }

        result += Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlRootReversoCep);

        return result;
    }


    [WebMethod]
    public string GeneraTramiteCEP(string cod_proceso, string usuario, string trama, string cod_localidad, string cod_banco, string cod_canal, string contrasena)
    {
        string result = Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlRootGeneraTramiteCep);

        if (LogIn(cod_banco, contrasena))
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "wsTransCTG Service.cs ProcesaPagoCEP");

            #region "Stored Procedure - Parameters"
            oDatos.Paquete(Constantes.StoredProcedures.GeneraTramiteCEP);
            oDatos.Parametro("PV_PROCESO", cod_proceso);
            oDatos.Parametro("PV_USUARIO", usuario);
            oDatos.Parametro("PV_TRAMA", trama);
            oDatos.Parametro("PV_LOCALIDAD", cod_localidad);
            oDatos.Parametro("PV_COD_BANCO", cod_banco);
            oDatos.Parametro("PV_COD_CANAL", cod_canal);
            oDatos.Parametro("PV_CEP", "V", 15, "O");
            oDatos.Parametro("PN_VALOR", "N", 6, "O");
            oDatos.Parametro("PV_CODIGO_ERROR", "V", 5, "O");
            oDatos.Parametro("PV_MENSAJE_ERROR", "V", 180, "O");
            oDatos.Parametro("PN_ERROR", "N", 1, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 180, "O");
            #endregion

            try
            {
                #region "Ejecutar"
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    if (error == "1")
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + oDatos.RetornarParametro("PV_CEP").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + oDatos.RetornarParametro("PN_VALOR").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    }
                    else
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    }
                }
                else
                {
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                    GuardarLogError("GeneraTramiteCEP", oDatos.Mensaje);
                }
                #endregion

            }
            catch// (Exception ex)
            {
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
            }
            finally
            {
                oDatos.Dispose();
            }
        }
        else
        {
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
        }
        result += Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlRootGeneraTramiteCep);

        return result;
    }


    [WebMethod]
    public string RegistrarDatosPersona(string identificacion, string tipo_identificacion, string nombre)
    {
        string result = Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlRootRegistraPersona);

        if (LogIn(cod_banco, contrasena))
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "wsTransCTG Service.cs ProcesaPagoCEP");

            #region "Stored Procedure - Parameters"
            oDatos.Paquete(Constantes.StoredProcedures.RegistraPersona);
            oDatos.Parametro("Pv_Identificacion", identificacion);
            oDatos.Parametro("Pv_Tipoident", "V", "A", tipo_identificacion);
            oDatos.Parametro("Pv_Idpersona", "N", 10, "A");
            oDatos.Parametro("Pv_Nombrecompleto", "V", "A", nombre);
            oDatos.Parametro("Pv_Nombres", "V", "A", nombre);
            oDatos.Parametro("Pv_Exito", "V", 3, "O");
            oDatos.Parametro("Pv_Msgerror", "V", 180, "O");
            #endregion

            try
            {
                #region "Ejecutar"
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    if (error == "1")
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    }
                    else
                    {
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + oDatos.RetornarParametro("pv_codigo_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                        result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + oDatos.RetornarParametro("pv_mensaje_error").ToString() + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);//mensaje especial (ej.: acerquese en 48 horas a retirar documento en ventanilla de ctg)
                    }
                }
                else
                {
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                    result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
                    //GuardarLogError("GeneraTramiteCEP", oDatos.Mensaje);
                }
                #endregion

            }
            catch// (Exception ex)
            {
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
                result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorConexionDB + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
            }
            finally
            {
                oDatos.Dispose();
            }
        }
        else
        {
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeValorPagarCep) + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeValorPagarCep);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeCodigoError) + Constantes.WebService.CodErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeCodigoError);
            result += Constantes.WebService.OpenXmlTag(Constantes.WebService.XmlNodeMensajeError) + Constantes.WebService.ErrorLoginBanco + Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlNodeMensajeError);
        }
        result += Constantes.WebService.CloseXmlTag(Constantes.WebService.XmlRootGeneraTramiteCep);

        return result;
    }

}
