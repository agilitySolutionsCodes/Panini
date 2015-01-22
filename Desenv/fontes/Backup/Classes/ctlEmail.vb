Imports System.Net.Mail

Public Class Email

#Region "Parametros para envio de E-mail"
    ' <param name="from">Endereco do Remetente</param>
    ' <param name="recepient">Destinatario</param>
    ' <param name="bcc">recipiente Bcc</param>
    ' <param name="cc">recipiente Cc</param>
    ' <param name="subject">Assunto do email</param>
    ' <param name="body">Corpo da mensagem de email</param>
#End Region

    Public Shared Function enviaMensagemEmail(ByVal from As String, ByVal recepient As ArrayList, ByVal subject As String, ByVal body As String) As RetornoGenerico

        Dim oRet As New RetornoGenerico
        Try
            Dim SmtpServer = New SmtpClient()
            Dim mail As New MailMessage()

            mail = New MailMessage()

            For Each Email As String In recepient
                mail.To.Add(Email.Replace(";", ""))
            Next

            mail.Subject = subject
            mail.Body = body

            ' Define o formato do email como HTML
            mail.IsBodyHtml = True
            ' Define a prioridade da mensagem como normal
            mail.Priority = MailPriority.Normal

            SmtpServer.Send(mail)
            oRet.Mensagem = "E-mail enviado com sucesso. "
        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = "Erro ao enviar e-mail. Por favor tente novamente mais tarde. " & ex.Message
        End Try

        Return oRet

    End Function

    Public Shared Function GruposEmail(ByVal sTela As String, ByVal sEmailUsuarioLogado As String) As ArrayList
        Dim aGrupo As New ArrayList

        aGrupo.Add("geditorial@panini.com.br")
        aGrupo.Add("gmarketing@panini.com.br")
        aGrupo.Add("gpcp@panini.com.br")
        aGrupo.Add("gfiscal@panini.com.br")

        If sTela = "GERAL" Then
            aGrupo.Add("geralpw@panini.com.br")
        ElseIf sTela = "EDITORIAL" Or sTela = "MERCADOAPROVA" Then
            aGrupo.Add(sEmailUsuarioLogado)
        End If

        Return aGrupo

    End Function

#Region "Grupos de Email"
    'GRUPOS DE E-MAIL
    'Grupo	Nome	Detalhe
    'GMSP	Mauricio de Souza	Fornecedor
    'GMhytos	Mythos	Fornecedor
    'GStudioBlog	Studio Blog	Fornecedor
    'GSK	SK	Fornecedor
    'GLuminis	Luminis	Fornecedor
    'GPrima	Prima	Fornecedor
    'GNovo	 	 
    'GEditorial	Editorial	Panini
    'GMarketing	Marketing	Panini
    'GPCP	PCP	Panini
    'GFiscal	Fiscal	Panini
#End Region

End Class
