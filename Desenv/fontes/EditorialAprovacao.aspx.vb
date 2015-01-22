Imports System.Data.SqlClient

Public Class EditorialAprovacao

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If PreviousPage IsNot Nothing And PreviousPage.IsCrossPagePostBack Then
                If PreviousPage.AppRelativeVirtualPath.Contains("DetalheEdicao.aspx") Then
                    Dim placeHolder As Control = CType(PreviousPage.Master.Controls(0).FindControl("cMaster"), Control).FindControl("MainContent")
                    hdnTpEditorial.Value = CType(placeHolder.FindControl("hdnDivisao"), HiddenField).Value
                Else
                    hdnTpEditorial.Value = Request("tipo")
                End If

                AlteraImagem(hdnTpEditorial.Value)

                If String.IsNullOrEmpty(hdnTpEditorial.Value) Then
                    CarregaAno()
                End If

                ConsultaDatas(hdnTpEditorial.Value)

            End If
        End If

    End Sub

    Private Sub PermiteAcesso()

        Dim aPermissoes As Array
        Dim bRet As Boolean = False

        aPermissoes = Util.Permissoes(2)
        If aPermissoes.Length > 0 Then
            If aPermissoes(8) = True Then 'Fornecedor
                btnNaoAprovar.Visible = False
                btnLiberar.Visible = False
                grdItens.Columns(9).Visible = False
                hdnFornecedor.Value = "1" 'Sim
            Else
                btnLiberar.Visible = True
                If aPermissoes(4) = True Then 'Aprovar
                    btnAprovar.Visible = True
                End If
                If aPermissoes(5) = True Then 'Reprovar
                    btnNaoAprovar.Visible = True
                End If
            End If
        Else
            btnNaoAprovar.Visible = False
            btnAprovar.Visible = False
            btnLiberar.Visible = False
            grdItens.Columns(9).Visible = False
        End If

    End Sub

    Private Sub AlteraImagem(ByVal sDivisao As String)

        If String.IsNullOrEmpty(sDivisao) Then
            imgDivisao.Visible = False
            pnlPesquisa.Visible = True
        Else
            imgDivisao.Visible = True
            pnlPesquisa.Visible = False
            If sDivisao = "MARVEL" Then
                imgDivisao.ImageUrl = "imagens/marvel.jpg"
            ElseIf sDivisao = "MANGÁS" Then
                imgDivisao.ImageUrl = "imagens/PLANET_MANGA.jpg"
            ElseIf sDivisao = "MSP" Then
                imgDivisao.ImageUrl = "imagens/msp.jpg"
            ElseIf sDivisao = "DC" Then
                imgDivisao.ImageUrl = "imagens/dc.jpg"
            ElseIf sDivisao = "VERTIGO" Then
                imgDivisao.ImageUrl = "imagens/vertigo.jpg"
            ElseIf sDivisao = "NICKELODEON" Then
                imgDivisao.ImageUrl = "imagens/nickelodeon.jpg"
            ElseIf sDivisao = "WB" Then
                imgDivisao.ImageUrl = "imagens/wb.jpg"
            ElseIf sDivisao = "DIVERSOS" Then
                imgDivisao.ImageUrl = "imagens/diversos.jpg"
            End If
        End If

    End Sub

    Public Sub FiltraEditorial()

        oMensagem.ClearMessage()

        ConsultaDatas(drpDivisao.SelectedValue, drpAnoPlan.SelectedValue, drpPlan.SelectedValue, drpEdicaoPlan.SelectedValue)

    End Sub

    Private Sub ConsultaDatas(ByVal sDivisao As String, Optional ByVal sAno As String = "", Optional ByVal sProduto As String = "", Optional ByVal sEdicao As String = "")

        Dim oRet As New RetornoGenerico
        Dim dt As New DataTable
        Dim oEdit As New Editorial

        Try
            'Necessario verificar se usuario logado é fornecedor
            Dim aPermissoes As Array
            Dim bRet As Boolean = False

            aPermissoes = Util.Permissoes(2)
            If aPermissoes.Length > 0 Then
                bRet = aPermissoes(8) 'Fornecedor
            End If

            oRet = oEdit.ValidaUsuarioFornecedor(Session("CodUsuario"))

            dt = oEdit.Pesquisar(sDivisao, sAno, sProduto, sEdicao, bRet, oRet.Descricao)

            grdItens.DataSource = dt
            grdItens.DataBind()

            If dt.Rows.Count > 0 Then
                pnlLegenda.Visible = True
                btnAprovar.Visible = True
                btnNaoAprovar.Visible = True
            Else
                pnlLegenda.Visible = False
                btnAprovar.Visible = False
                btnNaoAprovar.Visible = False
                btnLiberar.Visible = False
                oRet.Sucesso = False
                oRet.Mensagem = "Não foram encontrados planejamentos disponíveis para aprovação."
                oMensagem.SetMessage(oRet)
            End If
            PermiteAcesso()

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)

        For Each row As GridViewRow In grdItens.Rows
            Dim chk As CheckBox = DirectCast(row.FindControl("checkbox2"), CheckBox)

            chk.Checked = CType(sender, CheckBox).Checked
        Next

    End Sub

    Protected Sub btnLiberar_Click(sender As Object, e As EventArgs) Handles btnLiberar.Click

        Dim aCodPlan As New ArrayList
        Dim aEdicao As New ArrayList
        Dim oRet As New RetornoGenerico
        Dim aEmailF As String = ""
        Dim aCondPEmail As String = ""
        Dim oRetEmail As New RetornoGenerico

        For Each row As GridViewRow In grdItens.Rows
            Dim chk As CheckBox = DirectCast(row.FindControl("checkbox2"), CheckBox)
            Dim cod_plan As Label = DirectCast(row.FindControl("lblCodPlan"), Label)
            Dim email As Label = DirectCast(row.FindControl("lblemailForn"), Label)
            Dim edicao As Label = DirectCast(row.FindControl("lblEdicao"), Label)

            If chk.Checked Then
                aCodPlan.Add(cod_plan.Text)
                aEdicao.Add(edicao.Text)

                aEmailF += email.Text & ";"
            End If
        Next

        If aCodPlan.Count = 0 Then

            oRet.Sucesso = False
            oRet.Mensagem = "Selecione pelo menos um item."
            oMensagem.SetMessage(oRet)
        Else

            Try

                Dim oEditorial As New Editorial

                oEditorial.LiberaPlanFornecedor(aCodPlan, aEdicao)

                oRetEmail = EnviaEmailForncedor(aCodPlan, aEmailF)

                oRet.Sucesso = True
                oRet.Mensagem = "Itens liberados para aprovação do fornecedor. " & oRetEmail.Mensagem
                ConsultaDatas(hdnTpEditorial.Value)

            Catch ex As Exception
                oRet.Sucesso = False
                oRet.Mensagem = ex.Message.ToString

            End Try

        End If
        oMensagem.SetMessage(oRet)
    End Sub

    Protected Sub btnAprovar_Click(sender As Object, e As EventArgs) Handles btnAprovar.Click

        AprovaReprovaEditorial(True)

    End Sub

    Protected Sub btnNaoAprovar_Click(sender As Object, e As EventArgs) Handles btnNaoAprovar.Click

        AprovaReprovaEditorial(False)

    End Sub

    Private Sub AprovaReprovaEditorial(ByVal bAprova As Boolean)

        Dim sCodPlan As String = ""
        Dim sEdicao As String = ""
        Dim aCodPlanRej As New ArrayList
        Dim aEdicaoRej As New ArrayList
        Dim oRet As New RetornoGenerico
        Dim oRetEmail As New RetornoGenerico
        Dim nCont As Integer

        For Each row As GridViewRow In grdItens.Rows
            Dim chk As CheckBox = DirectCast(row.FindControl("checkbox2"), CheckBox)
            Dim cod_plan As Label = DirectCast(row.FindControl("lblCodPlan"), Label)
            Dim edicao As Label = DirectCast(row.FindControl("lblEdicao"), Label)
            Dim aprova_forn As Label = DirectCast(row.FindControl("lblAprovadoForn"), Label)
            Dim titulo As Label = DirectCast(row.FindControl("lblTitulo"), Label)

            If chk.Checked Then
                If Not bAprova Or (bAprova And aprova_forn.Text = "True") Or hdnFornecedor.Value = "1" Then
                    sCodPlan += cod_plan.Text & ";"
                    sEdicao += edicao.Text & ";"
                Else
                    aCodPlanRej.Add(titulo.ToolTip)
                    aEdicaoRej.Add(edicao.Text)
                End If
            End If
        Next

        If String.IsNullOrEmpty(sCodPlan) And aCodPlanRej.Count = 0 Then
            oRet.Sucesso = False
            oRet.Mensagem = "Selecione pelo menos um item."
            oMensagem.SetMessage(oRet)
        ElseIf String.IsNullOrEmpty(sCodPlan) And aCodPlanRej.Count > 0 Then
            oRet.Sucesso = False
            oRet.Mensagem = "Os seguintes itens não podem ser aprovados pelo editorial, pois não foram aprovados pelo fornecedor:"
            For nCont = 0 To aCodPlanRej.Count - 1 Step nCont + 1
                oRet.Mensagem += " <br/>Titulo: " & aCodPlanRej(nCont) & " - Edição: " & aEdicaoRej(nCont)
            Next
            oMensagem.SetMessage(oRet)
        Else

            Try
                Dim oEditorial As New Editorial
                oEditorial.AprovaReprovaPlan(sCodPlan, sEdicao, bAprova, hdnFornecedor.Value)
                oRetEmail = EnviaEmail(bAprova, sCodPlan)
                oRet.Sucesso = True
                If bAprova Then
                    oRet.Mensagem = "Itens aprovados com sucesso. " & oRetEmail.Mensagem
                    If aCodPlanRej.Count > 0 Then
                        oRet.Mensagem += "<br/>Os seguintes itens não podem ser aprovados pelo editorial, pois não foram aprovados pelo fornecedor:"
                        For nCont = 0 To aCodPlanRej.Count - 1 Step nCont + 1
                            oRet.Mensagem += " <br/>Titulo: " & aCodPlanRej(nCont) & " - Edição: " & aEdicaoRej(nCont)
                        Next
                    End If
                Else
                    oRet.Mensagem = "Itens não aprovados com sucesso. " & oRetEmail.Mensagem
                End If
                ConsultaDatas(hdnTpEditorial.Value)

            Catch ex As Exception
                oRet.Sucesso = False
                oRet.Mensagem = ex.Message.ToString

            End Try

        End If

        oMensagem.SetMessage(oRet)

    End Sub

    Protected Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click

        Response.Redirect("~/EditorialOpcoes.aspx")

    End Sub

    Private Sub CarregaAno()

        Dim oRel As New Relatorio
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try

            reader = oRel.ListarAnos()
            If reader.HasRows Then
                drpAnoPlan.DataSource = reader
                drpAnoPlan.DataBind()
                drpAnoPlan.Items.Insert(0, "Selecione o ano")
                drpAnoPlan.Items(0).Value = ""
                reader.Close()
            Else
                drpAnoPlan.Items.Insert(0, "Não existem registros")
                drpAnoPlan.Items(0).Value = ""
            End If
            ApagaCampos()

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Public Sub CarregarDivisao()
        Dim oRel As New Relatorio
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            drpDivisao.Items.Clear()

            reader = oRel.ListarDivisao(drpAnoPlan.SelectedValue)
            If reader.HasRows Then
                drpDivisao.DataSource = reader
                drpDivisao.DataBind()
                drpDivisao.Items.Insert(0, "Todos")
                drpDivisao.Items(0).Value = ""
                drpDivisao.Items.Insert(0, "Selecione")
                drpDivisao.Items(0).Value = "0"
                reader.Close()
            Else
                drpDivisao.Items.Insert(0, "Não existem registros")
                drpDivisao.Items(0).Value = ""
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try
    End Sub

    Public Sub CarregaStatus()

        Dim oPlan As New Planejamento
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try

            reader = oPlan.ListaStatus()

            If drpStatus.SelectedValue = "" Then
                drpStatus.DataSource = reader
                drpStatus.DataBind()
                drpStatus.Items.Clear()
                drpStatus.Items.Insert(0, "Pendentes")
                drpStatus.Items(0).Value = "0"
                drpStatus.Items.Insert(0, "Aprovados")
                drpStatus.Items(0).Value = "1"
                drpStatus.Items.Insert(0, "Todos")
                drpStatus.Items(0).Value = ""
                drpStatus.Items.Insert(0, "Selecione")
                drpStatus.Items(0).Value = "Null"
                reader.Close()
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Public Sub CarregarPDFS()

        Dim oPlan As New Planejamento
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            drpPlan.Items.Clear()
            drpEdicaoPlan.Items.Clear()
            drpEdicaoPlan.Items.Insert(0, "---------------------")
            drpEdicaoPlan.Items(0).Value = ""

            reader = oPlan.ListaPlan(drpAnoPlan.SelectedValue, drpDivisao.SelectedValue, "", drpStatus.SelectedValue)
            If reader.HasRows Then
                drpPlan.DataSource = reader
                drpPlan.DataBind()
                drpPlan.Items.Insert(0, "Selecione")
                drpPlan.Items(0).Value = ""
                reader.Close()

            Else
                drpPlan.Items.Insert(0, "Não existem registros")
                drpPlan.Items(0).Value = ""
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Public Sub CarregarEdicoes()

        Dim oPlan As New Planejamento
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            drpEdicaoPlan.Items.Clear()

            If drpPlan.SelectedValue = "" Then
                drpEdicaoPlan.Items.Insert(0, "-----------")
                drpEdicaoPlan.Items(0).Value = ""
            Else
                reader = oPlan.ListaEdicoes(drpPlan.SelectedValue)
                If reader.HasRows Then
                    drpEdicaoPlan.DataSource = reader
                    drpEdicaoPlan.DataBind()
                    drpEdicaoPlan.Items.Insert(0, "Todos")
                    drpEdicaoPlan.Items(0).Value = "0"
                    reader.Close()
                Else
                    drpEdicaoPlan.Items.Insert(0, "Não existem registros")
                    drpEdicaoPlan.Items(0).Value = ""
                End If
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Sub ApagaCampos()

        drpDivisao.Items.Clear()
        drpDivisao.Items.Insert(0, "---------------")
        drpDivisao.Items(0).Value = ""

        drpPlan.Items.Clear()
        drpPlan.Items.Insert(0, "---------------")
        drpPlan.Items(0).Value = ""

        drpStatus.Items.Clear()
        drpStatus.Items.Insert(0, "---------------")
        drpStatus.Items(0).Value = ""

        drpEdicaoPlan.Items.Clear()
        drpEdicaoPlan.Items.Insert(0, "---------------")
        drpEdicaoPlan.Items(0).Value = ""

    End Sub

    Private Function EnviaEmail(ByVal bAprova As Boolean, ByVal sCodPlan As String) As RetornoGenerico

        Dim oRet As New RetornoGenerico
        Dim sConteudo As String = ""
        Dim aEmail As New ArrayList

        For Each row As GridViewRow In grdItens.Rows
            Dim chk As CheckBox = DirectCast(row.FindControl("checkbox2"), CheckBox)
            Dim descricao As Label = DirectCast(row.FindControl("lblTitulo"), Label)
            Dim edicao As Label = DirectCast(row.FindControl("lblEdicao"), Label)
            Dim divisao As Label = DirectCast(row.FindControl("lblDivisao"), Label)
            Dim dtLancamento As Label = DirectCast(row.FindControl("lblDL"), Label)
            Dim cod_plan As Label = DirectCast(row.FindControl("lblCodPlan"), Label)
            If sCodPlan.Contains(cod_plan.Text) Then

                sConteudo += "<table style='border-color: #808080; width:550px;' align='center' border='1' cellpadding='7' cellspacing='0'>"
                sConteudo += "<tr>"
                sConteudo += "<td colspan='2' align='left' style='font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #666666; font-weight: 700' valign='middle' > "
                sConteudo += " <img src='http://www.pwpanini.com.br/pw/imagens/logo.gif' />"
                sConteudo += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                sConteudo += "Edição - " & edicao.Text & "</td>"
                sConteudo += " </tr>"
                sConteudo += "<tr>"
                sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Titulo:</td>"
                sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & descricao.ToolTip & "</td>"
                sConteudo += " </tr>"
                sConteudo += "<tr>"
                sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Divisão:</td>"
                sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & divisao.Text & "</td>"
                sConteudo += " </tr>"
                sConteudo += "<tr>"
                If bAprova Then
                    sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Data da aprovação:</td>"
                    sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & Date.Now & "</td>"
                    sConteudo += "</tr>"
                    sConteudo += "<tr>"
                    sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Aprovado por:</td>"
                    sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'> " & Session("NomeUsuario2") & "</td>"
                Else
                    sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Data da reprovação:</td>"
                    sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>  " & Date.Now & "</td>"
                    sConteudo += " </tr>"
                    sConteudo += "<tr>"
                    sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Reprovado por:</td>"
                    sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'> " & Session("NomeUsuario2") & "</td>"
                End If
                sConteudo += " </tr>"
                sConteudo += "<tr>"
                sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Data do lançamento:</td>"
                sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'> " & dtLancamento.Text & "</td>"
                sConteudo += "</tr>"
                sConteudo += "<tr>"
                sConteudo += "    <td colspan='2' align='center' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px;font-weight: bold;' >E-MAIL ENVIADO POR PW - PANINI WORKFLOW</td>"
                sConteudo += "</tr>"
                sConteudo += " </table>"
                sConteudo += "<br/><br/><br/>"

            End If
        Next

        aEmail = Email.GruposEmail("EDITORIAL", Session("EmailUsuario"))
        oRet = Email.enviaMensagemEmail("", aEmail, "PW - Editorial", sConteudo)

        Return oRet

    End Function

    Private Function EnviaEmailForncedor(ByVal sCodPlan As ArrayList, ByVal sEmailFornecedor As String) As RetornoGenerico

        Dim oRet As New RetornoGenerico
        Dim sConteudo As String = ""
        Dim aEmail As New ArrayList

        For Each row As GridViewRow In grdItens.Rows
            Dim chk As CheckBox = DirectCast(row.FindControl("checkbox2"), CheckBox)
            Dim descricao As Label = DirectCast(row.FindControl("lblTitulo"), Label)
            Dim edicao As Label = DirectCast(row.FindControl("lblEdicao"), Label)
            Dim divisao As Label = DirectCast(row.FindControl("lblDivisao"), Label)
            Dim dtLancamento As Label = DirectCast(row.FindControl("lblDL"), Label)
            Dim cod_plan As Label = DirectCast(row.FindControl("lblCodPlan"), Label)
            If sCodPlan.Contains(cod_plan.Text) Then

                sConteudo += "<table style='border-color: #808080; width:550px;' align='center' border='1' cellpadding='7' cellspacing='0'>"
                sConteudo += "<tr>"
                sConteudo += "<td colspan='2' align='left' style='font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #666666; font-weight: 700' valign='middle' > "
                sConteudo += " <img src='http://www.pwpanini.com.br/pw/imagens/logo.gif' />"
                sConteudo += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                sConteudo += "Edição - " & edicao.Text & "</td>"
                sConteudo += " </tr>"
                sConteudo += "<tr>"
                sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Titulo:</td>"
                sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & descricao.ToolTip & "</td>"
                sConteudo += " </tr>"
                sConteudo += "<tr>"
                sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Divisão:</td>"
                sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & divisao.Text & "</td>"
                sConteudo += " </tr>"
                sConteudo += "<tr>"
                sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Data do lançamento:</td>"
                sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'> " & dtLancamento.Text & "</td>"
                sConteudo += "</tr>"
                sConteudo += "<tr>"
                sConteudo += "    <td colspan='2' align='center' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px;font-weight: bold;' >E-MAIL ENVIADO POR PW - PANINI WORKFLOW</td>"
                sConteudo += "</tr>"
                sConteudo += " </table>"
                sConteudo += "<br/><br/><br/>"

            End If
        Next

        aEmail = Email.GruposEmail("EDITORIAL", sEmailFornecedor)
        oRet = Email.enviaMensagemEmail("", aEmail, "PW - EDITORIAL", sConteudo)

        Return oRet

    End Function

End Class