Public Class Infracciones_Pendientes
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    'Protected WithEvents ImageButton1 As System.Web.UI.WebControls.ImageButton
    'Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    'Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    'Protected WithEvents TxtDato As System.Web.UI.WebControls.TextBox
    'Protected WithEvents consultarValido1 As System.Web.UI.WebControls.ImageButton
    'Protected WithEvents Rad_Consulta As System.Web.UI.WebControls.RadioButtonList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub consultarValido1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles consultarValido1.Click
        Me.TxtDato.Text = Trim(UCase(Me.TxtDato.Text))
        Dim OInfracciones As New WS_Infracciones.pendientes
        Select Case Me.Rad_Consulta.SelectedValue
            Case "P"
                If OInfracciones.Existe_Infraccion("P", Trim(Me.TxtDato.Text)) = "N" Then
                    Call PMensaje("No existe infracciones pendientes " & _
                                      "del vehículo de placas " & Trim(Me.TxtDato.Text))
                    Exit Sub
                End If
                Response.Redirect("Inf_x_placa.aspx?placa=" & Trim(Me.TxtDato.Text))
            Case "I"
                If OInfracciones.Existe_Infraccion("I", Trim(Me.TxtDato.Text)) = "N" Then
                    Call PMensaje("No existe infracciones pendientes " & _
                                  "del cliente con identificación " & Trim(Me.TxtDato.Text))
                    Exit Sub
                End If
                Response.Redirect("Inf_x_Identi.aspx?identificacion=" & Trim(Me.TxtDato.Text))
        End Select
    End Sub


    Sub PMensaje(ByVal VL_mensaje As String)
        Dim sb As New System.Text.StringBuilder("")
        With sb
            .Append("<script language='JavaScript'>")
            .Append("alert(' " + VL_mensaje + " ');")
            .Append("</script")
            .Append(">")
        End With
        'Page.RegisterClientScriptBlock("PMensaje", sb.ToString())
        ClientScript.RegisterClientScriptBlock(Me.Page.GetType(), "PMensaje", sb.ToString())

    End Sub
End Class