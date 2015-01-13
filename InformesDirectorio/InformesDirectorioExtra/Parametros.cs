using System;


namespace InformesDirectorioExtra
{
    public class Parametros
    {
        public static string InstitucionConfigPar
        {
            get { return "institucion"; }
        }
        
       

        public class Session
        {
            public static string FolderId
            {
                get
                {
                    return "FolderId";
                }
            }
            public static string UserName
            {
                get
                {
                    return "UserName";
                }
            }
            public static string IsAdmin
            {
                get
                {
                    return "IsAdmin";
                }
            }
        }

        public class InstitucionesPermitidas
        {

            public class ComisionTransitoEcuador
            {
                
                public const string Codigo = "CTE";
                #region "Código de Roles y Módulos/Páginas"
                public static string RolUsuarioAdminAppWeb
                {
                    get
                    {
                        return "DIREC";
                    }
                }
                public static string RolUsuarioAppWeb
                {
                    get
                    {
                        return "DIRER";
                    }
                }
                #endregion
            }

            public class AgenciaNacionalTransito
            {
                public const string Codigo = "ANT"; 

                #region "Código de Roles y Módulos/Páginas"
                public static string RolUsuarioAdminAppWeb
                {
                    get
                    {
                        return "ANTDA";
                    }
                }
                public static string RolUsuarioAppWeb
                {
                    get
                    {
                        return "ANTDU";
                    }
                }
                #endregion
            }

        }

        public class UI
        {
            public class DocumentsTable
            {
                public class Columns
                {
                    public class ID
                    {
                        public static string DbSpFieldName
                        {
                            get { return "ID_DOCUMENTO"; }
                        }
                        public static string Title
                        {
                            get { return "IdDocumento"; }
                        }
                    }
                    public class Icon
                    {
                        public static string UnknownFilename
                        {
                            get { return "~/images/unknown.png"; }
                        }
                        public static string Title
                        {
                            get { return "icono"; }
                        }
                    }
                    public class Filename
                    {
                        public static string DbSpFieldName
                        {
                            get { return "nombre"; }
                        }
                        public static string Title
                        {
                            get { return "Nombre"; }
                        }
                    }
                    public class Title
                    {
                        public static string DbSpFieldName
                        {
                            get { return "titulo"; }
                        }
                        public static string Titulo
                        {
                            get { return "Título"; }
                        }
                    }
                    public class Created
                    {
                        public static string DbSpFieldName
                        {
                            get { return "fecha_creacion"; }
                        }
                        public static string Title
                        {
                            get { return "Fecha de creación"; }
                        }
                    }
                    public class Creator
                    {
                        public static string DbSpFieldName
                        {
                            get { return "usuario_crea"; }
                        }
                        public static string Title
                        {
                            get { return "Creado por"; }
                        }
                    }
                    public class Modified
                    {
                        public static string DbSpFieldName
                        {
                            get { return "fecha_modificacion"; }
                        }
                        public static string Title
                        {
                            get { return "Fecha de modificación"; }
                        }
                    }
                    public class ModifiedBy
                    {
                        public static string DbSpFieldName
                        {
                            get { return "usuario_modifica"; }
                        }
                        public static string Title
                        {
                            get { return "Modificado por"; }
                        }
                    }
                    public class FolderID
                    {
                        public static string DbSpFieldName
                        {
                            get { return "id_carpeta"; }
                        }
                        public static string Title
                        {
                            get { return "id_carpeta"; }
                        }
                    }
                    public static string url
                    {
                        get { return "url"; }
                    }
                    public class Privilegio
                    {
                        public static string DbSpFieldName
                        {
                            get { return "id_permiso"; }
                        }
                        public static string Title
                        {
                            get { return "IdPermiso"; }
                        }
                    }
                }
            }
            public class FoldersTable
            {
                public class Columns
                {
                    public class ID
                    {
                        public static string DbSpFieldName
                        {
                            get { return "id_carpeta"; }
                        }
                        public static string Title
                        {
                            get { return "IdCarpeta"; }
                        }
                    }
                    public class Icon
                    {
                        public static string Filename
                        {
                            get { return "images/folder.png"; /*return "~/images/folder.png";*/ }
                        }
                        public static string Title
                        {
                            get { return "icono"; }
                        }
                    }
                    public class Name
                    {
                        public static string DbSpFieldName
                        {
                            get { return "nombre"; }
                        }
                        public static string Title
                        {
                            get { return "Nombre"; }
                        }
                    }
                    public class Created
                    {
                        public static string DbSpFieldName
                        {
                            get { return "fecha_creacion"; }
                        }
                        public static string Title
                        {
                            get { return "Fecha de creación"; }
                        }
                    }
                    public class Creator
                    {
                        public static string DbSpFieldName
                        {
                            get { return "usuario_crea"; }
                        }
                        public static string Title
                        {
                            get { return "Creado por"; }
                        }
                    }
                    public class Privilegio
                    {
                        public static string DbSpFieldName
                        {
                            get { return "id_permiso"; }
                        }
                        public static string Title
                        {
                            get { return "IdPermiso"; }
                        }
                    }
                }
            }
        }

        public class Sharing
        {
            public static string Read
            {
                get { return "QRY"; }
            }
        }

    }
}
