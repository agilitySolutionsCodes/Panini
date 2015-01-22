Imports System.Data.SqlClient

Public Class DatasCadastro

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            oMensagem.ClearMessage()
            If PermiteAcesso() Then
                CarregarDatas()
            Else
                Response.Redirect("~/AcessoRestrito.aspx")
            End If
        End If

    End Sub

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        Dim oRet = Validacao()

        Dim oEmail As New RetornoGenerico
        Dim oData As New Data
        Dim reader As SqlDataReader
        Dim aDados As New ArrayList

        Try
            If oRet.Sucesso = True Then

                aDados.Add(hdnCodigo.Value)
                aDados.Add(drpLicenca.SelectedValue)
                aDados.Add(txtNome.Text)
                aDados.Add(txtPE.Text)
                aDados.Add(txtAL.Text)
                aDados.Add(txtAP.Text)
                aDados.Add(txtPG.Text)
                aDados.Add(txtED.Text)
                aDados.Add(txtEA.Text)

                reader = oData.IncluirData(aDados)

                If Not reader.HasRows Then

                    If String.IsNullOrEmpty(hdnCodigo.Value) Then
                        oEmail = EnviaEmail("I")
                    Else
                        oEmail = EnviaEmail("A")
                    End If

                    oRet.Sucesso = True
                    If String.IsNullOrEmpty(hdnCodigo.Value) Then
                        oRet.Mensagem = "Data incluida com sucesso. " & oEmail.Mensagem
                    Else
                        oRet.Mensagem = "Data alterada com sucesso. " & oEmail.Mensagem
                    End If

                    LimparCampos()
                    CarregarDatas()

                Else
                    oRet.Sucesso = False
                    oRet.Mensagem = "Erro na inclusão da data: " & reader.Read()
                End If
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
        End Try

        oMensagem.SetMessage(oRet)

    End Sub

    Public Function Validacao() As RetornoGenerico

        Dim oret As New RetornoGenerico

        oret.Sucesso = True


        If txtNome.Text = "" Then
            oret.Sucesso = False
            oret.Mensagem = "Título" + "<br/><br/>" + oret.Mensagem

        End If
        If txtPE.Text = "" Then
            oret.Sucesso = False
            oret.Mensagem = "Produção Editorial" + "<br/>" + oret.Mensagem
        End If

        If txtAL.Text = "" Then
            oret.Sucesso = False
            oret.Mensagem = "Aprovação Licenciante" + "<br/>" + oret.Mensagem
        End If

        If txtAP.Text = "" Then
            oret.Sucesso = False
            oret.Mensagem = "Liberação de Provas" + "<br/>" + oret.Mensagem
        End If

        If txtPG.Text = "" Then
            oret.Sucesso = False
            oret.Mensagem = "Produção Gráfica" + "<br/>" + oret.Mensagem
        End If

        If txtED.Text = "" Then
            oret.Sucesso = False
            oret.Mensagem = "Distribuidor" + "<br/>" + oret.Mensagem
        End If

        If txtEA.Text = "" Then
            oret.Sucesso = False
            oret.Mensagem = "Entrega Assinaturas" + "<br/>" + oret.Mensagem
        End If

        If drpLicenca.SelectedValue = "" Then
            oret.Sucesso = False
            oret.Mensagem = "Licenciante" + "<br/>" + oret.Mensagem
        End If

        oret.Mensagem = "Por Favor preencha os seguintes campos:" + "<br/><br/>" + oret.Mensagem

        Return oret

    End Function

    Public Sub CarregaLicenciantes()

        Dim oRet As New RetornoGenerico
        Dim oCLicenciante As New Cadastro
        Dim dt As DataTable

        Try
            If Not String.IsNullOrEmpty(drpLicenca.SelectedValue) Then
                oMensagem.ClearMessage()
            Else
                drpLicenca.Items.Clear()

                dt = oCLicenciante.ListarLicenciantes()
                If dt.Rows.Count > 0 Then
                    drpLicenca.DataSource = dt
                    drpLicenca.DataBind()
                    drpLicenca.Items.Insert(0, "Selecione o Licenciante")
                    drpLicenca.Items(0).Value = ""

                Else
                    drpLicenca.Items.Insert(0, "Não existem registros")
                    drpLicenca.Items(0).Value = ""
                End If
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Sub CarregarDatas()

        Dim oRet As New RetornoGenerico
        Dim oData As New Data
        Dim dt As New DataTable

        Try

            dt = oData.ListarDatas()

            If dt.Rows.Count > 0 Then
                grdItens.DataSource = dt
                grdItens.DataBind()

            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Sub LimparCampos()

        drpLicenca.ClearSelection()
        txtNome.Text = String.Empty
        txtPE.Text = String.Empty
        txtAL.Text = String.Empty
        txtAP.Text = String.Empty
        txtPG.Text = String.Empty
        txtED.Text = String.Empty
        txtEA.Text = String.Empty
        hdnCodigo.Value = String.Empty
        txtNome.Enabled = True
        drpLicenca.Enabled = True
        oMensagem.ClearMessage()

    End Sub

    Private Sub btnLimpar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpar.Click

        LimparCampos()

    End Sub

    Private Sub grdItens_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdItens.RowDeleting

    End Sub

    Private Sub grdItens_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdItens.RowCommand

        Dim gv As GridView = DirectCast(sender, GridView)
        oMensagem.ClearMessage()
        If e.CommandName = "Select" Or e.CommandName = "Delete" Then
            Dim nIndice As Integer = Integer.Parse(e.CommandArgument.ToString)
            Select Case e.CommandName
                Case "Select"
                    AtualizarCampos(grdItens.Rows(nIndice))
                Case "Delete"
                    RemoverItem(grdItens.Rows(nIndice))
            End Select
        End If

    End Sub

    Private Sub AtualizarCampos(ByVal oRow As GridViewRow)

        hdnCodigo.Value = DirectCast(oRow.FindControl("lblCodigo"), Label).Text
        txtNome.Text = DirectCast(oRow.FindControl("lblTitulo"), Label).Text
        drpLicenca.SelectedValue = DirectCast(oRow.FindControl("lblLicenca"), Label).Text
        txtPE.Text = DirectCast(oRow.FindControl("lblPE"), Label).Text
        txtAL.Text = DirectCast(oRow.FindControl("lblAL"), Label).Text
        txtAP.Text = DirectCast(oRow.FindControl("lblAP"), Label).Text
        txtPG.Text = DirectCast(oRow.FindControl("lblPG"), Label).Text
        txtED.Text = DirectCast(oRow.FindControl("lblED"), Label).Text
        txtEA.Text = DirectCast(oRow.FindControl("lblEA"), Label).Text

        txtNome.Enabled = False
        drpLicenca.Enabled = False

    End Sub

    Private Sub RemoverItem(ByVal oRow As GridViewRow)

        Dim sCodData As String = DirectCast(oRow.FindControl("lblCodigo"), Label).Text
        Dim oRet As New RetornoGenerico
        Dim oEmail As New RetornoGenerico
        Dim oData As New Data
        Dim reader As SqlDataReader

        Try
            If Not String.IsNullOrEmpty(sCodData) Then
                reader = oData.DeletarData(sCodData)
                If Not reader.HasRows Then
                    CarregarDatas()
                    oRet.Sucesso = True
                    oEmail = EnviaEmail("E")
                    oRet.Mensagem = "Data excluída com sucesso. " & oEmail.Mensagem
                Else
                    oRet.Sucesso = False
                    oRet.Mensagem = "Erro na exclusão da data."
                End If
            Else
                oRet.Sucesso = False
                oRet.Mensagem = "Erro na exclusão da data. Este item não possui código. Favor efetuar a exclusão novamente."
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
        End Try

        oMensagem.SetMessage(oRet)
        oMensagem.Focus()

    End Sub

    Private Function PermiteAcesso() As Boolean

        Dim aPermissoes As Array
        Dim bRet As Boolean = False

        aPermissoes = Util.Permissoes(5)
        If aPermissoes.Length > 0 Then
            If aPermissoes(0) = True Then 'Visualizar
                bRet = True
                If aPermissoes(1) = False Then 'Incluir
                    btnSalvar.Visible = False
                End If
                If aPermissoes(2) = False Then 'Alterar
                    DirectCast(grdItens.Columns(0), CommandField).ShowSelectButton = False
                End If
                If aPermissoes(3) = False Then 'Excluir
                    DirectCast(grdItens.Columns(0), CommandField).ShowDeleteButton = False
                End If
            End If
        End If

        Return bRet
    End Function

    Private Function EnviaEmail(ByVal sTipo As String) As RetornoGenerico

        Dim oRet As New RetornoGenerico
        Dim sConteudo As String
        Dim sTexto1 As String = ""
        Dim sTexto2 As String = ""
        Dim aEmail As New ArrayList

        sConteudo = "<table style='border-color: #808080; width:550px;' align='center' border='1' cellpadding='7' cellspacing='0'>"
        sConteudo += "<tr>"
        sConteudo += "<td colspan='2' align='left' style='font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #666666; font-weight: 700' valign='middle' > "
        sConteudo += " <img src='http://www.pwpanini.com.br/pw/imagens/logo.gif' />"
        sConteudo += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        If sTipo = "I" Then
            sConteudo += "Inclusão de Data"
            sTexto1 = "inclusão"
            sTexto2 = "Incluído"
        ElseIf sTipo = "A" Then
            sConteudo += "Alteração de Data"
            sTexto1 = "alteração"
            sTexto2 = "Alterado"
        ElseIf sTipo = "E" Then
            sConteudo += "Exclusão de Data"
            sTexto1 = "exclusão"
            sTexto2 = "Excluído"
        End If
        sConteudo += "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Titulo:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & txtNome.Text & "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Data da " & sTexto1 & ":</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & Date.Now & "</td>"
        sConteudo += "</tr>"
        sConteudo += "<tr>"
        sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'> " & sTexto2 & " por:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'> " & Session("NomeUsuario2") & "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "    <td colspan='2' align='center' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px;font-weight: bold;' >E-MAIL ENVIADO POR PW - PANINI WORKFLOW</td>"
        sConteudo += "</tr>"
        sConteudo += " </table>"
        sConteudo += "<br/><br/><br/>"

        aEmail = Email.GruposEmail("DATAS", Session("EmailUsuario"))
        oRet = Email.enviaMensagemEmail("", aEmail, "PW - Datas", sConteudo)

        Return oRet
    End Function

End Class