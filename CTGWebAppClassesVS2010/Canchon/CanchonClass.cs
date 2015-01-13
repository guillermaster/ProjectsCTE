using System.Data;


namespace Canchon
{
    public class CanchonClass
    {
        private string _sUsuario = "";
        private string _sClave = "";
        private string _sBaseDatos = "";
        private string _error = "";
        
        // Constructor de la Clase
        public CanchonClass(string sUsuario, string sClave, string sBaseDatos)
        {
            _sUsuario = sUsuario;
            _sClave = sClave;
            _sBaseDatos = sBaseDatos;
        }


        // Consulta si un vehículo se encuentra en canchón
        public DataTable ConsultarVehiculo(string tipoIdentificacion, string identificacion)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_sUsuario, _sClave, _sBaseDatos, "Canchon.CanchonClass.cs ConsultarVehiculo()");

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaVehiculoCanchon);
            if(tipoIdentificacion == Constantes.TipoIdentificacion.Vehiculo.PLACA.ToString())
            {
                oDatos.Parametro("pv_placa", identificacion.ToUpper());
                oDatos.Parametro("pv_ramv", "");
                oDatos.Parametro("pv_iden", "");
            }
            else if(tipoIdentificacion == Constantes.TipoIdentificacion.Vehiculo.RAMV.ToString())
            {
                oDatos.Parametro("pv_placa", "");
                oDatos.Parametro("pv_ramv", identificacion.ToUpper());
                oDatos.Parametro("pv_iden", "");
            }
            else if(tipoIdentificacion == Constantes.TipoIdentificacion.Vehiculo.CEDRUCPAS.ToString())
            {
                oDatos.Parametro("pv_placa", "");
                    oDatos.Parametro("pv_ramv", "");
                    oDatos.Parametro("pv_iden", identificacion);
            }
            oDatos.Parametro("C_CANCHON", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");

            string[] dtColumnsValues = { "fecha_ingreso", "nombre_canchon", "dias_retenido", "placa", "color", "marca", "modelo" };
            string[] dtColumnsHeader = { "Fecha de ingreso al CRV", "Centro de Retención Vehicular (CRV)", "Número de días en CRV", "Placa del vehículo", "Color", "Marca", "Modelo" };

            DataTable dtVehiculosRet = new DataTable("vehiculosxpersona");
            for (int i = 0; i < dtColumnsHeader.Length; i++)
                dtVehiculosRet.Columns.Add(dtColumnsHeader[i]);

            _error = string.Empty;

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtVehiculosRet.NewRow();
                            for (int i = 0; i < dtColumnsHeader.Length; i++)
                                dr[dtColumnsHeader[i]] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtColumnsValues[i])).ToString();
                            dtVehiculosRet.Rows.Add(dr);
                        }
                    }
                    else
                        _error = oDatos.RetornarParametro("pv_error").ToString();
                }
                else
                    _error = "Error al consultar información.";
            }
            catch
            {
                _error = "Error al consultar información";
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtVehiculosRet;
        }

        public string Error
        {
            get { return _error; }
        }
    }   
     
}
