Public Class AlterarSenha
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oMensagem.ClearMessage()
    End Sub

    Protected Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        Dim sSenhaAntiga As String = Util.CriptoSenha(txtSenhaAntiga.Text)
        Dim sSenhaNova As String = Util.CriptoSenha(txtNovaSenha.Text)
        Dim usu As New Usuario
        Dim oRet As New RetornoGenerico

        Try
            oRet = usu.AlterarSenha(sSenhaAntiga, sSenhaNova)

        Catch ex As Exception

        End Try
        oMensagem.SetMessage(oRet)
    End Sub
End Class