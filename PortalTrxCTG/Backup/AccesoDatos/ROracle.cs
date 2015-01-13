using System;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Collections;
using System.Diagnostics;

namespace AccesoDatos
{
    public class ROracle
    {
        private Oracle.DataAccess.Client.OracleConnection _oConexion;
        private Oracle.DataAccess.Client.OracleCommand _oComando = new OracleCommand();
        private OracleDataReader _oDataReader = null;
        private string _BaseDatos;
        private string _sConexion;
        private string _Mensaje = "";
        private string _App = "";
        //public IDbTransaction Transaccion;
        //private DataSet _oDataSet = new DataSet();
        /*************************************************/
        #region Constructor
        //  Constructor de la clase, necesaria la sesion del usuario conectado
        public ROracle(string sUsuario, string sClave, string sBaseDatos)
        {
            _sConexion = "Data Source=" + sBaseDatos +
                "; User Id=" + sUsuario +
                "; Password=" + sClave +
                "; Pooling=False";
            //"; Enlist=no; Persist Security Info=no; Pooling=False; Connection Lifetime=1;";
            _BaseDatos = sBaseDatos;
        }
        //  Constructor de la clase, necesaria la sesion del usuario conectado
        public ROracle(string sUsuario, string sClave, string sBaseDatos, string app)
        {
            _sConexion = "Data Source=" + sBaseDatos +
                "; User Id=" + sUsuario +
                "; Password=" + sClave +
                "; Pooling=False";
            _App = app;
            _BaseDatos = sBaseDatos;
        }
        #endregion

        #region "Alertas"
        private void SendErrorAlert(string errorMsg)
        {
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress("webmaster@ctg.gov.ec");
            correo.To.Add("gpincay@ctg.gov.ec");
            //correo.CC.Add("bramos@ctg.gov.ec");
            correo.Subject = "Error de conexión a base de datos";
            string body = "Un usuario de la CTG ha recibido un mensaje de error de conexión a la base. <br /><br />" +
                "<b>Error:</b>  " + errorMsg +
                "<br /><b>Aplicación Web:</b> " + this._App +
                "<br /><b>Base de Datos (tnsname):</b> " + this._BaseDatos;
            correo.Body = body;
            correo.IsBodyHtml = true;
            correo.Priority = System.Net.Mail.MailPriority.High;
            //
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            //---------------------------------------------
            // Estos datos debes rellanarlos correctamente
            //---------------------------------------------
            smtp.Host = "201.218.0.228";
            smtp.Credentials = new System.Net.NetworkCredential("webmaster", "123456");
            //smtp.EnableSsl = false;
            try
            {
                smtp.Send(correo);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        /*************************************************/
        #region Conexión

        private bool AbrirConexion()
        {
            this._oConexion = new OracleConnection(this._sConexion);
            try
            {                
                this._oConexion.Open();
                StackTrace st = new StackTrace(true);
                ConnectionSpy sl = new ConnectionSpy(this._oConexion, st);
                return true;
            }
            catch (Exception Ex)
            {
                this._Mensaje = Ex.Message;
                SendErrorAlert(Ex.Message);
                return false;
            }
        }

        public void CerrarConexion()
        {
            Dispose();
        }

        #endregion

        /*************************************************/
        #region Propiedades Públicas

        public OracleDataReader oDataReader
        {
            get { return this._oDataReader; }
            set { this._oDataReader = value; }
        }

        public OracleConnection oConexion
        {
            get { return this._oConexion; }
            set { this._oConexion = value; }
        }

        public string Mensaje
        {
            get { return this._Mensaje; }
        }

        #endregion

        /*************************************************/
        #region Stored Procedures
        
        public void Paquete(string sPaquete)
        {
            this._oComando.Connection = this._oConexion;
            this._oComando.CommandText = sPaquete;
            this._oComando.CommandType = CommandType.StoredProcedure;
            this._oComando.Parameters.Clear();
        }


        //  Seteo de los parametros de entrada
        public void Parametro(string sParametro, object oValor)
        {
            _oComando.Parameters.Add(sParametro, oValor);
        }

        public void Parametro(string sParametro, byte[] bValue)
        {
            Parametro(sParametro, "I", bValue);
        }

        public void Parametro(string sParametro, string sDireccion, byte[] bValue)
        {
            ParameterDirection oDireccion;
            if (sDireccion == "I")
                oDireccion = ParameterDirection.Input;
            else
                oDireccion = ParameterDirection.Output;
            _oComando.Parameters.Add(sParametro, OracleDbType.Blob, bValue, oDireccion);
        }

        public void Parametro(string sParametro, string sTipo, string sDireccion, string sValue)
        {
            OracleDbType oTipo = OracleDbType.Varchar2;
            ParameterDirection oDireccion = ParameterDirection.Input;
            switch (sTipo)
            {
                case "V":
                    oTipo = OracleDbType.Varchar2;
                    break;
                case "D":
                    oTipo = OracleDbType.Date;
                    break;
                case "N":
                    oTipo = OracleDbType.Int64;
                    break;
                case "R":
                    oTipo = OracleDbType.RefCursor;
                    break;
                case "F":
                    oTipo = OracleDbType.Double;
                    break;
                case "DEC":
                    oTipo = OracleDbType.Decimal;
                    break;
                case "L":
                    oTipo = OracleDbType.Long;
                    break;
                case "CLOB":
                    oTipo = OracleDbType.Clob;
                    break;
                case "BLOB":
                    oTipo = OracleDbType.Blob;
                    break;
                case "T":
                    oTipo = OracleDbType.TimeStamp;
                    break;
            }
            switch (sDireccion)
            {
                case "I":
                    oDireccion = ParameterDirection.Input;
                    break;
                case "O":
                    oDireccion = ParameterDirection.Output;
                    break;
                case "A":
                    oDireccion = ParameterDirection.InputOutput;
                    break;
            }

            _oComando.Parameters.Add(sParametro, oTipo, sValue, oDireccion);
            
        }

        //  Seteo de los parametros - Arreglos
        public void Parametro(string sParametro, ArrayList oValor, string sTipo, Int16 iLong, string sDireccion)
        {
            OracleDbType oTipo = OracleDbType.Varchar2;
            ParameterDirection oDireccion = ParameterDirection.Input;
            switch (sTipo)
            {
                case "V":
                    oTipo = OracleDbType.Varchar2;
                    break;
                case "D":
                    oTipo = OracleDbType.Date;
                    break;
                case "N":
                    oTipo = OracleDbType.Int64;
                    break;
                case "R":
                    oTipo = OracleDbType.RefCursor;
                    break;
            }
            switch (sDireccion)
            {
                case "I":
                    oDireccion = ParameterDirection.Input;
                    break;
                case "O":
                    oDireccion = ParameterDirection.Output;
                    break;
                case "A":
                    oDireccion = ParameterDirection.InputOutput;
                    break;
            }
            OracleParameter Param = _oComando.Parameters.Add(sParametro, oTipo);
            Param.Size = oValor.Count;
            Param.Direction = oDireccion;
            Param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            switch (sTipo)
            {
                case "V":
                    Param.Value = (string[])oValor.ToArray(typeof(String));
                    break;
                case "N":
                    Param.Value = (long[])oValor.ToArray(typeof(long));
                    break;
            }
        }


        //  Seteo de los parametros de salida
        public void Parametro(string sParametro, string sTipo, Int16 iLong, string sDireccion)
        {
            OracleDbType oTipo = OracleDbType.Varchar2;
            ParameterDirection oDireccion = ParameterDirection.Input;
            switch (sTipo)
            {
                case "V":
                    oTipo = OracleDbType.Varchar2;
                    break;
                case "D":
                    oTipo = OracleDbType.Date;
                    break;
                case "N":
                    oTipo = OracleDbType.Int64;
                    break;
                case "R":
                    oTipo = OracleDbType.RefCursor;
                    break;
                case "F":
                    oTipo = OracleDbType.Double;
                    break;
                case "DEC":
                    oTipo = OracleDbType.Decimal;
                    break;
                case "L":
                    oTipo = OracleDbType.Long;
                    break;
                case "CLOB":
                    oTipo = OracleDbType.Clob;
                    break;
                case "BLOB":
                    oTipo = OracleDbType.Blob;
                    break;
            }
            switch (sDireccion)
            {
                case "I":
                    oDireccion = ParameterDirection.Input;
                    break;
                case "O":
                    oDireccion = ParameterDirection.Output;
                    break;
                case "A":
                    oDireccion = ParameterDirection.InputOutput;
                    break;
            }
            if (sTipo != "R" & sTipo != "BLOB")
            {
                _oComando.Parameters.Add(sParametro, oTipo, iLong).Direction = oDireccion;
            }
            else
            {
                _oComando.Parameters.Add(sParametro, oTipo).Direction = oDireccion;
            }
        }

        //  Seteo de los parametros de salida
        public void Parametro(string sParametro, string sTipo, string sDireccion)
        {
            OracleDbType oTipo = OracleDbType.Varchar2;
            ParameterDirection oDireccion = ParameterDirection.Input;
            switch (sTipo)
            {
                case "V":
                    oTipo = OracleDbType.Varchar2;
                    break;
                case "D":
                    oTipo = OracleDbType.Date;
                    break;
                case "N":
                    oTipo = OracleDbType.Int64;
                    break;
                case "R":
                    oTipo = OracleDbType.RefCursor;
                    break;
                case "F":
                    oTipo = OracleDbType.Double;
                    break;
                case "DEC":
                    oTipo = OracleDbType.Decimal;
                    break;
                case "L":
                    oTipo = OracleDbType.Long;
                    break;
                case "CLOB":
                    oTipo = OracleDbType.Clob;
                    break;
            }
            switch (sDireccion)
            {
                case "I":
                    oDireccion = ParameterDirection.Input;
                    break;
                case "O":
                    oDireccion = ParameterDirection.Output;
                    break;
                case "A":
                    oDireccion = ParameterDirection.InputOutput;
                    break;
            }
            
            _oComando.Parameters.Add(sParametro, oTipo).Direction = oDireccion;
            
        }

        #endregion


        public bool Consultar_Imagen(string sSql)
        {
            bool bRetorno = false;
            try
            {
                if ((_oConexion == null) || (_oConexion.State == ConnectionState.Closed))
                {
                    if (!AbrirConexion())
                    {
                        return bRetorno;
                    }
                }
                _oComando.Connection = _oConexion;
                _oComando.CommandText = sSql;
                _oComando.CommandType = CommandType.Text;
                _oComando.Parameters.Clear();
                if (Ejecutar("R"))
                {
                    bRetorno = true;
                }
            }
            catch (Exception ex)
            {
                this._Mensaje = ex.Message;
            }
            finally
            {
                _oComando.Dispose();
            }
            return bRetorno;
        }

        

        /*************************************************/
        #region Ejecutar
        //  Ejecutar DML a Oracle
        public bool Ejecutar(string sTipoEjecucion)
        {
            bool _bProceso = false;
            try
            {
                if ((_oConexion == null) || (_oConexion.State == ConnectionState.Closed))
                {
                    if (!AbrirConexion())
                    {
                        return _bProceso;
                    }
                    _oComando.Connection = _oConexion;
                }
                switch (sTipoEjecucion)
                {
                    case "R":
                        _oDataReader = _oComando.ExecuteReader(CommandBehavior.CloseConnection);
                        break;
                    /*case "A":
                        _oDataAdapter = new OracleDataAdapter(_oComando);
                        _oDataAdapter.Fill(this._oDataSet);
                        _oDataAdapter = null;
                        break;*/
                    case "N":
                        _oComando.ExecuteNonQuery();
                        break;
                }
                _bProceso = true;
            }
            catch (Exception ex)
            {
                this._Mensaje = ex.Message;
                SendErrorAlert(ex.Message);
            }
            
            
            return _bProceso;
        }

        public bool EjecutarQuery(string sSql)
        {
            bool bRetorno = false;
            this._Mensaje = "";
            try
            {
                if ((_oConexion == null) || (_oConexion.State == ConnectionState.Closed))
                {
                    if (!AbrirConexion())
                    {
                        return bRetorno;
                    }
                }
                _oComando.Connection = _oConexion;
                _oComando.CommandText = sSql;
                _oComando.CommandType = CommandType.Text;
                _oComando.Parameters.Clear();
                if (Ejecutar("R"))
                {
                    bRetorno = true;
                }
            }
            catch (Exception ex)
            {
                this._Mensaje = ex.Message;
                SendErrorAlert(ex.Message);
            }
            //finally
            //{
            //    _oComando.Dispose();
            //}
            return bRetorno;
        }

        public bool EjecutarSPfillDataSet(string query, DataSet ds)
        {
            bool bRetorno = false;
            try
            {
                if ((_oConexion == null) || (_oConexion.State == ConnectionState.Closed))
                {
                    if (!AbrirConexion())
                    {
                        return bRetorno;
                    }
                }

                OracleCommand oraCmd = new OracleCommand(query, _oConexion);
                OracleDataAdapter oraAdap = new OracleDataAdapter(oraCmd);
                oraAdap.Fill(ds);
                bRetorno = true;
                //_oComando.Connection = _oConexion;
                //_oComando.CommandText = sSql;
                //_oComando.CommandType = CommandType.Text;
                //_oComando.Parameters.Clear();
                //if (Ejecutar("R"))
                //{
                //    bRetorno = true;
                //}
            }
            catch (Exception ex)
            {
                this._Mensaje = ex.Message;
            }
            return bRetorno;
        }

        #endregion


        #region RetornarParametro - Parametros de Salida
        public object RetornarParametro(string sParametro)
        {
            object oDato = DBNull.Value;
            try
            {
                oDato = _oComando.Parameters[sParametro].Value;
            }
            catch (Exception ex)
            {
                this._Mensaje = ex.Message;
            }
            return oDato;
        }
        #endregion RetornarParametro - Parametros de Salida

        #region RetornarDatos
        //  Lee el DataReader y lo transforma a object[]
        //  Solo para el caso de consulta específica
        public object[] RetornarDatos()
        {
            object[] oRetorno = null;
            if (_oDataReader.Read())
            {
                ArrayList oDatos = new ArrayList();
                oDatos.Add("S");
                for (Int32 Indice = 0; Indice < _oDataReader.FieldCount; Indice++)
                {
                    oDatos.Add(_oDataReader[Indice]);
                }
                oRetorno = oDatos.ToArray();
            }
            return oRetorno;
        }

        public object[] RetornarDatos(object[] oParamOut)
        {
            object[] oRetorno = null;
            if (_oDataReader.Read())
            {
                ArrayList oDatos = new ArrayList();
                oDatos.Add("S");
                foreach (string oCampo in oParamOut)
                {
                    if (_oDataReader[oCampo] == DBNull.Value)
                    {
                        oDatos.Add("");
                    }
                    else
                    {
                        oDatos.Add(_oDataReader[oCampo]);
                    }
                }
                oRetorno = oDatos.ToArray();
            }
            return oRetorno;
        }

        #endregion

        #region RetornarMensaje
        //  Lee el DataReader y lo transforma a object[]
        //  Solo para el caso de consulta específica
        public object[] RetornarMensaje()
        {
            ArrayList oDatos = new ArrayList();
            oDatos.Add("N");
            oDatos.Add(this._Mensaje);
            return oDatos.ToArray();
        }

        public string RetornarMensajeString()
        {
            return this._Mensaje;
        }

        #endregion

        #region RetornarMensaje
        //  Lee el DataReader y lo transforma a object[]
        //  Solo para el caso de consulta específica
        public object[] RetornarMensaje(
            string sEjecucion,  // (S-N)
            string sMensaje)
        {
            ArrayList oDatos = new ArrayList();
            oDatos.Add(sEjecucion);
            if (sEjecucion == "N")
            {
                oDatos.Add(sMensaje);
            }
            return oDatos.ToArray();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            try
            {
                this._oDataReader.Dispose();
                this._oDataReader.Close();
                this._oComando.Dispose();
                this._oConexion.Dispose();
                this._oConexion.Close();                
                //this._oConexion = null;                
                //this._oComando = null;
                //this._oDataReader = null;
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
    }
}
