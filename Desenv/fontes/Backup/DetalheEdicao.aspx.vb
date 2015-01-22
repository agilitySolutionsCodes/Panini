Imports System.Data.SqlClient

Public Class DetalheEdicao

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Preencher(Request("codigo"), Request("edicao"))
            PermiteAcesso()
        End If

    End Sub

    Private Sub Preencher(ByVal sCodigo As String, ByVal sEdicao As String)

        Dim dt As DataTable
        Dim oRel As New Relatorio
        Dim oRet As New RetornoGenerico

        Try
            hdnCodPlan.Value = sCodigo
            dt = oRel.ConsultaEdicao(sEdicao, sCodigo)

            If dt.Rows.Count > 0 Then
                hdnDivisao.Value = dt.Rows(0).Item("divisao").ToString
                lblMes.Text = Util.DescricaoMes(dt.Rows(0).Item("mes")).ToUpper
                lblEdicao.Text = sEdicao
                lblTitulo.Text = dt.Rows(0).Item("descricao_pdfs").ToString
                lblPDFS.Text = dt.Rows(0).Item("cod_pdfs").ToString
                lblFase.Text = dt.Rows(0).Item("fase").ToString
                lblDistribuicao.Text = Util.DescricaoDistribuicao(dt.Rows(0).Item("distribuicao").ToString)
                lblPreco.Text = Format(dt.Rows(0).Item("preco"), "#,###.00")
                lblFormato.Text = dt.Rows(0).Item("formato").ToString
                lblBinding.Text = dt.Rows(0).Item("binding").ToString
                lblPaginas.Text = dt.Rows(0).Item("qtde_paginas").ToString
                lblPeriod.Text = Util.DescricaoPeriodicidade(dt.Rows(0).Item("periodicidade").ToString)
                If dt.Rows(0).Item("aprovacao_edit").ToString = "False" Or dt.Rows(0).Item("aprovacao_mkt").ToString = "False" Then
                    imgSemaforo.ImageUrl = "imagens/Vermelho.jpg"
                End If
                PreencherStatus(dt.Rows(0))
            Else
                oRet.Sucesso = False
                oRet.Mensagem = "Registro não encontrado."
                oMensagem.SetMessage(oRet)
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = "Erro ao consultar detalhe da edição: " & ex.Message.ToString

            oMensagem.SetMessage(oRet)
            oMensagem.Focus()

        End Try

    End Sub

    Private Sub PreencherStatus(ByVal dr As DataRow)

        imgStatusEditorial.Visible = False

        If String.IsNullOrEmpty(dr.Item("cod_data").ToString) Then
            imgStatusMkt.ImageUrl = "imagens/sem_datas.gif"
        Else
            imgStatusMkt.ImageUrl = "imagens/com_datas_n_aprovacao.gif"
            If dr.Item("aprovacao_mkt").ToString.ToUpper = "FALSE" Then
                imgStatusMkt.ImageUrl = "imagens/mkt_n_aprova.gif"
            ElseIf dr.Item("aprovacao_mkt").ToString.ToUpper = "TRUE" Then
                imgStatusMkt.ImageUrl = "imagens/mkt_aprova.gif"
            End If
            If dr.Item("aprovacao_edit").ToString.ToUpper = "FALSE" Then
                imgStatusEditorial.ImageUrl = "imagens/edt_n_aprova.gif"
                imgStatusEditorial.Visible = True
            ElseIf dr.Item("aprovacao_edit").ToString.ToUpper = "TRUE" Then
                imgStatusEditorial.ImageUrl = "imagens/edt_aprova.gif"
                imgStatusEditorial.Visible = True
            End If
        End If

    End Sub

    Private Sub PermiteAcesso()

        Dim aPermissoes As Array
        Dim bBloq As Boolean = False

        aPermissoes = Util.Permissoes(1)
        If aPermissoes.Length > 0 Then
            If aPermissoes(1) = False Or aPermissoes(2) = False Then 'Incluir ou Alterar
                bBloq = True
            End If
        Else
            bBloq = True
        End If

        If bBloq Then
            imgEditorial.Visible = False
            imgReserva.Visible = False
            imgStatusEditorial.Visible = False
            imgStatusMkt.Visible = False
            pnlLegenda.Visible = False
        End If

    End Sub

End Class