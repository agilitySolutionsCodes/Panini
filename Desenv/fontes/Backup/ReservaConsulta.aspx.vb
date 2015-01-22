Imports System.Data.SqlClient

Public Class ReservaConsulta

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
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

        drpEdicao.Items.Clear()
        drpEdicao.Items.Insert(0, "----------------")
        drpEdicao.Items(0).Value = ""

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
                drpEdicao.Items.Clear()
                drpEdicao.Items.Insert(0, "----------------")
                drpEdicao.Items(0).Value = ""

                reader = oPlan.FiltrarPlan(drpAno.SelectedValue, drpDivisao.SelectedValue, drpTipo.SelectedValue)
                If reader.HasRows Then
                    drpPDFS.DataSource = reader
                    drpPDFS.DataBind()
                    drpPDFS.Items.Insert(0, "Selecione o produto")
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

    Public Sub CarregarEdicoes()

        Dim oPlan As New Planejamento
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            drpEdicao.Items.Clear()

            If String.IsNullOrEmpty(drpPDFS.SelectedValue) Then
                drpEdicao.Items.Insert(0, "----------------")
                drpEdicao.Items(0).Value = ""
            Else
                reader = oPlan.ListaEdicoes(drpPDFS.SelectedValue)
                If reader.HasRows Then
                    drpEdicao.DataSource = reader
                    drpEdicao.DataBind()
                    drpEdicao.Items.Insert(0, "Selecione a edição")
                    drpEdicao.Items(0).Value = ""
                    reader.Close()
                Else
                    drpEdicao.Items.Insert(0, "Não existem registros")
                    drpEdicao.Items(0).Value = ""
                End If
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Function PermiteAcesso() As Boolean

        Dim aPermissoes As Array
        Dim bRet As Boolean = False

        aPermissoes = Util.Permissoes(3)
        If aPermissoes.Length > 0 Then
            If aPermissoes(0) = True Then 'Visualizar
                bRet = True
            End If
        End If

        Return bRet
    End Function

End Class