using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using AccesoDatos;

namespace DatosUVC
{
    public class DatosRepUVC
    {
        private string user;
        private string password;
        private string database;
        private string error;


        public DatosRepUVC(string sUsuario, string sClave, string sBaseDatos)
        {
            this.user = sUsuario;
            this.password = sClave;
            this.database = sBaseDatos;
        }


        public DataTable UbicacionesUVC()
        {
            DataTable dtUbicacUVC = new DataTable("UVC");
            dtUbicacUVC.Columns.Add("patrulla");
            dtUbicacUVC.Columns.Add("placa");
            dtUbicacUVC.Columns.Add("fecha");
            dtUbicacUVC.Columns.Add("latitud");
            dtUbicacUVC.Columns.Add("longitud");
            ROracle oDatos = new ROracle(user, password, database);

            oDatos.Paquete("clk_reportes_uvc.clp_consulta_ubicaciones_uvc");
            oDatos.Parametro("C_UVC", "R", 0, "O");
            oDatos.Parametro("Pv_Mensaje_Error", "V", 280, "O");
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("Pv_Mensaje_Error").ToString() == string.Empty)
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtUbicacUVC.NewRow();
                            for (int i = 0; i < dtUbicacUVC.Columns.Count; i++)
                            {
                                dr[i] = oDatos.oDataReader.GetValue(i).ToString();
                            }
                            dtUbicacUVC.Rows.Add(dr);
                        }
                    }
                }
                else
                {
                    error = oDatos.Mensaje;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                oDatos.Dispose();
            }

            return dtUbicacUVC;
        }


        public DataTable DatosTotalesReporteCitacionesUVC(string fechaDesde, string fechaHasta)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Delegación");
            dt.Columns.Add("Citaciones");
            dt.Columns.Add("%");
            dt.Columns.Add("Vehículos");
            //dt.g
            string query;

            query  = "select b.descripcion delegacion, nvl(a.total,0) tot_citaciones, rtrim(ltrim(to_char(round((nvl(a.total,0)/nvl((select count(*) cantidad from axisctg.fa_facturas fac where fac.id_tipo_factura = 1";
            query += " and anulada ='N' and reclamo ='N' and origen = 'PT' and id_factura > 0 and fecha_factura between to_date('" + fechaDesde + "','dd/mm/rrrr')";
            query += " and (to_date('" + fechaHasta + "','dd/mm/rrrr') +0.999999999)),1) *100) ,2),'990.99'))) porcentaje, (select count(*) from axisctg.ge_equipos_patrulla q where q.descripcion = b.descripcion) vehiculos";
            query += " from ( select patrulla.descripcion,sum(cantidad) total from (select decode(substr(sec_libretines,1,3),'192','242',substr(sec_libretines,1,3))  id_patrulla,";
            query += " count(*) cantidad from axisctg.fa_facturas fac where fac.id_tipo_factura = 1 and anulada ='N' and reclamo ='N' and origen = 'PT' and id_factura > 0 and fecha_factura between to_date('" + fechaDesde + "','dd/mm/rrrr')";
            query += " and (to_date('" + fechaHasta + "','dd/mm/rrrr') +0.999999999) group by substr(sec_libretines,1,3)) cit_uvc, (select id_patrulla, descripcion from axisctg.ge_equipos_patrulla) patrulla ";
            query += " where cit_uvc.id_patrulla = patrulla.id_patrulla group by patrulla.descripcion order by descripcion) a,(select distinct descripcion from axisctg.ge_equipos_patrulla)b where a.descripcion(+) = b.descripcion";
            query += " order by  b.descripcion";


            ROracle oDatos = new ROracle(user, password, database);
            try
            {
                if (oDatos.EjecutarQuery(query))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                        dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                        dr[2] = oDatos.oDataReader.GetValue(2).ToString();
                        dr[3] = oDatos.oDataReader.GetValue(3).ToString();
                        dt.Rows.Add(dr);
                    }
                }
                /*else
                    cargo = oDatos.Mensaje;*/
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return dt;
        }


        public DataSet DatosTotalesReporteConsultasUVC(string fechaDesde, string fechaHasta)
        {
            DataTable dtConsPlacas = new DataTable();
            dtConsPlacas.Columns.Add("Delegación");
            dtConsPlacas.Columns.Add("Cons");
            dtConsPlacas.Columns.Add("%");
            dtConsPlacas.Columns.Add("Veh");
            //dt.Columns.Add("Num. Vehículos");
            DataTable dtConsLicencias = new DataTable();
            dtConsLicencias.Columns.Add("Delegación");
            dtConsLicencias.Columns.Add("Cons");
            dtConsLicencias.Columns.Add("%");
            dtConsLicencias.Columns.Add("Veh");
            DataTable dtConsGenerales = new DataTable();
            dtConsGenerales.Columns.Add("Delegación");
            dtConsGenerales.Columns.Add("Cons");
            dtConsGenerales.Columns.Add("%");
            dtConsGenerales.Columns.Add("Veh");
            /*DataTable dtConsCitaciones = new DataTable();
            dtConsCitaciones.Columns.Add("Delegación");
            dtConsCitaciones.Columns.Add("No.Citac.");
            DataTable dtConsVehiculos = new DataTable();
            dtConsVehiculos.Columns.Add("Delegación");
            dtConsVehiculos.Columns.Add("No.Vehíc.");*/
            /*string query;

            query = "select '    ' || b.descripcion delegacion, nvl(a.tot_placas,0) tot_placas, nvl(a.tot_licencias,0) tot_licencias, nvl(a.total_general,0) total_general, 1 tot_citaciones, (select count(*) from axisctg.ge_equipos_patrulla q where q.descripcion = b.descripcion) vehiculos";
            query += " from ( select patrulla.descripcion,sum(cant_placas) tot_placas, sum(cant_licencias) tot_licencias, sum(cant_total) total_general from";
            query += " (select decode(patrulla,'192','242',patrulla) id_patrulla, count(placa) cant_placas, count(licencia) cant_licencias, (count(placa) + count(licencia)) cant_total ";
            query += " from axisctg.uv_comm_log fac where fecha between  to_date('" + fechaDesde + "','dd/mm/rrrr') and (to_date('" + fechaHasta + "','dd/mm/rrrr') +0.999999999) and patrulla is not null and not (licencia is null and placa is null) group by patrulla) cit_uvc, (select id_patrulla, descripcion from axisctg.ge_equipos_patrulla) patrulla";
            query += " where cit_uvc.id_patrulla = patrulla.id_patrulla group by patrulla.descripcion order by descripcion) a,(select distinct descripcion from axisctg.ge_equipos_patrulla)b";
            query += " where a.descripcion(+) = b.descripcion order by  b.descripcion";*/


            ROracle oDatos = new ROracle(user, password, database);

            oDatos.Paquete("clk_reportes_uvc.clp_consultas_p25_res");
            oDatos.Parametro("Pd_fecha_inicio", fechaDesde);
            oDatos.Parametro("Pd_fecha_final", fechaHasta);
            oDatos.Parametro("C_UVC_delegacion", "R", 0, "O");
            oDatos.Parametro("Pv_Mensaje_Error", "V", 280, "O");
            try
            {
                //if (oDatos.EjecutarQuery(query))
                if (oDatos.Ejecutar("R"))
                {
                    int totPlacas = 0, totLicenc = 0, totGral = 0;
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow drPlacas = dtConsPlacas.NewRow();
                        drPlacas[0] = oDatos.oDataReader.GetValue(0).ToString();
                        totPlacas += int.Parse(oDatos.oDataReader.GetValue(1).ToString());
                        drPlacas[1] = oDatos.oDataReader.GetValue(1).ToString();
                        drPlacas[3] = oDatos.oDataReader.GetValue(4).ToString();
                        dtConsPlacas.Rows.Add(drPlacas);
                        DataRow drLicenc = dtConsLicencias.NewRow();
                        drLicenc[0] = oDatos.oDataReader.GetValue(0).ToString();
                        totLicenc += int.Parse(oDatos.oDataReader.GetValue(2).ToString());
                        drLicenc[1] = oDatos.oDataReader.GetValue(2).ToString();
                        drLicenc[3] = oDatos.oDataReader.GetValue(4).ToString();
                        dtConsLicencias.Rows.Add(drLicenc);
                        DataRow drGral = dtConsGenerales.NewRow();
                        drGral[0] = oDatos.oDataReader.GetValue(0).ToString();
                        totGral += int.Parse(oDatos.oDataReader.GetValue(3).ToString());
                        drGral[1] = oDatos.oDataReader.GetValue(3).ToString();
                        drGral[3] = oDatos.oDataReader.GetValue(4).ToString();
                        dtConsGenerales.Rows.Add(drGral);
                        /*DataRow drCitac = dtConsCitaciones.NewRow();
                        drCitac[0] = oDatos.oDataReader.GetValue(0).ToString();
                        drCitac[1] = oDatos.oDataReader.GetValue(4).ToString();
                        dtConsCitaciones.Rows.Add(drCitac);
                        DataRow drVeh = dtConsVehiculos.NewRow();
                        drVeh[0] = oDatos.oDataReader.GetValue(0).ToString();
                        drVeh[1] = oDatos.oDataReader.GetValue(5).ToString();
                        dtConsVehiculos.Rows.Add(drVeh);*/
                    }
                    foreach (DataRow dr in dtConsPlacas.Rows)
                    {
                        int currVal = int.Parse(dr[1].ToString());
                        dr[2] = currVal / totPlacas * 100;
                    }
                    foreach (DataRow dr in dtConsLicencias.Rows)
                    {
                        int currVal = int.Parse(dr[1].ToString());
                        dr[2] = currVal / totLicenc * 100;
                    }
                    foreach (DataRow dr in dtConsGenerales.Rows)
                    {
                        int currVal = int.Parse(dr[1].ToString());
                        dr[2] = currVal / totGral * 100;
                    }
                }
                /*else
                    cargo = oDatos.Mensaje;*/
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dtConsPlacas);
            ds.Tables.Add(dtConsLicencias);
            ds.Tables.Add(dtConsGenerales);
            /*ds.Tables.Add(dtConsCitaciones);
            ds.Tables.Add(dtConsVehiculos);*/

            return ds;
        }


        public DataSet DatosReporteCitaciones(string fechaInicio, string fechaFin)
        {
            DataSet firstLevelDS = new DataSet();
            DataSet secondLevelDS = new DataSet();

            string tagDescripcion = "descripcion", tagFecha = "fecha", tagPatrulla = "id_patrulla", tagTotal = "total";

            string query = "select equipos_fecha.descripcion, to_char(equipos_fecha.fecha,'dd/mm/rrrr') fecha, equipos_fecha.id_patrulla, nvl(total,0) total";
            query += " from (select x.fecha, x.id_patrulla, count(x.fecha) total from  (select trunc(fecha_factura) fecha, decode(substr(sec_libretines,1,3),'192','242',substr(sec_libretines,1,3)) id_patrulla";
            query += " from fa_facturas fac  where fac.id_tipo_factura = 1 and anulada ='N' and reclamo ='N' and origen = 'PT' and id_factura > 0 and fecha_factura between to_date('" + fechaInicio + "','dd/mm/rrrr') and (to_date('" + fechaFin + "','dd/mm/rrrr') +0.999999999)) x";
            query += " group by x.fecha, x.id_patrulla) citaciones, (select a.fecha,b.* from (select distinct(trunc(fecha_factura))  fecha from fa_facturas where fecha_factura between to_date('" + fechaInicio + "','dd/mm/rrrr') and (to_date('" + fechaFin + "','dd/mm/rrrr') +0.999999999)) a,";
            query += " (select * from axisctg.ge_equipos_patrulla e  where e.descripcion = decode('TODAS','TODAS',descripcion,'TODAS')) b order by a.fecha,b.id_patrulla ) equipos_fecha where citaciones.fecha(+) = equipos_fecha.fecha and citaciones.id_patrulla(+) = equipos_fecha.id_patrulla";
            query += " order by equipos_fecha.descripcion, equipos_fecha.fecha, equipos_fecha.id_patrulla";
            ROracle oDatos = new ROracle(user, password, database);
            try
            {
                if (oDatos.EjecutarQuery(query))
                {
                    DataTable dtTable = new DataTable();
                    string delegacion = "";
                    while (oDatos.oDataReader.Read())
                    {
                        if (string.IsNullOrEmpty(delegacion))
                        {
                            delegacion = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagDescripcion)).ToString();
                            dtTable = new DataTable(delegacion);
                            dtTable.Columns.Add(tagFecha);
                            dtTable.Columns.Add(tagPatrulla);
                            dtTable.Columns.Add(tagTotal);
                            DataRow dr = dtTable.NewRow();
                            dr[tagFecha] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagFecha)).ToString();
                            dr[tagPatrulla] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagPatrulla)).ToString();
                            dr[tagTotal] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagTotal)).ToString();
                            dtTable.Rows.Add(dr);
                        }
                        else
                        {
                            if (delegacion == oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagDescripcion)).ToString())
                            {
                                DataRow dr = dtTable.NewRow();
                                dr[tagFecha] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagFecha)).ToString();
                                dr[tagPatrulla] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagPatrulla)).ToString();
                                dr[tagTotal] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagTotal)).ToString();
                                dtTable.Rows.Add(dr);
                            }
                            else
                            {
                                firstLevelDS.Tables.Add(dtTable);
                                delegacion = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagDescripcion)).ToString();
                                dtTable = new DataTable(delegacion);
                                dtTable.Columns.Add(tagFecha);
                                dtTable.Columns.Add(tagPatrulla);
                                dtTable.Columns.Add(tagTotal);
                                DataRow dr = dtTable.NewRow();
                                dr[tagFecha] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagFecha)).ToString();
                                dr[tagPatrulla] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagPatrulla)).ToString();
                                dr[tagTotal] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagTotal)).ToString();
                                dtTable.Rows.Add(dr);
                            }
                        }
                        
                    }

                    firstLevelDS.Tables.Add(dtTable);

                    secondLevelDS = GetSecondLevelTables(firstLevelDS, fechaInicio, fechaFin, true, tagPatrulla, tagFecha, tagTotal);

                }
                /*else
                    cargo = oDatos.Mensaje;*/
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return secondLevelDS;
        }

        private DataTable SetTotalsRow(DataTable dtTable, bool singleValueCells)
        {
            DataRow drTotals = dtTable.NewRow();
            drTotals[0] = "Totales";
            for(int i=1; i < dtTable.Columns.Count; i++)
            {
                drTotals[i] = 0;
            }
            foreach (DataRow dr in dtTable.Rows)
            {
                for (int i = 1; i < dtTable.Columns.Count; i++)
                {
                    try
                    {
                        if (singleValueCells)
                            drTotals[i] = int.Parse(drTotals[i].ToString()) + int.Parse(dr[i].ToString());
                        else
                        {
                            string strVal = dr[i].ToString();
                            int firstVal = int.Parse(strVal.Substring(0, strVal.IndexOf('/')));
                            int secondVal = int.Parse(strVal.Substring(strVal.IndexOf('/') + 1, strVal.Length - strVal.IndexOf('/') - 1));
                            drTotals[i] = int.Parse(drTotals[i].ToString()) + firstVal + secondVal;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            dtTable.Rows.Add(drTotals);
            return dtTable;
        }

        private DataTable SetTotalsColumn(DataTable dtTable, bool singleValueCells)
        {
            dtTable.Columns.Add("Totales");
            foreach (DataRow dr in dtTable.Rows)
            {
                int totalRow = 0;
                for (int i = 1; i < dtTable.Columns.Count-1; i++)
                {
                    try
                    {
                        if (singleValueCells)
                            totalRow += int.Parse(dr[i].ToString());
                        else
                        {
                            string strVal = dr[i].ToString();
                            if (strVal.IndexOf('/') >= 0)
                            {
                                int firstVal = int.Parse(strVal.Substring(0, strVal.IndexOf('/')));
                                int secondVal = int.Parse(strVal.Substring(strVal.IndexOf('/') + 1, strVal.Length - strVal.IndexOf('/') - 1));
                                totalRow += firstVal + secondVal;
                            }
                            else
                                totalRow += int.Parse(dr[i].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                dr[dtTable.Columns.Count - 1] = totalRow;
            }
            return dtTable;
        }

        private DataSet GetSecondLevelTables(DataSet firstLevelTables, string fechaIni, string fechaFin, bool singleValueRow, string tagPatrulla, string tagFecha, string tagTotal)
        {
            DataSet secondLvlTbls = new DataSet();

            foreach (DataTable dtFirstLvl in firstLevelTables.Tables)
            {
                ArrayList listPatrullas = GetPatrullas(dtFirstLvl, tagPatrulla);
                DataTable dt2 = new DataTable(dtFirstLvl.TableName);
                string tagFechaUVC = "Fecha/UVC";
                dt2.Columns.Add(tagFechaUVC);
                foreach (string patrulla in listPatrullas)
                {
                    dt2.Columns.Add(patrulla);
                }

                DateTime dtIni = new DateTime(int.Parse(fechaIni.Substring(fechaIni.LastIndexOf('/') + 1, 4)), int.Parse(fechaIni.Substring(fechaIni.IndexOf('/') + 1, 2)), int.Parse(fechaIni.Substring(0, 2)));
                DateTime dtFin = new DateTime(int.Parse(fechaFin.Substring(fechaFin.LastIndexOf('/') + 1, 4)), int.Parse(fechaFin.Substring(fechaFin.IndexOf('/') + 1, 2)), int.Parse(fechaFin.Substring(0, 2)));
                TimeSpan tspan = dtFin - dtIni;

                for (int i = 0; i < tspan.Days; i++)
                {
                    DataRow dr_final = dt2.NewRow();
                    string fecha = dtIni.AddDays(i).ToString("dd/MM/yyyy");
                    dr_final[tagFechaUVC] = fecha;
                    foreach (string patrulla in listPatrullas)
                    {
                        DataRow[] filterRows = dtFirstLvl.Select(tagFecha + " = '" + fecha + "' AND " + tagPatrulla + " = " + patrulla);
                        foreach (DataRow filtRow in filterRows)
                        {
                            /*DataRow dr_dt2 = dt2.NewRow();
                            dr_dt2["Fecha"] = filtRow["fecha"];
                            dr_dt2[patrulla] = filtRow["total"];
                            dt2.Rows.Add(dr_dt2);*/
                            dr_final[patrulla] = filtRow[tagTotal];
                        }
                    }
                    dt2.Rows.Add(dr_final);
                }
                secondLvlTbls.Tables.Add(SetTotalsColumn(SetTotalsRow(dt2, singleValueRow), singleValueRow));
            }

            return secondLvlTbls;
        }

        private ArrayList GetPatrullas(DataTable dt, string tagPatrulla)
        {
            ArrayList list = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                if (list.Count == 0)
                    list.Add(dr[tagPatrulla].ToString());
                else
                {
                    bool existe = false;
                    foreach (string li in list)
                    {
                        if (li == dr[tagPatrulla].ToString())
                            existe = true;
                    }
                    if (!existe)
                        list.Add(dr[tagPatrulla].ToString());
                }
            }
            return list;
        }


        public DataSet DatosReporteConsultas(string fechaInicio, string fechaFin)
        {
            DataSet firstLevelDS = new DataSet();
            DataSet secondLevelDS = new DataSet();

            string tagDescripcion = "descripcion", tagFecha = "fecha", tagPatrulla = "id_patrulla", tagTotal = "lic_pla";

            

            /*string query = "select equipos_fecha.descripcion, to_char(equipos_fecha.fecha,'dd/mm/rrrr') fecha, equipos_fecha.id_patrulla,  nvl(total,0) total,";
            query += " nvl(licencias,0) licencias, nvl(placas,0) placas, to_char(nvl(licencias,0))  || '/' || to_char(nvl(placas,0)) lic_pla";
            query += " from (select x.fecha, x.id_patrulla, count(x.licencia) licencias, count(x.placa) placas, count(x.fecha) total";
            query += " from (select trunc(fecha) fecha, decode(patrulla,'192','242',patrulla) id_patrulla, licencia, placa from axisctg.uv_comm_log fac";
            query += " where fecha between to_date('" + fechaInicio + "','dd/mm/rrrr') and (to_date('" + fechaFin + "','dd/mm/rrrr') +0.999999999) and patrulla is not null and not (licencia is null and placa is null)) x";
            query += " group by x.fecha, x.id_patrulla) citaciones, (select a.fecha,b.* from (select distinct(trunc(fecha_factura)) fecha";
            query += " from fa_facturas where fecha_factura between to_date('" + fechaInicio + "','dd/mm/rrrr') and (to_date('" + fechaFin + "','dd/mm/rrrr') +0.999999999)) a, (select * from axisctg.ge_equipos_patrulla e ) b ";
            query += " order by a.fecha,b.id_patrulla ) equipos_fecha";
            query += " where citaciones.fecha(+) = equipos_fecha.fecha and citaciones.id_patrulla(+) = equipos_fecha.id_patrulla";
            query += " order by equipos_fecha.descripcion, equipos_fecha.fecha, equipos_fecha.id_patrulla";
            */

            ROracle oDatos = new ROracle(user, password, database);

            oDatos.Paquete("clk_reportes_uvc.clp_consultas_p25_det");
            oDatos.Parametro("pd_delegacion", "TODAS");
            oDatos.Parametro("pd_fecha_inicio", fechaInicio);
            oDatos.Parametro("pd_fecha_final", fechaFin);
            oDatos.Parametro("c_cit_uvc_delegacion", "R", 0, "O");
            oDatos.Parametro("pv_mensaje_error", "V", 280, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    DataTable dtTable = new DataTable();
                    string delegacion = "";
                    while (oDatos.oDataReader.Read())
                    {
                        if (string.IsNullOrEmpty(delegacion))
                        {
                            delegacion = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagDescripcion)).ToString();
                            dtTable = new DataTable(delegacion);
                            dtTable.Columns.Add(tagFecha);
                            dtTable.Columns.Add(tagPatrulla);
                            dtTable.Columns.Add(tagTotal);
                            DataRow dr = dtTable.NewRow();
                            dr[tagFecha] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagFecha)).ToString();
                            dr[tagPatrulla] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagPatrulla)).ToString();
                            dr[tagTotal] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagTotal)).ToString();
                            dtTable.Rows.Add(dr);
                        }
                        else
                        {
                            if (delegacion == oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagDescripcion)).ToString())
                            {
                                DataRow dr = dtTable.NewRow();
                                dr[tagFecha] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagFecha)).ToString();
                                dr[tagPatrulla] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagPatrulla)).ToString();
                                dr[tagTotal] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagTotal)).ToString();
                                dtTable.Rows.Add(dr);
                            }
                            else
                            {
                                firstLevelDS.Tables.Add(dtTable);
                                delegacion = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagDescripcion)).ToString();
                                dtTable = new DataTable(delegacion);
                                dtTable.Columns.Add(tagFecha);
                                dtTable.Columns.Add(tagPatrulla);
                                dtTable.Columns.Add(tagTotal);
                                DataRow dr = dtTable.NewRow();
                                dr[tagFecha] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagFecha)).ToString();
                                dr[tagPatrulla] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagPatrulla)).ToString();
                                dr[tagTotal] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(tagTotal)).ToString();
                                dtTable.Rows.Add(dr);
                            }
                        }

                    }
                    firstLevelDS.Tables.Add(dtTable);

                    secondLevelDS = GetSecondLevelTables(firstLevelDS, fechaInicio, fechaFin, false, tagPatrulla, tagFecha, tagTotal);

                }
                /*else
                    cargo = oDatos.Mensaje;*/
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return secondLevelDS;
        }
    }
}
