Imports System.Data

Public Class Inf_x_Identi
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents btn_regresar As System.Web.UI.WebControls.Button
    'Protected WithEvents Datagrid1 As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents lblPlaca As System.Web.UI.WebControls.Label
    'Protected WithEvents lbltitulo As System.Web.UI.WebControls.Label
    'Protected WithEvents GrdInfrac As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents lblMonto As System.Web.UI.WebControls.Label
    'Protected WithEvents lbltotal As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lbltitulo.Text = Request.QueryString("identificacion")
        Leer_datos(Me.lbltitulo.Text)
    End Sub


    Private Sub GrdInfrac_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GrdInfrac.PageIndexChanged
        Me.GrdInfrac.CurrentPageIndex = e.NewPageIndex
        Enlazar_DataGrid()
        Me.GrdInfrac.DataBind()
    End Sub

    Private Sub btn_regresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_regresar.Click
        Response.Redirect("infracciones_pendientes.aspx")
    End Sub

    Private Sub GrdInfrac_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles GrdInfrac.SortCommand
        Dim SortActual As String
        Dim SortNuevo As String

        SortActual = GrdInfrac.Attributes("sortexpr")
        SortNuevo = e.SortExpression

        If Not (SortActual Is Nothing) AndAlso SortActual = SortNuevo Then
            SortNuevo &= " DESC"
        End If

        Me.GrdInfrac.Attributes("sortexpr") = SortNuevo
        Enlazar_DataGrid()


    End Sub

    ''Property SortField() As String
    ''    Get
    ''        Dim o As Object = ViewState("SortField")
    ''        If o Is Nothing Then
    ''            Return [String].Empty
    ''        End If
    ''        Return CStr(o)
    ''    End Get
    ''    Set(ByVal Value As String)
    ''        If Value = SortField Then
    ''            SortAscending = Not SortAscending
    ''        End If
    ''        ViewState("SortField") = Value
    ''    End Set
    ''End Property

    Property SortAscending() As Boolean
        Get
            Dim o As Object = ViewState("SortAscending")

            If o Is Nothing Then
                Return True
            End If
            Return CBool(o)
        End Get
        Set(ByVal Value As Boolean)
            ViewState("SortAscending") = Value
        End Set
    End Property

    Private Sub Enlazar_DataGrid()
        Dim sortexpr As String
        Dim ODataTable As DataTable
        Dim OInfracciones As New WS_Infracciones.pendientes
        ODataTable = OInfracciones.Inf_x_Identificacion(Me.lbltitulo.Text).Tables(0)
        sortexpr = Me.GrdInfrac.Attributes("SortExpr")
        If Not sortexpr Is Nothing AndAlso sortexpr.Length.ToString <> "" Then
            Dim dv As DataView
            dv = ODataTable.DefaultView
            dv.Sort = sortexpr.ToString
            Me.GrdInfrac.DataSource = dv
        End If

        Me.GrdInfrac.DataBind()
    End Sub



    Private Sub Leer_datos(ByVal VL_Rango As String)
        Try
            Dim ODataTable As DataTable
            Dim LIndice As Long
            Dim LSuma As Double
            Dim OInfracciones As New WS_Infracciones.pendientes
            ODataTable = OInfracciones.Inf_x_Identificacion(Me.lbltitulo.Text).Tables(0)
            For LIndice = 0 To ODataTable.Rows.Count - 1
                LSuma = LSuma + ODataTable.Rows(LIndice).Item("total")
            Next LIndice
            lbltotal.Text = Format(LSuma, "###,##0.00")
            Me.lblCantidad.Text = Format(ODataTable.Rows.Count, "###,##0")
            Me.GrdInfrac.DataSource = ODataTable
            Me.GrdInfrac.DataMember = "WEB_INTER_INFRACCIONES"
            Me.GrdInfrac.DataBind()

        Catch exFoto As System.Web.HttpException
            Me.GrdInfrac.CurrentPageIndex = 0
            Enlazar_DataGrid()
        End Try
    End Sub

End Class
