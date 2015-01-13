using System;
using System.Collections.Generic;
using System.Text;

namespace Constantes
{
    public class UsuarioWeb
    {
        public static readonly string EstadoVerificado = "A";//usuario ha validado sus datos - full privileges
        public static readonly string EstadoNoVerificado = "I";//usuario no ha validado sus datos en oficinas de ctg
        public static readonly string EstadoActivo = "A";
        public static readonly string EstadoInactivo = "I";
        public static readonly string DBFieldNameIdentificacion = "IDENTIFICACION";
        public static readonly string DBFieldNameNombres = "NOMBRES";
        public static readonly string DBFieldNameApellidos = "APELLIDOS";
        public static readonly string DBFieldNameEmail = "EMAIL";
        public static readonly string DBFieldNameContrasena = "CONTRASEÑA";
        public static readonly string DBFieldNameEstado = "ESTADO_ACTUALIZACION";
        public static readonly string DBFieldNameEstadoRegistro = "ESTADO";
        public static readonly string CodErrorUsuarioNoExiste = "1";
        public static readonly string CodErrorUsuarioNoExisteEnCTG = "WEB_API_TRANSACCIONES.CONSULTA_USUARIO_WEB - 2";
        public static readonly string CodErrorUsuarioInactivoEnCTG = "WEB_API_TRANSACCIONES.CONSULTA_USUARIO_WEB - 3";

        public static readonly string SessionVarNameFullAccess = "webctgusrFullAccess";
        public static readonly string SessionFullAccess = "FULL";
        public static readonly string SessionLimitedAccess = "LIMITED";
        public static readonly string SessionIP = "startaddress";
        public static readonly string SessionDNS = "startdns";
        public static readonly string SessionAgent = "startagent";

        public static readonly string URLParamEncIdent = "validate";
        public static readonly string URLParamIdent = "newuser";

        public static readonly string ErrorMsgWrongPassword = "Contraseña incorrecta";
        public static readonly string ErrorMsgUserDoesntExists = "El usuario especificado no existe";
        public static readonly string ErrorMsgLoginUnknown = "Error de acceso inesperado";
        public static readonly string ErrorMsgUserAlreadyExists = "El usuario ya se encuentra registrado";
        public static readonly string ErrorMsgUserInactive = "El usuario está inactivo";
        public static readonly string ErrorMsgUserMustChangePassword = "Debe de cambiar su contraseña";

        
    }
}
