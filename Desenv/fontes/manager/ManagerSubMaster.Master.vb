Public Class ManagerSubMaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pagina As String = System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath)
        If pagina = "Manager.aspx" Then
            lblItemMenu.Text = ">> MANUTENÇÃO USUÁRIO"
        ElseIf pagina = "AlterarSenha.aspx" Then
            lblItemMenu.Text = ">> ALTERAR SENHA"
        End If
    End Sub

End Class