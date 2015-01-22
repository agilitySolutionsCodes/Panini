Imports System.Web.Security.FormsAuthentication

Public Class Login1

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub objLogin_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles objLogin.Authenticate

        Dim oRet As New RetornoGenerico
        Dim oLogin As WebControls.Login = DirectCast(sender, WebControls.Login)
        Dim usuario As New Usuario
        Dim sSenha As String = Util.CriptoSenha(objLogin.Password)

        Try
            oRet = usuario.Autenticar(objLogin.UserName.Trim, sSenha)
            If oRet.Sucesso Then
                Dim ticket As New FormsAuthenticationTicket(1, oRet.Codigo, Now, Now.AddMinutes(55), True, "")
                Response.Cookies(".ASPXAUTH").Value = Encrypt(ticket)
                Session.Add("CodUsuario", oRet.Codigo)
                Session.Add("NomeUsuario", "Olá " & oRet.Descricao)
                Session.Add("NomeUsuario2", oRet.Descricao)
                Session.Add("EmailUsuario", objLogin.UserName.Trim)

                oRet = usuario.RedirecionarUsuario(oRet.Codigo)

                RedirecionarPagInicial(oRet.Codigo)

            Else
                oLogin.FailureText = oRet.Mensagem
            End If

        Catch ex As Exception
            oLogin.FailureText = ex.Message

        End Try

    End Sub

    Public Function RedirecionarPagInicial(ByVal sCodigoTela As Integer)

        Select Case sCodigoTela
            Case 1
                Response.Redirect("RelatorioConsulta.aspx")
                Stop
            Case 2
                Response.Redirect("EditorialOpcoes.aspx")
                Stop
            Case 3
                Response.Redirect("ReservaCadastro.aspx")
                Stop
            Case 4
                Response.Redirect("MercadoConsulta.aspx")
                Stop
            Case 5
                Response.Redirect("MercadoConsulta.aspx")
                Stop
            Case 6
                Response.Redirect("DatasCadastro.aspx")
                Stop
            Case 7
                Response.Redirect("ConsultaPDFS.aspx")
                Stop
            Case 8
                Response.Redirect("PlanejamentoConsulta.aspx")
                Stop
            Case 9
                Response.Redirect("UploadPDFS.aspx")
                Stop
            Case 10
                Response.Redirect("UploadPlan.aspx")
                Stop
            Case 11
                Response.Redirect("Manager/Manager.aspx")
                Stop
            Case 12
                Response.Redirect("Manager/Manager.aspx")
                Stop
            Case 13
                Response.Redirect("CadastroFornecedor.aspx")
                Stop
        End Select

        Return sCodigoTela

    End Function

End Class