Imports System.Data.SqlClient

Public Class PlanejamentoConsulta

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If PermiteAcesso() Then
                CarregaAno()
                CarregaTipo()
            Else
                Response.Redirect("~/AcessoRestrito.aspx")
            End If

        End If

    End Sub

    Private Sub CarregaAno()

        Dim oPDFS As New PDFS
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            reader = oPDFS.ListaAnosPDFS
            If reader.HasRows Then
                drpAno.DataSource = reader
                drpAno.DataBind()
                drpAno.Items.Insert(0, "Selecione")
                drpAno.Items(0).Value = ""
                reader.Close()
            Else
                drpAno.Items.Insert(0, "Não existem registros")
                drpAno.Items(0).Value = ""
            End If

            CamposEmBranco()

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Public Sub CarregarDivisao()

        Dim oPDFS As New PDFS
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            CamposEmBranco()
            If Not String.IsNullOrEmpty(drpAno.SelectedValue) Then
                drpDivisao.Items.Clear()

                reader = oPDFS.ListaDivisaoPDFS(drpAno.SelectedValue)
                If reader.HasRows Then
                    drpDivisao.DataSource = reader
                    drpDivisao.DataBind()
                    drpDivisao.Items.Insert(0, "Todos")
                    drpDivisao.Items(0).Value = "0"
                    drpDivisao.Items.Insert(0, "Selecione a Divisão")
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

    Public Sub CarregaTipo()

        Dim oPDFS As New PDFS
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            drpTipo.Items.Insert(0, "-------------------------------------")
            drpTipo.Items(0).Value = ""

            If Not String.IsNullOrEmpty(drpDivisao.SelectedValue) Then

                drpTipo.Items.Clear()

                If drpDivisao.SelectedValue = "0" Then
                    drpDivisao.Items(0).Value = ""
                End If

                reader = oPDFS.ListaTipo(drpAno.SelectedValue, drpDivisao.SelectedValue)

                If reader.HasRows Then
                    drpTipo.DataSource = reader
                    drpTipo.DataBind()
                    drpTipo.Items.Insert(0, "Selecione")
                    drpTipo.Items(0).Value = ""
                    reader.Close()

                Else
                    drpTipo.Items.Clear()
                    drpTipo.Items.Insert(0, "Não existem registros")
                    drpTipo.Items(0).Value = ""
                End If
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Public Sub CarregaPDFS()

        Dim oPDFS As New PDFS
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            drpPDFS.Items.Clear()
            If String.IsNullOrEmpty(drpDivisao.SelectedValue) Then
                drpPDFS.Items.Insert(0, "--------------------------------")
                drpPDFS.Items(0).Value = ""
            Else
                reader = oPDFS.ListaPDFS(drpAno.SelectedValue, drpDivisao.SelectedValue, drpTipo.SelectedValue)
                If reader.HasRows Then
                    drpPDFS.DataSource = reader
                    drpPDFS.DataBind()
                    drpPDFS.Items.Insert(0, "Selecione")
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

    Private Sub CamposEmBranco()
        drpDivisao.Items.Clear()
        drpDivisao.Items.Insert(0, "----------")
        drpDivisao.Items(0).Value = ""

        drpPDFS.Items.Clear()
        drpPDFS.Items.Insert(0, "--------------------------------")
        drpPDFS.Items(0).Value = ""
    End Sub

    Private Function PermiteAcesso() As Boolean

        Dim aPermissoes As Array
        Dim bRet As Boolean = False

        aPermissoes = Util.Permissoes(7)
        If aPermissoes.Length > 0 Then
            If aPermissoes(1) = True Then 'Incluir
                bRet = True
            End If
        End If

        Return bRet

    End Function

End Class