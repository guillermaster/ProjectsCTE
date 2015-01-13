using System;
using System.Collections.Generic;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace InformesDirectorioExtra
{
    public class Document
    {
        private string _dbUser;
        private string _dbPassword;
        private string _dbServer;
        private string _appUser;
        private string _error;

        public Document(string appUser, string dbUser, string dbPassword, string dbServer)
        {
            _dbUser = dbUser;
            _dbPassword = dbPassword;
            _dbServer = dbServer;
            _appUser = appUser;
        }


        public bool CreateNewDocument(string nombre, string titulo, byte[] archivo, int idCarpeta, string codInstitucion)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "InformesDirectorioExtra.Document.cs CreateNewDocument");

            oDatos.Paquete("clk_documentos_directorio.clp_inserta_documento");
            oDatos.Parametro("pv_nombre_documento", nombre);
            oDatos.Parametro("pv_titulo", titulo);
            oDatos.Parametro("pv_archivo", archivo);
            oDatos.Parametro("pv_usuario", _appUser);
            oDatos.Parametro("pn_carpeta", idCarpeta);
            oDatos.Parametro("pv_institucion", codInstitucion);
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


        public byte[] LoadDocumentFile(int idDocument)
        {
            byte[] file = null;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "InformesDirectorioExtra.Document.cs LoadDocumentFile");

            oDatos.Paquete("clk_documentos_directorio.clp_recupera_documento");
            oDatos.Parametro("pn_documento", idDocument);
            oDatos.Parametro("pb_archivo", "BLOB", 0, "O");
            oDatos.Parametro("pv_error", "V", 500, "O");

            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(error))
                        file = ((OracleBlob)oDatos.RetornarParametro("pb_archivo")).Value;
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

            return file;
        }


        public DataSet LoadDocumentsAndFolders(int idCarpeta, string codInstitucion)
        {
            DataSet dsCursors = null;
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "InformesDirectorioExtra.Folder.cs CreateNewFolder");

            oDatos.Paquete("clk_documentos_directorio.clp_consulta_doc_carp");
            oDatos.Parametro("pv_usuario", _appUser);
            oDatos.Parametro("pn_carpeta_padre", idCarpeta);
            oDatos.Parametro("pv_institucion", codInstitucion);
            oDatos.Parametro("c_documentos", "R", 0, "O");
            oDatos.Parametro("c_carpetas", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");

            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(error))
                    {
                        dsCursors = new DataSet();
                        //oDatos.DataAdapter.Fill(dsCursors);
                        oDatos.RetornarDatosCursores(dsCursors);
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

            if (retValue)
            {
                return LoadDatasetOnUI(dsCursors);
            }
            else
                return new DataSet();
            //return retValue;
        }


        public DataSet LoadDatasetOnUI(DataSet dsDocsNFolders)
        {
            DataSet dsData = new DataSet();
            try
            {
                #region "Folders"
                DataTable dtFolders = new DataTable();
                dtFolders.Columns.Add(Parametros.UI.FoldersTable.Columns.ID.Title);
                dtFolders.Columns.Add(Parametros.UI.FoldersTable.Columns.Icon.Title);
                dtFolders.Columns.Add(Parametros.UI.FoldersTable.Columns.Name.Title);
                dtFolders.Columns.Add(Parametros.UI.FoldersTable.Columns.Created.Title);
                dtFolders.Columns.Add(Parametros.UI.FoldersTable.Columns.Creator.Title);
                dtFolders.Columns.Add(Parametros.UI.FoldersTable.Columns.Privilegio.Title);
                
                foreach (DataRow dr in dsDocsNFolders.Tables[1].Rows)
                {
                    DataRow drFolder = dtFolders.NewRow();
                    drFolder[Parametros.UI.FoldersTable.Columns.ID.Title] = dr[Parametros.UI.FoldersTable.Columns.ID.DbSpFieldName];
                    drFolder[Parametros.UI.FoldersTable.Columns.Icon.Title] = Parametros.UI.FoldersTable.Columns.Icon.Filename;
                    drFolder[Parametros.UI.FoldersTable.Columns.Name.Title] = dr[Parametros.UI.FoldersTable.Columns.Name.DbSpFieldName];
                    drFolder[Parametros.UI.FoldersTable.Columns.Created.Title] = dr[Parametros.UI.FoldersTable.Columns.Created.DbSpFieldName];
                    drFolder[Parametros.UI.FoldersTable.Columns.Creator.Title] = dr[Parametros.UI.FoldersTable.Columns.Creator.DbSpFieldName];
                    drFolder[Parametros.UI.FoldersTable.Columns.Privilegio.Title] = dr[Parametros.UI.FoldersTable.Columns.Privilegio.DbSpFieldName];
                    dtFolders.Rows.Add(drFolder);
                }
                dsData.Tables.Add(dtFolders);
                #endregion

                #region "Documents"
                DataTable dtDocuments = new DataTable();
                dtDocuments.Columns.Add(Parametros.UI.DocumentsTable.Columns.ID.Title);
                dtDocuments.Columns.Add(Parametros.UI.DocumentsTable.Columns.Icon.Title);
                dtDocuments.Columns.Add(Parametros.UI.DocumentsTable.Columns.Filename.Title);
                dtDocuments.Columns.Add(Parametros.UI.DocumentsTable.Columns.Title.Titulo);
                dtDocuments.Columns.Add(Parametros.UI.DocumentsTable.Columns.Created.Title);
                dtDocuments.Columns.Add(Parametros.UI.DocumentsTable.Columns.Creator.Title);
                dtDocuments.Columns.Add(Parametros.UI.DocumentsTable.Columns.Modified.Title);
                dtDocuments.Columns.Add(Parametros.UI.DocumentsTable.Columns.ModifiedBy.Title);
                dtDocuments.Columns.Add(Parametros.UI.DocumentsTable.Columns.FolderID.Title);
                dtDocuments.Columns.Add(Parametros.UI.DocumentsTable.Columns.url);
                dtDocuments.Columns.Add(Parametros.UI.DocumentsTable.Columns.Privilegio.Title);
                
                foreach (DataRow dr in dsDocsNFolders.Tables[0].Rows)
                {
                    DataRow drDocument = dtDocuments.NewRow();
                    drDocument[Parametros.UI.DocumentsTable.Columns.ID.Title] = dr[Parametros.UI.DocumentsTable.Columns.ID.DbSpFieldName];
                    drDocument[Parametros.UI.DocumentsTable.Columns.Icon.Title] = GetFileTypeIcon(dr[Parametros.UI.DocumentsTable.Columns.Filename.DbSpFieldName].ToString());
                    drDocument[Parametros.UI.DocumentsTable.Columns.Filename.Title] = dr[Parametros.UI.DocumentsTable.Columns.Filename.DbSpFieldName];
                    drDocument[Parametros.UI.DocumentsTable.Columns.Title.Titulo] = dr[Parametros.UI.DocumentsTable.Columns.Title.DbSpFieldName];
                    drDocument[Parametros.UI.DocumentsTable.Columns.Created.Title] = dr[Parametros.UI.DocumentsTable.Columns.Created.DbSpFieldName];
                    drDocument[Parametros.UI.DocumentsTable.Columns.Creator.Title] = dr[Parametros.UI.DocumentsTable.Columns.Creator.DbSpFieldName];
                    drDocument[Parametros.UI.DocumentsTable.Columns.Modified.Title] = dr[Parametros.UI.DocumentsTable.Columns.Modified.DbSpFieldName];
                    drDocument[Parametros.UI.DocumentsTable.Columns.ModifiedBy.Title] = dr[Parametros.UI.DocumentsTable.Columns.ModifiedBy.DbSpFieldName];
                    drDocument[Parametros.UI.DocumentsTable.Columns.FolderID.Title] = dr[Parametros.UI.DocumentsTable.Columns.FolderID.DbSpFieldName];
                    drDocument[Parametros.UI.DocumentsTable.Columns.Privilegio.Title] = dr[Parametros.UI.DocumentsTable.Columns.Privilegio.DbSpFieldName];
                    drDocument[Parametros.UI.DocumentsTable.Columns.url] = "ViewDoc.aspx?id=" + dr[Parametros.UI.DocumentsTable.Columns.ID.DbSpFieldName] +
                        "&ext=" + GetFileExtension(dr[Parametros.UI.DocumentsTable.Columns.Filename.DbSpFieldName].ToString()) +
                        "&name=" + dr[Parametros.UI.DocumentsTable.Columns.Filename.DbSpFieldName].ToString();
                    dtDocuments.Rows.Add(drDocument);
                }
                dsData.Tables.Add(dtDocuments);
                #endregion
            }
            catch(Exception ex)
            {
                _error = ex.Message;
            }
            return dsData;
        }

        private string GetFileTypeIcon(string filename)
        {
            
            return "~/images/" + GetFileExtension(filename) + ".png";
        }

        private string GetFileExtension(string filename)
        {
            int lastDotPos = filename.LastIndexOf('.');
            return filename.Substring(lastDotPos + 1, filename.Length - (lastDotPos + 1)).ToLower();
        }


        public bool Share(DataTable dtUsers, string sharingUserEmail, int idDocument, string title, string url)
        {
            bool retValue = false;
            _error = string.Empty;
            if (dtUsers.Rows.Count > 0)
            {
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "InformesDirectorioExtra.Document.cs Share");
                try
                {
                    if (oDatos.AbrirConexion())
                    {
                        foreach (DataRow dr in dtUsers.Rows)
                        {
                            if (ShareSingleUser(oDatos, dr[0].ToString(), idDocument, InformesDirectorioExtra.Parametros.Sharing.Read))
                            {
                                if (!Utilities.SendEmailNotificationForSharedDocument(dr[1].ToString(), sharingUserEmail, _appUser, title, url))
                                    _error += dr[0].ToString() + ": No se pudo enviar notificación a " + dr[1].ToString() + "<br />";
                                retValue = true;
                            }
                        }
                    }
                    else
                    {
                        _error += oDatos.Mensaje + "<br />";
                    }
                }
                catch (Exception ex)
                {
                    _error += ex.Message + "<br />";
                }
                finally
                {
                    oDatos.Dispose();
                }
            }
            return retValue;
        }


        public bool ShareSingleUser(AccesoDatos.ROracle oDatos, string user, int idDocument, string codPermiso)
        {
            try
            {
                oDatos.Paquete("clk_documentos_directorio.clp_permiso_documento");
                oDatos.Parametro("pv_usuario", user);
                oDatos.Parametro("pn_cod_archivo", idDocument);
                oDatos.Parametro("pv_cod_permiso", codPermiso);
                oDatos.Parametro("pv_error", "V", 300, "O");

                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(error))
                        return true;
                    else
                    {
                        _error += user + ":  " + error +"<br />";
                        return false;
                    }
                }
                else
                {
                    _error += user + ": " + oDatos.Mensaje + "<br />";
                    return false;
                }
            }
            catch (Exception ex)
            {
                _error += user + ": " + ex.Message + "<br />";
                return false;
            }
        }


        public string Error
        {
            get { return _error; }
        }
    }



}
