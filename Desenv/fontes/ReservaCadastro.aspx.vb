Imports System.Data.SqlClient

Public Class ReservaCadastro

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If PreviousPage IsNot Nothing And PreviousPage.IsCrossPagePostBack Then

                Dim sCodPlan As String = ""
                Dim sEdicao As String = ""
                Dim placeHolder As Control = CType(PreviousPage.Master.Controls(0).FindControl("cMaster"), Control).FindControl("MainContent")

                If PreviousPage.AppRelativeVirtualPath.Contains("DetalheEdicao.aspx") Then
                    sCodPlan = CType(placeHolder.FindControl("hdnCodPlan"), HiddenField).Value
                    sEdicao = CType(placeHolder.FindControl("lblEdicao"), Label).Text
                Else
                    sCodPlan = CType(placeHolder.FindControl("drpPDFS"), DropDownList).SelectedValue
                    sEdicao = CType(placeHolder.FindControl("drpEdicao"), DropDownList).SelectedValue
                End If
                hdnCodPlan.Value = sCodPlan
                CarregaReserva(sCodPlan, sEdicao)
                PermiteAcesso()

            Else
                Dim oRet As New RetornoGenerico
                oRet.Sucesso = False
                oRet.Mensagem = "Erro ao abrir a página. Por favor refazer a consulta."
                oMensagem.SetMessage(oRet)
            End If
        End If

    End Sub

    Public Sub CarregaEdicao(ByVal sender As Object, ByVal e As System.EventArgs)

        oMensagem.ClearMessage()
        CarregaReserva(hdnCodPlan.Value, sender.text)

    End Sub

    Private Sub CarregaReserva(ByVal sCodPlan As String, ByVal sEdicao As String)

        Dim dt As New DataTable
        Dim oPlan As New Planejamento
        Dim oReserva As New Reserva
        Dim oRet As New RetornoGenerico

        Try
            hdnEdicao.Value = sEdicao

            'Cabeçalho
            dt = oPlan.BuscaPlan(sCodPlan)
            If dt.Rows.Count > 0 Then

                hdnDivisao.Value = dt.Rows(0).Item("divisao").ToString

                If dt.Rows(0).Item("tipo").ToString = "R" Then

                    lblDescricao.Text = dt.Rows(0).Item("descricao_pdfs").ToString
                    lblPDFS.Text = dt.Rows(0).Item("cod_pdfs").ToString
                    lnkJan.Text = dt.Rows(0).Item("edicao_jan").ToString
                    lnkFev.Text = dt.Rows(0).Item("edicao_fev").ToString
                    lnkMar.Text = dt.Rows(0).Item("edicao_mar").ToString
                    lnkAbr.Text = dt.Rows(0).Item("edicao_abr").ToString
                    lnkMai.Text = dt.Rows(0).Item("edicao_mai").ToString
                    lnkJun.Text = dt.Rows(0).Item("edicao_jun").ToString
                    lnkJul.Text = dt.Rows(0).Item("edicao_jul").ToString
                    lnkAgo.Text = dt.Rows(0).Item("edicao_ago").ToString
                    lnkSet.Text = dt.Rows(0).Item("edicao_set").ToString
                    lnkOut.Text = dt.Rows(0).Item("edicao_out").ToString
                    lnkNov.Text = dt.Rows(0).Item("edicao_nov").ToString
                    lnkDez.Text = dt.Rows(0).Item("edicao_dez").ToString

                    lnkJan.CssClass = "labelVermelho"
                    lnkFev.CssClass = "labelVermelho"
                    lnkMar.CssClass = "labelVermelho"
                    lnkAbr.CssClass = "labelVermelho"
                    lnkMai.CssClass = "labelVermelho"
                    lnkJun.CssClass = "labelVermelho"
                    lnkJul.CssClass = "labelVermelho"
                    lnkAgo.CssClass = "labelVermelho"
                    lnkSet.CssClass = "labelVermelho"
                    lnkOut.CssClass = "labelVermelho"
                    lnkNov.CssClass = "labelVermelho"
                    lnkDez.CssClass = "labelVermelho"
                    If dt.Rows(0).Item("edicao_jan").ToString = sEdicao Then
                        lnkJan.CssClass = "linkSelecionado"
                    ElseIf dt.Rows(0).Item("edicao_fev").ToString = sEdicao Then
                        lnkFev.CssClass = "linkSelecionado"
                    ElseIf dt.Rows(0).Item("edicao_mar").ToString = sEdicao Then
                        lnkMar.CssClass = "linkSelecionado"
                    ElseIf dt.Rows(0).Item("edicao_abr").ToString = sEdicao Then
                        lnkAbr.CssClass = "linkSelecionado"
                    ElseIf dt.Rows(0).Item("edicao_mai").ToString = sEdicao Then
                        lnkMai.CssClass = "linkSelecionado"
                    ElseIf dt.Rows(0).Item("edicao_jun").ToString = sEdicao Then
                        lnkJun.CssClass = "linkSelecionado"
                    ElseIf dt.Rows(0).Item("edicao_jul").ToString = sEdicao Then
                        lnkJul.CssClass = "linkSelecionado"
                    ElseIf dt.Rows(0).Item("edicao_ago").ToString = sEdicao Then
                        lnkAgo.CssClass = "linkSelecionado"
                    ElseIf dt.Rows(0).Item("edicao_set").ToString = sEdicao Then
                        lnkSet.CssClass = "linkSelecionado"
                    ElseIf dt.Rows(0).Item("edicao_out").ToString = sEdicao Then
                        lnkOut.CssClass = "linkSelecionado"
                    ElseIf dt.Rows(0).Item("edicao_nov").ToString = sEdicao Then
                        lnkNov.CssClass = "linkSelecionado"
                    ElseIf dt.Rows(0).Item("edicao_dez").ToString = sEdicao Then
                        lnkDez.CssClass = "linkSelecionado"
                    End If

                    pnlSemEdicoes.Visible = False
                    pnlEdicoes.Visible = True
                Else
                    lblUnicoTitulo.Text = dt.Rows(0).Item("descricao_pdfs").ToString
                    lblUnicoPDFS.Text = dt.Rows(0).Item("cod_pdfs").ToString
                    If dt.Rows(0).Item("tipo").ToString = "L" Then
                        lblUnicoEdicao.Text = "LIVRO" 'dt.Rows(0).Item("edicao_jan").ToString
                    Else
                        lblUnicoEdicao.Text = "COLECIONÁVEL" 'dt.Rows(0).Item("edicao_jan").ToString
                    End If

                    pnlSemEdicoes.Visible = True
                    pnlEdicoes.Visible = False
                End If
            End If

            'Reserva
            dt = oReserva.ListaReserva(sCodPlan, sEdicao)
            If dt.Rows.Count > 0 Then

                txtVarejo.Text = dt.Rows(0).Item("varejo").ToString
                txtAss.Text = dt.Rows(0).Item("assinaturas").ToString
                txtBienal.Text = dt.Rows(0).Item("bienal").ToString
                txtDoacao.Text = dt.Rows(0).Item("doacao").ToString
                txtExp.Text = dt.Rows(0).Item("exportacao").ToString
                txtPacote.Text = dt.Rows(0).Item("pacote").ToString
                txtOutros1.Text = dt.Rows(0).Item("outros1").ToString
                txtOutros2.Text = dt.Rows(0).Item("outros2").ToString
                txtOutros3.Text = dt.Rows(0).Item("outros3").ToString
                txtOutros4.Text = dt.Rows(0).Item("outros4").ToString
                lblTotal.Text = dt.Rows(0).Item("total").ToString
            Else
                ZerarCampos()
            End If


        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
            oMensagem.SetMessage(oRet)
        End Try



    End Sub

    Private Sub ZerarCampos()

        txtVarejo.Text = "0"
        txtAss.Text = "0"
        txtBienal.Text = "0"
        txtDoacao.Text = "0"
        txtExp.Text = "0"
        txtPacote.Text = "0"
        txtOutros1.Text = "0"
        txtOutros2.Text = "0"
        txtOutros3.Text = "0"
        txtOutros4.Text = "0"
        lblTotal.Text = "0"
        oMensagem.ClearMessage()

    End Sub

    Public Sub CalculaTotal(sender As Object, e As EventArgs)

        If String.IsNullOrEmpty(sender.text) Then
            sender.text = "0"
        End If

        lblTotal.Text = (CInt(txtVarejo.Text) + CInt(txtAss.Text) + CInt(txtExp.Text) + CInt(txtBienal.Text) + CInt(txtDoacao.Text) + CInt(txtPacote.Text) + CInt(txtOutros1.Text) + CInt(txtOutros2.Text) + CInt(txtOutros3.Text) + CInt(txtOutros4.Text)).ToString
        'Verificar uma maneira de colocar o foco no proximo campo

    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        Dim oReserva As New Reserva
        Dim aDados As New ArrayList
        Dim oRet As New RetornoGenerico
        Dim oEmail As New RetornoGenerico
        Dim reader As SqlDataReader

        Try
            aDados.Add(txtVarejo.Text) 'Varejo
            aDados.Add(txtAss.Text) 'Assinatura
            aDados.Add(txtExp.Text) 'Exportação
            aDados.Add(txtBienal.Text) 'Bienal
            aDados.Add(txtDoacao.Text) 'Doação
            aDados.Add(txtPacote.Text) 'Pacote
            aDados.Add(txtOutros1.Text) 'Outros1
            aDados.Add(txtOutros2.Text) 'Outros2
            aDados.Add(txtOutros3.Text) 'Outros3
            aDados.Add(txtOutros4.Text) 'Outros4
            aDados.Add(lblTotal.Text) 'Total

            reader = oReserva.IncluirReserva(hdnCodPlan.Value, hdnEdicao.Value, aDados)

            oEmail = EnviaEmail()

            oRet.Mensagem = "Reserva incluída com sucesso. " & oEmail.Mensagem

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
        End Try

        oMensagem.SetMessage(oRet)

    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        ZerarCampos()

    End Sub

    Private Sub PermiteAcesso()

        Dim aPermissoes As Array
        Dim bRet As Boolean = False

        aPermissoes = Util.Permissoes(3)
        If aPermissoes.Length > 0 Then
            If aPermissoes(1) = False Then 'Incluir
                btnGravar.Visible = False
                btnLimpar.Visible = False
            End If
        End If

    End Sub

    Private Function EnviaEmail() As RetornoGenerico

        Dim oRet As New RetornoGenerico
        Dim sConteudo As String
        Dim aEmail As New ArrayList

        sConteudo = "<table style='border-color: #808080; width:550px;' align='center' border='1' cellpadding='7' cellspacing='0'>"
        sConteudo += "<tr>"
        sConteudo += "<td colspan='2' align='left' style='font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #666666; font-weight: 700' valign='middle' > "
        sConteudo += " <img src='http://www.pwpanini.com.br/pw/imagens/logo.gif' />"
        sConteudo += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        sConteudo += "Edição - " & hdnEdicao.Value & "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Titulo:</td>"
        If String.IsNullOrEmpty(lblDescricao.Text) Then
            sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & lblUnicoTitulo.Text & "</td>"
        Else
            sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & lblDescricao.Text & "</td>"
        End If
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Divisão:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & hdnDivisao.Value & "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Data da alteração:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & Date.Now & "</td>"
        sConteudo += "</tr>"
        sConteudo += "<tr>"
        sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Alterado por:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'> " & Session("NomeUsuario2") & "</td>"
        
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "    <td colspan='2' align='center' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px;font-weight: bold;' >E-MAIL ENVIADO POR PW - PANINI WORKFLOW</td>"
        sConteudo += "</tr>"
        sConteudo += " </table>"
        sConteudo += "<br/><br/><br/>"

        aEmail = Email.GruposEmail("RESERVA", Session("EmailUsuario"))
        oRet = Email.enviaMensagemEmail("", aEmail, "PW - RESERVA DE ESTOQUE", sConteudo)

        Return oRet

    End Function

End Class