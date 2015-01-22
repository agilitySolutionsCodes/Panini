Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports System.IO

Public Class RelatorioConsulta

    Inherits System.Web.UI.Page 'BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            pnlExcel.Visible = False
            oMensagem.ClearMessage()
            If PermiteAcesso() Then
                CarregaAno()
            Else
                Response.Redirect("~/AcessoRestrito.aspx")
            End If

        End If

    End Sub

    Private Sub CarregaAno()

        Dim oRel As New Relatorio
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            reader = oRel.ListarAnos()
            If reader.HasRows Then
                drpAno.DataSource = reader
                drpAno.DataBind()
                drpAno.Items.Insert(0, "Selecione o ano")
                drpAno.Items(0).Value = ""
                reader.Close()
            Else
                drpAno.Items.Insert(0, "Não existem registros")
                drpAno.Items(0).Value = ""
            End If

            ApagaCampos()

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Sub ApagaCampos()

        drpDivisao.Items.Clear()
        drpDivisao.Items.Insert(0, "----------------")
        drpDivisao.Items(0).Value = ""

        drpPDFS.Items.Clear()
        drpPDFS.Items.Insert(0, "----------------")
        drpPDFS.Items(0).Value = ""

    End Sub

    Public Sub CarregarDivisao()

        Dim oRel As New Relatorio
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            If String.IsNullOrEmpty(drpAno.SelectedValue) Then
                ApagaCampos()
            Else
                drpDivisao.Items.Clear()

                reader = oRel.ListarDivisao(drpAno.SelectedValue)
                If reader.HasRows Then
                    drpDivisao.DataSource = reader
                    drpDivisao.DataBind()
                    drpDivisao.Items.Insert(0, "Todos")
                    drpDivisao.Items(0).Value = "0"
                    drpDivisao.Items.Insert(0, "Selecione a divisão")
                    drpDivisao.Items(0).Value = ""
                    reader.Close()
                Else
                    drpDivisao.Items.Insert(0, "Não existem registros")
                    drpDivisao.Items(0).Value = ""
                End If
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
            If String.IsNullOrEmpty(drpAno.SelectedValue) Then
                ApagaCampos()
            ElseIf Not String.IsNullOrEmpty(drpDivisao.SelectedValue) And Not String.IsNullOrEmpty(drpTipo.SelectedValue) Then
                drpPDFS.Items.Clear()

                reader = oPlan.FiltrarPlan(drpAno.SelectedValue, drpDivisao.SelectedValue, drpTipo.SelectedValue)
                If reader.HasRows Then
                    drpPDFS.DataSource = reader
                    drpPDFS.DataBind()
                    drpPDFS.Items.Insert(0, "Todos")
                    drpPDFS.Items(0).Value = "0"
                    drpPDFS.Items.Insert(0, "Selecione o Produto")
                    drpPDFS.Items(0).Value = ""
                    reader.Close()
                Else
                    drpPDFS.Items.Insert(0, "Não existem registros")
                    drpPDFS.Items(0).Value = ""
                End If
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Protected Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click

        Dim sAno = drpAno.SelectedValue
        Dim sDivisao = drpDivisao.SelectedValue
        Dim sTipo = drpTipo.SelectedValue
        Dim sProduto = drpPDFS.SelectedValue
        Dim oRet = New RetornoGenerico

        hdnAno.Value = sAno
        hdnDivisao.Value = sDivisao
        hdnTipo.Value = sTipo
        hdnProduto.Value = sProduto

        oMensagem.ClearMessage()
        CarregaRelatorio(sAno, sDivisao, sTipo, sProduto)

    End Sub

    Private Sub CarregaRelatorio(ByVal sAno As String, ByVal sDivisao As String, ByVal sTipo As String, ByVal sProduto As String)

        Dim oRel As New Relatorio
        Dim dt As New DataTable
        Dim oRet As New RetornoGenerico

        Try
            dt = oRel.GerarRelatorio(sAno, sDivisao, sTipo, sProduto)

            grdItens.DataSource = dt
            grdItens.DataBind()

            pnlExcel.Visible = True
            If dt.Rows.Count = 0 Then
                pnlExcel.Visible = False
                oRet.Sucesso = False
                oRet.Mensagem = "Não existem registros para esta pesquisa."
                oMensagem.SetMessage(oRet)
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Public Sub ExportarRelatorio()

        Dim dt As New DataTable
        Dim oRel As New Relatorio
        Dim oRet As New RetornoGenerico

        Try

            If String.IsNullOrEmpty(txtNomeArq.Text) Then
                oRet.Sucesso = False
                oRet.Mensagem = "Favor preencher o nome do arquivo."
                oMensagem.SetMessage(oRet)
            Else
                dt = oRel.GerarRelatorioExcel(hdnAno.Value, hdnDivisao.Value, hdnTipo.Value, hdnProduto.Value, txtNomeArq.Text.ToUpper, drpVersao.SelectedValue)

                exportarExcel(dt, txtNomeArq.Text)
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = "Erro ao gerar arquivo excel: " & ex.Message.ToString
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

  

    Private Sub exportarExcel(dt As DataTable, saveAsFile As String)

        grdRelatorio1.DataSource = dt
        grdRelatorio1.DataBind()


        Dim dgDados = New DataGrid()
        dgDados.DataSource = dt
        dgDados.DataBind()


        'Se o Grid tiver mais que 65536  linhas não podemos exportar
        If grdRelatorio1.Rows.Count.ToString + 1 < 65536 Then

            Response.Clear()

            'Response.AddHeader("content-disposition", "attachment;filename=" & saveAsFile & ".xlsx")
            'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment; filename=" & saveAsFile & ".xls")
            Response.Charset = ""
            Response.ContentType = "application/x-excel" '"application/vnd.ms-excel"



            'grdRelatorio1.DataType = System.Type.GetType("System.String")

            'Abaixo codifica os caracteres para o alfabeto latino
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("Windows-1252")
            Response.Charset = "ISO-8859-1"
            'Desabilita o ViewState
            EnableViewState = False

            Dim writer As New System.IO.StringWriter()
            Dim html As New System.Web.UI.HtmlTextWriter(writer)
            dgDados.RenderControl(html)
            'grdRelatorio1.RenderControl(html)

            Response.Write(writer)

            Response.End()

        Else
            Dim oRet As New RetornoGenerico
            oRet.Sucesso = False
            oRet.Mensagem = "planilha possui muitas linhas, não é possível exportar para o Excel"
            oMensagem.SetMessage(oRet)
        End If

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    Public Sub ExportarRelatorio1()

        Dim dt As New DataTable
        Dim oRel As New Relatorio
        Dim linha As Integer = 3
        Dim nColunas As Integer
        Dim oRet As New RetornoGenerico
        Dim objExcel As New Excel.Application
        Dim Pasta1 As Excel.Workbook
        Dim Plan1 As Excel.Worksheet

        Try

            If String.IsNullOrEmpty(txtNomeArq.Text) Then
                oRet.Sucesso = False
                oRet.Mensagem = "Favor preencher o nome do arquivo."
                oMensagem.SetMessage(oRet)
            Else
                dt = oRel.GerarRelatorioExcel(hdnAno.Value, hdnDivisao.Value, hdnTipo.Value, hdnProduto.Value, txtNomeArq.Text.ToUpper, drpVersao.SelectedValue)

                If dt.Rows.Count > 0 Then

                    'Define uma aplicação em Excel
                    'objExcel = New Excel.Application()

                    'Define uma (Pasta) (Arquivo de planilhas)
                    Pasta1 = objExcel.Workbooks.Add()

                    'Define uma planilha
                    Plan1 = objExcel.Worksheets.Add()
                    Plan1.Name = "RELATORIO"
                    Plan1.Activate()

                    'Definindo Títulos
                    objExcel.ActiveSheet.Range("A1:U1").merge()
                    objExcel.ActiveSheet.Cells(1, 1).Font.Bold = True
                    objExcel.ActiveSheet.Cells(1, 1).Value = txtNomeArq.Text.ToUpper
                    objExcel.ActiveSheet.cells(1, 1).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                    objExcel.ActiveSheet.Range("A2:U2").merge()
                    objExcel.ActiveSheet.Cells(2, 1).Font.Bold = True
                    If hdnDivisao.Value = "0" Then
                        objExcel.ActiveSheet.Cells(2, 1).Value = "TODAS AS DIVISÕES - BUDGET"
                    Else
                        objExcel.ActiveSheet.Cells(2, 1).Value = hdnDivisao.Value & " - BUDGET"
                    End If
                    objExcel.ActiveSheet.cells(2, 1).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                    objExcel.ActiveSheet.Cells(3, 1).Value = "TITLE"
                    objExcel.ActiveSheet.Cells(3, 2).Value = "FASE"
                    objExcel.ActiveSheet.Cells(3, 3).Value = "PDFS"
                    objExcel.ActiveSheet.Cells(3, 4).Value = "DISTR."
                    objExcel.ActiveSheet.Cells(3, 5).Value = "PRICE"
                    objExcel.ActiveSheet.Cells(3, 6).Value = "FORMAT"
                    objExcel.ActiveSheet.Cells(3, 7).Value = "BINDING"
                    objExcel.ActiveSheet.Cells(3, 8).Value = "PAGES"
                    objExcel.ActiveSheet.Cells(3, 9).Value = "P."
                    objExcel.ActiveSheet.Cells(3, 10).Value = "JAN."
                    objExcel.ActiveSheet.Cells(3, 11).Value = "FEB."
                    objExcel.ActiveSheet.Cells(3, 12).Value = "MAR."
                    objExcel.ActiveSheet.Cells(3, 13).Value = "APR."
                    objExcel.ActiveSheet.Cells(3, 14).Value = "MAY"
                    objExcel.ActiveSheet.Cells(3, 15).Value = "JUN."
                    objExcel.ActiveSheet.Cells(3, 16).Value = "JUL."
                    objExcel.ActiveSheet.Cells(3, 17).Value = "AUG."
                    objExcel.ActiveSheet.Cells(3, 18).Value = "SEPT."
                    objExcel.ActiveSheet.Cells(3, 19).Value = "OCT."
                    objExcel.ActiveSheet.Cells(3, 20).Value = "NOV."
                    objExcel.ActiveSheet.Cells(3, 21).Value = "DEC."

                    'Preenchendo Células
                    For Each dr As DataRow In dt.Rows
                        linha += 1

                        objExcel.ActiveSheet.Cells(linha, 1).Value = dr.Item("descricao_pdfs")
                        objExcel.ActiveSheet.Cells(linha, 2).Value = dr.Item("fase")
                        objExcel.ActiveSheet.Cells(linha, 3).Value = dr.Item("cod_pdfs")
                        objExcel.ActiveSheet.Cells(linha, 4).Value = dr.Item("distribuicao")
                        objExcel.ActiveSheet.Cells(linha, 5).Value = dr.Item("preco")
                        objExcel.ActiveSheet.Cells(linha, 6).Value = dr.Item("formato")
                        objExcel.ActiveSheet.Cells(linha, 7).Value = dr.Item("binding")
                        objExcel.ActiveSheet.Cells(linha, 8).Value = dr.Item("qtde_paginas")
                        objExcel.ActiveSheet.Cells(linha, 9).Value = dr.Item("periodicidade")
                        objExcel.ActiveSheet.Cells(linha, 10).Value = dr.Item("edicao_jan")
                        objExcel.ActiveSheet.Cells(linha, 11).Value = dr.Item("edicao_fev")
                        objExcel.ActiveSheet.Cells(linha, 12).Value = dr.Item("edicao_mar")
                        objExcel.ActiveSheet.Cells(linha, 13).Value = dr.Item("edicao_abr")
                        objExcel.ActiveSheet.Cells(linha, 14).Value = dr.Item("edicao_mai")
                        objExcel.ActiveSheet.Cells(linha, 15).Value = dr.Item("edicao_jun")
                        objExcel.ActiveSheet.Cells(linha, 16).Value = dr.Item("edicao_jul")
                        objExcel.ActiveSheet.Cells(linha, 17).Value = dr.Item("edicao_ago")
                        objExcel.ActiveSheet.Cells(linha, 18).Value = dr.Item("edicao_set")
                        objExcel.ActiveSheet.Cells(linha, 19).Value = dr.Item("edicao_out")
                        objExcel.ActiveSheet.Cells(linha, 20).Value = dr.Item("edicao_nov")
                        objExcel.ActiveSheet.Cells(linha, 21).Value = dr.Item("edicao_dez")

                        If dr.Item("edicao_jan").ToString.Length = 3 Then
                            objExcel.ActiveSheet.Cells(linha, 10).NumberFormat = "000"
                        End If
                        If dr.Item("edicao_fev").ToString.Length = 3 Then
                            objExcel.ActiveSheet.Cells(linha, 11).NumberFormat = "000"
                        End If
                        If dr.Item("edicao_mar").ToString.Length = 3 Then
                            objExcel.ActiveSheet.Cells(linha, 12).NumberFormat = "000"
                        End If
                        If dr.Item("edicao_abr").ToString.Length = 3 Then
                            objExcel.ActiveSheet.Cells(linha, 13).NumberFormat = "000"
                        End If
                        If dr.Item("edicao_mai").ToString.Length = 3 Then
                            objExcel.ActiveSheet.Cells(linha, 14).NumberFormat = "000"
                        End If
                        If dr.Item("edicao_jun").ToString.Length = 3 Then
                            objExcel.ActiveSheet.Cells(linha, 15).NumberFormat = "000"
                        End If
                        If dr.Item("edicao_jul").ToString.Length = 3 Then
                            objExcel.ActiveSheet.Cells(linha, 16).NumberFormat = "000"
                        End If
                        If dr.Item("edicao_ago").ToString.Length = 3 Then
                            objExcel.ActiveSheet.Cells(linha, 17).NumberFormat = "000"
                        End If
                        If dr.Item("edicao_set").ToString.Length = 3 Then
                            objExcel.ActiveSheet.Cells(linha, 18).NumberFormat = "000"
                        End If
                        If dr.Item("edicao_out").ToString.Length = 3 Then
                            objExcel.ActiveSheet.Cells(linha, 19).NumberFormat = "000"
                        End If
                        If dr.Item("edicao_nov").ToString.Length = 3 Then
                            objExcel.ActiveSheet.Cells(linha, 20).NumberFormat = "000"
                        End If
                        If dr.Item("edicao_dez").ToString.Length = 3 Then
                            objExcel.ActiveSheet.Cells(linha, 21).NumberFormat = "000"
                        End If

                        'objExcel.ActiveSheet.Cells(linha, 4).NumberFormat = "###.###.##0,00"
                    Next

                    'Formatando Colunas
                    objExcel.ActiveSheet.Columns(1).EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
                    For nColunas = 2 To 21
                        objExcel.ActiveSheet.Columns(nColunas).EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                    Next

                    For nColunas = 1 To 21
                        objExcel.ActiveSheet.Cells(3, nColunas).Font.Bold = True
                        objExcel.ActiveSheet.Columns(nColunas).EntireColumn.AutoFit()
                    Next

                    'Desenhando Bordas
                    For nColunas = 1 To 21
                        objExcel.ActiveCell.Cells(1, nColunas).Borders.LineStyle = Excel.XlBordersIndex.xlEdgeLeft
                    Next

                    For y As Integer = 3 To linha
                        For z As Integer = 1 To 21
                            objExcel.ActiveCell.Cells(y, z).Borders.LineStyle = Excel.XlBordersIndex.xlEdgeLeft
                            objExcel.ActiveCell.Cells(1, 1).Borders.LineStyle = Excel.XlBordersIndex.xlEdgeLeft
                        Next
                    Next

                    'Tipo e Tamanho de Fonte
                    With objExcel.ActiveSheet.Cells.Font
                        .Name = "Arial"
                        .Size = 10
                    End With
                    objExcel.ActiveSheet.Cells(1, 1).Font.Size = 24
                    objExcel.ActiveSheet.Cells(2, 1).Font.Size = 22

                    'objExcel.Application.Visible = True
                    objExcel.Visible = True
                    objExcel.UserControl = True
                    objExcel = Nothing
                    Pasta1 = Nothing
                    Plan1 = Nothing

                Else
                    oRet.Sucesso = False
                    oRet.Mensagem = "Erro ao gerar arquivo excel."
                    oMensagem.SetMessage(oRet)
                End If
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = "Erro ao gerar arquivo excel: " & ex.Message.ToString
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Function PermiteAcesso() As Boolean

        Dim aPermissoes As Array
        Dim bRet As Boolean = False

        aPermissoes = Util.Permissoes(1)
        If aPermissoes.Length > 0 Then
            If aPermissoes(0) = True Then 'Visualizar
                bRet = True
            End If
        End If

        Return bRet
    End Function

End Class