using System;
using System.Collections.Generic;
using System.Text;

namespace Constantes
{
    public class WebService
    {
        public static readonly string CodErrorConexionDB = "T9999";
        public static readonly string ErrorConexionDB = "Error al conectarse a base de datos de la CTG. Por favor notificar a soporte de sistemas.";

        public static readonly string CodErrorLoginBanco = "T9998";
        public static readonly string ErrorLoginBanco = "Código de Banco y contraseña no coinciden.";

        public static readonly string XmlRootConsultaCep = "ConsultaCEP";
        public static readonly string XmlRootPagoCep = "PagoCEP";
        public static readonly string XmlRootReversoCep = "ReversoCEP";
        public static readonly string XmlRootConsultaCitaciones = "ConsultaCitaciones";

        public static readonly string XmlNodeCep = "CEP";
        public static readonly string XmlNodeValorPagarCep = "Valor";
        public static readonly string XmlNodeCodigoError = "CodError";
        public static readonly string XmlNodeMensajeError = "MsjError";
        public static readonly string XmlNodeNumeroAutorizacion = "NumAutorizacion";
        public static readonly string XmlNodeNombreTramite = "Tramite";
        public static readonly string XmlNodeNombreUsuario = "NombreUsuario";
        public static readonly string XmlNodeIdentificacionUsuario = "IdentUsuario";
        public static readonly string XmlNodeEstadoReverso = "Reversado";
        public static readonly string XmlNodeNumeroCitaciones = "NumCitac";
        public static readonly string XmlNodeValorCitaciones = "ValorCitac";
        public static readonly string XmlNodeValorMultaCitaciones = "ValorMulta";
        public static readonly string XmlNodeValorTotalCitaciones = "ValorTotal";

        public static readonly string CepReversado = "SI";
        public static readonly string CepNoReversado = "NO";

        public static string OpenXmlTag(string tagName)
        {
            return "<" + tagName + ">";
        }

        public static string CloseXmlTag(string tagName)
        {
            return "</" + tagName + ">";
        }
    }
}
