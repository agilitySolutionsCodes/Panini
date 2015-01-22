Public Class EsqueciSenha

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click

        Dim oRet As New RetornoGenerico
        Dim oUsuario As New Usuario
        Dim sSenha As String = ""
        Dim aEmail As New ArrayList

        Try
            oRet = oUsuario.EsqueciSenha(txtEmail.Text.Trim)
            If oRet.Sucesso Then
                sSenha = Util.DescriptoSenha(oRet.Chave)
                oRet = EnviaEmail(oRet.Descricao.Trim, sSenha)
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = "Erro ao consultar senha: " & ex.Message.ToString
        End Try

        oMensagem.SetMessage(oRet)

    End Sub

    Private Function EnviaEmail(ByVal sUsuario As String, ByVal sSenha As String) As RetornoGenerico

        Dim oRet As New RetornoGenerico
        Dim sConteudo As String
        Dim sTexto1 As String = ""
        Dim sTexto2 As String = ""
        Dim aEmail As New ArrayList

        sConteudo = "<table style='border-color: #808080; width:550px;' align='center' border='1' cellpadding='7' cellspacing='0'>"
        sConteudo += "<tr>"
        sConteudo += "<td colspan='2' align='left' style='font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #666666; font-weight: 700' valign='middle' > "
        sConteudo += " <img src='http://www.pwpanini.com.br/pw/imagens/logo.gif' />"
        sConteudo += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        sConteudo += "Senha PW - Solicitação de Resgate</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Usuário:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & sUsuario & "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>E-mail:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & txtEmail.Text & "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Senha:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & sSenha & "</td>"
        sConteudo += "</tr>"
        sConteudo += "<tr>"
        sConteudo += "    <td colspan='2' align='center' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px;font-weight: bold;' >E-mail enviado por PW - Pnanini Workflow </td>"
        sConteudo += "</tr>"
        sConteudo += " </table>"
        sConteudo += "<br/><br/><br/>"

        aEmail.Add(txtEmail.Text.Trim)
        oRet = Email.enviaMensagemEmail("", aEmail, "Senha PW", sConteudo)

        Return oRet

    End Function

End Class