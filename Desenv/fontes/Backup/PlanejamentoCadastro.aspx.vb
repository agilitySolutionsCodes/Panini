Imports System.Data.SqlClient
Imports System.IO

Public Class PlanejamentoCadastro

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim oRet As New RetornoGenerico

            If PreviousPage IsNot Nothing Then
                Try
                    Dim placeHolder As Control = CType(PreviousPage.Master.Controls(0).FindControl("cMaster"), Control).FindControl("MainContent")
                    Dim hdnIncAlt As HiddenField = CType(placeHolder.FindControl("hdnIncAlt"), HiddenField)

                    CarregarFornecedores()
                    If hdnIncAlt.Value = "I" Then
                        PreencheInclusao(placeHolder)
                    Else
                        PreencheAlteracao(placeHolder)
                        PermiteAcesso()
                    End If

                Catch ex As Exception
                    oRet.Sucesso = False
                    oRet.Mensagem = ex.Message.ToString
                    oMensagem.SetMessage(oRet)
                    oMensagem.Focus()
                End Try

            Else
                oRet.Sucesso = False
                oRet.Mensagem = "Chamada a partir de página incorreta. Favor reiniciar o site."
                oMensagem.SetMessage(oRet)
                oMensagem.Focus()
            End If
        End If

    End Sub

    Private Sub PermiteAcesso()

        Dim aPermissoes As Array
        Dim bBloq As Boolean = False

        aPermissoes = Util.Permissoes(8)
        If aPermissoes.Length > 0 Then
            If aPermissoes(2) = False Then 'Alterar
                bBloq = True
            End If
        Else
            bBloq = True
        End If

        If bBloq Then
            btnSalvar.Visible = False
            btnLimpar1.Visible = False
            btnLimpar2.Visible = False
            btnLimpar3.Visible = False
            hdnValidaTab.Value = "N" 'Não faz validação de campos da tab já que é apenas visualização
        End If

    End Sub

    Private Sub CarregarFornecedores()

        Dim oPlan As New Planejamento
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            reader = oPlan.ListaFornecedores

            If reader.HasRows Then
                drpFornecedor.DataSource = reader
                drpFornecedor.DataBind()
                drpFornecedor.Items.Insert(0, "Selecione")
                drpFornecedor.Items(0).Value = ""
                reader.Close()
            Else
                drpFornecedor.Items.Insert(0, "Não existem registros")
                drpFornecedor.Items(0).Value = ""
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
            oMensagem.Focus()
        End Try

    End Sub

    Private Sub PreencheInclusao(ByVal placeHolder As Control)

        Dim oRet As New RetornoGenerico
        Dim drpTipo As DropDownList = CType(placeHolder.FindControl("drpTipo"), DropDownList)
        Dim drpPDFS As DropDownList = CType(placeHolder.FindControl("drpPDFS"), DropDownList)

        Dim oPDFS As New PDFS
        Dim dt As DataTable
        Dim sCodPlan = drpPDFS.SelectedValue

        drpFase.Items.Insert(0, " ")
        drpFase.Items(0).Value = ""

        If Not drpTipo.Text.Contains("COL") Then

            lblDescTipo.Text = "Publicação"

            pnlOcultarCol.Visible = True
            pnlEtapa2.Visible = False
            pnlEtapa3.Visible = False
            pnlEtapa1CodColec.Visible = False
            pnlEtapa1Outros.Visible = True
        Else
            lblDescTipo.Text = "COLECIONAVEIS"
            drpCategoria.Items.Insert(0, "COLECIONAVEIS")
            drpCategoria.Items(0).Value = "C"
            drpCategoria.SelectedValue = "C"
            drpCategoria.Enabled = False
            pnlEdicoes.Visible = False
            pnlOcultarCol.Visible = False
            pnlEtapa2.Visible = True
            pnlEtapa3.Visible = True
            lblDtLancto.Visible = True
            txtDtLancto.Visible = True
            lblFormato.Visible = False
            txtFormato.Visible = False
            lblPreco.Visible = False
            txtPreco1.Visible = False
            lblValReais.Visible = False
            drpPeriod.Enabled = False
            pnlEtapa1CodColec.Visible = True
            pnlEtapa1Outros.Visible = False
            txtAcabamento.Enabled = False
            txtCapa.Enabled = False
            txtMiolo.Enabled = False

        End If

        dt = oPDFS.SelecionarPDFS(sCodPlan)
        If dt.Rows.Count > 0 Then
            lblPDFS.Text = sCodPlan
            txtPDFSAlbum.Text = sCodPlan
            txtPDFSEnv.Text = sCodPlan
            lblDescPDFS.Text = dt.Rows(0).Item("descricao").ToString
            lblDescDivisao.Text = dt.Rows(0).Item("licenciante").ToString
            lblCodPanini.Text = dt.Rows(0).Item("cod_panini").ToString
            txtCodAlbum.Text = dt.Rows(0).Item("cod_panini").ToString
            lblColecao.Text = dt.Rows(0).Item("colecao").ToString
        Else
            oRet.Sucesso = False
            oRet.Mensagem = "PDFS não encontrado."
            oMensagem.SetMessage(oRet)
            oMensagem.Focus()
        End If

    End Sub

    Private Sub PreencheAlteracao(ByVal placeHolder As Control)

        Dim oRet As New RetornoGenerico
        Dim sCodPlan As String = CType(placeHolder.FindControl("drpPlan"), DropDownList).SelectedValue
        Dim sEdicao As String = CType(placeHolder.FindControl("drpEdicaoPlan"), DropDownList).SelectedValue
        Dim oPlan As New Planejamento
        Dim dt As DataTable

        dt = oPlan.BuscaPlanDetalhado(sCodPlan, sEdicao)

        If dt.Rows.Count > 0 Then
            hdnCodPlan.Value = sCodPlan
            hdnEdicao.Value = sEdicao
            tab2.Enabled = True
            tab3.Enabled = True
            TabContainer1.ActiveTabIndex = 0

            lblPDFS.Text = dt.Rows(0).Item("cod_pdfs").ToString
            txtPDFSAlbum.Text = dt.Rows(0).Item("cod_pdfs_album").ToString
            txtPDFSEnv.Text = dt.Rows(0).Item("cod_pdfs_envelope").ToString
            lblDescPDFS.Text = dt.Rows(0).Item("descricao_pdfs").ToString

            chkAssinatura.Checked = dt.Rows(0).Item("assinaturas")

            lblDescDivisao.Text = dt.Rows(0).Item("divisao").ToString & " - " & dt.Rows(0).Item("licenciante").ToString
            lblCodPanini.Text = dt.Rows(0).Item("cod_panini").ToString
            txtCodAlbum.Text = dt.Rows(0).Item("cod_panini_album").ToString
            txtCodEnv.Text = dt.Rows(0).Item("cod_panini_envelope").ToString
            txtAno.Text = dt.Rows(0).Item("ano").ToString
            lblColecao.Text = dt.Rows(0).Item("colecao").ToString
            txtFormato.Text = dt.Rows(0).Item("formato").ToString
            drpPeriod.SelectedValue = dt.Rows(0).Item("periodicidade").ToString
            VerificaPeriodicidade()
            txtPreco1.Text = Format(dt.Rows(0).Item("preco"), "#,##0.00")
            txtCustoEditorial.Text = Format(dt.Rows(0).Item("custo"), "#,##0.00")
            txtCustoEditorialP.Text = Format(dt.Rows(0).Item("preco_pagina"), "#,##0.00")
            txtCapa.Text = dt.Rows(0).Item("capa").ToString
            txtMiolo.Text = dt.Rows(0).Item("miolo").ToString

            txtQtdeCapa.Text = dt.Rows(0).Item("qtde_capa").ToString
            txtQtdeMiolo.Text = dt.Rows(0).Item("qtde_miolo").ToString
            txtQtdePag.Text = dt.Rows(0).Item("qtde_paginas").ToString

            PreencheCustoEdiTotal()

            If Not String.IsNullOrEmpty(dt.Rows(0).Item("imagem").ToString) Then
                imgUpload.ImageUrl = dt.Rows(0).Item("imagem").ToString
                hdnImgUpload.Value = dt.Rows(0).Item("imagem").ToString
            End If

            txtAcabamento.Text = dt.Rows(0).Item("acabamento").ToString
            txtBinding.Text = dt.Rows(0).Item("binding").ToString
            txtTipoServico.Text = dt.Rows(0).Item("shrink").ToString.Trim

            txtManuseio.Text = dt.Rows(0).Item("manuseio").ToString

            If dt.Rows(0).Item("existe_brinde") = True Then
                rdNao.Checked = False
                rdSim.Checked = True
                txtBrinde.Enabled = True
                txtBrinde.Text = dt.Rows(0).Item("brinde").ToString
            Else
                rdNao.Checked = True
            End If

            drpDistr.SelectedValue = dt.Rows(0).Item("distribuicao").ToString
            CarregarFase()
            drpFase.SelectedValue = dt.Rows(0).Item("fase").ToString
            drpFornecedor.SelectedValue = dt.Rows(0).Item("cod_fornecedor").ToString


            If dt.Rows(0).Item("tipo").ToString = "C" Then
                lblDescTipo.Text = "Colecionáveis"
                drpCategoria.Items.Insert(0, "Colecionáveis")
                drpCategoria.Items(0).Value = "C"
                drpCategoria.SelectedValue = "C"
                drpPeriod.Enabled = False
                txtAcabamento.Enabled = False
                txtCapa.Enabled = False
                txtMiolo.Enabled = False

                pnlOcultarCol.Visible = False
                pnlEtapa2.Visible = True
                pnlEtapa3.Visible = True
                lblDtLancto.Visible = True
                txtDtLancto.Visible = True
                lblFormato.Visible = False
                txtFormato.Visible = False
                lblPreco.Visible = False
                txtPreco1.Visible = False
                lblValReais.Visible = False
                pnlEtapa1CodColec.Visible = True
                pnlEtapa1Outros.Visible = False

                txtDtLancto.Text = dt.Rows(0).Item("data_lcto").ToString
                txtCromo.Text = dt.Rows(0).Item("formato_cromo").ToString
                txtEnvelope.Text = dt.Rows(0).Item("formato_envelope").ToString
                txtQtdCN.Text = dt.Rows(0).Item("qtde_cromo_normal").ToString
                txtQtdCE.Text = dt.Rows(0).Item("qtde_cromo_especial").ToString
                txtQtdTotal.Text = dt.Rows(0).Item("total_cromos").ToString

                txtPrecoCromo.Text = Format(dt.Rows(0).Item("preco_cromo"), "#,##0.00")
                txtPrecoEnvelope.Text = Format(dt.Rows(0).Item("preco_envelope"), "#,##0.00")
                txtCromoEnv.Text = dt.Rows(0).Item("cromo_por_env").ToString
                txtEnvPacote.Text = dt.Rows(0).Item("env_por_pacote").ToString
                txtEnvCaixa.Text = dt.Rows(0).Item("env_por_caixa").ToString
                txtQtdeAlbumPacote.Text = dt.Rows(0).Item("qtde_album_pacote").ToString
                txtPais.Text = dt.Rows(0).Item("pais").ToString

                txtFormatoCol.Text = dt.Rows(0).Item("li_formato").ToString
                txtQtdPagCol.Text = dt.Rows(0).Item("li_qtde_pag").ToString
                txtPapelCol.Text = dt.Rows(0).Item("li_capa_venda").ToString
                txtCortesiaCol.Text = dt.Rows(0).Item("li_capa_cortesia").ToString
                txtMioloCol.Text = dt.Rows(0).Item("li_papel_miolo").ToString

                If dt.Rows(0).Item("editorial").ToString = "I" Then
                    rdItalia.Checked = True
                ElseIf dt.Rows(0).Item("editorial").ToString = "B" Then
                    rdBrasil.Checked = True
                Else
                    rdOutros.Checked = True
                End If

                If dt.Rows(0).Item("env_mercosul") = True Then
                    rdNaoMerc.Checked = False
                    rdSimMerc.Checked = True
                Else
                    rdNaoMerc.Checked = True
                End If

                If dt.Rows(0).Item("li_encarte_col") = True Then
                    rdSimCol.Checked = True
                    rdNaoCol.Checked = False
                Else
                    rdNaoCol.Checked = True
                End If

                If dt.Rows(0).Item("li_encarte_esp") = True Then
                    rdSimEsp.Checked = True
                    rdNaoEsp.Checked = False
                Else
                    rdNaoEsp.Checked = True
                End If

                If dt.Rows(0).Item("li_poster") = True Then
                    rdSimPoster.Checked = True
                    rdNaoPoster.Checked = False
                Else
                    rdNaoPoster.Checked = True
                End If

            Else
                If dt.Rows(0).Item("tipo").ToString = "L" Then
                    drpCategoria.Items.Insert(0, "Livros")
                    drpCategoria.Items(0).Value = "L"
                    drpCategoria.SelectedValue = "L"
                Else
                    drpCategoria.Items.Insert(0, "Revistas")
                    drpCategoria.Items(0).Value = "R"
                    drpCategoria.SelectedValue = "R"
                End If
                lblDescTipo.Text = "Publicação"
                pnlEtapa2.Visible = False
                pnlEtapa3.Visible = False
                pnlEtapa1CodColec.Visible = False
                pnlEtapa1Outros.Visible = True
            End If

            txtAno.Enabled = False
            pnlEdicoes.Visible = False
            drpCategoria.Enabled = False

        Else
            'Tela de erro
        End If

    End Sub

    Public Sub VerificaPeriodicidade()
        Dim sDescricao As String = ""

        Select Case drpPeriod.SelectedValue
            Case "Q"
                sDescricao = "Permanência em banca = 37 dias"
            Case "B"
                sDescricao = "Permanência em banca = 60 dias"
            Case "M"
                sDescricao = "Permanência em banca = 37 dias"
            Case "T"
                sDescricao = "Permanência em banca = 60 dias"
            Case "S"
                sDescricao = "Permanência em banca = 60 dias"
            Case "E"
                sDescricao = "Permanência em banca = 60 dias"
            Case "A"
                sDescricao = "Permanência em banca = 60 dias"
        End Select
        lblPeriod.Text = sDescricao

    End Sub

    Protected Sub btnProximo1_Click(sender As Object, e As EventArgs) Handles btnProximo1.Click

        Dim oRet As New RetornoGenerico
        oRet = ValidarTab1()
        If oRet.Sucesso Or hdnValidaTab.Value = "N" Then
            oMensagem.ClearMessage()
            TabContainer1.ActiveTabIndex = 1
            tab2.Enabled = True
        Else
            oMensagem.SetMessage(oRet)
            oMensagem.Focus()
        End If

    End Sub

    Protected Sub btnProximo2_Click(sender As Object, e As EventArgs) Handles btnProximo2.Click

        Dim oRet As New RetornoGenerico
        oRet = ValidarTab2()
        If oRet.Sucesso Or hdnValidaTab.Value = "N" Then
            oMensagem.ClearMessage()
            TabContainer1.ActiveTabIndex = 2
            tab3.Enabled = True
        Else
            oMensagem.SetMessage(oRet)
            oMensagem.Focus()
        End If

    End Sub

    Protected Sub btnVoltar2_Click(sender As Object, e As EventArgs) Handles btnVoltar2.Click

        oMensagem.ClearMessage()
        TabContainer1.ActiveTabIndex = 0

    End Sub

    Protected Sub btnVoltar3_Click(sender As Object, e As EventArgs) Handles btnVoltar3.Click

        oMensagem.ClearMessage()
        TabContainer1.ActiveTabIndex = 1

    End Sub

    Private Function ValidarTab1() As RetornoGenerico

        Dim oRet As New RetornoGenerico

        oRet.Sucesso = False

        If drpCategoria.SelectedValue = "" Then
            oRet.Mensagem = "Selecione a categoria"
        Else
            If drpPeriod.SelectedValue = "" And drpCategoria.SelectedValue <> "C" Then
                oRet.Mensagem = "Selecione a periodicidade"
            ElseIf String.IsNullOrEmpty(txtAno.Text) Then
                oRet.Mensagem = "Preencha o ano"
            ElseIf Not chkAssinatura.Checked Then
                chkAssinatura.Checked = False
            ElseIf chkAssinatura.Checked Then
                chkAssinatura.Checked = True
            Else
                If drpCategoria.SelectedValue = "R" Then
                    If String.IsNullOrEmpty(hdnCodPlan.Value) Then
                        If String.IsNullOrEmpty(txtJan.Text) And
                           String.IsNullOrEmpty(txtFev.Text) And
                           String.IsNullOrEmpty(txtMar.Text) And
                           String.IsNullOrEmpty(txtAbr.Text) And
                           String.IsNullOrEmpty(txtMai.Text) And
                           String.IsNullOrEmpty(txtJun.Text) And
                           String.IsNullOrEmpty(txtJul.Text) And
                           String.IsNullOrEmpty(txtAgo.Text) And
                           String.IsNullOrEmpty(txtSet.Text) And
                           String.IsNullOrEmpty(txtOut.Text) And
                           String.IsNullOrEmpty(txtNov.Text) And
                           String.IsNullOrEmpty(txtDez.Text) Then
                            oRet.Mensagem = "Preencha pelo menos uma edição"
                        End If
                    End If
                End If
            End If
        End If

        If String.IsNullOrEmpty(oRet.Mensagem) Then
            oRet.Sucesso = True
        End If

        Return oRet

    End Function

    Private Function ValidarTab2() As RetornoGenerico

        Dim oRet As New RetornoGenerico

        oRet.Sucesso = False

        ' Validação Para Categoria Diferente de Colecionável
        If String.IsNullOrEmpty(txtFormato.Text) And drpCategoria.SelectedValue <> "C" Then
            oRet.Mensagem = "Preencha o formato"
            txtFormato.Focus()

        ElseIf String.IsNullOrEmpty(txtPreco1.Text) And drpCategoria.SelectedValue <> "C" Then
            oRet.Mensagem = "Preencha o preço"
            txtPreco1.Focus()

        ElseIf String.IsNullOrEmpty(txtQtdeCapa.Text) And drpCategoria.SelectedValue <> "C" Then
            oRet.Mensagem = "Preencha a quantidade da capa"
            txtQtdeCapa.Focus()

        ElseIf String.IsNullOrEmpty(txtQtdeMiolo.Text) And drpCategoria.SelectedValue <> "C" Then
            oRet.Mensagem = "Preencha a quantidade do miolo"
            txtQtdeMiolo.Focus()

        ElseIf String.IsNullOrEmpty(txtCapa.Text) And drpCategoria.SelectedValue <> "C" Then
            oRet.Mensagem = "Preencha a capa"
            txtCapa.Focus()

        ElseIf String.IsNullOrEmpty(txtMiolo.Text) And drpCategoria.SelectedValue <> "C" Then
            oRet.Mensagem = "Preencha o miolo"
            txtMiolo.Focus()

            'Fim da Validação Para Categoria Diferente de Colecionável
        Else
            If drpCategoria.SelectedValue = "C" Then
                If String.IsNullOrEmpty(txtCromo.Text) Then
                    oRet.Mensagem = "Preencha o formato do cromo"
                ElseIf String.IsNullOrEmpty(txtEnvelope.Text) Then
                    oRet.Mensagem = "Preencha o formato do envelope"
                    'ElseIf String.IsNullOrEmpty(txtQtdCN.Text) Then
                    '    oRet.Mensagem = "Preencha a quantidade de itens colecionáveis normais"
                    'ElseIf String.IsNullOrEmpty(txtQtdCE.Text) Then
                    '    oRet.Mensagem = "Preencha a quantidade de itens colecionáveis especiais"
                ElseIf String.IsNullOrEmpty(txtQtdTotal.Text) Then
                    oRet.Mensagem = "Preencha o total de itens colecionáveis"
                End If
            End If
        End If

        If String.IsNullOrEmpty(oRet.Mensagem) Then
            oRet.Sucesso = True
        End If

        Return oRet

    End Function

    Private Function ValidarTab3() As RetornoGenerico

        Dim oRet As New RetornoGenerico

        oRet.Sucesso = False

        'ElseIf String.IsNullOrEmpty(txtShrink.Text) Then
        'ElseIf drpTpServico.SelectedValue = "" Then

        If rdSim.Checked = True And String.IsNullOrEmpty(txtBrinde.Text) Then
            oRet.Mensagem = "Preencha o brinde"

        ElseIf String.IsNullOrEmpty(txtTipoServico.Text) Then
            oRet.Mensagem = "Preencha o tipo de serviço"

        ElseIf String.IsNullOrEmpty(txtManuseio.Text) Then
            oRet.Mensagem = "Preencha o tipo de manuseio"

        ElseIf String.IsNullOrEmpty(txtAcabamento.Text) And drpCategoria.SelectedValue <> "C" Then
            oRet.Mensagem = "Preencha o acabamento"

        ElseIf drpDistr.SelectedValue = "" Then
            oRet.Mensagem = "Selecione a distribuição"

        ElseIf String.IsNullOrEmpty(drpFase.Text) Then
            oRet.Mensagem = "Selecione a fase"

        ElseIf String.IsNullOrEmpty(txtBinding.Text) Then
            oRet.Mensagem = "Preencha o binding"

        Else
            If drpCategoria.SelectedValue = "C" Then
                If String.IsNullOrEmpty(txtFormatoCol.Text) Then
                    oRet.Mensagem = "Preencha o formato do livro"
                ElseIf String.IsNullOrEmpty(txtQtdPagCol.Text) Then
                    oRet.Mensagem = "Preencha a quantidade de páginas"
                ElseIf String.IsNullOrEmpty(txtPapelCol.Text) Then
                    oRet.Mensagem = "Preencha o campo papel capa venda"
                ElseIf String.IsNullOrEmpty(txtCortesiaCol.Text) Then
                    oRet.Mensagem = "Preencha o campo papel capa cortesia"
                ElseIf String.IsNullOrEmpty(txtMioloCol.Text) Then
                    oRet.Mensagem = "Preencha o campo papel miolo"
                End If
            End If
        End If

        If String.IsNullOrEmpty(oRet.Mensagem) Then
            oRet.Sucesso = True
        End If

        Return oRet

    End Function

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        Dim oRet As New RetornoGenerico
        Dim oEmail As New RetornoGenerico
        Dim oEmailGeral As New RetornoGenerico

        Try
            Dim oPlan As New Planejamento
            Dim aDados As New ArrayList
            Dim aDadosFig As New ArrayList
            Dim sEdicao As String
            Dim sMes As String = ""

            oMensagem.ClearMessage()
            oRet = ValidarTab3()

            If oRet.Sucesso = True Then
                oPlan.CodPDFS = lblPDFS.Text
                oPlan.DescPDFS = lblDescPDFS.Text
                oPlan.Assinaturas = chkAssinatura.Checked
                oPlan.Licenciante = lblDescDivisao.Text
                oPlan.Fase = drpFase.SelectedValue
                oPlan.Distribuicao = drpDistr.SelectedValue
                If IsNumeric(txtCustoEditorialP.Text) Then
                    oPlan.Preco_pagina = Convert.ToDouble(txtCustoEditorialP.Text) '.Replace(".", "").Replace(",", ".")
                End If
                If IsNumeric(txtCustoEditorial.Text) Then
                    oPlan.Custo = Convert.ToDouble(txtCustoEditorial.Text) '.Replace(".", "").Replace(",", ".")
                End If
                If IsNumeric(txtPreco1.Text) Then
                    oPlan.Preco = Convert.ToDouble(txtPreco1.Text) '.Replace(".", "").Replace(",", ".")
                End If
                'oPlan.CodPDFS = txtCustoEditTotal.Text.Replace(".", "").Replace(",", "."))
                oPlan.Formato = txtFormato.Text
                oPlan.Colecao = lblColecao.Text
                oPlan.QtdePaginas = txtQtdePag.Text
                oPlan.Periodicidade = drpPeriod.SelectedValue
                oPlan.Categoria = drpCategoria.SelectedValue
                oPlan.Acabamento = txtAcabamento.Text
                'Acabamento
                'oPlan.CodPDFS = drpAcabamento.SelectedValue)

                oPlan.Existe_brinde = rdSim.Checked
                'If rdSim.Checked Then
                '    oPlan.Existe_brinde = "true"
                'Else
                '    oPlan.Existe_brinde = "false"
                'End If

                oPlan.Brinde = txtBrinde.Text
                oPlan.Imagem = hdnImgUpload.Value
                'oPlan.CodPDFS = txtShrink.Text)
                oPlan.Shrink = txtTipoServico.Text
                'oPlan.CodPDFS = drpTpServico.SelectedValue)
                oPlan.Capa = txtCapa.Text
                'oPlan.CodPDFS = drpCapa.SelectedValue)
                oPlan.Miolo = txtMiolo.Text
                'oPlan.CodPDFS = drpMiolo.SelectedValue)
                If IsNumeric(txtQtdeCapa.Text) Then
                    oPlan.QtdeCapa = txtQtdeCapa.Text
                End If
                If IsNumeric(txtQtdeMiolo.Text) Then
                    oPlan.QtdeMiolo = txtQtdeMiolo.Text
                End If

                For nI = 1 To 12
                    sMes += nI.ToString & ";" 'mes
                Next
                If String.IsNullOrEmpty(hdnCodPlan.Value) Then
                    sEdicao = txtJan.Text & ";" & txtFev.Text & ";" & txtMar.Text & ";" & txtAbr.Text & ";" & txtMai.Text & ";" & txtJun.Text & ";" & txtJul.Text & ";" & txtAgo.Text & ";" & txtSet.Text & ";" & txtOut.Text & ";" & txtNov.Text & ";" & txtDez.Text & ";"
                    If drpCategoria.SelectedValue = "C" Then
                        If IsDate(txtDtLancto.Text) Then
                            sEdicao = ""
                            For nI = 1 To 12
                                If Month(CDate(txtDtLancto.Text)) = nI Then
                                    sEdicao += "001;"
                                Else
                                    sEdicao += ";"
                                End If

                            Next
                        End If
                    End If
                Else
                    sEdicao = hdnEdicao.Value
                End If

                oPlan.Edicao = sEdicao
                oPlan.Ano = txtAno.Text
                oPlan.Binding = txtBinding.Text
                oPlan.Manuseio = txtManuseio.Text
                oPlan.CodPanini = lblCodPanini.Text
                oPlan.CodFornecedor = drpFornecedor.SelectedValue

                oPlan.CodPDFSAlbum = txtPDFSAlbum.Text
                oPlan.CodPDFSEnvelope = txtPDFSEnv.Text
                oPlan.CodPaniniAlbum = txtCodAlbum.Text
                oPlan.CodPaniniEnvelope = txtCodEnv.Text


                'Se for figurinha, gravar os dados
                If drpCategoria.SelectedValue = "C" Then
                    aDadosFig.Add(txtCromo.Text)
                    aDadosFig.Add(txtEnvelope.Text)
                    aDadosFig.Add(txtQtdCN.Text)
                    aDadosFig.Add(txtQtdCE.Text)
                    aDadosFig.Add(Convert.ToInt32(txtQtdTotal.Text.Replace(",", "")))
                    If rdItalia.Checked = True Then
                        aDadosFig.Add("I")
                    ElseIf rdBrasil.Checked = True Then
                        aDadosFig.Add("B")
                    Else
                        aDadosFig.Add("O")
                    End If
                    If rdSimMerc.Checked = True Then
                        aDadosFig.Add("true")
                    Else
                        aDadosFig.Add("false")
                    End If

                    aDadosFig.Add(txtPais.Text)
                    aDadosFig.Add(txtFormatoCol.Text)
                    aDadosFig.Add(txtQtdPagCol.Text)
                    aDadosFig.Add(txtPapelCol.Text)
                    aDadosFig.Add(txtCortesiaCol.Text)
                    aDadosFig.Add(txtMioloCol.Text)

                    If rdSimCol.Checked = True Then
                        aDadosFig.Add("true")
                    Else
                        aDadosFig.Add("false")
                    End If
                    If rdSimEsp.Checked = True Then
                        aDadosFig.Add("true")
                    Else
                        aDadosFig.Add("false")
                    End If
                    If rdSimPoster.Checked = True Then
                        aDadosFig.Add("true")
                    Else
                        aDadosFig.Add("false")
                    End If
                    aDadosFig.Add(txtDtLancto.Text)
                    aDadosFig.Add(txtPrecoCromo.Text.Replace(".", "").Replace(",", "."))
                    aDadosFig.Add(txtPrecoEnvelope.Text.Replace(".", "").Replace(",", "."))
                    aDadosFig.Add(txtCromoEnv.Text)
                    aDadosFig.Add(txtEnvPacote.Text)
                    aDadosFig.Add(txtEnvCaixa.Text)
                    aDadosFig.Add(txtQtdeAlbumPacote.Text)
                End If

                If String.IsNullOrEmpty(hdnCodPlan.Value) Then

                    oPlan.IncluirPlanejamento(oPlan, aDadosFig)
                    oEmail = EnviaEmail(True)

                    oRet.Mensagem = "Planejamento incluido com sucesso. " & oEmail.Mensagem

                    'Se for edição 001, envia E-mail para todos
                    If sEdicao.Contains("001") Then
                        oEmailGeral = EnviaEmailGeral()

                        oRet.Mensagem += " Envio de e-mail geral com informações da primeira edição: " & oEmailGeral.Mensagem

                    End If

                    btnSalvar.Enabled = False
                Else
                    oPlan.AlterarPlanejamento(oPlan, aDadosFig, hdnCodPlan.Value)
                    oEmail = EnviaEmail(False)

                    oRet.Mensagem = "Planejamento alterado com sucesso. " & oEmail.Mensagem

                End If

                oRet.Sucesso = True

                If IsPostBack Then
                    Page.SetFocus(pnlMensagem)
                End If

            End If

        Catch SqlEx As SqlException
            Dim myError As SqlError
            oRet.Sucesso = False
            For Each myError In SqlEx.Errors
                oRet.Mensagem += myError.Number & " - " & myError.Message & ". "
            Next
        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString

        End Try
        oMensagem.SetMessage(oRet)
        'Return oRet

    End Sub

    Public Sub CarregarFase()

        drpFase.Items.Clear()
        drpFase.Items.Insert(0, "Selecione")
        drpFase.Items(0).Value = " "
        If drpDistr.SelectedValue = "N" Then
            drpFase.Items.Insert(1, " ")
            drpFase.Items(1).Value = "F1"
            drpFase.Items.Insert(2, "REL")
            drpFase.Items(2).Value = "REL"

        ElseIf drpDistr.SelectedValue = "S" Then
            drpFase.Items.Insert(1, "Fase 1")
            drpFase.Items(1).Value = "F1"
            drpFase.Items.Insert(2, "Fase 2")
            drpFase.Items(2).Value = "F2"
            drpFase.Items.Insert(3, "REL")
            drpFase.Items(3).Value = "REL"
        End If
    End Sub

    Public Sub MostraEdicao()

        pnlEdicoes.Visible = False
        If drpCategoria.SelectedValue = "R" Then
            pnlEdicoes.Visible = True
        End If

    End Sub

    Private Function PastaImagem() As String

        Dim sPasta As String = Server.MapPath("~") & "\imagens_planejamento\"
        If Not My.Computer.FileSystem.DirectoryExists(sPasta) Then
            Directory.CreateDirectory(sPasta)
        End If
        Return sPasta '& "\chamado_tecnico_" & HttpContext.Current.Session("NomeUsuario").ToString.Trim & ".xml"

    End Function

    Protected Sub btnCarregar_Click(sender As Object, e As EventArgs) Handles btnCarregar.Click

        If txtArquivo.HasFile Then
            Dim FileName As String = Path.GetFileName(txtArquivo.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(txtArquivo.PostedFile.FileName)
            Dim FilePath As String = PastaImagem() & FileName
            txtArquivo.SaveAs(FilePath)
            imgUpload.ImageUrl = "imagens_planejamento/" & FileName
            hdnImgUpload.Value = "imagens_planejamento/" & FileName
        End If

    End Sub

    Public Sub LiberaCampoBrinde()

        txtBrinde.Enabled = True

    End Sub

    Public Sub BloqueiaCampoBrinde()

        txtBrinde.Enabled = False

    End Sub

    Protected Sub btnLimpar1_Click(sender As Object, e As EventArgs) Handles btnLimpar1.Click

        LimparEtapa1()

    End Sub

    Protected Sub btnLimpar2_Click(sender As Object, e As EventArgs) Handles btnLimpar2.Click

        LimparEtapa2()

    End Sub

    Protected Sub btnLimpar3_Click(sender As Object, e As EventArgs) Handles btnLimpar3.Click

        LimparEtapa3()

    End Sub

    Private Sub LimparEtapa1()

        If drpCategoria.SelectedValue <> "C" Then
            drpCategoria.SelectedValue = ""
        End If
        chkAssinatura.Checked = False
        drpPeriod.SelectedValue = ""
        lblPeriod.Text = String.Empty
        If String.IsNullOrEmpty(hdnCodPlan.Value) Then
            txtAno.Text = String.Empty
        End If
        txtJan.Text = String.Empty
        txtFev.Text = String.Empty
        txtMar.Text = String.Empty
        txtAbr.Text = String.Empty
        txtMai.Text = String.Empty
        txtJun.Text = String.Empty
        txtJul.Text = String.Empty
        txtAgo.Text = String.Empty
        txtSet.Text = String.Empty
        txtOut.Text = String.Empty
        txtNov.Text = String.Empty
        txtDez.Text = String.Empty

    End Sub

    Private Sub LimparEtapa2()
        txtFormato.Text = String.Empty
        txtPreco1.Text = String.Empty
        txtCapa.Text = String.Empty
        txtMiolo.Text = String.Empty
        txtQtdeCapa.Text = String.Empty
        txtQtdeMiolo.Text = String.Empty
        txtQtdePag.Text = String.Empty
        txtAcabamento.Text = String.Empty

        If drpCategoria.SelectedValue = "C" Then
            txtCromo.Text = String.Empty
            txtEnvelope.Text = String.Empty
            txtQtdCN.Text = String.Empty
            txtQtdCE.Text = String.Empty
            txtQtdTotal.Text = String.Empty
        End If

    End Sub

    Private Sub LimparEtapa3()

        rdNao.Checked = True
        txtBrinde.Text = String.Empty
        txtTipoServico.Text = String.Empty
        txtManuseio.Text = String.Empty
        drpDistr.SelectedValue = ""
        CarregarFase()
        txtBinding.Text = String.Empty

        If drpCategoria.SelectedValue = "C" Then
            rdBrasil.Checked = True
            rdNaoMerc.Checked = True
            txtPais.Text = String.Empty
            txtFormatoCol.Text = String.Empty
            txtQtdPagCol.Text = String.Empty
            txtPapelCol.Text = String.Empty
            txtCortesiaCol.Text = String.Empty
            txtMioloCol.Text = String.Empty
            rdNaoCol.Checked = True
            rdNaoEsp.Checked = True
            rdNaoPoster.Checked = True
        End If

    End Sub

    Public Sub PreencheQtdePag()

        txtQtdePag.Text = ""
        PreencheCustoEdiTotal()

        If IsNumeric(txtQtdeCapa.Text) And IsNumeric(txtQtdeMiolo.Text) Then
            txtQtdePag.Text = txtQtdeCapa.Text & "+" & txtQtdeMiolo.Text
        End If

    End Sub

    Public Sub PreencheCustoEdiTotal()

        If IsNumeric(txtCustoEditorialP.Text) Then
            txtCustoEditorialP.Text = Format(Convert.ToDouble(txtCustoEditorialP.Text), "#,###,##0.00")
        End If
        If IsNumeric(txtQtdeCapa.Text) And IsNumeric(txtQtdeMiolo.Text) And IsNumeric(txtCustoEditorialP.Text) Then
            Dim qntd As Integer
            Dim custoEdit As Double = Convert.ToDouble(txtCustoEditorialP.Text)

            qntd = Convert.ToInt32(txtQtdeCapa.Text) + Convert.ToInt32(txtQtdeMiolo.Text)
            txtCustoEditTotal.Text = Format((qntd * custoEdit), "#,###,##0.00")
        End If

    End Sub

    Private Function EnviaEmail(ByVal bInclui As Boolean) As RetornoGenerico

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
        If bInclui Then
            sConteudo += "Inclusão de Planejamento"
            sTexto1 = "Inclusão"
            sTexto2 = "Incluído"
        Else
            sConteudo += "Alteração de Planejamento"
            sTexto1 = "alteração"
            sTexto2 = "Alterado"
        End If
        sConteudo += "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Titulo:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & lblDescPDFS.Text & "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>PDFS:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & lblPDFS.Text & "</td>"
        sConteudo += "</tr>"
        sConteudo += "<tr>"
        sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Divisão:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & lblDescDivisao.Text & "</td>"
        sConteudo += "</tr>"
        If Not bInclui Then
            sConteudo += "<tr>"
            sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Edição:</td>"
            sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & hdnEdicao.Value & "</td>"
            sConteudo += "</tr>"
        End If
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

        aEmail = Email.GruposEmail("PLANEJAMENTO", Session("EmailUsuario"))
        oRet = Email.enviaMensagemEmail("", aEmail, "PW - Planejamento", sConteudo)

        Return oRet

    End Function

    Private Function EnviaEmailGeral() As RetornoGenerico

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
        sConteudo += "Inclusão de Planejamento - Novo Produto"
        sConteudo += "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Titulo:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & lblDescPDFS.Text & "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>PDFS:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & lblPDFS.Text & "</td>"
        sConteudo += "</tr>"
        sConteudo += "<tr>"
        sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Divisão:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & lblDescDivisao.Text & "</td>"
        sConteudo += "</tr>"
        sConteudo += "<tr>"
        sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Data da inclusão:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & Date.Now & "</td>"
        sConteudo += "</tr>"
        sConteudo += "<tr>"
        sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'> Incluído por:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'> " & Session("NomeUsuario2") & "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "    <td colspan='2' align='center' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px;font-weight: bold;' >E-MAIL ENVIADO POR PW - PANINI WORKFLOW</td>"
        sConteudo += "</tr>"
        sConteudo += " </table>"
        sConteudo += "<br/><br/><br/>"

        aEmail = Email.GruposEmail("GERAL", Session("EmailUsuario"))
        oRet = Email.enviaMensagemEmail("", aEmail, "PW - Planejamento - Edição 001", sConteudo)

        Return oRet

    End Function

End Class