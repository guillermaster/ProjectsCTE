using System;
using System.Collections.Generic;
using System.Text;

namespace Constantes
{
    public class StoredProcedures
    {
        //******
        public static readonly string ConsultaTramitesPorCategoria = "WFK_WORKFLOW_PORTAL.WFP_PROCESOS_PORTAL";
        public static readonly string ConsultaRequisitosPorTramite = "WFK_WORKFLOW_PORTAL.WFP_DATOS_REQ_PROC";
        public static readonly string ValoresRequisitoTramite = "WFK_WORKFLOW_PORTAL.WFP_VALORES_SENTENCIA";
        public static readonly string ValoresDominioTramite = "WFK_WORKFLOW_PORTAL.WFP_VALORES_DOMINIOS";
        public static readonly string SolicitaCEP = "WFK_TRX_PORTAL_SERVICIOS.WFP_GENERA_SOLICITUD";
        public static readonly string ConsultaCEP = "WFK_TRX_CEP_REP.WFP_CONSULTA_CEP";
        public static readonly string ConsultaCEPxUsuario = "WFK_WORKFLOW_PORTAL.WFP_CEP_POR_USUARIO";
        public static readonly string GeneraCEP = "wfk_trx_portal_servicios.wfp_solicitud_cep";
        public static readonly string ConsultaNumCEP = "wfk_trx_portal_servicios.WFP_CONSULTA_SOLICITUD_CEP";
        public static readonly string ConsultaCitacionesCEP = "web_trx_pago_citaciones.consulta_citacion";
        public static readonly string ProcesaPagoCEP = "wfk_trx_cep_rep.wfp_pago_cep";
        public static readonly string ReversaPagoCEP = "wfk_trx_cep_rep.wfp_reverso_pago_cep";
        public static readonly string LoginBanco = "WFK_TRX_CEP_REP.WFP_VERIFICA_BANCO";
        public static readonly string ConsultaTramite = "WFK_WORKFLOW_PORTAL.WFP_DET_TRAMITE";
        public static readonly string ConsultaTramitesPorUsuario = "WFK_WORKFLOW_PORTAL.wfp_consulta_tramite";
        //******
        public static readonly string LoginUsuarioVerificador = "web_api_transacciones.valida_usuario_web";
        public static readonly string ConsultaCitacionesPendPorPlaca = "WEB_INTER_INFRACCIONES_1.Infracciones_x_placa";
        public static readonly string ConsultaCitacionesPendPorIdent = "WEB_INTER_INFRACCIONES_1.Infracciones_x_identificacion";
        public static readonly string ConsultaCitacionPendiente = "WEB_INTER_INFRACCIONES_1.Infracciones_x_citacion";
        public static readonly string ConsultaUniformadoSancCitacion = "WEB_INTER_INFRACCIONES_1.retorna_uniformado";
        public static readonly string ConsultaDatosBasicosVehiculo = "WEB_INTER_INFRACCIONES_1.retorna_datos_veh";
        public static readonly string ConsultaTipoLicencias = "web_validaciones_generales.retorna_tipo_lic";
        public static readonly string ConsultaTipoColores = "web_validaciones_generales.retorna_color";
        public static readonly string ConsultaVehiculoCanchon = "WEB_INTER_INFRACCIONES_1.consulta_veh_retenido";
        public static readonly string ConsultaDatosLicencia = "WEB_INTER_LICENCIAS.Datos_Licencia";
        public static readonly string ConsultaDatosDetLicencia = "WEB_INTER_LICENCIAS.Datos_Licencia2";
        public static readonly string ConsultaTiposSangre = "WEB_INTER_INFRACCIONES_1.retorna_tipo_sangre";
        public static readonly string ConsultaRangosEdades = "WEB_INTER_INFRACCIONES_1.retorna_edad_sangre";
        public static readonly string ConsultaLicenciasPorTipoSangre = "WEB_INTER_INFRACCIONES_1.consulta_tipo_sangre";
        public static readonly string ConsultaTurnosExamenPorLicencia = "WEB_API_EXAMEN.p_examen_practico";
        public static readonly string ConsultaCalificacionesExamenPorLicencia = "web_api_examen.p_calificacion_exa_prac";
        public static readonly string WebUserLogin = "GCK_API_CONSULTA_WEB_2.GCP_CONSULTA_USER";
        public static readonly string WebUserRegister = "GCK_API_CONSULTA_WEB_2.GCP_INSERTA_USUARIOS";
        public static readonly string WebUserGetPassword = "GCK_API_CONSULTA_WEB_2.GCP_RETORNA_EMAIL";
        public static readonly string WebUserGetData = "web_api_transacciones.consulta_usuario_web";
        public static readonly string WebUserUpdateData = "web_api_transacciones.actualiza_usuario_web";
        public static readonly string WebUserChangePassword = "web_api_transacciones.cambia_contraseña";
        public static readonly string WebUserExisteAxis = "GCK_API_CONSULTA_WEB_2.GCP_EXISTE_PERSONA_1";
    }
}
