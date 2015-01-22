Imports System.Data.SqlClient

Public Class MercadoCadastro

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
                ElseIf PreviousPage.AppRelativeVirtualPath.Contains("EditorialAprovacao.aspx") Then
                    sCodPlan = Request("cod_plan")
                    sEdicao = Request("edicao")
                Else
                    sCodPlan = CType(placeHolder.FindControl("drpPDFS"), DropDownList).SelectedValue
                    sEdicao = CType(placeHolder.FindControl("drpEdicao"), DropDownList).SelectedValue
                End If


                hdnCodPlan.Value = sCodPlan
                Carregar(sCodPlan, sEdicao)

                PermiteAcesso()

            Else
                Dim oRet As New RetornoGenerico
                oRet.Sucesso = False
                oRet.Mensagem = "Esta página foi carregada pelo link errado. Por favor refaça o processo."
                oMensagem.SetMessage(oRet)
            End If
        End If

    End Sub

    Private Sub Carregar(ByVal sCodPlan As String, ByVal sEdicao As String)

        Dim oRet As New RetornoGenerico
        Dim oMercado As New Mercado
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim oData As New Data
        Dim sCodData As String

        Try
            'Limpar todos os campos de data
            LimparCampos()
            hdnEdicao.Value = sEdicao
            dt = oMercado.ConsultaMercado(sCodPlan, sEdicao)

            If dt.Rows.Count > 0 Then
                lblTitulo.Text = dt.Rows(0).Item("descricao_pdfs").ToString.Trim
                lblFase.Text = dt.Rows(0).Item("fase").ToString
                lblPDFS.Text = dt.Rows(0).Item("cod_pdfs").ToString
                lblDistr.Text = dt.Rows(0).Item("distribuicao").ToString
                lblPreco.Text = Format(dt.Rows(0).Item("preco"), "#,###.00")
                lblFormato.Text = dt.Rows(0).Item("formato").ToString
                lblBinding.Text = dt.Rows(0).Item("binding").ToString
                lblPag.Text = dt.Rows(0).Item("qtde_paginas").ToString
                lblPer.Text = dt.Rows(0).Item("periodicidade").ToString
                hdnDivisao.Value = dt.Rows(0).Item("divisao").ToString

                'Verificar se existe mais de uma edição para o mês e mostrar data
                dt2 = oMercado.PesquisarOutrasEdicoes(lblPDFS.Text, dt.Rows(0).Item("mes").ToString, dt.Rows(0).Item("ano").ToString, dt.Rows(0).Item("tipo").ToString, sEdicao)
                If dt2.Rows.Count > 0 Then
                    pnlMsgEdicoes.Visible = True
                    lblMsgEdicoes.Text = "Existe mais de uma edição para este mês: "
                    For Each linha In dt2.Rows
                        lblMsgEdicoes.Text += linha("edicao") & "   "
                    Next
                End If

                pnlEdicoes.Visible = False
                pnlSemEdicoes.Visible = True
                If dt.Rows(0).Item("tipo").ToString = "R" Then
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
                    LinkSelecionado(dt.Rows(0), sEdicao)
                    pnlEdicoes.Visible = True
                    pnlSemEdicoes.Visible = False
                ElseIf dt.Rows(0).Item("tipo").ToString = "L" Then
                    lblUnicoEdicao.Text = "LIVRO"
                Else
                    lblUnicoEdicao.Text = "COLECIONÁVEL"
                End If
                sCodData = dt.Rows(0).Item("cod_data").ToString

                drpMkt.SelectedValue = dt.Rows(0).Item("tipo_ocorrencia_mkt").ToString
                drpEditorial.SelectedValue = dt.Rows(0).Item("tipo_ocorrencia_edit").ToString
                txtMkt.Text = dt.Rows(0).Item("obs_ocorrencia_mkt").ToString
                txtEditorial.Text = dt.Rows(0).Item("obs_ocorrencia_edit").ToString

                CarregarDatas(dt)
                AtualizarSemaforo()

                dt = oData.ListarDatasPorLicenca()
                If dt.Rows.Count > 0 Then
                    drpDatas.DataSource = dt
                    drpDatas.DataBind()
                    drpDatas.Items.Insert(0, "Selecione")
                    drpDatas.Items(0).Value = ""

                    drpDatas.SelectedValue = sCodData
                Else
                    drpDatas.Items.Insert(0, "Datas não cadastradas")
                    drpDatas.Items(0).Value = ""
                End If

            Else
                oRet.Sucesso = False
                oRet.Mensagem = "Registros não encontrados"
                oMensagem.SetMessage(oRet)
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Sub LimparCampos()

        txtDPL.Text = String.Empty
        txtDRL.Text = String.Empty
        txtDAL.Text = String.Empty
        txtDPPE.Text = String.Empty
        txtDRPE.Text = String.Empty
        txtDAPE.Text = String.Empty
        txtDPAL.Text = String.Empty
        txtDRAL.Text = String.Empty
        txtDAAL.Text = String.Empty
        txtDPAP.Text = String.Empty
        txtDRAP.Text = String.Empty
        txtDAAP.Text = String.Empty
        txtDPPG.Text = String.Empty
        txtDRPG.Text = String.Empty
        txtDAPG.Text = String.Empty
        txtDPED.Text = String.Empty
        txtDRED.Text = String.Empty
        txtDAED.Text = String.Empty
        txtDPEA.Text = String.Empty
        txtDREA.Text = String.Empty
        txtDAEA.Text = String.Empty
        txtDPF2.Text = String.Empty
        txtDRF2.Text = String.Empty
        txtDAF2.Text = String.Empty
        txtDPREL.Text = String.Empty
        txtDRREL.Text = String.Empty
        txtDAREL.Text = String.Empty

        imgSemaforo.ImageUrl = "imagens/Verde.jpg"

    End Sub

    Private Sub AtualizarSemaforo()

        If IsDate(txtDPL.Text) And IsDate(txtDRL.Text) Then
            If DateTime.Compare(txtDPL.Text, txtDRL.Text) < 0 Then
                imgSemaforo.ImageUrl = "imagens/Vermelho.jpg"
            ElseIf DateTime.Compare(txtDPL.Text, txtDRL.Text) = 0 Then
                imgSemaforo.ImageUrl = "imagens/Amarelo.jpg"
            Else
                imgSemaforo.ImageUrl = "imagens/Verde.jpg"
            End If
        End If

    End Sub

    Private Sub CarregarDatas(ByVal dt As DataTable)

        For Each dr As DataRow In dt.Rows
            If dr.Item("tipo_data").ToString = "DL" Then
                txtDPL.Text = dr.Item("dt_prevista").ToString
                txtDRL.Text = dr.Item("dt_real").ToString
                txtDAL.Text = dr.Item("diferenca").ToString
                txtDAL.CssClass = VerificaDatas(CInt(txtDAL.Text))
                hdnAlteracao.Value = "S"
            ElseIf dr.Item("tipo_data").ToString = "DPE" Then
                txtDPPE.Text = dr.Item("dt_prevista").ToString
                txtDRPE.Text = dr.Item("dt_real").ToString
                txtDAPE.Text = dr.Item("diferenca").ToString
                txtDAPE.CssClass = VerificaDatas(CInt(txtDAPE.Text))
                hdnAlteracao.Value = "S"
            ElseIf dr.Item("tipo_data").ToString = "DAL" Then
                txtDPAL.Text = dr.Item("dt_prevista").ToString
                txtDRAL.Text = dr.Item("dt_real").ToString
                txtDAAL.Text = dr.Item("diferenca").ToString
                txtDAAL.CssClass = VerificaDatas(CInt(txtDAAL.Text))
                hdnAlteracao.Value = "S"
            ElseIf dr.Item("tipo_data").ToString = "DAP" Then
                txtDPAP.Text = dr.Item("dt_prevista").ToString
                txtDRAP.Text = dr.Item("dt_real").ToString
                txtDAAP.Text = dr.Item("diferenca").ToString
                txtDAAP.CssClass = VerificaDatas(CInt(txtDAAP.Text))
                hdnAlteracao.Value = "S"
            ElseIf dr.Item("tipo_data").ToString = "DPG" Then
                txtDPPG.Text = dr.Item("dt_prevista").ToString
                txtDRPG.Text = dr.Item("dt_real").ToString
                txtDAPG.Text = dr.Item("diferenca").ToString
                txtDAPG.CssClass = VerificaDatas(CInt(txtDAPG.Text))
                hdnAlteracao.Value = "S"
            ElseIf dr.Item("tipo_data").ToString = "DED" Then
                txtDPED.Text = dr.Item("dt_prevista").ToString
                txtDRED.Text = dr.Item("dt_real").ToString
                txtDAED.Text = dr.Item("diferenca").ToString
                txtDAED.CssClass = VerificaDatas(CInt(txtDAED.Text))
                hdnAlteracao.Value = "S"
            ElseIf dr.Item("tipo_data").ToString = "DEA" Then
                txtDPEA.Text = dr.Item("dt_prevista").ToString
                txtDREA.Text = dr.Item("dt_real").ToString
                txtDAEA.Text = dr.Item("diferenca").ToString
                txtDAEA.CssClass = VerificaDatas(CInt(txtDAEA.Text))
                hdnAlteracao.Value = "S"
            ElseIf dr.Item("tipo_data").ToString = "DF2" Then
                txtDPF2.Text = dr.Item("dt_prevista").ToString
                txtDRF2.Text = dr.Item("dt_real").ToString
                txtDAF2.Text = dr.Item("diferenca").ToString
                txtDAF2.CssClass = VerificaDatas(CInt(txtDAF2.Text))
                hdnAlteracao.Value = "S"
            ElseIf dr.Item("tipo_data").ToString = "DREL" Then
                txtDPREL.Text = dr.Item("dt_prevista").ToString
                txtDRREL.Text = dr.Item("dt_real").ToString
                txtDAREL.Text = dr.Item("diferenca").ToString
                txtDAREL.CssClass = VerificaDatas(CInt(txtDAREL.Text))
                hdnAlteracao.Value = "S"
            End If

            'Caso seja colecionavel e nao tiver a data de lancamento preenchido, preencher com a data cadastrada no planejamento
            If Not IsDate(txtDPL.Text) And dt.Rows(0).Item("tipo").ToString = "C" Then
                txtDPL.Text = dt.Rows(0).Item("data_lcto").ToString
            End If

        Next

    End Sub

    Public Sub CarregaEdicao(ByVal sender As Object, ByVal e As System.EventArgs)

        Carregar(hdnCodPlan.Value, sender.text)

    End Sub

    Private Sub LinkSelecionado(ByVal dt As DataRow, ByVal sEdicao As String)

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
        If dt.Item("edicao_jan").ToString = sEdicao Then
            lnkJan.CssClass = "linkSelecionado"
        ElseIf dt.Item("edicao_fev").ToString = sEdicao Then
            lnkFev.CssClass = "linkSelecionado"
        ElseIf dt.Item("edicao_mar").ToString = sEdicao Then
            lnkMar.CssClass = "linkSelecionado"
        ElseIf dt.Item("edicao_abr").ToString = sEdicao Then
            lnkAbr.CssClass = "linkSelecionado"
        ElseIf dt.Item("edicao_mai").ToString = sEdicao Then
            lnkMai.CssClass = "linkSelecionado"
        ElseIf dt.Item("edicao_jun").ToString = sEdicao Then
            lnkJun.CssClass = "linkSelecionado"
        ElseIf dt.Item("edicao_jul").ToString = sEdicao Then
            lnkJul.CssClass = "linkSelecionado"
        ElseIf dt.Item("edicao_ago").ToString = sEdicao Then
            lnkAgo.CssClass = "linkSelecionado"
        ElseIf dt.Item("edicao_set").ToString = sEdicao Then
            lnkSet.CssClass = "linkSelecionado"
        ElseIf dt.Item("edicao_out").ToString = sEdicao Then
            lnkOut.CssClass = "linkSelecionado"
        ElseIf dt.Item("edicao_nov").ToString = sEdicao Then
            lnkNov.CssClass = "linkSelecionado"
        ElseIf dt.Item("edicao_dez").ToString = sEdicao Then
            lnkDez.CssClass = "linkSelecionado"
        End If

    End Sub

    Public Sub ConfereData(ByVal sender As Object, ByVal e As System.EventArgs)

        If drpDatas.SelectedValue <> "" And IsDate(txtDPL.Text) Then
            PreencheDatas()
        End If

    End Sub

    Private Sub PreencheDatas()
        Dim dt As DataTable
        Dim oData As New Data
        Dim oRet As New RetornoGenerico

        Try

            dt = oData.ListarDatas(drpDatas.SelectedValue)

            If dt.Rows.Count > 0 Then

                txtDPPE.Text = CalculaData(FormatDateTime(txtDPL.Text, DateFormat.ShortDate), dt.Rows(0).Item("dt_producao"))
                txtDPAL.Text = CalculaDataSoma(FormatDateTime(txtDPPE.Text, DateFormat.ShortDate), dt.Rows(0).Item("dt_aprov_licenc"))
                txtDPAP.Text = CalculaDataSoma(FormatDateTime(txtDPAL.Text, DateFormat.ShortDate), dt.Rows(0).Item("dt_aprov_provas"))
                txtDPPG.Text = CalculaDataSoma(FormatDateTime(txtDPAP.Text, DateFormat.ShortDate), dt.Rows(0).Item("dt_grafica"))
                txtDPED.Text = CalculaData(FormatDateTime(txtDPPE.Text, DateFormat.ShortDate), dt.Rows(0).Item("dt_entr_distr"))
                txtDPEA.Text = CalculaData(FormatDateTime(txtDPPE.Text, DateFormat.ShortDate), dt.Rows(0).Item("dt_entr_ass"))
                'Outro Cálculo
                'F2 - Data lançamento + PEB + 60 dias
                Dim nPEB As Integer = VerificaPEB()
                Dim DataLcto As Date = FormatDateTime(txtDPL.Text, DateFormat.ShortDate)
                txtDPF2.Text = DateAdd(DateInterval.Day, nPEB + 60, DataLcto)
                'Rel - data lançamento + peb + 120 dias
                txtDPREL.Text = DateAdd(DateInterval.Day, nPEB + 120, DataLcto)

                txtDRL.Text = txtDPL.Text
                txtDRPE.Text = txtDPPE.Text
                txtDRAL.Text = txtDPAL.Text
                txtDRAP.Text = txtDPAP.Text
                txtDRPG.Text = txtDPPG.Text
                txtDRED.Text = txtDPED.Text
                txtDREA.Text = txtDPEA.Text
                txtDRF2.Text = txtDPF2.Text
                txtDRREL.Text = txtDPREL.Text

                txtDAL.Text = "0"
                txtDAPE.Text = "0"
                txtDAAL.Text = "0"
                txtDAAP.Text = "0"
                txtDAPG.Text = "0"
                txtDAED.Text = "0"
                txtDAEA.Text = "0"
                txtDAF2.Text = "0"
                txtDAREL.Text = "0"

                txtDAL.CssClass = "fontVerde"
                txtDAPE.CssClass = "fontVerde"
                txtDAAL.CssClass = "fontVerde"
                txtDAAP.CssClass = "fontVerde"
                txtDAPG.CssClass = "fontVerde"
                txtDAED.CssClass = "fontVerde"
                txtDAEA.CssClass = "fontVerde"
                txtDAF2.CssClass = "fontVerde"
                txtDAREL.CssClass = "fontVerde"
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Sub PreencheDatasReais()

        Dim nDiferenca As Integer
        Dim sClasse As String

        'Outro Cálculo
        'F2 - Data lançamento + PEB + 60 dias
        Dim nPEB As Integer = VerificaPEB()
        Dim DataLcto As Date = FormatDateTime(txtDRL.Text, DateFormat.ShortDate)
        txtDRF2.Text = DateAdd(DateInterval.Day, nPEB + 60, DataLcto)

        'Rel - data lançamento + peb + 120 dias
        txtDRREL.Text = DateAdd(DateInterval.Day, nPEB + 120, DataLcto)


        If IsDate(txtDRF2.Text) And IsDate(txtDPF2.Text) Then
            nDiferenca = DateDiff(DateInterval.Day, CDate(txtDRF2.Text), CDate(txtDPF2.Text))
            sClasse = VerificaDatas(nDiferenca)
            txtDAF2.Text = nDiferenca.ToString
            txtDAF2.CssClass = sClasse
        End If

        If IsDate(txtDRREL.Text) And IsDate(txtDPREL.Text) Then
            nDiferenca = DateDiff(DateInterval.Day, CDate(txtDRREL.Text), CDate(txtDPREL.Text))
            sClasse = VerificaDatas(nDiferenca)
            txtDAREL.Text = nDiferenca.ToString
            txtDAREL.CssClass = sClasse
        End If

    End Sub

    Private Function CalculaData(ByVal pDataInicial As DateTime, ByVal pDias As Integer) As String

        Dim resultado As DateTime = pDataInicial

        While (pDias > -1)
            'Se é sábado, domingo ou feriado, ando um dia pra frente
            If (resultado.DayOfWeek = DayOfWeek.Saturday Or resultado.DayOfWeek = DayOfWeek.Sunday) Then
                resultado = resultado.AddDays(-1)
                'Ou se quiser adicionar um dia útil (X horas trabalhadas = 1 dia útil)
            ElseIf (pDias > 0) Then
                resultado = resultado.AddDays(-1)
                pDias -= 1
                'Se a data final for no fim de semana ou feriado
            ElseIf (pDias = 0) Then
                While (resultado.DayOfWeek = DayOfWeek.Saturday Or resultado.DayOfWeek = DayOfWeek.Sunday)
                    resultado = resultado.AddDays(-1)
                End While
                pDias = -1
            End If
        End While

        Return resultado

    End Function

    Private Function CalculaDataSoma(ByVal pDataInicial As DateTime, ByVal pDias As Integer) As String

        Dim resultado As DateTime = pDataInicial

        While (pDias > -1)
            'Se é sábado, domingo ou feriado, ando um dia pra frente
            If (resultado.DayOfWeek = DayOfWeek.Saturday Or resultado.DayOfWeek = DayOfWeek.Sunday) Then
                resultado = resultado.AddDays(1)
                'Ou se quiser adicionar um dia útil (X horas trabalhadas = 1 dia útil)
            ElseIf (pDias > 0) Then
                resultado = resultado.AddDays(1)
                pDias -= 1
                'Se a data final for no fim de semana ou feriado
            ElseIf (pDias = 0) Then
                While (resultado.DayOfWeek = DayOfWeek.Saturday Or resultado.DayOfWeek = DayOfWeek.Sunday)
                    resultado = resultado.AddDays(-1)
                End While
                pDias = -1
            End If
        End While

        Return resultado

    End Function

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        Dim oRet As New RetornoGenerico
        Dim oRetEmail As New RetornoGenerico
        Dim oMercado As New Mercado
        Dim sDatasPrevistas As String = String.Empty
        Dim sDatasReais As String = String.Empty
        Dim sDiferencaDatas As String = String.Empty
        Dim sTiposData As String = String.Empty

        Try
            oRet = Validar()

            If oRet.Sucesso Then

                'Monta string com datas previstas, reais e atrasadas
                sDatasPrevistas += txtDPL.Text & ";" & txtDPPE.Text & ";" & txtDPAL.Text & ";" & txtDPAP.Text & ";" & txtDPPG.Text & ";" & txtDPED.Text & ";" & txtDPEA.Text & ";" & txtDPF2.Text & ";" & txtDPREL.Text & ";"
                sDatasReais += txtDRL.Text & ";" & txtDRPE.Text & ";" & txtDRAL.Text & ";" & txtDRAP.Text & ";" & txtDRPG.Text & ";" & txtDRED.Text & ";" & txtDREA.Text & ";" & txtDRF2.Text & ";" & txtDRREL.Text & ";"
                sDiferencaDatas += txtDAL.Text & ";" & txtDAPE.Text & ";" & txtDAAL.Text & ";" & txtDAAP.Text & ";" & txtDAPG.Text & ";" & txtDAED.Text & ";" & txtDAEA.Text & ";" & txtDAF2.Text & ";" & txtDAREL.Text & ";"
                sTiposData += "DL;DPE;DAL;DAP;DPG;DED;DEA;DF2;DREL;"
                sDatasPrevistas = sDatasPrevistas.Replace("__/__/____", "")
                sDatasReais = sDatasReais.Replace("__/__/____", "")

                oMercado.IncluirDatasMercado(hdnCodPlan.Value, hdnEdicao.Value, drpDatas.SelectedValue, sDatasPrevistas, sDatasReais, sDiferencaDatas, sTiposData, drpMkt.SelectedValue, txtMkt.Text, drpEditorial.SelectedValue, txtEditorial.Text)

                oRetEmail = EnviaEmail("I")

                Carregar(hdnCodPlan.Value, hdnEdicao.Value)
                PermiteAcesso()

                oRet.Sucesso = True
                oRet.Mensagem = "Dados salvos com sucesso. " & oRetEmail.Mensagem
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
        End Try

        oMensagem.SetMessage(oRet)

    End Sub

    Private Function Validar() As RetornoGenerico
        Dim oRet As New RetornoGenerico

        If oRet.Sucesso Then
            If drpMkt.SelectedValue = "" Or String.IsNullOrEmpty(txtMkt.Text.Trim) Then
                If IsDate(txtDPL.Text) And IsDate(txtDRL.Text) Then
                    If DateTime.Compare(txtDPL.Text, txtDRL.Text) < 0 Then
                        oRet.Sucesso = False
                        oRet.Mensagem = "Data de lançamento real é maior que a prevista. Favor preencher a ocorrência e a observação."
                        pnlMkt.Visible = True
                        drpMkt.Enabled = True
                        txtMkt.Enabled = True
                    End If
                End If
            End If
        End If

        Return oRet
    End Function

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        LimparCampos()

    End Sub

    Protected Sub btnAprovar_Click(sender As Object, e As EventArgs) Handles btnAprovar.Click

        AprovaReprova(hdnCodPlan.Value, hdnEdicao.Value, "M", True)

    End Sub

    Protected Sub btnReprovar_Click(sender As Object, e As EventArgs) Handles btnReprovar.Click

        AprovaReprova(hdnCodPlan.Value, hdnEdicao.Value, "M", False)

    End Sub

    Private Sub AprovaReprova(ByVal sCodPlan As String, sEdicao As String, ByVal sAprovador As String, ByVal bAprova As Boolean)

        Dim oRet As New RetornoGenerico
        Dim oRetEmail As New RetornoGenerico
        Dim oMercado As New Mercado

        Try
            oMercado.AprovaReprovaPlan(sCodPlan, sEdicao, sAprovador, bAprova)
            oRet.Sucesso = True

            If bAprova Then
                oRetEmail = EnviaEmail("A")
                oRet.Mensagem = "Edição aprovada com sucesso. " & oRetEmail.Mensagem
            Else
                oRetEmail = EnviaEmail("R")
                oRet.Mensagem = "Edição reprovada com sucesso. " & oRetEmail.Mensagem
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
        End Try

        oMensagem.SetMessage(oRet)

    End Sub

    Public Sub CalculaDiferenca(sender As Object, e As EventArgs)

        Dim sClasse As String
        Dim nDiferenca As Integer = 0
        Dim sNomeCampo As String = CType(sender, TextBox).NamingContainer.ID

        If sNomeCampo = "txtDRL" Then
            If IsDate(txtDRL.Text) And IsDate(txtDPL.Text) Then
                PreencheDatasReais()
                nDiferenca = DateDiff(DateInterval.Day, CDate(txtDRL.Text), CDate(txtDPL.Text))
                sClasse = VerificaDatas(nDiferenca)
                txtDAL.Text = nDiferenca.ToString
                txtDAL.CssClass = sClasse
                AtualizarSemaforo()
            End If
        ElseIf sNomeCampo = "txtDRPE" Then
            If IsDate(txtDRPE.Text) And IsDate(txtDPPE.Text) Then
                nDiferenca = DateDiff(DateInterval.Day, CDate(txtDRPE.Text), CDate(txtDPPE.Text))
                sClasse = VerificaDatas(nDiferenca)
                txtDAPE.Text = nDiferenca.ToString
                txtDAPE.CssClass = sClasse
            End If
        ElseIf sNomeCampo = "txtDRAL" Then
            If IsDate(txtDRAL.Text) And IsDate(txtDPAL.Text) Then
                nDiferenca = DateDiff(DateInterval.Day, CDate(txtDRAL.Text), CDate(txtDPAL.Text))
                sClasse = VerificaDatas(nDiferenca)
                txtDAAL.Text = nDiferenca.ToString
                txtDAAL.CssClass = sClasse
            End If
        ElseIf sNomeCampo = "txtDRAP" Then
            If IsDate(txtDRAP.Text) And IsDate(txtDPAP.Text) Then
                nDiferenca = DateDiff(DateInterval.Day, CDate(txtDRAP.Text), CDate(txtDPAP.Text))
                sClasse = VerificaDatas(nDiferenca)
                txtDAAP.Text = nDiferenca.ToString
                txtDAAP.CssClass = sClasse
            End If
        ElseIf sNomeCampo = "txtDRPG" Then
            If IsDate(txtDRPG.Text) And IsDate(txtDPPG.Text) Then
                nDiferenca = DateDiff(DateInterval.Day, CDate(txtDRPG.Text), CDate(txtDPPG.Text))
                sClasse = VerificaDatas(nDiferenca)
                txtDAPG.Text = nDiferenca.ToString
                txtDAPG.CssClass = sClasse
            End If
        ElseIf sNomeCampo = "txtDRED" Then
            If IsDate(txtDRED.Text) And IsDate(txtDPED.Text) Then
                nDiferenca = DateDiff(DateInterval.Day, CDate(txtDRED.Text), CDate(txtDPED.Text))
                sClasse = VerificaDatas(nDiferenca)
                txtDAED.Text = nDiferenca.ToString
                txtDAED.CssClass = sClasse
            End If
        ElseIf sNomeCampo = "txtDREA" Then
            If IsDate(txtDREA.Text) And IsDate(txtDPEA.Text) Then
                nDiferenca = DateDiff(DateInterval.Day, CDate(txtDREA.Text), CDate(txtDPEA.Text))
                sClasse = VerificaDatas(nDiferenca)
                txtDAEA.Text = nDiferenca.ToString
                txtDAEA.CssClass = sClasse
            End If
        ElseIf sNomeCampo = "txtDRF2" Then
            If IsDate(txtDRF2.Text) And IsDate(txtDPF2.Text) Then
                nDiferenca = DateDiff(DateInterval.Day, CDate(txtDRF2.Text), CDate(txtDPF2.Text))
                sClasse = VerificaDatas(nDiferenca)
                txtDAF2.Text = nDiferenca.ToString
                txtDAF2.CssClass = sClasse
            End If
        ElseIf sNomeCampo = "txtDRREL" Then
            If IsDate(txtDRREL.Text) And IsDate(txtDPREL.Text) Then
                nDiferenca = DateDiff(DateInterval.Day, CDate(txtDRREL.Text), CDate(txtDPREL.Text))
                sClasse = VerificaDatas(nDiferenca)
                txtDAREL.Text = nDiferenca.ToString
                txtDAREL.CssClass = sClasse
            End If
        End If

    End Sub

    Private Function VerificaDatas(ByVal nDiferenca As Integer) As String

        Dim sCor As String

        If nDiferenca < 0 Then
            sCor = "fontVermelho"
        Else
            sCor = "fontVerde"
        End If

        Return sCor

    End Function

    Public Function VerificaPEB() As Integer
        Dim nValor As Integer = 0

        If lblPer.Text = "Q" Then
            nValor = 15
        ElseIf lblPer.Text = "M" Then
            nValor = 37
        ElseIf lblPer.Text.Contains("BTSEA") Then
            nValor = 60
        End If

        Return nValor

    End Function

    Private Sub PermiteAcesso()

        Dim aPermissoes As Array
        aPermissoes = Util.Permissoes(4)

        btnAprovar.Visible = False
        btnLimpar.Visible = False
        btnReprovar.Visible = False
        btnSalvar.Visible = False
        pnlEditorial.Visible = False
        pnlMkt.Visible = False
        If aPermissoes.Length > 0 Then
            If aPermissoes(1) = True Then 'Incluir
                btnLimpar.Visible = True
                btnSalvar.Visible = True
            End If

            If hdnAlteracao.Value = "S" Then
                If aPermissoes(4) = True Then 'Aprovar
                    btnAprovar.Visible = True
                End If
                If aPermissoes(5) = True Then 'REPROVAR
                    btnReprovar.Visible = True
                End If
                If aPermissoes(6) = True Then 'MARKETING
                    pnlMkt.Visible = True
                ElseIf aPermissoes(7) = True Then 'EDITORIAL
                    pnlEditorial.Visible = True
                    pnlMkt.Visible = True
                    drpMkt.Enabled = False
                    txtMkt.Enabled = False
                End If
                If aPermissoes(8) = True Then 'FORNECEDOR
                    'Só poderá mudar uma data se for pra menos e nunca pra mais 
                End If
            End If
        End If
    End Sub

    Private Function EnviaEmail(ByVal sTipo As String) As RetornoGenerico

        Dim oRet As New RetornoGenerico
        Dim sConteudo As String
        Dim sTexto1 As String = ""
        Dim sTexto2 As String = ""
        Dim aEmail As New ArrayList

        If sTipo = "I" Then
            sTexto1 = "Alteração"
            sTexto2 = "Alterado"
        ElseIf sTipo = "A" Then
            sTexto1 = "Aprovação"
            sTexto2 = "Aprovado"
        ElseIf sTipo = "R" Then
            sTexto1 = "Reprovação"
            sTexto2 = "Reprovado"
        End If

        sConteudo = "<table style='border-color: #808080; width:550px;' align='center' border='1' cellpadding='7' cellspacing='0'>"
        sConteudo += "<tr>"
        sConteudo += "<td colspan='2' align='left' style='font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #666666; font-weight: 700' valign='middle' > "
        sConteudo += " <img src='http://www.pwpanini.com.br/pw/imagens/logo.gif' />"
        sConteudo += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        sConteudo += "Edição - " & hdnEdicao.Value & "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Titulo:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & lblTitulo.Text & "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Divisão:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & hdnDivisao.Value & "</td>"
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

        aEmail = Email.GruposEmail("MERCADO", Session("EmailUsuario"))
        oRet = Email.enviaMensagemEmail("", aEmail, "PW - Mercado", sConteudo)

        Return oRet

    End Function

End Class